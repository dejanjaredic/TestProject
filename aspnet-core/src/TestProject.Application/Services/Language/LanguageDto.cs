using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Services.Language
{
    [AutoMap(typeof(MyLanguage))]
    public class LanguageDto
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
    }
}