using System.Data;

namespace C2H5OH.Data
{
    interface IAllTableManager
    {
        public void LoadTable(DataTable table);
        public DataTable GetAllTable();
        public DataColumnCollection GetAllTableColumn();
        public DataRow[] GetDrinkFromName(string name);
        public DataRow[] GetDrinkFromPartialName(string name);
        public DataRow[] GetDrinkFromType(string type);
        public DataRow[] GetDrinkFromMap(string map);
    }
}
