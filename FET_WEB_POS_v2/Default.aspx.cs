using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        /*
        Document pdf = new Document(PageSize.LETTER);
        PdfWriter writer = PdfWriter.GetInstance(pdf,
        new FileStream(Request.PhysicalApplicationPath + "~1.pdf", FileMode.Create));
        pdf.Open();

        //This action leads directly to printer dialogue
        PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
        writer.AddJavaScript(jAction);

        pdf.Add(new Paragraph("My first PDF on line"));
        pdf.Close();

        //Open the pdf in the frame
        frame1.Attributes["src"] = "~1.pdf";
        */
       //tooltip1.AddTooltipControl(lb1);
    }
}
