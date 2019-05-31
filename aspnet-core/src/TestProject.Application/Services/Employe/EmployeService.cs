using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Abp.Domain.Repositories;

namespace TestProject.Services.Employe
{
    public class EmployeService : TestProjectAppServiceBase, IEmployeService
    {
        private readonly IRepository<Models.Employe> _employeRepository;

        public EmployeService(IRepository<Models.Employe> employeRepository)
        {
            _employeRepository = employeRepository;
        }
        public void Create(EmloyeDto input)
        {
            var emp = input.MapTo<Models.Employe>();
            _employeRepository.Insert(emp);
        }

        public EmloyeDto GetById(int id)
        {
            var employe = _employeRepository.Get(id);
            return employe.MapTo<EmloyeDto>();
        }
    }
}
