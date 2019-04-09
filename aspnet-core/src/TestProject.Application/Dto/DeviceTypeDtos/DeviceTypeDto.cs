using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Dto.DeviceTypeDtos
{
    [AutoMap(typeof(DeviceType))]
    public class DeviceTypeDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<DeviceTypeProperty> Properties { get; set; } = new List<DeviceTypeProperty>();
    }
}
