using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Services.Experience
{
    [AutoMap(typeof(Models.Experience))]
    public class ExperienceDto
    {
        public string Organisation { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeId { get; set; }
        public int LanguageId { get; set; }
    }
}