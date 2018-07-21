using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynCrmExp.Deduplicator.Models
{
    public class AttributeMetadataModel
    {
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }
        public bool IsMatch { get; set; }
        public bool IsView { get; set; }
    }
}
