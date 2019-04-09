using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Dto.DeviceTypePropertyDtos
{
    public class DeviceTypePropertyCreateDto
    {
        public int Id { get; set; }
        public string NameProperty { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
    }
}
