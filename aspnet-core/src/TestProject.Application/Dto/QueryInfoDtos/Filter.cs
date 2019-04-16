using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Dto.QueryInfoDtos
{
    public class Filter
    {
        public string Condition { get; set; }
        public List<Rules> Rule { get; set; } = new List<Rules>();
    }
}
