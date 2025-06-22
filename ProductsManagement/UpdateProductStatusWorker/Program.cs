using Microsoft.EntityFrameworkCore;
using UpdateProductStatusWorker;
using UpdateProductStatusWorker.Data;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureServices((context, services) =>
     {
         var configuration = context.Configuration;

         services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

         services.AddQuartzJobs(configuration);
     })
    .Build();

await host.RunAsync();