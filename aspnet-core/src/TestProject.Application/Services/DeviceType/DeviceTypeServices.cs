using System.Collections.Generic;
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
        private readonly IRepository<DevicePropertyValue> _devicePropValueRepository;
        private readonly IRepository<Models.Device> _deviceRepository;

        public DeviceTypeServices(IRepository<Models.DeviceType> deviceTypeRepository, IRepository<DevicePropertyValue> devicePropValueRepository, IRepository<Models.Device> deviceRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _devicePropValueRepository = devicePropValueRepository;
            _deviceRepository = deviceRepository;
        }
        public IEnumerable<DeviceTypePropertiesNestedDto>  Create(DeviceTypeCreateDto input)
        {
            if (input.Id == 0)
            {
                int id = _deviceTypeRepository.InsertAndGetId(ObjectMapper.Map<Models.DeviceType>(input));
                return DeviceTypeTreeWithProperties(id);
            }
            else
            {
                var deviceType = _deviceTypeRepository.Get(input.Id);
                ObjectMapper.Map(input, deviceType);
                return DeviceTypeTreeWithProperties(input.Id);
            }

           
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

        public IEnumerable<Models.DeviceType> GetTypesWithProp(int deviceId)
        {
            var type = _deviceTypeRepository.GetAll().Include(y => y.DeviceTypeProperty).Where(x => x.Id == deviceId).First();
            var list = new List<Models.DeviceType>();
            var childList = _deviceTypeRepository.GetAll().Include(y => y.DeviceTypeProperty).Where(x => x.ParentId == deviceId);
           
            if (!childList.Any())
            {
                list.Add(type);
                return list;
            }

            foreach (var child in childList)
            {
                list.AddRange(GetTypesWithProp(child.Id));
            }
            list.Add(type);

            return list;
        }

        public void DeleteType(int id)
        {
            var list = _deviceRepository.GetAll().Include(x => x.DevicePropertyValue).Where(x => x.DeviceType.Id == id);
            var getAllWithChilds = GetTypesWithProp(id).ToList();
            var getOrderedChildren = getAllWithChilds.OrderByDescending(x => x.Id);
            foreach (var devices in list)
            {
                foreach (var deviceValue in devices.DevicePropertyValue)
                {
                    _devicePropValueRepository.Delete(deviceValue);
                }
                _deviceRepository.Delete(devices);
            }
            foreach (var type in getOrderedChildren)
            {
                _deviceTypeRepository.Delete(type);
            }

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
                listDeviceTypes.Items = DeviceTypeTree(listType.Id);
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
            if (_deviceTypeRepository.GetAll().Count() == 1)
            {
                list.Add(device);
                return list;
            }
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
