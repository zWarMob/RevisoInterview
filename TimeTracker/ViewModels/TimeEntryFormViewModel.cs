namespace TimeTracker.ViewModels
{
    public class TimeEntryFormViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }

        public string DescriptionAndPrice { get { return Description + " - " + TotalPrice + " DKK"; } }
    }
}
