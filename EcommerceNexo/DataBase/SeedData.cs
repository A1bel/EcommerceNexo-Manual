using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class SeedData
    {
        public static void EnsureAdminUser(IServiceProvider serviceProvider)
        {
            DatabaseAccess dba = new DatabaseAccess();
            using var connection = dba.OpenConnection();

            string checkQuerry = "SELECT COUNT(1) FROM usuarios WHERE email = @email";
            using(NpgsqlCommand checkCmd = new NpgsqlCommand(checkQuerry, connection))
            {
                checkCmd.Parameters.AddWithValue("@email", "admin@admin.com");

                long count = (long)checkCmd.ExecuteScalar();
                if(count > 0)
                {
                    return;
                }
            }

            string senha = BCrypt.Net.BCrypt.HashPassword("admin123");
            string insertQuery = @"INSERT INTO usuarios (nome, email, senha_hash, perfil, data_criacao)
                                    VALUES(@nome, @email, @senha, @perfil, @data_criacao);";

            using (NpgsqlCommand insertCmd = new NpgsqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@nome", "Administrador");
                insertCmd.Parameters.AddWithValue("@email", "admin@admin.com");
                insertCmd.Parameters.AddWithValue("@senha", senha);
                insertCmd.Parameters.AddWithValue("@perfil", "admin");
                insertCmd.Parameters.AddWithValue("@data_criacao", DateTime.Now);

                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
