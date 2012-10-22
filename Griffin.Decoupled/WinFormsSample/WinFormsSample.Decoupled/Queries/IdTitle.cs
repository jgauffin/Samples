namespace WinFormsSample.Decoupled.Queries
{
    public class IdTitle
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}