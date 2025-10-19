using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared.DomainTranferObjects
{
    public class CrmSelectListItem
    {
        public int? Key { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}

