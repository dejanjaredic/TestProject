﻿using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TestProject.Dto.DeviceTypeDtos;
using TestProject.Dto.DeviceTypePropertyDtos;
using TestProject.Models;

namespace TestProject.Services.DeviceType
{
    public class DeviceTypeServices : TestProjectAppServiceBase, IDeviceTypeServices
    {
        private readonly IRepository<Models.DeviceType> _deviceTypeRepository;

        public DeviceTypeServices(IRepository<Models.DeviceType> deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
            
        }
        public IEnumerable<DeviceTypePropertiesNestedDto>  Create(DeviceTypeCreateDto input)
        {
            if (input.Id == 0)
            {
                _deviceTypeRepository.Insert(ObjectMapper.Map<Models.DeviceType>(input));
            }
            else
            {
                var deviceType = _deviceTypeRepository.Get(input.Id);
                ObjectMapper.Map(input, deviceType);
            }

            return DeviceTypeTreeWithProperties(input.ParentId);

        }

        public void CreatePropertyForDeviceTpe(List<DeviceTypePropertyCreateDto> input)
        {
            foreach (var property in input)
            {
                var getDeviceType = _deviceTypeRepository.Get(property.Id);
                var addProperty = new DeviceTypePropertyCreateDto()
                {

                    NameProperty = property.NameProperty,
                    Required = property.Required,
                    Type = property.Type
                };

                getDeviceType.DeviceTypeProperty.Add(ObjectMapper.Map<DeviceTypeProperty>(addProperty));

            }
        }

        public void Delete(int id)
        {
            var deviceType = _deviceTypeRepository.Get(id);
            _deviceTypeRepository.Delete(deviceType);
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

        public List<DeviceTypeNestedDto> DeviceTypeTree(int? parentId)
        {
            var allDeviceTypes = _deviceTypeRepository.GetAll().Where(x => x.ParentId == parentId).ToList();
            var list = new List<DeviceTypeNestedDto>();
            foreach (var listType in allDeviceTypes)
            {
                var listDeviceTypes = new DeviceTypeNestedDto();
                listDeviceTypes.Id = listType.Id;
                listDeviceTypes.Name = listType.Name;
                listDeviceTypes.ParentId = listType.ParentId;
                listDeviceTypes.Description = listType.Description;
                listDeviceTypes.Children = DeviceTypeTree(listType.Id);
                list.Add(listDeviceTypes);
            }
            return ObjectMapper.Map<List<DeviceTypeNestedDto>>(list);
        }


        public IEnumerable<DeviceTypePropertiesNestedDto> DeviceTypeTreeWithProperties(int? deviceId)
        {
            var allDeviceTypes = _deviceTypeRepository.GetAll().Include(y => y.DeviceTypeProperty).Where(x => x.Id == deviceId).First();
            var list = new List<DeviceTypePropertiesNestedDto>();
            
            var device = new DeviceTypePropertiesNestedDto()
            {
                Id = allDeviceTypes.Id,
                Name = allDeviceTypes.Name,
                Description = allDeviceTypes.Description,
                ParentId = allDeviceTypes.ParentId,
                Properties = ObjectMapper.Map<List<DeviceTypePropertyDto>>(allDeviceTypes.DeviceTypeProperty)
                
            };
            if (allDeviceTypes.ParentId == null)
            {
                list.Add(device);
                return list;
            }
            list.Add(device);

            return list.Concat(DeviceTypeTreeWithProperties(allDeviceTypes.ParentId));
        }
    }
}
