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
    public class OnsiteTransactionRepository
    {
        private static OnsiteTransactionRepository instance = null;
        private OnsiteTransactionRepository()
        {

        }
        public static OnsiteTransactionRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new OnsiteTransactionRepository();
            }
            return instance;
        }
        public async Task<MySqlConnection> InsertNewTransaction(OnsiteTransactionModel onsiteTransaction)
        {
            MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING);
            await connection.OpenAsync();
            string queryString = "INSERT INTO onsite_transaction_table(total_sale)VALUES(@totalSale)";
            MySqlCommand command = new MySqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@totalSale", onsiteTransaction.TotalSale);
            await command.ExecuteNonQueryAsync();
            return connection;
        }
        public async Task<int> FetchLastInsertID(MySqlConnection connection)
        {
            int lastIdInserted = 0;
            string queryString = "SELECT LAST_INSERT_ID()";
            MySqlCommand command = new MySqlCommand(queryString, connection);
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                lastIdInserted = reader.GetInt32(0);
            }
            await connection.CloseAsync();
            return lastIdInserted;
        }
        public async Task<int> CalculateTotalSaleOnsite(int transactionID)
        {
            int totalOnsiteSale = 0;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT SUM(total_sale) as TotalSale FROM onsite_transaction_table WHERE transaction_id = @transactionId";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@transactionId", transactionID);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        if (reader["TotalSale"] != DBNull.Value)
                        {
                            totalOnsiteSale = int.Parse(reader["TotalSale"].ToString());
                        }
                    }
                }
            }
            return totalOnsiteSale;
        }
        public async Task<OnsiteTransactionModel> FetchOnsiteTransaction(int transactionID)
        {
            OnsiteTransactionModel onsiteTransactionModel = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM onsite_transaction_table WHERE transaction_id=@transactionID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@transactionID", transactionID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    onsiteTransactionModel = new OnsiteTransactionModel()
                    {
                        TransactionID = int.Parse(reader["transaction_id"].ToString()),
                        //Generate Customer Model
                        TotalSale = int.Parse(reader["total_sale"].ToString()),
                        OnsiteProductTransactionList = await OnsiteProductsTransactionRepository.GetInstance().FetchTransactionsGivenByID(int.Parse(reader["transaction_id"].ToString()))
                    };
                }
            }
            return onsiteTransactionModel;
        }
    }
}