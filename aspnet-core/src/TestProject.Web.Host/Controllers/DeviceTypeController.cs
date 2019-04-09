using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models;
using TestProject.Services.DeviceType;

namespace TestProject.Web.Host.Controllers
{
    public class DeviceTypeController : Controller
    {
        private readonly IDeviceTypeServices _deviceTypeServices;

        public DeviceTypeController(IDeviceTypeServices deviceTypeServices)
        {
            _deviceTypeServices = deviceTypeServices;
        }
        public IActionResult Index()
        {
            return View();
        }

       
    }
}