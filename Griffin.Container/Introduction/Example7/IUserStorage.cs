namespace Example7
{
    public interface IUserStorage
    {
        User Create(string userName);
        void Save(User user);
        void Delete(User user);
    }
}