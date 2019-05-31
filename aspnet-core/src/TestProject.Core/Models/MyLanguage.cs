using Abp.Domain.Entities;

namespace TestProject.Models
{
    public class MyLanguage : Entity
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
    }
}