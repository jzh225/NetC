using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Model.Custom
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsShow { get; set; }
        public int SortId { get; set; }
    }
}
