using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Dto.QueryInfoDtos
{
    public class QueryInfo
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchText { get; set; }
        public List<Sorters> Sorters { get; set; } = new List<Sorters>();
        public List<string> SearchProperties { get; set; } = new List<string>();
        public Filter Filter { get; set; }
}
