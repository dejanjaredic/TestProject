using TestProject.Dto.DeviceTypeDtos;

namespace TestProject.Dto.DeviceDtos
{
    public class DeviceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DeviceTypeDto DeviceType { get; set; }
    }
}
