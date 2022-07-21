using System.Text;
using Microsoft.EntityFrameworkCore;
using ApiWorkbench.Models;

namespace ApiWorkbench.Properties
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("KonekKeDB"));
                //optionsBuilder.UseSqlite("@Data source=/Users/erwin/Documents/net/AppApi/API/DB/MyDb.db");
            }
        }

        public DbSet<UserModel> Users { set; get; }
        public DbSet<EmployeeModel> Employees { set; get; }
        public DbSet<BloodPressureRules> BloodPressureRules { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    UserId = 1,
                    Name = "ADMIN",
                    Username = "admin",
                    Password = Operation.sha256_hash("password"),
                    LastLogin = 0
                }
            );
            modelBuilder.Entity<BloodPressureRules>().HasData(
                new BloodPressureRules
                {
                    Id = 1,
                    RuleName = "BP.Normal",
                    Expression = "( Sistolik <= 120 && Sistolik >= 90 ) && ( Diastolik <= 80 && Diastolik >= 60 )",
                    SuccessEvent = "Normal"
                },
                new BloodPressureRules
                {
                    Id = 2,
                    RuleName = "BP.HypoTension",
                    Expression = "Sistolik < 90 && Diastolik < 60",
                    SuccessEvent = "Hypotension"
                },
                new BloodPressureRules
                {
                    Id = 3,
                    RuleName = "BP.HyperTension",
                    Expression = "Sistolik > 120 && Diastolik > 80",
                    SuccessEvent = "Hypertension"
                },
                new BloodPressureRules
                {
                    Id = 4,
                    RuleName = "BP.PreHyperTension",
                    Expression = "Sistolik > 120 || Diastolik > 80",
                    SuccessEvent = "Prehypertension"
                },
                new BloodPressureRules
                {
                    Id = 5,
                    RuleName = "BP.PreHypoTension",
                    Expression = "Sistolik < 90 || Diastolik < 60",
                    SuccessEvent = "Prehypotension"
                }

            );
        }
    }
}
