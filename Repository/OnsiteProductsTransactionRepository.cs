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
        public static OnsiteProductsTransactionRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OnsiteProductsTransactionRepository();
                }
                return instance;
            }            
        }
        /// <summary>
        ///     This function lets the administrator to insert a transaction
        /// </summary>
        /// <param name="onsiteProductsTransactionModel">
        ///     Passes onsiteProductsTransactionModel as parameter
        /// </param>

        public async Task InsertTransactionsAsync(OnsiteProductsTransactionModel onsiteProductsTransactionModel)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "INSERT INTO onsite_products_transaction_table(transaction_id,product_id,administrator_username,total_product_count,subtotal_price)" +
                    "VALUES(@transactionID,@productID,@administrator,@totalCount,@subtotal)";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@transactionID", onsiteProductsTransactionModel.TransactionID);
                command.Parameters.AddWithValue("@productID", onsiteProductsTransactionModel.Product.Product_ID);
                command.Parameters.AddWithValue("@administrator", onsiteProductsTransactionModel.Administrator);
                command.Parameters.AddWithValue("@totalCount", onsiteProductsTransactionModel.TotalProductsCount);
                command.Parameters.AddWithValue("@subtotal", onsiteProductsTransactionModel.SubTotalPrice);
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        ///     This function retrieves onsite transaction details
        /// </summary>
        /// <param name="transactionID">
        ///     Passes a transaction ID as a parameter
        /// </param>
        /// <returns>
        /// <para>Returns the list of onsite transactions</para>
        /// <para>Type: List<OnsiteProductsTransactionModel></para>
        /// </returns>
        public async Task<List<OnsiteProductsTransactionModel>> FetchTransactionsGivenByIDAsync(int transactionID)
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
                                Product = await ProductRepository.SingleInstance.GetProductsAsync(int.Parse(reader["product_id"].ToString())),
                                TotalProductsCount = int.Parse(reader["total_product_count"].ToString())
                            }
                        );
                }
            }
            return onsiteProductList;
        }
    }
}