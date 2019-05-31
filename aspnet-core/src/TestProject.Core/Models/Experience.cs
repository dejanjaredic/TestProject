using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace TestProject.Models
{
    public class Experience : Entity
    {
        public string Organisation { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeId { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("EmployeId")]
        public Employe Employe { get; set; }
        [ForeignKey("LanguageId")]
        public MyLanguage Language { get; set; }
    }
}
