using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using TestProject.Services.Experience;

namespace TestProject.Services.Employe
{
    [AutoMap(typeof(Models.Employe))]
    public class EmployeeDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
    }
}
