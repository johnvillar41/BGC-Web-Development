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
            if(instance == null)
            {
                instance = new OnsiteTransactionRepository();
            }
            return instance;
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
                while(await reader.ReadAsync())
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