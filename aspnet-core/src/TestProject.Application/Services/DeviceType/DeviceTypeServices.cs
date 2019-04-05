using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestProject.Dto;

namespace TestProject.Services.DeviceType
{
    public class DeviceTypeServices : TestProjectAppServiceBase, IDeviceTypeServices
    {
        private readonly IRepository<Models.DeviceType> _deviceTypeRepository;

        public DeviceTypeServices(IRepository<Models.DeviceType> deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }
        public void Create(Models.DeviceType input)
        {
            _deviceTypeRepository.Insert(input);
        }

        public void Edit(int id, Models.DeviceType input)
        {
            _deviceTypeRepository.Update(input);
        }

        public void Delete(int id)
        {
            _deviceTypeRepository.Delete(id);
        }

        public List<DeviceTypeDto> GetAll()
        {
             var devices = _deviceTypeRepository.GetAll().Include(x => x.DeviceTypeProperty);
            
            return ObjectMapper.Map<List<DeviceTypeDto>>(devices);
        }

        public DeviceTypeDto GetById(int id)
        {
            var device = _deviceTypeRepository.Get(id);

            return ObjectMapper.Map<DeviceTypeDto>(device);
        }

        public List<DeviceTypeDto> DeviceTypeTree(int? parentId)
        {
            // Recursion grupisati pomprednt Id
        }
    }
}
