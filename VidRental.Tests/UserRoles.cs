using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.Roles;
using Xunit;

namespace VidRental.Tests
{
    public class UserRoles
    {
        [Fact]
        public void AdminRole_AdminStr()
        {
            var str = ApiRoles.Admin;
            Assert.Equal("Admin", str);
        }

        [Fact]
        public void UserRole_UserStr()
        {
            var str = ApiRoles.User;
            Assert.Equal("User", str);
        }

        [Fact]
        public void EmployeeRole_EmployeeStr()
        {
            var str = ApiRoles.Employee;
            Assert.Equal("Employee", str);
        }

        [Fact]
        public void AdminStr_AdminRole()
        {
            var role = ApiRoles.ToRole("Admin");
            Assert.Equal(RolesEnum.Admin, role);
        }

        [Fact]
        public void UserStr_UserRole()
        {
            var role = ApiRoles.ToRole("User");
            Assert.Equal(RolesEnum.User, role);
        }

        [Fact]
        public void EmployeeStr_EmployeeRole()
        {
            var role = ApiRoles.ToRole("Employee");
            Assert.Equal(RolesEnum.Employee, role);
        }

        [Fact]
        public void RolesToIdentityRoles_IdentityRolesCorrectType()
        {
            var identityRoles = ApiRoles.IdentityRoles;

            Assert.IsType<IdentityRole>(identityRoles[0]);
            Assert.IsType<IdentityRole>(identityRoles[1]);
            Assert.IsType<IdentityRole>(identityRoles[2]);
        }

        [Fact]
        public void RolesToIdentityRoles_IdentityRolesCorrectRole()
        {
            var identityRoles = ApiRoles.IdentityRoles;

            Assert.Equal("Admin", identityRoles[0].Name);
            Assert.Equal("Employee", identityRoles[1].Name);
            Assert.Equal("User", identityRoles[2].Name);
        }
    }
}
