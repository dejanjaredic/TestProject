using System.Collections.Generic;
using Abp.Application.Services;
using TestProject.Dto.DeviceTypeDtos;

namespace TestProject.Services.DeviceType
{
    public interface IDeviceTypeServices : IApplicationService
    {
        IEnumerable<DeviceTypePropertiesNestedDto> Create(DeviceTypeCreateDto input);
        void DeleteType(int id);
        List<DeviceTypeDto> GetAll();
        DeviceTypeDto GetById(int id);
        List<DeviceTypeNestedDto> DeviceTypeTree(int? parentId);
        IEnumerable<DeviceTypePropertiesNestedDto> DeviceTypeTreeWithProperties(int? deviceId);
    }
}
