using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Dto
{
    [AutoMap(typeof(DeviceType))]
    public class DeviceTypeNestedDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        //[CanBeNull] public DeviceType Parent { get; set; }
        //public List<DeviceTypeProperty> Properties { get; set; } = new List<DeviceTypeProperty>();
        public List<DeviceTypeNestedDto> Children { get; set; } = new List<DeviceTypeNestedDto>();
    }
}
