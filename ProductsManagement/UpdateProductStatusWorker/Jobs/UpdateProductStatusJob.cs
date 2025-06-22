using Microsoft.EntityFrameworkCore;
using Quartz;
using UpdateProductStatusWorker.Data;

namespace UpdateProductStatusWorker.Jobs
{
    public class UpdateProductStatusJob : IJob
    {
        private readonly ILogger<UpdateProductStatusJob> _logger;
        private readonly AppDbContext _dbContext;

        public UpdateProductStatusJob(ILogger<UpdateProductStatusJob> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executando atualização de status de produtos em {time}", DateTime.Now);

            var products = await _dbContext
                                 .Products
                                 .ToListAsync();

            products.ForEach(p => p.SetProductStatus());

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Atualização de status finalizada com sucesso.");
        }
    }
}
