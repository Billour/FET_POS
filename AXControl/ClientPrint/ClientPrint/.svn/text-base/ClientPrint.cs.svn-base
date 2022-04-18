using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Management;

namespace Advtek.Web
{
    [ProgId("Advtek.Web.FilePrint")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComSourceInterfaces(typeof(IExposedComEvents))] //Implementing interface that will be visible from JS
    [Guid("71C71AB8-30AD-477A-98B6-8E99A4953D22")]
    [ComVisible(true)]
    public class ClientPrint : IObjectSafety
    {
        private const string IID_IDispatch = "{00020400-0000-0000-C000-000000000046}";
        private const string IID_IDispatchEx = "{A6EF9860-C720-11D0-9337-00A0C90DCAA9}";
        private const string IID_IPersistStorage = "{0000010A-0000-0000-C000-000000000046}";
        private const string IID_IPersistStream = "{00000109-0000-0000-C000-000000000046}";
        private const string IID_IPersistPropertyBag = "{37D84F60-42CB-11CE-8135-00AA004BB851}";

        private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
        private const int S_OK = 0;
        private const int E_FAIL = unchecked((int)0x80004005);
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        private bool _safeForScripting = true;
        private bool _safeForInitializing = true;

        public delegate void CompletedEventHandler(CompletedEventArgs e);
        public event CompletedEventHandler Completed;

        public ClientPrint()
        {
            PrinterSettings settings = new PrinterSettings();
            PrinterName = settings.PrinterName;
            MilliSecondsToWait = 3000;
        }

        public string PrinterName
        {
            set;
            get;
        }

        public int MilliSecondsToWait
        {
            set;
            get;
        }

        public int GetInterfaceSafetyOptions(ref Guid riid, ref int pdwSupportedOptions, ref int pdwEnabledOptions)
        {
            int result = E_FAIL;

            pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;

            switch (riid.ToString("B"))
            {
                case IID_IDispatch:
                case IID_IDispatchEx:
                    result = S_OK;
                    pdwEnabledOptions = 0;
                    if (_safeForScripting)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER;
                    break;
                case IID_IPersistStorage:
                case IID_IPersistStream:
                case IID_IPersistPropertyBag:
                    result = S_OK;
                    pdwEnabledOptions = 0;
                    if (_safeForInitializing)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_DATA;
                    break;
                default:
                    result = E_NOINTERFACE;
                    break;
            }

            return result;
        }

        public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
        {
            int result = E_FAIL;
            switch (riid.ToString("B"))
            {
                case IID_IDispatch:
                case IID_IDispatchEx:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_CALLER) &&
                         _safeForScripting)
                        result = S_OK;
                    break;
                case IID_IPersistStorage:
                case IID_IPersistStream:
                case IID_IPersistPropertyBag:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_DATA) &&
                         _safeForInitializing)
                        result = S_OK;
                    break;
                default:
                    result = E_NOINTERFACE;
                    break;
            }

            return result;
        }

        public string Uri
        {
            set;
            get;
        }

        public void Print()
        {
            DownloadFile(Uri);
        }

        private void PrintFile(string fileName)
        {            
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = GetAdobeReaderPath();
                startInfo.Arguments = string.Format("/t \"{0}\" \"{1}\"", fileName, PrinterName);//"/C start acrord32 /P /h " + @"C:\Users\Ricky\Downloads\SomeLayout.pdf";//str2;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                startInfo.ErrorDialog = false;
                startInfo.UseShellExecute = false;
                Process process = Process.Start(startInfo);
                if (!process.WaitForExit(MilliSecondsToWait))
                {
                    process.Kill();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void RunExecutable(string executable, string arguments) 
        {
            ProcessStartInfo starter = new ProcessStartInfo(executable, arguments);
            starter.CreateNoWindow = true;
            starter.RedirectStandardOutput = true;
            starter.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = starter;
            process.Start();
            StringBuilder buffer = new StringBuilder();
            using (StreamReader reader = process.StandardOutput) 
            {
                string line = reader.ReadLine();
                while (line != null) 
                {
                    buffer.Append(line);
                    buffer.Append(Environment.NewLine);
                    line = reader.ReadLine();
                    Thread.Sleep(100);
                }
            }
            if (process.ExitCode != 0) 
            {
                throw new Exception(string.Format(@"""{0}"" exited with ExitCode {1}. Output: {2}", 
                executable, process.ExitCode, buffer.ToString()));  
            }
      }

        private string GetAdobeReaderPath()
        {
            RegistryKey adobe = Registry.LocalMachine.OpenSubKey(Wow.Is64BitOperatingSystem ? "Software\\Wow6432Node" : "Software").OpenSubKey("Adobe");
            RegistryKey acroReader;
            if (adobe != null
                && (acroReader = adobe.OpenSubKey("Acrobat Reader")) != null)
            {
                string versionNumber = acroReader.GetSubKeyNames()[0];
                return Path.Combine(ProgramFilesx86(), string.Format(@"Adobe\Reader {0}\Reader\AcroRd32.exe", versionNumber));
            }

            throw new Exception("您必須安裝 Adobe Reader 後，才可以使用本功能。");
        }

        private void DownloadFile(string uriString)
        {
            using (WebClient client = new WebClient())
            {
                client.Proxy = null;

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                StateObject state = new StateObject(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(uriString)));
                client.DownloadFileAsync(new Uri(uriString), state.FileName, state);
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        protected void OnCompleted(Exception error)
        {
            if (Completed != null)
                Completed(new CompletedEventArgs(error));
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                string fileName = ((StateObject)e.UserState).FileName;
                try
                {
                    PrintFile(fileName);
                    OnCompleted(null);
                }
                catch (Exception ex)
                {
                    OnCompleted(ex);
                }
            }
            else
            {
                OnCompleted(new Exception("這個作業已經被取消"));
            }
        }

        private string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }

    internal class StateObject
    {
        public readonly string FileName;
        public StateObject(string fileName)
        {
            this.FileName = fileName;
        }
    }

    //this class is used to make error messages available on the client
    public class CompletedEventArgs
    {
        public CompletedEventArgs(Exception error)
        {
            Error = error;
        }

        //public properties enable javascript to access the internal class members on the client
        public Exception Error
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// This interface shows events to javascript
    /// </summary>
    [Guid("B12C8265-F751-45BC-BA86-D841BBF8A4AD")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IExposedComEvents
    {
        //Add a DispIdAttribute to any members in the source interface to specify the COM DispId.
        [DispIdAttribute(0x60020001)]
        void OnCompleted(); //This method will be visible from JS
    }
}
