using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.DataContext
{
    public class VidContext : IdentityDbContext<User>
    {
        public VidContext(DbContextOptions<VidContext> options) : base(options)
        {

        }
    }
}
