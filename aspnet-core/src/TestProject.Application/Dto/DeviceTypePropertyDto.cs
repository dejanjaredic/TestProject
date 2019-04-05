using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestProject.Dto
{
    public class DeviceTypePropertyDto
    {
        [Required]
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
    }
}
