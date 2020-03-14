using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynCrmExp.Deduplicator
{
    public static class CrmHelper
    {
        //public static EntityMetadata[] GetEntitiesMetadata(IOrganizationService service)
        //{
        //    var req = new RetrieveAllEntitiesRequest()
        //    {
        //        RetrieveAsIfPublished = true,
        //        EntityFilters = EntityFilters.Entity                
        //    };            
        //    var response = (RetrieveAllEntitiesResponse)service.Execute(req);
        //    return response.EntityMetadata;
        //}
        static String[] excludedEntities = {
            "WorkflowLog",
            "Template",
            "CustomerOpportunityRole",
            "Import",
            "UserQueryVisualization",
            "UserEntityInstanceData",
            "ImportLog",
            "RecurrenceRule",
            "QuoteClose",
            "UserForm",
            "SharePointDocumentLocation",
            "Queue",
            "DuplicateRule",
            "OpportunityClose",
            "Workflow",
            "RecurringAppointmentMaster",
            "CustomerRelationship",
            "Annotation",
            "SharePointSite",
            "ImportData",
            "ImportFile",
            "OrderClose",
            "Contract",
            "BulkOperation",
            "CampaignResponse",
            "Connection",
            "Report",
            "CampaignActivity",
            "UserEntityUISettings",
            "IncidentResolution",
            "GoalRollupQuery",
            "MailMergeTemplate",
            "Campaign",
            "PostFollow",
            "ImportMap",
            "Goal",
            "AsyncOperation",
            "ProcessSession",
            "UserQuery",
            "ActivityPointer",
            "List",
            "ServiceAppointment"
        };

        public static RetrieveMetadataChangesResponse GetEntitiesMetadata(IOrganizationService service, bool BPFEntitySupported)
        {
            var entityFilter = new MetadataFilterExpression(Microsoft.Xrm.Sdk.Query.LogicalOperator.And);
            entityFilter.Conditions.Add(new MetadataConditionExpression("IsIntersect", MetadataConditionOperator.Equals, false));
            if (BPFEntitySupported)
            {
                entityFilter.Conditions.Add(new MetadataConditionExpression("IsBPFEntity", MetadataConditionOperator.Equals, false));
            }
            entityFilter.Conditions.Add(new MetadataConditionExpression("SchemaName", MetadataConditionOperator.NotIn, excludedEntities));

            var entityProps = new MetadataPropertiesExpression("DisplayName", "LogicalName", "SchemaName", "ObjectTypeCode");                       

            var query = new EntityQueryExpression()
            {
                Criteria = entityFilter,
                Properties = entityProps
            };

            var request = new RetrieveMetadataChangesRequest() { Query = query };
            var response = (RetrieveMetadataChangesResponse)service.Execute(request);
            return response;
        }

        public static RetrieveMetadataChangesResponse GetEntityAttributes(IOrganizationService service, string entity)
        {
            var entityFilter = new MetadataFilterExpression(Microsoft.Xrm.Sdk.Query.LogicalOperator.And);
            entityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entity));

            //var entityProps = new MetadataPropertiesExpression("DisplayName", "LogicalName", "SchemaName", "ObjectTypeCode");

            var attributeProps = new MetadataPropertiesExpression("DisplayName", "LogicalName", "SchemaName", "AttributeType", "IsPrimaryId", "IsPrimaryName", "IsValidForRead");

            var query = new EntityQueryExpression()
            {
                Criteria = entityFilter,
                //Properties = entityProps,
                AttributeQuery = new AttributeQueryExpression()
                {
                    
                    Properties = attributeProps
                },                
            };

            var request = new RetrieveMetadataChangesRequest() { Query = query };
            var response = (RetrieveMetadataChangesResponse)service.Execute(request);
            return response;
        }

        public static IEnumerable<Entity> GetAllData(IOrganizationService service, string entity, IEnumerable<string> attributes, IEnumerable<string> matchAttributes, bool allowNull)
        {
            var result = new List<Entity>();

            var query = new QueryExpression(entity);
            query.ColumnSet = new ColumnSet(attributes.ToArray());
            query.PageInfo = new PagingInfo();
            query.PageInfo.Count = 5000;
            query.PageInfo.PageNumber = 1;
            query.PageInfo.PagingCookie = null;

            if (!allowNull)
            {
                foreach (var attr in matchAttributes)
                {
                    query.Criteria.AddCondition(attr, ConditionOperator.NotNull);
                }
            }

            var pageResult = new EntityCollection();

            do
            {
                pageResult = service.RetrieveMultiple(query);
                if(pageResult.Entities != null)
                {
                    result.AddRange(pageResult.Entities);
                }

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = pageResult.PagingCookie;
            } while (pageResult.MoreRecords);

            return result;
        }

        public static string BuildRecordUrl(string orgUrl, int entityCode, Guid recordId)
        {
            var uriBuilder = new UriBuilder(orgUrl);
            string recordPath = string.Format($"etc={entityCode}&id=%7b{recordId}%7d&pagetype=entityrecord");
            uriBuilder.Path = "main.aspx";
            uriBuilder.Query = recordPath;
            return uriBuilder.ToString();
        }
    }
}
