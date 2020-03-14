using DynCrmExp.Deduplicator.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynCrmExp.Deduplicator
{
    public class DataProcessor
    {       
        private IEnumerable<string> _selectAttributes { get; set; }
        private ILookup<string, Entity> _duplicationGroups;
        public readonly DuplicationConfiguration Config;
        public DataProcessor(DuplicationConfiguration config)
        {
            Config = config;
            _selectAttributes = Config.MatchAttributes.Concat(Config.ViewAttributes).Distinct();            
        }

        public IEnumerable<DuplicatedDataModel> GetDuplicationGroups(IOrganizationService service)
        {
            var results = CrmHelper.GetAllData(service, Config.TargetEntity, _selectAttributes, Config.MatchAttributes, Config.ShouldIgnoreBlank);
            Func<Entity, IEnumerable<string>, string> groupingFunction = BuildMatchingKey;
            _duplicationGroups = results.ToLookup(e => groupingFunction(e, Config.MatchAttributes));
            //.SelectMany(g => g, (parent, child) => EntityToDynamic(child, parent.Key));

            return _duplicationGroups.Where(g => g.Count() > 1)
                .Select(g => new DuplicatedDataModel
                {
                    MatchingKey = g.Key,
                    DuplicatedData = EntityToDynamic(g.FirstOrDefault(), g.Key) as IDictionary<string, object>,
                    DuplicatesCount = g.Count()
                });
        }        

        public IEnumerable<ExpandoObject> GetDuplicates(string key)
        {
            var results = _duplicationGroups[key];
            return results.Select(e => EntityToDynamic(e, key));
        }

        public IEnumerable<ExpandoObject> GetExportData()
        {
            var results = _duplicationGroups
                .Where(g => g.Count() > 1)
                .SelectMany(g => g, (parent, child) => EntityToDynamic(child, parent.Key));
            return results;
        }

        private string BuildMatchingKey(Entity entity, IEnumerable<string> groupAttributes)
        {
            var sb = new StringBuilder();
            var keyString = string.Empty;
            sb.Clear();
            foreach (var attr in groupAttributes)
            {
                var stringvalue = GetAttributeStringValue(entity, attr);
                if (!Config.IsCaseSensitive)
                {
                    stringvalue = stringvalue.ToLower();
                }

                if (Config.ShouldIgnoreWhiteSpace)
                {
                    stringvalue = stringvalue.Trim();
                }

                if (Config.ShouldIgnoreBlank && string.IsNullOrEmpty(stringvalue))
                {
                    continue;
                }

                sb.Append(stringvalue);
                sb.Append("_");
            }
            keyString = sb.ToString();
            return keyString;
        }

        private string GetAttributeStringValue(Entity entity, string attribute)
        {
            var result = string.Empty;
            if (entity.Contains(attribute) && entity[attribute] != null)
            {
                var attrType = Config.AttributesMeta[attribute].AttributeType.Value;
                if (attrType == AttributeTypeCode.Lookup ||
                    attrType == AttributeTypeCode.Customer ||
                    attrType == AttributeTypeCode.Owner)
                {
                    var val = entity.GetAttributeValue<EntityReference>(attribute);
                    result = val.Id.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Boolean)
                {
                    var val = entity.GetAttributeValue<bool>(attribute);
                    result = val.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Uniqueidentifier)
                {
                    var val = entity.GetAttributeValue<Guid>(attribute);
                    result = val.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Picklist ||
                    attrType == AttributeTypeCode.State ||
                    attrType == AttributeTypeCode.Status)
                {
                    var val = entity.GetAttributeValue<OptionSetValue>(attribute);
                    result = val.Value.ToString().ToLower();
                }
                else if (entity.FormattedValues.Contains(attribute))
                {
                    result = entity.FormattedValues[attribute];
                }
                else
                {
                    // double check value type, just in case
                    var val = entity[attribute];
                    if(val is EntityReference)
                    {
                        result = ((EntityReference)val).Id.ToString();
                    }
                    else if(val is OptionSetValue)
                    {
                        result = ((OptionSetValue)val).Value.ToString();
                    }
                    else
                    {
                        result = val.ToString();
                    }
                }
            }
            return result;
        }

        private string GetAttributeDisplayValue(Entity entity, string attribute)
        {
            var result = string.Empty;
            if (entity.Contains(attribute) && entity[attribute] != null)
            {
                if (entity.FormattedValues.Contains(attribute))
                {
                    result = entity.FormattedValues[attribute];
                }
                else
                {
                    //result = entity[attribute].ToString();
                    var attrType = Config.AttributesMeta[attribute].AttributeType.Value;
                    if (attrType == AttributeTypeCode.Lookup ||
                        attrType == AttributeTypeCode.Customer ||
                        attrType == AttributeTypeCode.Owner)
                    {
                        var val = entity.GetAttributeValue<EntityReference>(attribute);
                        result = val.Name.ToString();
                        if (string.IsNullOrEmpty(result))
                        {
                            result = val.Id.ToString();
                        }
                    }
                    else if (attrType == AttributeTypeCode.Boolean)
                    {
                        var val = entity.GetAttributeValue<bool>(attribute);
                        result = val.ToString().ToLower();
                    }
                    else if (attrType == AttributeTypeCode.Uniqueidentifier)
                    {
                        var val = entity.GetAttributeValue<Guid>(attribute);
                        result = val.ToString().ToLower();
                    }
                    else if (attrType == AttributeTypeCode.Picklist ||
                        attrType == AttributeTypeCode.State ||
                        attrType == AttributeTypeCode.Status)
                    {
                        var val = entity.GetAttributeValue<OptionSetValue>(attribute);
                        result = val.Value.ToString().ToLower();
                    }                    
                    else
                    {
                        // double check value type, just in case
                        var val = entity[attribute];
                        if (val is EntityReference)
                        {
                            result = ((EntityReference)val).Name.ToString();
                            if (string.IsNullOrEmpty(result))
                            {
                                result = ((EntityReference)val).Id.ToString();
                            }
                        }
                        else if (val is OptionSetValue)
                        {
                            result = ((OptionSetValue)val).Value.ToString();
                        }
                        else
                        {
                            result = val.ToString();
                        }
                    }
                }
            }
            return result;
        }

        private ExpandoObject EntityToDynamic(Entity entity, string key)
        {
            var result = new ExpandoObject() as IDictionary<string, object>;
            result.Add("DuplicationKey", key);
            result.Add("Id", entity.Id);
            foreach (var attr in _selectAttributes)
            {
                result.Add(attr, GetAttributeDisplayValue(entity, attr));
            }
            return (dynamic)result;
        }
    }
}
