using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynCrmExp.Deduplicator.Models
{
    public class DuplicatedDataModel
    {
        public string MatchingKey { get; set; }
        public Entity DuplicatedData { get; set; }
        public int DuplicatesCount { get; set; }
    }
}
