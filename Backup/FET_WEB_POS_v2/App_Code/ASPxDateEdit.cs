using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace AdvTek.CustomControls
{
    /// <summary>
    /// ASPxDateEditEx 的摘要描述
    /// </summary>
    public class ASPxDateEdit : DevExpress.Web.ASPxEditors.ASPxDateEdit
    {
        public ASPxDateEdit()
        {
            PreRender += (o, e) => DoPreRender();
        }

        private void DoPreRender()
        {
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "IncludeCommonJS", ResolveClientUrl("~/ClientUtility/CheckDate.js"));

            ClientSideEvents.ParseDate = @"
                        function (s,e) {{
                            var sValue = e.value;
                            if (sValue != null && sValue != '') {
                                if (!IsDate(sValue)) {
                                    alert(e.value + ' 格式不正確');
                                }
                            }

                        }}
                        ";
          
//            var callbackScript = @"
//                        function OnError(result) {{
//                          alert(result.get_message());
//                        }}
//
//                        function GetDate(control, symbolicDate) {{
//                          myDateService(
//                              symbolicDate,
//                              function(result) {{
//                                control.SetDate(result);
//                                control.Enabled = true;
//                              }},
//                              OnError);
//                        }}
//                        ";

//            Page.ClientScript.RegisterClientScriptBlock(typeof(ASPxDateEdit), ClientID, callbackScript, true);
        }



        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ButtonStyle.BackgroundImage.ImageUrl = "~/Icon/calendar.jpg";
            this.Width = 100;
            this.DateOnError = DateOnError.Null;

        }

        protected override void Render(HtmlTextWriter output)
        {
            StringWriter writer = new StringWriter();
            HtmlTextWriter buffer = new HtmlTextWriter(writer);
            base.Render(buffer);

            string dateEditMarkup = writer.ToString();           
            Regex re = new Regex(@"<td\b[^>]*class=""dxeCalendarDayHeader""[^>]*>(.*?)</td>", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            dateEditMarkup = re.Replace(dateEditMarkup, (Match m) =>
            {                    
                return m.Value.Replace("星期", string.Empty);
            });

            output.Write(dateEditMarkup);
        }

    }
}