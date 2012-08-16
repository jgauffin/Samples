namespace BeforeTheWorld.FirstExample
{

    public class UserRepository : IUserRepository
    {
        public User Get(string id)
        {
            return new User();
        }
    }
}