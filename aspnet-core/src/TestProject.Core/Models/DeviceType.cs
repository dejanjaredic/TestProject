using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using JetBrains.Annotations;

namespace TestProject.Models
{
    public class DeviceType : Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string Description { get; set; }
        [CanBeNull] public DeviceType Parent { get; set; }
        public List<Device> Devices { get; set;} = new List<Device>();
        public List<DeviceTypeProperty> DeviceTypeProperty { get; set; } = new List<DeviceTypeProperty>();
    }
}
