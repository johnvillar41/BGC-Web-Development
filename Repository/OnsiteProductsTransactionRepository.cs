using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class OnsiteProductsTransactionRepository
    {
        private static OnsiteProductsTransactionRepository instance = null;
        private OnsiteProductsTransactionRepository()
        {

        }
        public static OnsiteProductsTransactionRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new OnsiteProductsTransactionRepository();
            }
            return instance;
        }
        public async Task<List<OnsiteProductsTransactionModel>> FetchTransactionsGivenByID(int transactionID)
        {
            List<OnsiteProductsTransactionModel> onsiteProductList = new List<OnsiteProductsTransactionModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM onsite_products_transaction_table WHERE transaction_id=@transactionID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@transactionID", transactionID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    onsiteProductList.Add(
                            new OnsiteProductsTransactionModel()
                            {
                                OnsiteProductTransactionID = int.Parse(reader["optt_id"].ToString()),
                                TransactionID = int.Parse(reader["transaction_id"].ToString()),
                                Product = await ProductRepository.GetInstance().GetProducts(int.Parse(reader["product_id"].ToString())),
                                TotalProductsCount = int.Parse(reader["total_product_count"].ToString())
                            }
                        );
                }
            }
            return onsiteProductList;
        }
    }
}