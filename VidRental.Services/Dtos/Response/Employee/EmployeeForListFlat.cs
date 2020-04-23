using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Employee
{
    public class EmployeeForListFlat
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
