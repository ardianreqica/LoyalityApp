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
                            Ammount=5000,
                            Date = new DateTime(2021,12,27)
                        },
                        new Transaction
                        {
                            Ammount=5000,
                            Date = new DateTime(2021,12,24)
                        }
                    }
                };
                context.Customers.Add(customer1);

                var customer2 = new Customer
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
                            Ammount=5000,
                            Date = new DateTime(2021,12,27)
                        },
                        new Transaction
                        {
                            Ammount=5000,
                            Date = new DateTime(2021,12,24)
                        }
                    }
                };
                context.Customers.Add(customer2);

                var customer3 = new Customer
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
                            Ammount=5000,
                            Date = new DateTime(2021,12,27)
                        },
                        new Transaction
                        {
                            Ammount=5000,
                            Date = new DateTime(2021,12,24)
                        }
                    }
                };
                context.Customers.Add(customer3);


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
                        Min = 5001,
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

                #endregion




                context.SaveChanges();
            }

            //services.AddControllers();

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
