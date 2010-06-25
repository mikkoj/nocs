using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

using Nocs.Forms;
using Nocs.Helpers;


namespace Nocs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main(string[] args)
        {
            // let's make sure we have Output window added as a listener for debug modes
            #if DEBUG
            var writer = new TextWriterTraceListener(Console.Out);
            Debug.Listeners.Add(writer);
            #endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += UIThreadException;

            // let's set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            Application.Run(new Main(args));
        }


        // Handles unhandled exceptions for non-UI thread
        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    Trace.WriteLine(DateTime.Now + " - Non-UIThreadException: " + exception.Message + "\n\nStack Trace:\n" + exception.StackTrace);

                    if (exception.InnerException != null)
                    {
                        Trace.WriteLine(DateTime.Now + " - Non-UIThreadInnerException: " + exception.InnerException.Message + "\n\nStack Trace:\n" + exception.InnerException.StackTrace);
                    }
                }
                MessageBox.Show(new Form { TopMost = true }, "A fatal error occurred, inspect nocs.log and report found errors to http://nocs.googlecode.com/. Thanks!", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Trace.Flush();
                Application.Exit();
            }
        }


        // Handles the unhandled exceptions for UI thread
        static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                Trace.WriteLine(DateTime.Now + " - UIThreadException: " + t.Exception.Message + "\n\nStack Trace:\n" + t.Exception.StackTrace);
                if (t.Exception != null)
                {
                    Trace.WriteLine(DateTime.Now + " - UIThreadInnerException: " + t.Exception.Message + "\n\nStack Trace:\n" + t.Exception.StackTrace);

                    if (t.Exception.InnerException != null)
                    {
                        Trace.WriteLine(DateTime.Now + " - UIThreadInnerException: " + t.Exception.InnerException.Message + "\n\nStack Trace:\n" + t.Exception.InnerException.StackTrace);
                    }
                }
                MessageBox.Show(new Form { TopMost = true }, "A fatal error occurred, inspect nocs.log and report found errors to http://nocs.googlecode.com/. Thanks!", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Trace.Flush();
                Application.Exit();
            }
        }
    }
}