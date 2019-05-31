using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TestProject.Models;

namespace TestProject.Services.Language
{
    public class LanguageService : TestProjectAppServiceBase, ILanguageService
    {
        private readonly IRepository<MyLanguage> _languageRepository;

        public LanguageService(IRepository<MyLanguage> languageRepository)
        {
            _languageRepository = languageRepository;
        }
        public void Create(LanguageDto input)
        {
            var lan = input.MapTo<MyLanguage>();
            _languageRepository.Insert(lan);
        }

        public LanguageDto GetById(int id)
        {
            var lan = _languageRepository.Get(id);
            return lan.MapTo<LanguageDto>();

        }
    }
}
