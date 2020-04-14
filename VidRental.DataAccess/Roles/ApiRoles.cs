using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidRental.DataAccess.Roles
{
    public static class ApiRoles
    {
        public static List<string> Roles => Enum.GetNames(typeof(RolesEnum)).ToList();
        public static List<IdentityRole> IdentityRoles => Roles.Select(r => new IdentityRole { Name = r }).ToList(); 

        public static string ToStr(RolesEnum role) => role.ToString();

        public static RolesEnum ToRole(string role) => Enum.Parse<RolesEnum>(role);

        public static string Admin => ToStr(RolesEnum.Admin);
        public static string Employee => ToStr(RolesEnum.Employee);
        public static string User => ToStr(RolesEnum.User);
    }
    public enum RolesEnum
    {
        Admin, 
        Employee, 
        User
    } 
}
