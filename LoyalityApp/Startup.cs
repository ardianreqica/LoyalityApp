using LoyalityApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<LoyalityContext>(opt => opt.UseInMemoryDatabase(databaseName: "LoyalityDB"));


            var options = new DbContextOptionsBuilder<LoyalityContext>()
                                .UseInMemoryDatabase(databaseName: "LoyalityDB")
                                .Options;

            using (var context = new LoyalityContext(options))
            {
                #region Customers

                // The customer has spent at least 500 euro that week
                var customer1 = new Customer
                {
                    Id = new Guid(),
                    Name = "Ardian Recica",
                    CustomerLoyalityPoints = new CustomerLoyalityPoints()
                    {
                        LoyalityPoint = 5000
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Ammount=250,
                            Date = new DateTime(2021,12,28)
                        },
                        new Transaction
                        {
                            Ammount=150,
                            Date = new DateTime(2021,12,29)
                        }
                    },
                    LoyalityPointsTransactions = new List<LoyalityPointsTransaction>()
                    {
                        new LoyalityPointsTransaction()
                        {
                            Ammount=500,
                            Date = new DateTime(2021,11,27)
                        },
                        new LoyalityPointsTransaction()
                        {
                            Ammount=300,
                            Date = new DateTime(2021,12,23)
                        }
                    }
                };
                context.Customers.Add(customer1);

                // At least one transaction exists for that customer on every day of the week
                var customer2 = new Customer
                {
                    Id = new Guid(),
                    Name = "Ardian Recica2",
                    CustomerLoyalityPoints = new CustomerLoyalityPoints()
                    {
                        LoyalityPoint = 5000
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Ammount=5,
                            Date = new DateTime(2021,12,27)
                        },
                        new Transaction
                        {
                            Ammount=6,
                            Date = new DateTime(2021,12,28)
                        },
                        new Transaction
                        {
                            Ammount=7,
                            Date = new DateTime(2021,12,29)
                        },
                        new Transaction
                        {
                            Ammount=8,
                            Date = new DateTime(2021,12,30)
                        },
                        new Transaction
                        {
                            Ammount=9,
                            Date = new DateTime(2021,12,31)
                        },
                        new Transaction
                        {
                            Ammount=6,
                            Date = new DateTime(2022,1,1)
                        },
                        new Transaction
                        {
                            Ammount=3,
                            Date = new DateTime(2022,1,2)
                        }
                    },
                    LoyalityPointsTransactions = new List<LoyalityPointsTransaction>()
                    {
                        new LoyalityPointsTransaction()
                        {
                            Ammount=500,
                            Date = new DateTime(2021,12,27)
                        },
                        new LoyalityPointsTransaction()
                        {
                            Ammount=300,
                            Date = new DateTime(2021,12,23)
                        }
                    }
                };
                context.Customers.Add(customer2);

                // The customer has spent at least 500 euro that week
                // At least one transaction exists for that customer on every day of the week
                var customer3 = new Customer
                {
                    Id = new Guid(),
                    Name = "Ardian Recica3",
                    CustomerLoyalityPoints = new CustomerLoyalityPoints()
                    {
                        LoyalityPoint = 5000
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Ammount=5,
                            Date = new DateTime(2021,12,27)
                        },
                        new Transaction
                        {
                            Ammount=6,
                            Date = new DateTime(2021,12,28)
                        },
                        new Transaction
                        {
                            Ammount=700,
                            Date = new DateTime(2021,12,29)
                        },
                        new Transaction
                        {
                            Ammount=8,
                            Date = new DateTime(2021,12,30)
                        },
                        new Transaction
                        {
                            Ammount=90,
                            Date = new DateTime(2021,12,31)
                        },
                        new Transaction
                        {
                            Ammount=6,
                            Date = new DateTime(2022,1,1)
                        },
                        new Transaction
                        {
                            Ammount=3,
                            Date = new DateTime(2022,1,2)
                        }
                    },
                    LoyalityPointsTransactions = new List<LoyalityPointsTransaction>()
                    {
                        new LoyalityPointsTransaction()
                        {
                            Ammount=500,
                            Date = new DateTime(2021,11,29)
                        },
                        new LoyalityPointsTransaction()
                        {
                            Ammount=300,
                            Date = new DateTime(2021,12,23)
                        }
                    }
                };
                context.Customers.Add(customer3);

                // A user will lose all the points if no transaction was made in the last 5 weeks.
                var customer4 = new Customer
                {
                    Id = new Guid(),
                    Name = "Ardian Recica4",
                    CustomerLoyalityPoints = new CustomerLoyalityPoints()
                    {
                        LoyalityPoint = 5000
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Ammount=250,
                            Date = new DateTime(2021,7,27)
                        },
                        new Transaction
                        {
                            Ammount=150,
                            Date = new DateTime(2021,8,24)
                        }
                    },
                    LoyalityPointsTransactions = new List<LoyalityPointsTransaction>()
                    {
                        new LoyalityPointsTransaction()
                        {
                            Ammount=500,
                            Date = new DateTime(2021,12,27)
                        },
                        new LoyalityPointsTransaction()
                        {
                            Ammount=300,
                            Date = new DateTime(2021,12,23)
                        }
                    }
                };
                context.Customers.Add(customer4);

                var transactionRange = new List<TransactionPointRange>()
                {
                    new TransactionPointRange()
                    {
                        Min = 0,
                        Max = 5000,
                        PointValue = 1
                    },
                    new TransactionPointRange()
                    {
                        Min = 5000,
                        Max = 7500,
                        PointValue = 2
                    },
                    new TransactionPointRange()
                    {
                        Min = 7500,
                        Max = 10000000000,
                        PointValue = 3
                    }
                };

                context.AddRange(transactionRange);

                #endregion

                context.SaveChanges();
            }

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoyalityApp", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoyalityApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
