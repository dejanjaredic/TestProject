using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TestProject.Services.Experience;

namespace TestProject.Services.Employe
{
    public class EmployeService : TestProjectAppServiceBase, IEmployeService
    {
        private readonly IRepository<Models.Employe> _employeRepository;
        private readonly IRepository<Models.Experience> _expRepository;

        public EmployeService(IRepository<Models.Employe> employeRepository, IRepository<Models.Experience> expRepository)
        {
            _employeRepository = employeRepository;
            _expRepository = expRepository;
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

        public List<ExperienceDto> Details(int id, int lid)
        {
            var details = _expRepository.GetAll().Include(x => x.Employe)
                .Where(y => y.EmployeId == id && y.LanguageId == lid).ToList();

            var list = new List<ExperienceDto>();

            foreach (var detail in details)
            {
                 var exp = new ExperienceDto();
                exp.StartDate = detail.StartDate;
                exp.EndDate = detail.EndDate;
                exp.Description = detail.Description;
                exp.Organisation = detail.Organisation;

                list.Add(exp);
            }

            return ObjectMapper.Map<List<ExperienceDto>>(list);
        }
    }
}
