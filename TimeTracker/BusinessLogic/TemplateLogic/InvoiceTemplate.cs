using RazorLight;
using System.Linq;
using TimeTracker.BusinessLogic.Templates;
using TimeTracker.Models;
using TimeTracker.ViewModels;

namespace TimeTracker.BusinessLogic
{
    public class InvoiceTemplate : ITemplate<Invoice>
    {
        public InvoiceTemplate(Invoice invoice)
        {
            name = "Invoice";
            data = invoice;
        }

        public override string Populate()
        {
            InvoiceReportModel model = new InvoiceReportModel
            {
                Id = data.Id,
                Customer = data.Customer,
                TimeEntries = data.TimeEntries.Select(y => new TimeEntryFormViewModel()
                {
                    Description = y.Description,
                    TotalPrice = y.TotalPrice
                }).ToList(),
                User = data.User,
                Date = data.Date
            };

            var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(Program))
            .UseMemoryCachingProvider()
            .Build();

            string content = TemplateLogic.GetTemplate(name);

            return engine.CompileRenderStringAsync(name, content, model).Result;
        }
    }
}
