using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WebUI
{
    public class Startup{
        private CloudBlobContainer GetBlobContainer(){
            var storageAccount = 
                CloudStorageAccount.Parse(
                    Configuration["StorageConnectionString"]);

            var blobStorage = storageAccount.CreateCloudBlobClient();
            var container = blobStorage.GetContainerReference("dataprotection");
            container.CreateIfNotExistsAsync().Wait();
            return container;
        }

        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            var retryPolicy = HttpPolicyExtensions
                                    .HandleTransientHttpError()
                                    .Or<TimeoutRejectedException>()
                                    .RetryAsync(3);

            services.AddHttpClient<IQuoteClient, QuoteClient>()
                    .AddPolicyHandler(retryPolicy)
                    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10))
                    .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(2))
                    .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(
                                                2, TimeSpan.FromSeconds(30)));

            // Create a cloud storage account if we have a connectionString
            if(!string.IsNullOrEmpty(Configuration["StorageConnectionString"])){
                services.AddDataProtection()
                        .PersistKeysToAzureBlobStorage(GetBlobContainer(), "keys");
            }

            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }


        private void CreateQueueIfNotExists(CloudStorageAccount storageAccount){

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference(Configuration["QueueName"]);

            // Create the queue if it doesn't already exist
            //TODO: Move to startup and decide which of these things we should
            //use DI for.
            queue.CreateIfNotExistsAsync().Wait();
        }
    }
}
