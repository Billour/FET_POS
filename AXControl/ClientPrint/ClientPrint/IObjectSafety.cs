using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Advtek.Web
{
    [ComImport, GuidAttribute("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid,
        [MarshalAs(UnmanagedType.U4)] ref int pdwSupportedOptions,
        [MarshalAs(UnmanagedType.U4)] ref int pdwEnabledOptions);

        [PreserveSig()]
        int SetInterfaceSafetyOptions(ref Guid riid,
        [MarshalAs(UnmanagedType.U4)] int dwOptionSetMask,
        [MarshalAs(UnmanagedType.U4)] int dwEnabledOptions);
    }

    [Flags]
    public enum IObjectSafetyOpts : int //DWORD
    {
        // Object is safe for untrusted callers.
        INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001,
        // Object is safe for untrusted data.
        INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002,
        // Object uses IDispatchEx.
        INTERFACE_USES_DISPEX = 0x00000004,
        // Object uses IInternetHostSecurityManager.
        INTERFACE_USES_SECURITY_MANAGER = 0x00000008
    }

    public enum IObjectSafetyRetVals : uint //HRESULT
    {
        //The object is safe for loading.
        S_OK = 0x0,
        //The riid parameter specifies an interface that is unknown to the object.
        E_NOINTERFACE = 0x80000004
    }
}
