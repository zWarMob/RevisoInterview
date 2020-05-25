using TimeTracker.Models;

namespace TimeTracker.BusinessLogic
{
    public class InvoiceReport : IReport<InvoiceTemplate>
    {
        public InvoiceReport(Invoice invoice) : base(new InvoiceTemplate(invoice))
        {
            Content = Template.Populate();
        }

        public override string GetHtml()
        {
            return Content;
        }

        public override byte[] GetPDF()
        {
            //convert content to PDF and return
            throw new System.NotImplementedException();
        }
    }

}
