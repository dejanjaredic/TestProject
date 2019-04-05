using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Dto
{
    [AutoMap(typeof(DeviceTypeProperty))]
    public class DeviceTypePropertyDto
    {
        [Required]
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
    }
}
