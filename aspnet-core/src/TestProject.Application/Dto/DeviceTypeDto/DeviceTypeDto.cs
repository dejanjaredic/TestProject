using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JetBrains.Annotations;
using TestProject.Models;

namespace TestProject.Dto
{
    [AutoMap(typeof(DeviceType))]
    public class DeviceTypeDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        //[CanBeNull] public DeviceType Parent { get; set; }
        public List<DeviceTypeProperty> Properties { get; set; } = new List<DeviceTypeProperty>();
        //public List<DeviceTypeDto> Children { get; set; } = new List<DeviceTypeDto>();
    }
}
