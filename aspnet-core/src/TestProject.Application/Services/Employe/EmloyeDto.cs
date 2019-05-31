using Abp.AutoMapper;

namespace TestProject.Services.Employe
{
    [AutoMap(typeof(Models.Employe))]
    public class EmloyeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}