using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;
using VidRental.API.AutoMapper;
using VidRental.Services.Services;
using VidRental.DataAccess.Repositories;
using FluentValidation;
using VidRental.Services.Dtos.Request;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using VidRental.API.ActionFilters;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using VidRental.Services.ResponseWrapper;
using VidRental.API.Extensions;
using Microsoft.AspNetCore.Identity;
using VidRental.DataAccess.Roles;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace VidRental.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); })
                .AddFluentValidation(fv => fv.ImplicitlyValidateChildProperties = true);

            var migrationAssembly = $"{ nameof(VidRental) }.{ nameof(VidRental.DataAccess) }";
            services
                .AddDbContext<VidContext>(o => 
                    o.UseSqlServer(Configuration.GetConnectionString("defaultDb"), 
                    o => o.MigrationsAssembly(migrationAssembly)));

            services.AddDefaultIdentity<User>(
                options => 
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<VidContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiRoles.User, policy => policy.RequireRole(ApiRoles.Roles));
                options.AddPolicy(ApiRoles.Employee, policy => policy.RequireRole(ApiRoles.Admin, ApiRoles.Employee));
                options.AddPolicy(ApiRoles.Admin, policy => policy.RequireRole(ApiRoles.Admin));
            });

            services.AddAutoMapper(RootProfiles.Maps);

            services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IShopUserService, ShopUserService>();
            services.AddScoped<IShopEmployeeService, ShopEmployeeService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IDeleteImagesService, DeleteImagesService>();
            services.AddScoped<ICartridgeService, CartridgeService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IShopUserRepository, ShopUserRepository>();
            services.AddScoped<IShopEmployeeRepository, ShopEmployeeRepository>();
            services.AddScoped<ICartridgeRepository, CartridgeRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IMovieImageRepository, MovieImageRepository>();
            services.AddScoped<ICartridgeCopyRepository, CartridgeCopyRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<AddressAddRequest>, AddressAddRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(ApiResponse.Failure("api", "Something went wrong, try again"));
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            Seeder.SeedRolesAndAdmin(app.ApplicationServices).Wait();
            app.UseCors(
                options => options
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Static")),
                RequestPath = new PathString("/Static")
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
