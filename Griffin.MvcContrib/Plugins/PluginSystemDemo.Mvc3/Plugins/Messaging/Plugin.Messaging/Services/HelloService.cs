namespace Plugin.Messaging.Services
{
    public class HelloService : IHelloService
    {
        public string GetMessage()
        {
            return "Hello World";
        }
    }
}