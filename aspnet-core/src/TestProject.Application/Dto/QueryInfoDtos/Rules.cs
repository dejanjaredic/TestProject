using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Dto.QueryInfoDtos
{
    public class Rules
    {
        public string Property { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; }
        public int ParentId { get; set; }
        public List<Rules> Rule { get; set; } = new List<Rules>();
    }
}
