using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;

using Google.Documents;
using Google.GData.Documents;
using Google.GData.Client;

using Nocs.Models;
using Nocs.Properties;


namespace Nocs.Forms
{
    public partial class Browse : Form
    {
        // events for notifying the main form to load a document / handle renames/deletes
        public delegate void BrowseOpenEventHandler(Document document);
        public delegate void BrowseActionEventHandler(string documentId);

        public event BrowseOpenEventHandler AddDocumentToMainForm;
        public event BrowseActionEventHandler DocumentRenamed;
        public event BrowseActionEventHandler DocumentDeleted;
        private readonly Synchronizer _synchronizer;

        // an event for refreshing the document list
        private delegate void BrowseRefreshEntriesEventHandler(bool autoFetchAllCalling);

        // documentId for selecting the currently active tab in Browse-listbox
        private readonly string _activeResourceId;

        // helper TreeNodes for drag & drop purposes
        private TreeNode _lastNode;
        private TreeNode _lastSelectedNode;


        public Browse()
        {
            InitializeComponent();
        }

        public Browse(ref Synchronizer synchronizer)
        {
            InitializeComponent();
            _synchronizer = synchronizer;
            _synchronizer.AutoFetchAllEntriesFinished += SynchronizerAutoFetchAllEntriesFinished;
        }

        public Browse(ref Synchronizer synchronizer, string selectedResourceId)
        {
            InitializeComponent();
            _synchronizer = synchronizer;
            _synchronizer.AutoFetchAllEntriesFinished += SynchronizerAutoFetchAllEntriesFinished;
            _activeResourceId = selectedResourceId;
        }

        private void Browse_FormLoad(object sender, EventArgs e)
        {
            // let's set the window size / location
            if (Settings.Default.BrowseWindowLocation.X != 0 || Settings.Default.BrowseWindowLocation.Y != 0)
            {
                Location = Settings.Default.BrowseWindowLocation;
            }
            Size = Settings.Default.BrowseWindowSize;


            // fill the listbox with current folders and item titles
            if (!string.IsNullOrEmpty(_activeResourceId))
            {
                var foldersTheActiveDocumentIsIn = NocsService.AllDocuments[_activeResourceId].ParentFolders;
                if (foldersTheActiveDocumentIsIn.Count > 0)
                {
                    RefreshItems(foldersTheActiveDocumentIsIn[0], false);
                }
                else
                {
                    RefreshItems(null, false);
                }
            }
            else
            {
                RefreshItems(Settings.Default.DefaultSaveFolder, false);
            }
            lstItems.ClearSelected();


            // select the top (or currently open) item and activate the box (unless no items)
            if (!string.IsNullOrEmpty(_activeResourceId) && lstItems.Items.Count > 0)
            {
                for (var i = 0; i < lstItems.Items.Count; i++)
                {
                    var document = (Document)lstItems.Items[i];
                    if (document.ResourceId == _activeResourceId)
                    {
                        lstItems.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (lstItems.SelectedIndex == -1 && lstItems.Items.Count > 0)
            {
                lstItems.SelectedIndex = 0;
            }

            lstItems.Select();
        }


        private void Browse_FormClosing(object sender, FormClosingEventArgs e)
        {
            // let's remove the event hooks from synchronizer
            _synchronizer.AutoFetchAllEntriesFinished -= SynchronizerAutoFetchAllEntriesFinished;

            Settings.Default.BrowseWindowLocation = Location;
            Settings.Default.BrowseWindowSize = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;
            Settings.Default.Save();
        }


        #region Control Events

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable rename/delete only when a single item is selected
            if (lstItems.SelectedItems.Count == 1)
            {
                btnRename.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnRename.Enabled = false;
                btnDelete.Enabled = false;
            }

            // we'll disable the load button also if there's no selected items
            btnLoad.Enabled = lstItems.SelectedItems.Count != 0;
        }

        private void lstItems_KeyDown(object sender, KeyEventArgs e)
        {
            // shortcuts for deleting / renaming documents
            if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }

            if (e.KeyCode == Keys.F2)
            {
                btnRename_Click(null, null);
            }
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var selectedDocument = (Document)lstItems.SelectedItem;
            RenameEntry(selectedDocument);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedDocument = (Document)lstItems.SelectedItem;

            // display a warning message before deleting
            if (MessageBox.Show("Are you sure you want to delete " + selectedDocument.Title + "?", "Delete a document", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // run the backgroundworker for deleting an item
                lblError.Visible = false;
                lstItems.Enabled = false;
                treeFolders.Enabled = false;

                DisableActions();

                boxWorking.Visible = true;
                var args = new object[] { selectedDocument.ResourceId, Document.DocumentType.Document, false };
                BgWorkerDeleteEntry.RunWorkerAsync(args);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // close browser
            Close();
        }

        #endregion


        #region Background Workers

        private void BgWorkerRenameEntry_DoWork(object sender, DoWorkEventArgs e)
        {
            // rename an item with the title from the event argument
            var entryId = ((object[])e.Argument)[0].ToString();
            var newTitle = ((object[])e.Argument)[1].ToString();
            var entryType = (Document.DocumentType)((object[])e.Argument)[2];
            NocsService.RenameEntry(entryId, newTitle, entryType);
            e.Result = entryId;
        }

        private void BgWorkerRenameEntry_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation, inform user
                MessageBox.Show(new Form { TopMost = true }, e.Error.Message, "Error while renaming an entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // operation successful, clear and refill listbox items and disable buttons
                btnLoad.Enabled = false;
                btnRename.Enabled = false;
                btnDelete.Enabled = false;
                lstItems.Enabled = true;
                treeFolders.Enabled = true;
                boxWorking.Visible = false;

                // refresh the listbox with current item titles
                RefreshListBoxItemsFromSelectedFolder(false);

                // let's notify the main form (incase the file that was renamed was open in a tab
                // e.Result is the documentId of the renamed document
                DocumentRenamed(e.Result.ToString());
                lstItems.Focus();
            }
        }

        // ----------------------------------------------------------------------

        private void BgWorkerDeleteEntry_DoWork(object sender, DoWorkEventArgs e)
        {
            // delete an item with the given documentId
            var entryId = ((object[])e.Argument)[0].ToString();
            var entryType = (Document.DocumentType)((object[])e.Argument)[1];
            var entryIsFolderWithEntries = (bool)((object[])e.Argument)[2];

            NocsService.DeleteEntry(entryId, entryType);

            // if we removed a folder, let's update all entries also
            if (entryIsFolderWithEntries)
            {
                NocsService.UpdateAllEntries();
            }
            e.Result = entryId;
        }

        private void BgWorkerDeleteEntry_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation
                MessageBox.Show(new Form { TopMost = true }, e.Error.Message, "Error while deleting an entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // operation successful, delete the item from the listbox
                lstItems.Enabled = true;
                treeFolders.Enabled = true;
                boxWorking.Visible = false;

                // refresh the listbox with current item titles
                RefreshListBoxItemsFromSelectedFolder(false);

                // let's notify the main form (incase the file that was deleted was open in a tab)
                // e.Result is the documentId of the deleted document
                DocumentDeleted(e.Result.ToString());
                lstItems.Focus();
            }
        }

        // ----------------------------------------------------------------------

        private void BgWorkerMoveEntry_DoWork(object sender, DoWorkEventArgs e)
        {
            // rename an item with the title from the event argument
            var entryToMove = ((object[])e.Argument)[0] as Document;
            var folderToMoveIn = ((object[])e.Argument)[1] as Document;
            NocsService.MoveEntry(folderToMoveIn, entryToMove);
        }

        private void BgWorkerMoveEntry_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation
                MessageBox.Show(new Form { TopMost = true }, e.Error.Message, "Error while moving an entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // operation successful, let's refresh all content
                EnableAllAfterProcessing();
                boxWorking.Visible = false;

                // refresh the listbox with current item titles
                RefreshListBoxItemsFromSelectedFolder(false);

                // let's re-enable the event for refreshing all lists once the selected node changes
                treeFolders.AfterSelect += treeFolders_AfterSelect;
            }
        }

        // ----------------------------------------------------------------------

        private void BgWorkerRemoveEntryFromAllFolders_DoWork(object sender, DoWorkEventArgs e)
        {
            // rename an item with the title from the event argument
            var entryToRemove = e.Argument as Document;
            if (entryToRemove != null && entryToRemove.ParentFolders.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                NocsService.RemoveEntryFromAllFolders(entryToRemove);
            }
        }

        private void BgWorkerRemoveEntryFromAllFolders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation
                MessageBox.Show(new Form { TopMost = true }, e.Error.Message, "Error while moving an entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // operation successful, let's refresh all content
                EnableAllAfterProcessing();
                boxWorking.Visible = false;

                // refresh the listbox with current item titles
                RefreshListBoxItemsFromSelectedFolder(false);

                // let's re-enable the event for refreshing all lists once the selected node changes
                treeFolders.AfterSelect += treeFolders_AfterSelect;
            }
        }

        // ----------------------------------------------------------------------

        private void BgWorkerCreateFolder_DoWork(object sender, DoWorkEventArgs e)
        {
            // rename an item with the title from the event argument
            var folderNameToCreate = ((object[])e.Argument)[0].ToString();
            NocsService.CreateNewFolder(folderNameToCreate);
        }

        private void BgWorkerCreateFolder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation
                MessageBox.Show(new Form { TopMost = true }, e.Error.Message, "Error while creating a folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // operation successful, let's refresh all content
                EnableAllAfterProcessing();
                boxWorking.Visible = false;

                // refresh the listbox with current item titles
                RefreshListBoxItemsFromSelectedFolder(false);
            }
        }

        #endregion

        private void lstItems_Format(object sender, ListControlConvertEventArgs e)
        {
            var document = (Document)e.ListItem;
            e.Value = document.Title;
        }

        private void LoadDocuments()
        {
            if (lstItems.SelectedItems.Count > 0)
            {
                foreach (Document selectedDocument in lstItems.SelectedItems)
                {
                    AddDocumentToMainForm(selectedDocument);
                }
                Close();
            }
        }

        /// <summary>
        /// Will fire if when the Synchronizer finishes executing a autoFetchAll-job.
        /// When event fires, we'll have to refresh the listbox of documents incase some document was deleted/renamed.
        /// </summary>
        /// <param name="result"></param>
        private void SynchronizerAutoFetchAllEntriesFinished(SyncResult result)
        {
            // refresh the listbox with current item titles
            BrowseRefreshEntriesEventHandler refreshListBoxDelegate = RefreshListBoxItemsFromSelectedFolder;
            Invoke(refreshListBoxDelegate, true);
        }


        private void RefreshListBoxItemsFromSelectedFolder(bool autoFetchAllCalling)
        {
            if (treeFolders.SelectedNode == null || treeFolders.SelectedNode.Tag.GetType() != typeof(Document))
            {
                RefreshItems(null, autoFetchAllCalling);
            }
            else
            {
                var selectedFolder = (Document)treeFolders.SelectedNode.Tag;
                if (!selectedFolder.IsDraft)
                {

                    RefreshItems(selectedFolder.ResourceId, autoFetchAllCalling);
                }
                else
                {
                    RefreshItems(null, autoFetchAllCalling);
                }
            }
        }

        /// <summary>
        /// Will refresh the Google Docs items based on the dictionaries in NocsService.
        /// </summary>
        private void RefreshItems(string folderId, bool autoFetchAllCalling)
        {
            if (treeFolders.Nodes.Count > 0)
            {
                treeFolders.Nodes.Clear();
            }

            // let's first fill the TreeView with user's folders, starting by adding a root element
            var noFolderAtomEntry = new AtomEntry { IsDraft = true };
            var noFolder = new TreeNode("Items with no folder")
            {
                SelectedImageIndex = 3,
                ImageIndex = 3,
                Tag = new Document { AtomEntry = noFolderAtomEntry }
            };
            treeFolders.Nodes.Add(noFolder);

            foreach (var folder in NocsService.AllFolders.Values.OrderBy(d => d.Title))
            {
                // let's add those with no parents to the top level
                if (folder.ParentFolders.Count == 0)
                {
                    if (folder.Type == Document.DocumentType.Folder)
                    {
                        var n = AddFolderToTreeView(treeFolders.Nodes, folder);
                        AddAllChildFolders(n.Nodes, folder);
                    }
                }
            }

            // let's make sure there is a folder with the given folderId
            var foundFoldersForFolderId = NocsService.AllFolders.Values.Any(d => d.Self == folderId) ||
                                          NocsService.AllFolders.Values.Any(d => d.ResourceId == folderId);
            if (!foundFoldersForFolderId)
            {
                // no folders for this user with given folderId found, let's reset the folderId to avoid problems
                folderId = null;
            }
            
            // let's select the TreeNode with the given folderId
            treeFolders.AfterSelect -= treeFolders_AfterSelect;
            if (!string.IsNullOrEmpty(folderId))
            {
                treeFolders.SelectedNode = FindFolderNodeByFolderEntryId(treeFolders.Nodes, folderId);
            }
            else
            {
                // we'll select 'Items with no folder'
                treeFolders.SelectedNode = treeFolders.Nodes[0];
            }
            treeFolders.AfterSelect += treeFolders_AfterSelect;

            // --------------------------------------------------------------------------------------------------

            // we will filter the list of documents based on user settings and the folderId
            IEnumerable listOfDocuments;

            if (Settings.Default.DocumentSortOrder == "ByDate")
            {
                if (!string.IsNullOrEmpty(folderId))
                {
                    var folderIdUrl = folderId.Contains("http:") ? folderId : DocumentsListQuery.documentsBaseUri + "/" + folderId.Replace(":", "%3A");
                    listOfDocuments = NocsService.AllDocuments.Values.Where(d => d.ParentFolders.Contains(folderIdUrl)).OrderByDescending(d => d.Updated).ToList();
                }
                else
                {
                    listOfDocuments = NocsService.AllDocuments.Values.Where(d => d.ParentFolders.Count == 0).OrderByDescending(d => d.Updated).ToList();
                }
                lstItems.Sorted = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(folderId))
                {
                    var folderIdUrl = folderId.Contains("http:") ? folderId : DocumentsListQuery.documentsBaseUri + "/" + folderId.Replace(":", "%3A");
                    listOfDocuments = NocsService.AllDocuments.Values.Where(d => d.ParentFolders.Contains(folderIdUrl)).ToList();
                }
                else
                {
                    listOfDocuments = NocsService.AllDocuments.Values.Where(d => d.ParentFolders.Count == 0).ToList();
                }
                lstItems.Sorted = true;
            }

            IBindingList bindToAllDocuments = new BindingSource(listOfDocuments, null);
            lstItems.DataSource = null;
            lstItems.DataSource = bindToAllDocuments;

            // if AutoFetchAll calls this, we have to use CurrencyManager through a delegate to refresh the listbox
            if (autoFetchAllCalling)
            {
                ((CurrencyManager)lstItems.BindingContext[lstItems.DataSource]).Refresh();
            }
        }

        private void DisableActions()
        {
            btnDelete.Enabled = false;
            btnLoad.Enabled = false;
            btnRename.Enabled = false;
        }

        private void EnableAllAfterProcessing()
        {
            treeFolders.Enabled = true;
            lstItems.Enabled = true;
            btnDelete.Enabled = true;
            btnLoad.Enabled = true;
            btnRename.Enabled = true;
        }

        private void DisableAllForProcessing()
        {
            treeFolders.Enabled = false;
            lstItems.Enabled = false;
            btnDelete.Enabled = false;
            btnLoad.Enabled = false;
            btnRename.Enabled = false;
        }


        #region TreeView

        private static void AddAllChildFolders(TreeNodeCollection col, Entry entry)
        {
            foreach (var d in NocsService.AllFolders.Values.OrderBy(d => d.Title))
            {
                if (d.Type == Document.DocumentType.Folder && d.ParentFolders.Contains(entry.Self))
                {
                    var n = AddFolderToTreeView(col, d);
                    AddAllChildFolders(n.Nodes, d);
                }
            }
        }

        private static TreeNode FindFolderNodeByFolderEntryId(TreeNodeCollection coll, string folderId)
        {
            foreach (TreeNode n in coll)
            {
                var d = n.Tag as Document;
                if (d != null && !d.IsDraft && (d.ResourceId == folderId || d.Self == folderId))
                {
                    return n;
                }

                var x = FindFolderNodeByFolderEntryId(n.Nodes, folderId);
                if (x != null)
                {
                    return x;
                }
            }
            return null;
        }

        private static TreeNode AddFolderToTreeView(TreeNodeCollection parent, Entry doc)
        {
            var node = new TreeNode(doc.Title)
            {
                Tag = doc,
                ImageIndex = 0,
                SelectedImageIndex = 0
            };
            parent.Add(node);

            return node;
        }

        private void treeFolders_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            if (node.Nodes.Count > 0)
            {
                node.SelectedImageIndex = 1;
                node.ImageIndex = 1;
            }
        }

        private void treeFolders_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            if (node.Nodes.Count > 0)
            {
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;
            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshListBoxItemsFromSelectedFolder(false);
        }

        private void treeFolders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var cursorUnderSelectedNode = false;
            var currentFolderNode = treeFolders.GetNodeAt(new Point(e.X, e.Y));
            if (currentFolderNode != null)
            {
                var currentFolder = currentFolderNode.Tag as Document;
                if (currentFolder != null && !currentFolder.IsDraft && currentFolderNode.IsSelected)
                {
                    if (treeFolders.GetNodeAt(new Point(e.X, e.Y)).IsSelected)
                    {
                        cursorUnderSelectedNode = true;
                    }
                }
            }
            if (cursorUnderSelectedNode)
            {
                contextMenuFolders.Show(treeFolders, e.Location);
            }
            else
            {
                contextMenuCreateFolder.Show(treeFolders, e.Location);
            }
        }

        #endregion


        #region Drag & Drop

        private void lstItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstItems.SelectedItems.Count > 1 ||
                lstItems.SelectedItems.Count == 0 ||
                e.Button != MouseButtons.Right ||
                e.Clicks > 1 ||
                lstItems.SelectedIndex != lstItems.IndexFromPoint(e.Location))
            {
                return;
            }
            lstItems.DoDragDrop(lstItems.SelectedItem, DragDropEffects.Move);
        }

        private void lstItems_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeFolders_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            // let's also disable the event for refreshing all lists once the selected node changes
            treeFolders.AfterSelect -= treeFolders_AfterSelect;
        }

        private void treeFolders_DragLeave(object sender, EventArgs e)
        {
            // let's re-enable the event for refreshing all lists once the selected node changes
            treeFolders.AfterSelect += treeFolders_AfterSelect;
        }

        private void treeFolders_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            // let's determine the node we are currently dragging over
            var pt = new Point(e.X, e.Y);
            pt = treeFolders.PointToClient(pt);
            var currentNodeTarget = treeFolders.GetNodeAt(pt);
            if (currentNodeTarget != null && currentNodeTarget.Tag.GetType() == typeof(Document))
            {
                // we'll have to remember the node that was previously selected
                // so we can select it after drag & drop action is finished
                var currentTargetIsSelectedOne = currentNodeTarget.IsSelected;
                if (currentNodeTarget.IsSelected)
                {
                    _lastSelectedNode = currentNodeTarget;
                    
                    // now that we remember the selected node, let's unset the SelectedNode
                    // thus we can change it's foreground/background color
                    treeFolders.SelectedNode = null;
                }

                // we'll set the colors for this node to indicate that we're dragging over it
                currentNodeTarget.BackColor = Color.DarkBlue;
                currentNodeTarget.ForeColor = Color.White;

                // we'll also reset the previously "dragged-over"-node to normal colors
                if ((_lastNode != null) && (_lastNode != currentNodeTarget))
                {
                    _lastNode.BackColor = SystemColors.Window;
                    _lastNode.ForeColor = SystemColors.WindowText;

                    // let's determine if the previously "dragged-over"-node was a selected one
                    if (_lastSelectedNode != null && treeFolders.SelectedNode == null && !currentTargetIsSelectedOne)
                    {
                        // we'll reset the previously selected node back as it were
                        // now that we're no longer dragging over it
                        treeFolders.SelectedNode = _lastSelectedNode;
                        _lastSelectedNode = null;
                    }
                }

                // lastNode is used to remember this currentNode so we can
                // reset its colors once we're no longer dragging over it
                _lastNode = currentNodeTarget;
            }
        }

        private void treeFolders_DragDrop(object sender, DragEventArgs e)
        {
            // 1. let's get the folder to move the document in
            var folderNodeToDropIn = treeFolders.GetNodeAt(treeFolders.PointToClient(new Point(e.X, e.Y)));
            if (folderNodeToDropIn == null)
            {
                treeFolders.AfterSelect += treeFolders_AfterSelect;
                return;
            }
            if (folderNodeToDropIn.Level > 0)
            {
                folderNodeToDropIn = folderNodeToDropIn.Parent;
            }

            var folderToDropIn = folderNodeToDropIn.Tag as Document;
            if (folderToDropIn != null && folderToDropIn.IsDraft)
            {
                // ROOT: folderNode is a root level entry, we need to remove dragged item from all folders
                var entryToBeRemovedFromAllFolders = e.Data.GetData(typeof(Document)) as Document;
                if (entryToBeRemovedFromAllFolders == null ||
                    entryToBeRemovedFromAllFolders.ParentFolders.Count == 0)
                {
                    treeFolders.AfterSelect += treeFolders_AfterSelect;
                    return;
                }
                DisableAllForProcessing();
                boxWorking.Visible = true;
                BgWorkerRemoveEntryFromAllFolders.RunWorkerAsync(entryToBeRemovedFromAllFolders);
                return;
            }

            // -------------------------------------------------------------------------------

            // no need to continue if entry isn't a folder
            if (folderToDropIn == null || folderToDropIn.Type != Document.DocumentType.Folder)
            {
                treeFolders.AfterSelect += treeFolders_AfterSelect;
                return;
            }

            // 2. let's get the entry to be moved
            var entryToBeMoved = e.Data.GetData(typeof(Document)) as Document;
            if (entryToBeMoved == null)
            {
                treeFolders.AfterSelect += treeFolders_AfterSelect;
                return;
            }

            // 3. let's disable all controls and start moving the entry
            DisableAllForProcessing();
            boxWorking.Visible = true;
            var arguments = new object[] { entryToBeMoved, folderToDropIn };
            BgWorkerMoveEntry.RunWorkerAsync(arguments);
        }

        #endregion


        private void menuDeleteFolder_Click(object sender, EventArgs e)
        {
            var selectedFolder = (Document)treeFolders.SelectedNode.Tag;
            if (selectedFolder.IsDraft)
            {
                return;
            }

            // display a warning message before deleting
            if (MessageBox.Show("Are you sure you want to delete " + selectedFolder.Title + "?", "Delete a folder", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // run the backgroundworker for deleting an item
                lblError.Visible = false;
                lstItems.Enabled = false;
                treeFolders.Enabled = false;
                boxWorking.Visible = true;

                DisableActions();

                var parentFolderResourceId = DocumentsListQuery.documentsBaseUri + "/" + selectedFolder.ResourceId.Replace(":", "%3A");
                var entryIsFolderWithEntries = NocsService.AllDocuments.Values.Any(d => d.ParentFolders.Contains(parentFolderResourceId));

                var args = new object[] { selectedFolder.ResourceId, Document.DocumentType.Folder, entryIsFolderWithEntries };
                BgWorkerDeleteEntry.RunWorkerAsync(args);
            }
        }

        private void menuRenameFolder_Click(object sender, EventArgs e)
        {
            var selectedFolder = (Document)treeFolders.SelectedNode.Tag;
            if (selectedFolder.IsDraft)
            {
                return;
            }

            RenameEntry(selectedFolder);
        }

        private void RenameEntry(Document docEntry)
        {
            // let's get a new name
            var inputDialog = new InputDialog();
            var newTitle = inputDialog.InputBox("Enter a new name for '" + docEntry.Title + "':", "Rename", docEntry.Title);

            if (!string.IsNullOrEmpty(newTitle))
            {
                // let's run the backgroundworker for renaming an item
                boxWorking.Visible = true;
                lblError.Visible = false;
                lstItems.Enabled = false;
                treeFolders.Enabled = false;

                DisableActions();

                // let's get the documentId and rename the document
                var arguments = new object[] { docEntry.ResourceId, newTitle, docEntry.Type };
                BgWorkerRenameEntry.RunWorkerAsync(arguments);
            }
        }

        private void menuCreateFolder_Click(object sender, EventArgs e)
        {
            // let's get a name for the new folder
            var inputDialog = new InputDialog();
            var folderName = inputDialog.InputBox("Enter a name for a new folder:", "Create folder", string.Empty);

            if (!string.IsNullOrEmpty(folderName))
            {
                // let's run the backgroundworker for renaming an item
                boxWorking.Visible = true;
                lblError.Visible = false;
                lstItems.Enabled = false;
                treeFolders.Enabled = false;

                DisableActions();

                // let's create a new folder with the given name
                var arguments = new object[] { folderName };
                BgWorkerCreateFolder.RunWorkerAsync(arguments);
            }
        }

        private void splitContainer_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //contextMenuCreateFolder.Show(treeFolders, e.Location);
            }
        }
    }
}