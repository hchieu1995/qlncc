using System.Collections.Generic;

namespace Admin.EntityFrameworkCore.Repositories
{
    public class TableShowItem
    {
        public List<object> data { get; set; }
        public int totalCount { get; set; }
        public List<int> summary { get; set; }
        public int groupCount { get; set; }
    }
}
