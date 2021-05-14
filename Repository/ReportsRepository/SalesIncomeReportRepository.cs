using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftEngWebEmployee.Repository.ReportsRepository
{
    public class SalesIncomeReportRepository
    {
        private static SalesIncomeReportRepository instance = null;
        private SalesIncomeReportRepository()
        {

        }
        public static SalesIncomeReportRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SalesIncomeReportRepository();
                }
                return instance;
            }            
        }
        /// <summary>
        ///     Calculates the total sales sold by a specific administrator
        /// </summary>
        /// <param name="administrator">
        ///     Passes a parameter administrator name
        /// </param>
        /// <returns>
        ///     <para>Returns a model of model containing all the transactions made by the administrator</para>
        ///     <para>Type: SalesIncomeReportViewModel</para>
        /// </returns>
        public async Task<SalesIncomeReportViewModel> FetchTotalSaleOfAdminAsync(string administrator)
        {
            SalesIncomeReportViewModel salesIncome = new SalesIncomeReportViewModel();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT (SELECT SUM(onsite_products_transaction_table.subtotal_price) FROM onsite_products_transaction_table WHERE onsite_products_transaction_table.administrator_username = 'sample') as TotalSaleOnSite, (SELECT SUM(specific_orders_table.subtotal_price) FROM specific_orders_table WHERE specific_orders_table.administrator_username = 'sample') AS TotalSaleOrder";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@administrator", administrator);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    if (String.IsNullOrEmpty(reader["TotalSaleOnSite"].ToString()))
                        salesIncome.TotalSaleOnsite = 0;
                    else
                        salesIncome.TotalSaleOnsite = int.Parse(reader["TotalSaleOnSite"].ToString());
                    if (String.IsNullOrEmpty(reader["TotalSaleOrder"].ToString()))
                        salesIncome.TotalSaleOrders = 0;
                    else
                        salesIncome.TotalSaleOrders = int.Parse(reader["TotalSaleOrder"].ToString());
                    salesIncome.TotalSale = salesIncome.TotalSaleOnsite + salesIncome.TotalSaleOrders;
                    salesIncome.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(administrator);
                }
            }
            return salesIncome;
        }
        /// <summary>
        ///     Fetches all the OrderIds within a given date
        /// </summary>
        /// <param name="date">
        ///     Passes a date value format
        /// </param>
        /// <returns>
        ///     <para>Returns an IEnumerable of OrderIds</para>
        ///     <para>Type: IEnumerable<int></para>
        /// </returns>
        public async Task<IEnumerable<int>> FetchOrderIdsAsync(DateTime date)
        {
            List<int> orderIds = new List<int>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT DISTINCT(order_id) FROM sales_table WHERE date LIKE @dateToday";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                var dateToday = date.ToString("yyyy-MM-dd");
                command.Parameters.AddWithValue("@dateToday", dateToday + "%");
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {                   
                        if(reader["order_id"] != DBNull.Value)
                            orderIds.Add(int.Parse(reader["order_id"].ToString()));                                              
                    }
                }
            }
            return orderIds;
        }
        /// <summary>
        ///     Fetches all the onsiteIds within a given date
        /// </summary>
        /// <param name="date">
        ///     Passes a datetime parameter
        /// </param>
        /// <returns>
        ///     <para>Returns a list of onsite ids</para>
        ///     <para>Type: IEnumerable<int></para>
        /// </returns>
        public async Task<IEnumerable<int>> FetchOnsiteIdsAsync(DateTime date)
        {
            List<int> onsiteId = new List<int>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT DISTINCT(onsite_transaction_id) FROM sales_table WHERE date LIKE @dateToday";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                var dateToday = date.ToString("yyyy-MM-dd");
                command.Parameters.AddWithValue("@dateToday", dateToday + "%");
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (reader["onsite_transaction_id"] != DBNull.Value)
                            onsiteId.Add(int.Parse(reader["onsite_transaction_id"].ToString()));                       
                    }
                }
            }
            return onsiteId;
        }
    }
}