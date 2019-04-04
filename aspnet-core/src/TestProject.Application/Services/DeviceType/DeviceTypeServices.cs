using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;

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

        public List<Models.DeviceType> GetAll()
        {
             var devices = _deviceTypeRepository.GetAll();
            return devices.ToList();
        }

        public Models.DeviceType GetById(int id)
        {
            return _deviceTypeRepository.Get(id);
        }

        //public IEnumerable<Models.DeviceType> DeviceTypeTree(int parentId)
        //{
        //    List<int> allTypes = new List<int>();
        //    var getDeviceTypes = _deviceTypeRepository.GetAll().Where(x => x.ParentId == parentId);
        //    foreach (var deviceType in getDeviceTypes)
        //    {
               
        //    }
            
        //}
    }
}
