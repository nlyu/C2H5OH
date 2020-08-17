using Microsoft.Extensions.Logging;
using System.Data;

namespace C2H5OH.Data
{
    internal class AllTableManager : IAllTableManager
    {
        private DataTable allTableData;
        private readonly ILogger logger;

        public AllTableManager(ILogger<AllTableManager> logger)
        {
            this.logger = logger;
        }

        public void LoadTable(DataTable table)
        {
            this.allTableData = table;
            this.allTableData.CaseSensitive = false;
        }

        public DataTable GetAllTable()
        {
            return this.allTableData;
        }

        public DataColumnCollection GetAllTableColumn()
        {
            return this.allTableData.Columns;
        }

        public DataRow[] GetDrinkFromName(string name)
        {
            return this.allTableData.Select($"{AlcoholDataTableConst.AllTableName}={name}");
        }

        public DataRow[] GetDrinkFromPartialName(string name)
        {
            return this.allTableData.Select($"{AlcoholDataTableConst.AllTableName} like '%{name}%'");
        }

        public DataRow[] GetDrinkFromType(string type)
        {
            return this.allTableData.Select($"{AlcoholDataTableConst.AllTableType}={type}");
        }

        public DataRow[] GetDrinkFromMap(string map)
        {
            return this.allTableData.Select($"{AlcoholDataTableConst.AllTableOrigin}={map}");
        }
    }
}
