using System;
using System.ComponentModel;
using System.Diagnostics;
using Google.Documents;

using Nocs.Helpers;
using Nocs.Models;


namespace Nocs.Forms
{
    public partial class Main
    {

        #region Start Service

        private void BgWorkerStartService_DoWork(object sender, DoWorkEventArgs e)
        {
            // first check that user is connected to Internet
            if (Tools.IsConnected())
            {
                // start the DocumentService with user's credentials
                // (e.Argument is a boolean determining whether to force authToken retrieval)
                NocsService.AuthenticateUser(NocsService.Username, NocsService.Password, (bool)e.Argument);
            }
            else
            {
                Trace.WriteLine(DateTime.Now + " - Main: couldn't connect to internet");
                throw new Exception("Could not connect to internet!");
            }
        }

        private void BgWorkerStartService_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Trace.WriteLine(DateTime.Now + " - Main: error while starting service: " + e.Error.Message);
                
                // there was an error during the operation -> show it to user
                Status(StatusType.Error, e.Error.Message);
                menuGoogleAccount.Enabled = true;
            }
            else
            {
                // next, retrieve items
                Status(StatusType.Retrieve, "Retrieving documentlist..");
                BgWorkerGetAllItems.RunWorkerAsync();
            }
        }

        #endregion


        #region Get All Items

        private void BgWorkerGetAllItems_DoWork(object sender, DoWorkEventArgs e)
        {
            NocsService.UpdateAllEntries();
        }

        private void BgWorkerGetAllItems_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // functionality after GetAllDocuments-worker has finished
            if (e.Error != null)
            {
                Trace.WriteLine(DateTime.Now + " - Main: error while retrieving all documents: " + e.Error.Message);

                // there was an error during the operation
                Status(StatusType.Error, "Error: " + e.Error.Message + ".");
            }
            else
            {
                // the operation succeeded, let's update statusbar and enable menu controls
                Status(StatusType.Reset, null);

                // let's enable browse
                if (!InvokeRequired)
                {
                    menuBrowse.Enabled = true;
                    menuGoogleAccount.Enabled = true;
                    menuSave.Enabled = true;
                }
            }
        }

        #endregion


        #region Load Document Content

        private static void BgWorkerLoadDocumentContent_DoWork(object sender, DoWorkEventArgs e)
        {
            var document = e.Argument as Document;
            // if an error will occur, we'll keep the document as result so we can retry
            e.Result = document;
            e.Result = NocsService.GetDocumentContent(document);
        }

        private void BgWorkerLoadDocumentContent_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var updatedDocument = e.Result as Document;

            // let's first check for an error
            if (e.Error != null)
            {
                if (updatedDocument == null)
                {
                    Trace.WriteLine(DateTime.Now + " - Main: error while loading a new  document");
                }
                else
                {
                    Trace.WriteLine(DateTime.Now + " - Main: error while loading a new document: " + updatedDocument.Title);
                }

                Status(StatusType.ContentUpdateError, e.Error.Message);
                return;
            }
            if (updatedDocument != null)
            {
                // let's activate the document and update the content
                foreach (Noc tab in tabs.TabPages)
                {
                    if (!tab.Document.IsDraft && tab.Document.ResourceId == updatedDocument.ResourceId)
                    {
                        tab.Document = updatedDocument;
                        tab.Activate();
                    }
                }
            }

            // let's handle the loader icon with a thread lock
            lock (_threadLock)
            {
                // let's first decrement the current number of workers
                // (should always be over 0 at this point
                if (_contentUpdaterWorkers > 0)
                {
                    _contentUpdaterWorkers--;
                }

                if (_contentUpdaterWorkers == 0)
                {
                    // there are no more updaters active, we can disable loader/clear status
                    Status(StatusType.Reset, null);
                }
            }
        }

        #endregion

    }
}
