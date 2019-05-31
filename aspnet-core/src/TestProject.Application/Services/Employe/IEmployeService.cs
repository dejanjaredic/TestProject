using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;

namespace TestProject.Services.Employe
{
    public interface IEmployeService : IApplicationService
    {
        void Create(EmloyeDto input);
    }
}
