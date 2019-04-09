using System.Collections.Generic;
using Abp.Application.Services;
using TestProject.Dto.DeviceDtos;

namespace TestProject.Services.Device
{
    public interface IDeviceServices : IApplicationService
    {
        void Create(Models.Device input);
        void Edit(int id, Models.Device input);
        void Delete(int id);
        List<DeviceDto> GetAll();
        Models.Device GetById(int id);
    }
}
