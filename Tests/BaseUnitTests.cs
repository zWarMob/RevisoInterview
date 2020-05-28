using System;
using System.Collections.Generic;
using TimeTracker.BusinessLogic;
using TimeTracker.Models;
using Xunit;

namespace Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("Invoice")]
        public void TemplateLogic_GetTemplateAndEnsureNotEmpty(string templateName)
        {
            // Act
            var template = TemplateLogic.GetTemplate(templateName);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(template));
        }

        [Fact]
        public void ReportLogic_GetReportAndEnsureNotEmpty()
        {
            //Arrange
            Invoice invoice = new Invoice()
            {
                Customer = new Customer()
                {
                    Address = "Dream Boulevard",
                    CompanyName = "Yoggi",
                    Name = "Nicolaus Copernicus"
                },
                Date = DateTime.Today,
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Description = "Sky scraping",
                        HourlyPrice = 100,
                        Hours = 42
                    }
                }
            };

            //Act
            InvoiceReport report = new InvoiceReport(invoice);

            //Assert
            Assert.False(string.IsNullOrWhiteSpace(report.Content));
        }
    }
}
