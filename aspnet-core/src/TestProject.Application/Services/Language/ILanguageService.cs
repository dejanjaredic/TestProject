using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Users.Dto;

namespace TestProject.Services.Language
{
    public interface ILanguageService
    {
        void Create(LanguageDto input);
        LanguageDto GetById(int id);
    }
}
