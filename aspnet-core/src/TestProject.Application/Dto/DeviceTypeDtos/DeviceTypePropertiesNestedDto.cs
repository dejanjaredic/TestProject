﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;
using TestProject.Dto.DeviceTypePropertyDtos;

namespace TestProject.Dto.DeviceTypeDtos
{
    public class DeviceTypePropertiesNestedDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<DeviceTypePropertyDto> Properties { get; set; } = new List<DeviceTypePropertyDto>();
    }
}
