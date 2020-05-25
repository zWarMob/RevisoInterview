namespace TimeTracker.BusinessLogic
{
    public abstract class ITemplate<T>
    {
        public string name { get; set; }

        public T data { get; set; }

        public abstract string Populate();
    }
}
