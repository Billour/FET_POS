using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FetPos
{
	public class ListViewEx : ListView
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private bool _b = true;
        private string _msg = "There are no items to show in this view";

		#region Interop-Defines
		[DllImport("user32.dll")]
		private	static extern IntPtr SendMessage(IntPtr hWnd, int msg,	IntPtr wPar, IntPtr	lPar);

		// ListView messages
		private const int LVM_FIRST					= 0x1000;
		private const int LVM_GETCOLUMNORDERARRAY	= (LVM_FIRST + 59);
		
		// Windows Messages
		private const int WM_PAINT = 0x000F;
		#endregion

		/// <summary>
		/// Structure to hold an embedded control's info
		/// </summary>
		private struct EmbeddedControl
		{
			public Control Control;
			public int Column;
			public int Row;
			public DockStyle Dock;
			public ListViewItem Item;
		}

		private ArrayList _embeddedControls = new ArrayList();
		
		public ListViewEx()
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            InitializeComponent();
            this.Resize += new EventHandler(ListViewEx_Resize);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //                
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

		/// <summary>
		/// Retrieve the order in which columns appear
		/// </summary>
		/// <returns>Current display order of column indices</returns>
		protected int[] GetColumnOrder()
		{
			IntPtr lPar	= Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * Columns.Count);

			IntPtr res = SendMessage(Handle, LVM_GETCOLUMNORDERARRAY, new IntPtr(Columns.Count), lPar);
			if (res.ToInt32() == 0)	// Something went wrong
			{
				Marshal.FreeHGlobal(lPar);
				return null;
			}

			int	[] order = new int[Columns.Count];
			Marshal.Copy(lPar, order, 0, Columns.Count);

			Marshal.FreeHGlobal(lPar);

			return order;
		}

		/// <summary>
		/// Retrieve the bounds of a ListViewSubItem
		/// </summary>
		/// <param name="Item">The Item containing the SubItem</param>
		/// <param name="SubItem">Index of the SubItem</param>
		/// <returns>Subitem's bounds</returns>
		protected Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
		{
			Rectangle subItemRect = Rectangle.Empty;

			if (Item == null)
				throw new ArgumentNullException("Item");

			int[] order = GetColumnOrder();
			if (order == null) // No Columns
				return subItemRect;

			if (SubItem >= order.Length)
				throw new IndexOutOfRangeException("SubItem "+SubItem+" out of range");

			// Retrieve the bounds of the entire ListViewItem (all subitems)
			Rectangle lviBounds = Item.GetBounds(ItemBoundsPortion.Entire);
			int	subItemX = lviBounds.Left;

			// Calculate the X position of the SubItem.
			// Because the columns can be reordered we have to use Columns[order[i]] instead of Columns[i] !
			ColumnHeader col;
			int i;
			for (i=0; i<order.Length; i++)
			{
				col = this.Columns[order[i]];
				if (col.Index == SubItem)
					break;
				subItemX += col.Width;
			}
 
			subItemRect	= new Rectangle(subItemX, lviBounds.Top, this.Columns[order[i]].Width, lviBounds.Height);

			return subItemRect;
		}

		/// <summary>
		/// Add a control to the ListView
		/// </summary>
		/// <param name="c">Control to be added</param>
		/// <param name="col">Index of column</param>
		/// <param name="row">Index of row</param>
		public void AddEmbeddedControl(Control c, int col, int row)
		{
			AddEmbeddedControl(c,col,row,DockStyle.Fill);
		}
		/// <summary>
		/// Add a control to the ListView
		/// </summary>
		/// <param name="c">Control to be added</param>
		/// <param name="col">Index of column</param>
		/// <param name="row">Index of row</param>
		/// <param name="dock">Location and resize behavior of embedded control</param>
		public void AddEmbeddedControl(Control c, int col, int row, DockStyle dock)
		{
			if (c==null)
				throw new ArgumentNullException();
			if (col>=Columns.Count || row>=Items.Count)
				throw new ArgumentOutOfRangeException();

			EmbeddedControl ec;
			ec.Control = c;
			ec.Column = col;
			ec.Row = row;
			ec.Dock = dock;
			ec.Item = Items[row];

			_embeddedControls.Add(ec);

			// Add a Click event handler to select the ListView row when an embedded control is clicked
			c.Click += new EventHandler(_embeddedControl_Click);
			
			this.Controls.Add(c);
		}
		
		/// <summary>
		/// Remove a control from the ListView
		/// </summary>
		/// <param name="c">Control to be removed</param>
		public void RemoveEmbeddedControl(Control c)
		{
			if (c == null)
				throw new ArgumentNullException();

			for (int i=0; i<_embeddedControls.Count; i++)
			{
				EmbeddedControl ec = (EmbeddedControl)_embeddedControls[i];
				if (ec.Control == c)
				{
					c.Click -= new EventHandler(_embeddedControl_Click);
					this.Controls.Remove(c);
					_embeddedControls.RemoveAt(i);
					return;
				}
			}
			throw new Exception("Control not found!");
		}
		
		/// <summary>
		/// Retrieve the control embedded at a given location
		/// </summary>
		/// <param name="col">Index of Column</param>
		/// <param name="row">Index of Row</param>
		/// <returns>Control found at given location or null if none assigned.</returns>
		public Control GetEmbeddedControl(int col, int row)
		{
			foreach (EmbeddedControl ec in _embeddedControls)
				if (ec.Row == row && ec.Column == col)
					return ec.Control;

			return null;
		}

		[DefaultValue(View.LargeIcon)]
		public new View View
		{
			get 
			{
				return base.View;
			}
			set
			{
				// Embedded controls are rendered only when we're in Details mode
				foreach (EmbeddedControl ec in _embeddedControls)
					ec.Control.Visible = (value == View.Details);

				base.View = value;
			}
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_PAINT:
					if (View != View.Details)
						break;

					// Calculate the position of all embedded controls
					foreach (EmbeddedControl ec in _embeddedControls)
					{
						Rectangle rc = this.GetSubItemBounds(ec.Item, ec.Column);

						if ((this.HeaderStyle != ColumnHeaderStyle.None) &&
							(rc.Top<this.Font.Height)) // Control overlaps ColumnHeader
						{
							ec.Control.Visible = false;
							continue;
						}
						else
						{
							ec.Control.Visible = true;
						}

						switch (ec.Dock)
						{
							case DockStyle.Fill:
								break;
							case DockStyle.Top:
								rc.Height = ec.Control.Height;
								break;
							case DockStyle.Left:
								rc.Width = ec.Control.Width;
								break;
							case DockStyle.Bottom:
								rc.Offset(0, rc.Height-ec.Control.Height);
								rc.Height = ec.Control.Height;
								break;
							case DockStyle.Right:
								rc.Offset(rc.Width-ec.Control.Width, 0);
								rc.Width = ec.Control.Width;
								break;
							case DockStyle.None:
								rc.Size = ec.Control.Size;
								break;
						}

						// Set embedded control's bounds
						ec.Control.Bounds = rc;
					}
					break;
			}
			base.WndProc (ref m);

            if (m.Msg == 20)
            {
                if (this.Items.Count == 0)
                {
                    _b = true;
                    Graphics g = this.CreateGraphics();
                    int w = 0;
                    w = this.Width;
                    w -= g.MeasureString(_msg, this.Font).ToSize().Width;
                    w = w / 2;
                    //g.DrawString(_msg, this.Font, SystemBrushes.ControlText, w, 30);
                    //this.GridLines = false;
                }
                else
                {
                    if (_b)
                    {
                        this.Invalidate();
                        _b = false;
                    }
                }
            }

            if (m.Msg == 4127)
            {
                this.Invalidate();
            }
		}

		private void _embeddedControl_Click(object sender, EventArgs e)
		{
			// When a control is clicked the ListViewItem holding it is selected
			foreach (EmbeddedControl ec in _embeddedControls)
			{
				if (ec.Control == (Control)sender)
				{
					this.SelectedItems.Clear();
					ec.Item.Selected = true;
				}
			}
		}

        private void ListViewEx_Resize(object sender, EventArgs e)
        {
            if (_b) Invalidate();
        }
	}
}
