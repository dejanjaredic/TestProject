using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using TestProject.Models;

namespace TestProject.Dto.DeviceTypePropertyDtos
{
    //[AutoMap(typeof(DeviceTypeProperty))]
    public class DeviceTypePropertyDto
    {
        public string NameProperty { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
    }
}
