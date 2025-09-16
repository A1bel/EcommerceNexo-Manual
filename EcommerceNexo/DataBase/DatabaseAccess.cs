using EcommerceNexo.Global;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class DatabaseAccess
    {
        public NpgsqlConnection OpenConnection()
        {
            string connectionString = String.Format(
                "Server = {0}; User Id = {1}; Database = {2}; Port = {3}; Password = {4};",
                Config.dbHost, Config.dbUser, Config.dbName, Config.dbPort, Config.dbPort
            );

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
