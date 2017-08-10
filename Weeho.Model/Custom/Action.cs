using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Model.Custom
{
    public class Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string ActionIcon { get; set; }
        public string ActionTitle { get; set; }
        public int IsShow { get; set; }
    }
}
