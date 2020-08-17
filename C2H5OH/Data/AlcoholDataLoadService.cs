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
        private readonly int time = 5*60;
        private readonly string basePath = System.Environment.CurrentDirectory;
        private readonly string tablePath = "/Data/Tables/";
        private readonly string allTable = "All.csv";
        private readonly string labelsTable = "Labels.csv";
        private readonly string mapsTable = "Maps.csv";

        private IAllTableManager allTableManager;

        public AlcoholDataLoadService(ILogger<AlcoholDataLoadService> logger, IAllTableManager allTableManager)
        {
            this.logger = logger;
            this.time = 10000;
            this.allTableManager = allTableManager;
        }

        public async  Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var targetTablePath = basePath + tablePath + allTable;
                try
                {
                    var allTableData = AlcoholDataLoadHelper.ConvertCSVtoDataTable(targetTablePath);
                    this.allTableManager.LoadTable(allTableData);
                    this.logger.LogInformation($"C2H5OH: Loading talbe from {targetTablePath}. Column:{allTableData.Columns.Count}, Row:{allTableData.Rows.Count}.");
                }
                catch (Exception ex)
                {
                    this.logger.LogError($"C2H5OH: Error when loading talbe from {targetTablePath}. Ex msg: {ex.Message}.");
                }

                this.logger.LogInformation($"C2H5OH: Drinking alcohol for every {this.time}s.");
                await Task.Delay(this.time* 1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
