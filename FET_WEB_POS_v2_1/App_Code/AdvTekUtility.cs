using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// AdvTekUtility 的摘要描述
/// </summary>
public static class AdvTekUtility
{
    /// <summary>     
    /// Similar to Control.FindControl, but recurses through child controls.
    /// Assumes that startingControl is NOT the control you are searching for.
    /// </summary>
    public static T FindChildControl<T>(this Control startingControl, string id) where T : Control
    {
        T found = null;

        foreach (Control activeControl in startingControl.Controls)
        {
            found = activeControl as T;

            if (found == null || (string.Compare(id, found.ID, true) != 0))
            {
                found = FindChildControl<T>(activeControl, id);
            }

            if (found != null)
            {
                break;
            }
        }

        return found;
    }
}
