using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository.ReportsRepository
{
    public class SalesIncomeReportRepository
    {
        private static SalesIncomeReportRepository instance = null;
        private SalesIncomeReportRepository()
        {

        }
        public static SalesIncomeReportRepository GetInstance()
        {
            if(instance == null)
            {
                instance = new SalesIncomeReportRepository();
            }
            return instance;
        }
        public async Task<SalesIncomeReportViewModel> FetchTotalSaleOfAdmin(string administrator)
        {
            SalesIncomeReportViewModel salesIncome = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT " +
                    "SUM(onsite_transaction_table.total_sale + customer_orders_table.order_total_price) as TotalSale, " +
                    "SUM(onsite_transaction_table.total_sale) as TotalSaleOnsite, " +
                    "SUM(customer_orders_table.order_total_price) as TotalSaleOrders " +
                    "FROM onsite_transaction_table INNER JOIN customer_orders_table INNER JOIN login_table WHERE login_table.user_username = '@administrator'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@administrator", administrator);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    salesIncome = new SalesIncomeReportViewModel
                    {
                        Administrator = await AdministratorRepository.GetInstance().FindAdministrator(administrator),
                        TotalSale = int.Parse(reader["TotalSale"].ToString()),
                        TotalSaleOrders = int.Parse(reader["TotalSaleOrders"].ToString()),
                        TotalSaleOnsite = int.Parse(reader["TotalSaleOrders"].ToString())
                    };
                }
            }
            return salesIncome;
        }
    }
}