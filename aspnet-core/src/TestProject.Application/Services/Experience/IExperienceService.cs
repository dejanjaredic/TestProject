using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Services.Experience
{
    public interface IExperienceService
    {
        void Create(ExperienceDto input);
        ExperienceDto GetById(int id);
    }
}
