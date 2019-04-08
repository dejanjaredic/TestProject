using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using TestProject.Dto.DeviceTypePropertyDtos;
using TestProject.Models;

namespace TestProject.Dto
{
    public class DeviceTypePropertiesNestedDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<DeviceTypePropertyDto> Properties { get; set; } = new List<DeviceTypePropertyDto>();
        //public List<DeviceTypePropertiesNestedDto> Children { get; set; } = new List<DeviceTypePropertiesNestedDto>();
    }
}
