using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Dto.DeviceTypeDtos;

namespace TestProject.Dto.DeviceDtos
{
    public class DeviceCreateDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public List<DeviceTypeCreateDeviceDto> DeviceTypes { get; set; }
    }
}
