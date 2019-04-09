using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using TestProject.Dto.DeviceDtos;

namespace TestProject.Services.Device
{
    
    public class DeviceServices : TestProjectAppServiceBase, IDeviceServices
    {
        private readonly IRepository<Models.Device> _deviceRepository;

        public DeviceServices(IRepository<Models.Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        public void Create(Models.Device input)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Models.Device input)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var device = _deviceRepository.Get(id);
            if (device == null)
            {
                throw new UserFriendlyException("Nepostojeci uredjaj");
            }
            _deviceRepository.Delete(device);
        }

        public List<DeviceDto> GetAll()
        {
            var allDevices = _deviceRepository.GetAll().Include(x => x.DeviceType);
            return  ObjectMapper.Map<List<DeviceDto>>(allDevices);
        }

        public Models.Device GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
