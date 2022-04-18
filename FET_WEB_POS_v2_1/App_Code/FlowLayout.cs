using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AdvTek.CustomControls
{
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class FlowLayout : System.Web.UI.UserControl, INamingContainer
    {

        public Unit HorizontalGap
        {
            set
            {                                                
                ViewState["HorizontalGap"] = value;
            }

            get
            {
                if (ViewState["HorizontalGap"] == null)
                {
                    return new Unit(2d, UnitType.Pixel);
                }

                return (Unit)ViewState["HorizontalGap"];
            }
        }

        public Unit VerticalGap
        {
            set
            {
                ViewState["VerticalGap"] = value;
            }

            get
            {
                if (ViewState["VerticalGap"] == null)
                {
                    return new Unit(2d, UnitType.Pixel);
                }

                return (Unit)ViewState["VerticalGap"];
            }
        }


        public RepeatDirection RepeatDirection
        {
            set
            {
                ViewState["RepeatDirection"] = value;
            }

            get
            {
                if (ViewState["RepeatDirection"] == null)
                {
                    return RepeatDirection.Horizontal;
                }

                return (RepeatDirection)ViewState["RepeatDirection"];
            }
        }

        // Notifies the server control that an element, either XML or HTML, 
        // was parsed, and adds the element to the server control's ControlCollection object.
        protected override void AddParsedSubObject(object obj)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("style", "float:left; padding-right: 5px;");
            div.Controls.Add(obj as Control);                                 
            base.AddParsedSubObject(div);
        }

        public virtual void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);            
        }

        protected virtual void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute("align", "center");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);            
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);            
            base.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();            
        }

        public virtual void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderBeginTag(writer); 
            RenderContents(writer);            
            RenderEndTag(writer);            
        }
    }
}
