namespace Example5
{
    /// <summary>
    /// Just fake a dependency
    /// </summary>
    public class OurDependency
    {
        private static int _id;
        private int _myId;

        public OurDependency()
        {
            _myId = _id++;
        }

        public override string ToString()
        {
            return string.Format("OurDependency ID: {0}", _myId);
        }
    }
}