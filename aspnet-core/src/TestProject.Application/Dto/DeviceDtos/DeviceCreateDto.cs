using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using TestProject.Dto.DeviceTypeDtos;
using TestProject.Models;

namespace TestProject.Dto.DeviceDtos
{
    public class DeviceCreateDto : EntityDto
    {

        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public List<DeviceTypeCreateDeviceDto> DeviceTypes { get; set; }
    }
}
