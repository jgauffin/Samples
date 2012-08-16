namespace Example8
{
    public class UserCreated
    {
        public UserCreated(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}