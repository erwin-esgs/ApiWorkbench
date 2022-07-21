using System;
using System.Data.SqlClient;
namespace ApiWorkbench.Properties
{
    public class DapperContext
    {
        public DapperContext()
        {
        }

        public SqlConnection GetConnection() {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();

            return new SqlConnection(configuration.GetConnectionString("KonekKeDB"));
            // Dapper will open for us
            //connection.Open();
            //return connection.Query<string>("SELECT * FROM Thing").FirstOrDefault();
        }
       
}
}

