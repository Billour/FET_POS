using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FetPos
{
    public static class Utility
    {
        public static T[] ToArray<T>(this ICollection collection)
        {           
            T[] t = new T[collection.Count];
            collection.CopyTo(t, 0);            
            return t;            
        }

        public static ListViewItem GetLastItem(this ListView listView)
        {
            if (listView.Items.Count == 0)
                return null;

            return listView.Items[listView.Items.Count - 1];
        }

    }
}
