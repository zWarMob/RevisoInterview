namespace TimeTracker.BusinessLogic
{
    public abstract class IReport<ITemplate>
    {
        public string Content { get; set; }

        public ITemplate Template { get; set; }

        protected IReport(ITemplate template)
        {
            this.Template = template;
        }

        public abstract string GetHtml();

        public abstract byte[] GetPDF();
    }
}
