using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TestProject.Models
{
    public class Device : Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("DeviceTypeId")]
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public List<DevicePropertyValue>  DevicePropertyValue { get; set; }
    }
}
