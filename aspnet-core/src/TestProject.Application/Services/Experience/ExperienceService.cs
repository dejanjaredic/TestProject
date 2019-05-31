using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Abp.Domain.Repositories;

namespace TestProject.Services.Experience
{
    public class ExperienceService : TestProjectAppServiceBase, IExperienceService
    {
        private readonly IRepository<Models.Experience> _expRepository;

        public ExperienceService(IRepository<Models.Experience> expRepository)
        {
            _expRepository = expRepository;
        }
        public void Create(ExperienceDto input)
        {
            var exp = input.MapTo<Models.Experience>();
            _expRepository.Insert(exp);
        }

        public ExperienceDto GetById(int id)
        {
            var exp = _expRepository.Get(id);
            return exp.MapTo<ExperienceDto>();
        }
    }
}
