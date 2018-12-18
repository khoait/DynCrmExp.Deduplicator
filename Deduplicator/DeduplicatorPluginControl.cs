using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using DynCrmExp.Deduplicator.Models;
using System.Diagnostics;
using XrmToolBox.Extensibility.Interfaces;

namespace DynCrmExp.Deduplicator
{
    public partial class DeduplicatorPluginControl : PluginControlBase, IGitHubPlugin
    {
        private Settings _mySettings;
        public Settings MySettings
        {
            get
            {
                if (_mySettings == null)
                {
                    // Loads or creates the settings for the plugin
                    if (!SettingsManager.Instance.TryLoad(GetType(), out _mySettings))
                    {
                        _mySettings = new Settings();

                        LogWarning("Settings not found => a new settings file has been created!");
                    }
                }
                LogInfo("Settings found and loaded");
                return _mySettings;
            }
            set
            {
                _mySettings = value;
            }
        }

        public string RepositoryName => "DynCrmExp.Deduplicator";

        public string UserName => "khoait";

        private Dictionary<string, EntityMetadata> _entities;
        private Dictionary<string, AttributeMetadata> _attributes;
        private ILookup<string, Entity> _duplicated;
        
        public DeduplicatorPluginControl()
        {
            InitializeComponent();

            cbEntities.DisplayMember = "DisplayName";
            cbEntities.ValueMember = "LogicalName";

            dgvFields.Columns["colDisplayName"].DataPropertyName = "DisplayName";
            dgvFields.Columns["colLogicalName"].DataPropertyName = "LogicalName";
            dgvFields.Columns["colMatch"].DataPropertyName = "IsMatch";
            dgvFields.Columns["colView"].DataPropertyName = "IsView";            
        }

        #region Form Event Hanlders
        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Tip: Select only necessary fields for better performance.", null);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), MySettings);
        }


        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            MySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
        }

        private void btnLoadEntities_Click(object sender, EventArgs e)
        {
            ClearForm();

            ExecuteMethod(LoadEntities);
        }

        private void cbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {            
            var entity = (string)cbEntities.SelectedValue;
            if (!string.IsNullOrEmpty(entity))
            {
                ClearResults();
                ExecuteMethod(LoadFields, entity);
            }            
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            var entity = (string)cbEntities.SelectedValue;
            if (!string.IsNullOrEmpty(entity))
            {
                // make sure checkbox is saved
                dgvFields.CurrentCell = null;

                ExecuteMethod(LoadData, entity);
            }
        }

        private void dgvDuplicated_SelectionChanged(object sender, EventArgs e)
        {
            dgvDuplicates.Rows.Clear();
            if (dgvDuplicated.CurrentRow != null)
            {
                var selectedRow = dgvDuplicated.CurrentRow;
                var matchingKey = selectedRow.Cells["key"].Value != null ? selectedRow.Cells["key"].Value.ToString() : string.Empty;
                if (!string.IsNullOrEmpty(matchingKey))
                {
                    var duplicates = _duplicated[matchingKey];
                    foreach (var item in duplicates)
                    {
                        var rowIndex = dgvDuplicates.Rows.Add();
                        var row = dgvDuplicates.Rows[rowIndex];
                        foreach (DataGridViewColumn col in dgvDuplicates.Columns)
                        {
                            var attr = col.Name;
                            row.Cells[attr].Value = GetAttributeDisplayValue(item, attr);
                        }
                        var url = CrmHelper.BuildRecordUrl(this.ConnectionDetail.WebApplicationUrl, _entities[item.LogicalName].ObjectTypeCode.Value, item.Id);
                        row.Cells["hiddenurl"].Value = url;
                    }
                }
            }
        }

        private void dgvDuplicates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDuplicates.Columns[e.ColumnIndex].Name == "url")
            {
                var url = dgvDuplicates.Rows[e.RowIndex].Cells["hiddenurl"].Value;                
                if (url != null)
                {
                    try
                    {
                        Process.Start(url.ToString());
                    }
                    catch (Exception)
                    {
                        //TODO:?
                    }                    
                }
            }
        }
        #endregion

        #region Private Methods
        private void LoadEntities()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (worker, args) =>
                {
                    args.Result = CrmHelper.GetEntitiesMetadata(Service, IsBPFSupported());
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var result = args.Result as RetrieveMetadataChangesResponse;
                    if (result != null)
                    {
                        var entityMetadata = (EntityMetadataCollection) result.EntityMetadata;
                        _entities = entityMetadata.ToDictionary(m => m.LogicalName);

                        var entities = entityMetadata.Select(m => new EntityMetadataModel()
                        {
                            DisplayName = GetEntityDisplayName(m),
                            LogicalName = m.LogicalName
                        })
                        .OrderBy(e => e.DisplayName)
                        .ThenBy(e => e.LogicalName).ToList();

                        entities.Insert(0, new EntityMetadataModel());
                        
                        cbEntities.DataSource = entities;                        
                    }
                }
            });
        }

        private bool IsBPFSupported()
        {
            return ConnectionDetail.OrganizationMajorVersion >= 9 ||
                (ConnectionDetail.OrganizationMajorVersion == 8 && ConnectionDetail.OrganizationMinorVersion == 2);
        }

        private string GetEntityDisplayName(EntityMetadata metadata)
        {
            var result = metadata.LogicalName;
            if(metadata.DisplayName.UserLocalizedLabel != null && !string.IsNullOrEmpty(metadata.DisplayName.UserLocalizedLabel.Label))
            {
                result = string.Concat(metadata.DisplayName.UserLocalizedLabel.Label, " (", metadata.LogicalName, ")");
            }
            else if (result == metadata.LogicalName && metadata.DisplayName.LocalizedLabels.Count > 0)
            {
                result = metadata.DisplayName.LocalizedLabels[0].Label;
            }
            return result;
        }

        private string GetAttributeDisplayName(AttributeMetadata metadata)
        {
            var result = metadata.LogicalName;
            if (metadata.DisplayName.UserLocalizedLabel != null && !string.IsNullOrEmpty(metadata.DisplayName.UserLocalizedLabel.Label))
            {
                result = metadata.DisplayName.UserLocalizedLabel.Label;
            }
            else if (result == metadata.LogicalName && metadata.DisplayName.LocalizedLabels.Count > 0)
            {
                result = metadata.DisplayName.LocalizedLabels[0].Label;
            }
            return result;
        }

        private void LoadFields(string entity)
        {
            EntityMetadata metadata = null;
            if (_entities.TryGetValue(entity, out metadata))
            {
                var displayName = GetEntityDisplayName(metadata);
                WorkAsync(new WorkAsyncInfo
                {
                    Message = string.Format($"Loading {displayName}..."),
                    Work = (worker, args) =>
                    {
                        args.Result = CrmHelper.GetEntityAttributes(Service, entity);
                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var result = args.Result as RetrieveMetadataChangesResponse;
                        if (result != null)
                        {
                            var entityMetadata = (EntityMetadataCollection)result.EntityMetadata;
                            if (entityMetadata.Count == 1)
                            {
                                var entMetadata = entityMetadata[0];

                                _attributes = entMetadata.Attributes.ToDictionary(m => m.LogicalName);

                                var attritbutes = entMetadata.Attributes.Select(a => new AttributeMetadataModel()
                                {
                                    DisplayName = GetAttributeDisplayName(a),
                                    LogicalName = a.LogicalName,
                                    IsView = a.IsPrimaryName ?? false
                                })
                                .OrderBy(a => a.DisplayName)
                                .ThenBy(a => a.LogicalName).ToList();                                

                                // use unbound grid to support column sorting
                                foreach (var item in attritbutes)
                                {
                                    var rowIndex = dgvFields.Rows.Add(item.DisplayName, item.LogicalName, item.IsMatch, item.IsView);
                                    // disable matching on primary id column
                                    if (_attributes[item.LogicalName].IsPrimaryId?? false)
                                    {
                                        DisableCheckboxCell(dgvFields.Rows[rowIndex].Cells["colMatch"] as DataGridViewCheckBoxCell);
                                    }
                                    if (!(_attributes[item.LogicalName].IsValidForRead ?? true))
                                    {
                                        DisableCheckboxCell(dgvFields.Rows[rowIndex].Cells["colMatch"] as DataGridViewCheckBoxCell);

                                        DisableCheckboxCell(dgvFields.Rows[rowIndex].Cells["colView"] as DataGridViewCheckBoxCell);                                       
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Metadata not found for entity " + displayName, "Load attribute metadata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Metadata not found for entity " + displayName, "Load attribute metadata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                });
            }
        }

        private void DisableCheckboxCell(DataGridViewCheckBoxCell cell)
        {
            cell.Value = false;
            cell.FlatStyle = FlatStyle.Flat;
            cell.Style.ForeColor = Color.DarkGray;
            cell.ReadOnly = true;
        }

        private void LoadData(string entity)
        {
            var matchAttributes = dgvFields.Rows.Cast<DataGridViewRow>()
                .Where(r => (bool)(r.Cells["colMatch"] as DataGridViewCheckBoxCell).Value)
                .Select(r => r.Cells["colLogicalName"].Value.ToString());

            var viewAttritues = dgvFields.Rows.Cast<DataGridViewRow>()
                .Where(r => (bool)(r.Cells["colView"] as DataGridViewCheckBoxCell).Value)
                .Select(r => r.Cells["colLogicalName"].Value.ToString());

            //TODO: check at least one column is cheked to match, and view
            if(matchAttributes.Count() == 0 || viewAttritues.Count() ==0)
            {
                MessageBox.Show("Please select at least one field to match and view", "Select Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // add duplicated view columns
            AddViewColumns(dgvDuplicated, matchAttributes);
            dgvDuplicated.Columns.Add("count", "Duplicates Count");
            dgvDuplicated.Columns.Add("key", "Key");
            dgvDuplicated.Columns["key"].Visible = false;

            // add duplicates view columns
            AddViewColumns(dgvDuplicates, viewAttritues);
            dgvDuplicates.Columns.Add(new DataGridViewLinkColumn()
            {
                Name = "url",
                HeaderText = "Record URL",
                Text = "Link",
                UseColumnTextForLinkValue = true
            });
            dgvDuplicates.Columns.Add("hiddenurl", "hiddenurl");
            dgvDuplicates.Columns["hiddenurl"].Visible = false;

            // get selected attributes
            var attributes = new List<string>(matchAttributes.Count() + viewAttritues.Count());
            attributes.AddRange(matchAttributes);
            attributes.AddRange(viewAttritues);
            attributes = attributes.Distinct().ToList();

            WorkAsync(new WorkAsyncInfo
            {
                Message = string.Format($"Loading data..."),
                Work = (worker, args) =>
                {
                    args.Result = CrmHelper.GetAllData(Service, entity, attributes, matchAttributes, chkIgnoreBlank.Checked);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var result = args.Result as IEnumerable<Entity>;
                    if (result != null)
                    {
                        EntityMetadata metadata = null;
                        if (_entities.TryGetValue(entity, out metadata))
                        {
                            FindDuplicated(result, matchAttributes);
                        }
                    }                    
                }
            });
        }

        private void AddViewColumns(DataGridView grid, IEnumerable<string> attributes)
        {
            grid.Columns.Clear();
            foreach (var attr in attributes)
            {
                grid.Columns.Add(attr, GetAttributeDisplayName(_attributes[attr]));
            }
        }

        private void FindDuplicated(IEnumerable<Entity> data, IEnumerable<string> matchAttibutes)
        {
            Func<Entity, IEnumerable<string>, string> groupingFunction = BuildMatchingKey;
            _duplicated = data.ToLookup(e => groupingFunction(e, matchAttibutes));
            
            // build duplicated list
            var duplicated = _duplicated.Where(g => g.Count() > 1)
                .Select(g => new DuplicatedDataModel {
                    MatchingKey = g.Key,
                    DuplicatedData = g.FirstOrDefault(),
                    DuplicatesCount = g.Count()
                });
            foreach (var item in duplicated)
            {
                var rowIndex = dgvDuplicated.Rows.Add();
                var row = dgvDuplicated.Rows[rowIndex];
                foreach (var attr in matchAttibutes)
                {
                    row.Cells[attr].Value = GetAttributeDisplayValue(item.DuplicatedData, attr);
                }
                row.Cells["count"].Value = item.DuplicatesCount.ToString();
                row.Cells["key"].Value = item.MatchingKey;
            }

            dgvDuplicated.ClearSelection();
            dgvDuplicates.Rows.Clear();
            dgvDuplicates.Refresh();

            if (duplicated.Count() == 0)
            {
                gbDuplicated.Text = string.Format($"Duplicated Records");
                MessageBox.Show("No duplicates found.", "Process completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                gbDuplicated.Text = string.Format($"Duplicated Records ({duplicated.Count()})");
            }
        }

        private string BuildMatchingKey(Entity entity, IEnumerable<string> groupAttributes)
        {
            var sb = new StringBuilder();
            var keyString = string.Empty;
            sb.Clear();
            foreach (var attr in groupAttributes)
            {
                var stringvalue = GetAttributeStringValue(entity, attr);
                if (!chkCaseSensitive.Checked)
                {
                    stringvalue = stringvalue.ToLower();
                }

                if (chkIgnoreWhiteSpace.Checked)
                {
                    stringvalue = stringvalue.Trim();
                }

                if (chkIgnoreBlank.Checked && string.IsNullOrEmpty(stringvalue))
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
                var attrType = _attributes[attribute].AttributeType;
                if (attrType == AttributeTypeCode.Lookup ||
                    attrType == AttributeTypeCode.Customer ||
                    attrType == AttributeTypeCode.Owner)
                {
                    var val = (EntityReference)entity[attribute];
                    result = val.Id.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Boolean)
                {
                    var val = (bool)entity[attribute];
                    result = val.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Uniqueidentifier)
                {
                    var val = (Guid)entity[attribute];
                    result = val.ToString().ToLower();
                }
                else if (attrType == AttributeTypeCode.Picklist ||
                    attrType == AttributeTypeCode.State ||
                    attrType == AttributeTypeCode.Status)
                {
                    var val = (OptionSetValue)entity[attribute];
                    result = val.Value.ToString().ToLower();
                }
                else if(entity.FormattedValues.Contains(attribute))
                {
                    result = entity.FormattedValues[attribute];                    
                }
                else
                {
                    result = entity[attribute].ToString();
                }
            }
            return result;
        }

        private string GetAttributeDisplayValue(Entity entity, string attribute)
        {
            var result = string.Empty;
            if (entity.Contains(attribute) && entity[attribute] != null)
            {
                var attrType = _attributes[attribute].AttributeType;
                if (entity.FormattedValues.Contains(attribute))
                {
                    result = entity.FormattedValues[attribute];
                }
                else
                {
                    result = entity[attribute].ToString();
                }
            }
            return result;
        }

        private void ClearForm()
        {
            _entities = null;
            _attributes = null;
            _duplicated = null;

            cbEntities.DataSource = null;
            cbEntities.DisplayMember = "DisplayName";
            cbEntities.ValueMember = "LogicalName";            

            ClearResults();
        }

        private void ClearResults()
        {
            dgvFields.Rows.Clear();
            dgvFields.Refresh();

            dgvDuplicated.Columns.Clear();
            dgvDuplicated.Refresh();

            dgvDuplicates.Columns.Clear();
            dgvDuplicates.Rows.Clear();
            dgvDuplicates.Refresh();

            gbDuplicated.Text = string.Format($"Duplicated Records");
        }

        #endregion

        
    }
}