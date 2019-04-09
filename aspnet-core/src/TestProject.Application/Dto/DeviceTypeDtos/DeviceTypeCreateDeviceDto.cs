using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using TestProject.Models;

namespace TestProject.Dto.DeviceTypeDtos
{
    public class DeviceTypeCreateDeviceDto : EntityDto
    {
        public List<DevicePropertyValue> PropValues { get; set; }
    }
}
