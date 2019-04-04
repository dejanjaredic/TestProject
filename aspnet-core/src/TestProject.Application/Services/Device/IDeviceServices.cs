using Abp.Application.Services;
using TestProject.Models;

namespace TestProject.Services
{
    public interface IDeviceServices : IApplicationService
    {
        void Create(Device input);
        void Edit(int id, Device input);
        void Delete(int id);
        Device GetAll();
        Device GetById(int id);
    }
}
