using Microsoft.EntityFrameworkCore;
using UpdateProductStatusWorker;
using UpdateProductStatusWorker.Data;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureServices((context, services) =>
     {
         var configuration = context.Configuration;

         services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

         services.AddQuartzJobs();
     })
    .Build();

//builder.Services.AddHostedService<Worker>();

await host.RunAsync();
//var host = builder.Build();
//host.Run();
