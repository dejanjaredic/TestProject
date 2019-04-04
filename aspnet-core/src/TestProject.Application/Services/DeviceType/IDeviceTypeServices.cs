using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using TestProject.Models;

namespace TestProject.Services.DeviceType
{
    public interface IDeviceTypeServices : IApplicationService
    {
        void Create(Models.DeviceType input);
        void Edit(int id, Models.DeviceType input);
        void Delete(int id);
        List<Models.DeviceType> GetAll();
        Models.DeviceType GetById(int id);
    }
}
