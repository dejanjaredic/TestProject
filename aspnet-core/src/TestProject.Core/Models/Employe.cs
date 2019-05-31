using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using JetBrains.Annotations;

namespace TestProject.Models
{
    public class Employe : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Experience> Experiences { get; set; } = new List<Experience>();
    }
}
