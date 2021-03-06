﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using TestProject.Dto.DeviceDtos;
using TestProject.Dto.DeviceTypeDtos;
using TestProject.Dto.DeviceTypePropertyValueDtos;
using TestProject.Dto.QueryInfoDtos;
using TestProject.Models;
using TestProject.Services.DeviceType;

namespace TestProject.Services.Device
{
    
    public class DeviceServices : TestProjectAppServiceBase, IDeviceServices
    {
        private readonly IRepository<Models.Device> _deviceRepository;
        private readonly IRepository<Models.DeviceType> _deviceTypeRepository;
        private readonly IRepository<DeviceTypeProperty> _devicePropRepository;
        private readonly IRepository<DevicePropertyValue> _devicePropValueRepository;
        
        private readonly IDeviceTypeServices _deviceTypeService;

        public DeviceServices(IRepository<Models.Device> deviceRepository, IRepository<Models.DeviceType> deviceTypeRepository, IRepository<DeviceTypeProperty> devicePropRepository, IRepository<DevicePropertyValue> devicePropValueRepository, IDeviceTypeServices deviceTypeService)
        {
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
            _devicePropRepository = devicePropRepository;
            _devicePropValueRepository = devicePropValueRepository;
            _deviceTypeService = deviceTypeService;
        }
        public void Create(DeviceCreateDto input)
        {
            var device = new DeviceCreateDto();
            device.DeviceName = input.DeviceName;
            device.Description = input.Description;
            device.DeviceTypes = input.DeviceTypes;
            foreach (var deviceType in input.DeviceTypes)
            {
                
                var prList = new DeviceTypeCreateDeviceDto();
                prList.DeviceTypeId = deviceType.DeviceTypeId;

                prList.PropValues = new List<PropertyValuesCreateDeviceDto>();
                foreach (var propValue in prList.PropValues)
                {
                    var prValues = new PropertyValuesCreateDeviceDto();
                    prValues.PropName = propValue.PropName;
                    prValues.Value = propValue.Value;
                }
            }

            if (input.DeviceId == 0)
            {
                var newDevice = new Models.Device();
                newDevice.Name = device.DeviceName;
                newDevice.Description = device.Description;
                newDevice.DevicePropertyValue = new List<DevicePropertyValue>();
                var types = input.DeviceTypes;
                foreach (var type in types)
                {
                    foreach (var prop in type.PropValues)
                    {
                        var propId = _devicePropRepository.GetAll().Include(x =>
                            x.DeviceType).First(y => y.Name == prop.PropName && y.DeviceTypeId == type.DeviceTypeId).Id;
                        newDevice.DevicePropertyValue.Add(new DevicePropertyValue()
                        {
                            DeviceId = newDevice.Id,
                            Value = prop.Value,
                            DeviceTypePropertyId = propId
                        });
                    }
                    newDevice.DeviceTypeId = input.DeviceTypes.Max(x => x.DeviceTypeId);

                    _deviceRepository.Insert(newDevice);

                }
            }
            else
            {
                var oldDevice = _deviceRepository.GetAll().Include(x => x.DevicePropertyValue).FirstOrDefault(x => x.Id == input.DeviceId);
                oldDevice.Name = input.DeviceName;
                oldDevice.Description = input.Description;

                var types = input.DeviceTypes;
                foreach (var type in types)
                {
                    foreach (var prop in type.PropValues)
                    {
                        var getPropValue = _devicePropValueRepository.GetAll().Include(x => x.Device)
                            .Include(x => x.DeviceTypeProperty);

                        var getExistingDevice = getPropValue.Where(x =>
                            x.DeviceId == oldDevice.Id && x.DeviceTypeProperty.Name == prop.PropName).First();

                        getExistingDevice.Value = prop.Value;
                    }
                }
            }
            
        }
        // Izlistravanje svih device typova rekurzijom nested
        public List<DeviceTypeNestedDto> GetDeviceTypesTree()
        {
            var getTree = _deviceTypeService.DeviceTypeTree(null);
            return getTree;
        }
        // Izlistavanje svih device typova i njihovih propertija
        public IEnumerable<DeviceTypePropertiesNestedDto> GetAllPropertiesFromType(int deviceId)
        {
            var device = _deviceTypeService.DeviceTypeTreeWithProperties(deviceId);
            return device;
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

        public List<Models.Device> SearchFilter(QueryInfo input)
        {
            QueryInfo info = new QueryInfo();
            var getAllDevice = _deviceRepository.GetAll();
            var ruleInputs = input.Filter.Rule;
            var condition = input.Filter.Condition;
            Expression result;
            
            var sorters = input.Sorters;
            Expression containsExpression = Expression.Constant(false);
            Expression currContains;


            Expression<Func<Models.Device, bool>> whereLambdaExpression = null;
            BinaryExpression binWhereExp = null;

           
            ParameterExpression parExp = Expression.Parameter(typeof(Models.Device), "x");
            Expression findExp = null;

            foreach (var prop in input.SearchProperties)
            {
                var propExp = Expression.Property(parExp, prop);
                var search = input.SearchText;
                var convertedSearch = Convert.ChangeType(search, propExp.Type);
                var con = Expression.Constant(convertedSearch);
                switch (convertedSearch)
                {
                    case string _:
                        currContains = info.GetBinaryExpressionForString("ct", propExp, con);
                        containsExpression = Expression.OrElse(containsExpression, currContains);
                        break;
                    case int _:
                        currContains = info.GetBinaryExpressionForInt("ct", propExp, con);
                        containsExpression = Expression.OrElse(containsExpression, currContains);
                        break;
                    default:
                        throw new UserFriendlyException("Mrs");
                }
               
            }

            
            var filterResult = info.FilterRuleNested<Models.Device>(parExp, ruleInputs, condition);
            result = Expression.AndAlso(containsExpression, filterResult);

            //foreach (var ruleInput in ruleInputs)
            //{
            //    if (input.Filter.Condition == "and")
            //    {
            //        findExp = ConstantExpression.Constant(true);
            //        binWhereExp =
            //            info.GetWhereExp<Models.Device>(parExp, ruleInput.Operator, ruleInput.Property, ruleInput.Value);
            //        findExp = BinaryExpression.AndAlso(findExp, binWhereExp);
            //    }
            //    else if (input.Filter.Condition == "or")
            //    {
            //        findExp = Expression.Constant(false);
            //        binWhereExp =
            //            info.GetWhereExp<Models.Device>(parExp, ruleInput.Operator, ruleInput.Property,
            //                ruleInput.Value);
            //        findExp = BinaryExpression.OrElse(findExp, binWhereExp);
            //    }
            //}

            whereLambdaExpression = info.GetWhereLambda<Models.Device>(result, parExp);

            Expression<Func<Models.Device, object>> order = null;
            getAllDevice = getAllDevice.Where(whereLambdaExpression);
            var sorted = false;
            foreach (var sorter in sorters)
            {
                order = info.OrderThings<Models.Device>(sorter.Property, sorter.Direction);
                if (sorter.Direction.ToLower().Equals("asc"))
                {
                    if (!sorted)
                    {
                        getAllDevice = getAllDevice.OrderBy(order);
                        sorted = true;
                    }
                    else
                    {
                        getAllDevice = ((IOrderedQueryable<Models.Device>)getAllDevice).ThenBy(order);
                    }
                }

                if (sorter.Direction.ToLower().Equals("desc"))
                {
                    if (!sorted)
                    {
                        getAllDevice = getAllDevice.OrderByDescending(order);
                        sorted = true;
                    }
                    else
                    {
                        getAllDevice = ((IOrderedQueryable<Models.Device>)getAllDevice).ThenByDescending(order);
                    }
                }
            }

            return getAllDevice.Skip(info.Skip).Take(info.Take).ToList();
        }
    }
}
