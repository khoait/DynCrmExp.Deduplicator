using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynCrmExp.Deduplicator.Models
{
    public class DuplicationConfiguration
    {
        public string TargetEntity { get; set; }
        public IDictionary<string, AttributeMetadata> AttributesMeta { get; set; }
        public IEnumerable<string> MatchAttributes { get; set; }
        public IEnumerable<string> ViewAttributes { get; set; }
        public bool IsCaseSensitive { get; set; }
        public bool ShouldIgnoreWhiteSpace { get; set; }
        public bool ShouldIgnoreBlank { get; set; }

        public DuplicationConfiguration(string entity, IDictionary<string, AttributeMetadata> attributesMeta, IEnumerable<string> matchAttributes, IEnumerable<string> viewAttributes)
        {
            if (string.IsNullOrWhiteSpace(entity))
                throw new ArgumentNullException("entity", "Target entity name is required");
            if (string.IsNullOrWhiteSpace(entity))
                throw new ArgumentNullException("attributesMeta", "Metadata of attributes is required");
            if (matchAttributes == null || (matchAttributes != null && !matchAttributes.Any()))
                throw new ArgumentNullException("matchAttributes", "List of attributes to match is required");
            if (viewAttributes == null || (viewAttributes != null && !viewAttributes.Any()))
                throw new ArgumentNullException("matchAttributes", "List of attributes to view is required");

            TargetEntity = entity;
            AttributesMeta = attributesMeta;
            MatchAttributes = matchAttributes;
            ViewAttributes = viewAttributes;
        }
    }
}
