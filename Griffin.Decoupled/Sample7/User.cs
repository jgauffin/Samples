namespace Sample7
{
    public class User
    {
        public User(int id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public int Id { get; private set; }
        public string DisplayName { get; private set; }
    }
}