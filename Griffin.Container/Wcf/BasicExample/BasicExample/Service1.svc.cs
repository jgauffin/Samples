using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BasicExample.Core;
using Griffin.Container;

namespace BasicExample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [Component]
    public class Service1 : IService1
    {
        private readonly SampleService _sampleService;

        public Service1(SampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
            
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
