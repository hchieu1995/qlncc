namespace Admin.EntityFrameworkCore.Repositories
{
    public class TableFilterItem
    {
        public int? id { get; set; }
        public string filter { get; set; }
        public string filterext { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string sort { get; set; }
        public int? tenantId { get; set; }
        public int? trangthai { get; set; }
    }
    public class TableSorterItem
    {
        public string selector { get; set; }
        public bool desc { get; set; }
    }
    public class TableSearchItem
    {
        public string selector { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
}
