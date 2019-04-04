using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TestProject.Models
{
    public class DevicePropertyValue : Entity
    {
        
        public int DeviceTypePropertyId { get; set; }
        
        public int DeviceId { get; set; }
        public string Value { get; set; }
        [ForeignKey("DeviceTypePropertyId")]
        public DeviceTypeProperty DeviceTypeProperty { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }
}
