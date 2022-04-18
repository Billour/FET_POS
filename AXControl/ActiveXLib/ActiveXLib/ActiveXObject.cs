using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
//using System.Net;
using System.Net.NetworkInformation;


namespace Advtek.Web
{
    [ProgId("Advtek.Web.ActiveXLib")]
	[ClassInterface(ClassInterfaceType.AutoDual), ComSourceInterfaces(typeof(ControlEvents))] //Implementing interface that will be visible from JS
    [Guid("A15031ED-115A-4CB7-B2C8-DF655470B4D3")]
	[ComVisible(true)]
	public class ActiveXObject: IObjectSafety 
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

        public ActiveXObject()
        {
            NetworkChange.NetworkAvailabilityChanged += 
                new NetworkAvailabilityChangedEventHandler(NetworkAvailabilityChanged);
        }

        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            //bool available = e.IsAvailable;

            NetworkInterface[] networkConnections = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in networkConnections)
            {
                if (ni.Name == "Local Area Connection")
                {
                    if (ni.OperationalStatus == OperationalStatus.Down)
                    {
                        Console.WriteLine("LAN disconnected: " + ni.Description);
                    }
                }
                else if (ni.Name == "Wireless Network Connection")
                {
                    if (ni.OperationalStatus == OperationalStatus.Down)
                    {
                        Console.WriteLine("Wireless disconnected: " + ni.Description);
                    }
                }
            }
        }

        [ComVisible(true)]
        public void StartProcess(string command, string args)
        {
            Process p = new Process();
            p.StartInfo.FileName = command;
            if (!string.IsNullOrEmpty(args))
                p.StartInfo.Arguments = args;

            p.Start();
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
    }

    /// <summary>
    /// Event handler for events that will be visible from JavaScript
    /// </summary>
    public delegate void ControlEventHandler(string redirectUrl);

    /// <summary>
    /// This interface shows events to javascript
    /// </summary>
    [Guid("68BD4E0D-D7BC-4cf6-BEB7-CAB950161E79")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ControlEvents
    {
        //Add a DispIdAttribute to any members in the source interface to specify the COM DispId.
        [DispId(0x60020001)]
        void OnClose(string redirectUrl); //This method will be visible from JS
    }
    
}
