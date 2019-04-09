using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Dto.DeviceTypeDtos
{
    [AutoMap(typeof(DeviceType))]
    public class DeviceTypeCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
       
    }
}
