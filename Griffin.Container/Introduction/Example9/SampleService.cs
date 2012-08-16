using Griffin.Container;

namespace Example9
{
    /// <summary>
    /// Just to show interception
    /// </summary>
    [Component]
    public class SampleService
    {
        // Do note that all intercepted methods must be virtual
        public virtual void DoSomething(string abc)
        {
            
        }
    }
}