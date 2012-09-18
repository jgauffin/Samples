using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Griffin.Data.BasicLayer;
using Griffin.Data.Mappings;
using Griffin.Data.Queries;
using SimpleQueries.Datalayer;
using SimpleQueries.Models;

namespace SimpleQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            MapperProvider.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());

            // allows us to create ADO.NET classes
            // without knowing the actual driver
            // and by just using the app/web.config
            var factory = new AppConfigConnectionFactory("DemoDb");
            var connection = factory.Create();

            var queries = new UserQueries(connection);


            var constraints = new QueryConstraints<User>()
                .SortBy(x => x.FirstName)
                .Page(2, 50);
            var result = queries.FindAll(constraints);

            foreach (var user in result.Items)
            {
                // Note that each user is not mapped until it's requested
                // as opposed to the entire collection being mapped first.
                Console.WriteLine(user.FirstName);
            }
        }
    }
}
