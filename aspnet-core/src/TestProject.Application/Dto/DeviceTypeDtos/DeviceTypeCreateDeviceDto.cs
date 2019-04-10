using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using TestProject.Dto.DeviceTypePropertyValueDtos;
using TestProject.Models;

namespace TestProject.Dto.DeviceTypeDtos
{
    public class DeviceTypeCreateDeviceDto
    {
        public int DeviceTypeId { get; set; }
        public List<PropertyValuesCreateDeviceDto> PropValues { get; set; }
    }
}
