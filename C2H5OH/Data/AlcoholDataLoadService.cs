using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace C2H5OH.Data
{
    internal class AlcoholDataLoadService: IHostedService
    {
        private readonly ILogger logger;
        private readonly int time;
        private readonly string basePath = System.Environment.CurrentDirectory;
        private readonly string tablePath = "/Data/Tables/";
        private readonly string allTable = "All.csv";
        private readonly string labelsTable = "Labels.csv";
        private readonly string mapsTable = "Maps.csv";

        public AlcoholDataLoadService(ILogger<AlcoholDataLoadService> logger)
        {
            this.logger = logger;
            this.time = 10000; 
        }

        public async  Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                DataTable allTableData;
                var targetTablePath = basePath + tablePath + allTable;
                try
                {
                    allTableData = AlcoholDataLoadHelper.ConvertCSVtoDataTable(targetTablePath);
                }
                catch(Exception ex)
                {
                    this.logger.LogError($"C2H5OH: Error when loading talbe from {targetTablePath}. Ex msg: {ex.Message}.");
                }

                this.logger.LogInformation($"C2H5OH: Drinking alcohol for every {this.time}ms.");
                await Task.Delay(10000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
