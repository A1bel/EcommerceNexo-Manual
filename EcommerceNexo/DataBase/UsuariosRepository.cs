using EcommerceNexo.Models;
using Npgsql;
using System.Text;

namespace EcommerceNexo.DataBase
{
    public class UsuariosRepository
    {
        public bool Add(Usuario usuario)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO usuarios
                                        (nome, email, senha_hash, data_criacao)
                                        VALUES(@nome, @email, @senha_hash, @data_criacao);";

                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha_hash", usuario.Senha);
                    //cmd.Parameters.AddWithValue("@perfil", usuario.Perfil);
                    cmd.Parameters.AddWithValue("@data_criacao", usuario.DataCriacao);

                    using(cmd.Connection = dba.OpenConnection())
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public Usuario Get(int id)
        {
            Usuario usuario = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM usuarios WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                Senha = reader["senha_hash"].ToString(),
                                Perfil = reader["perfil"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"])
                            };

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return usuario;
        }

        public Usuario GetByEmail(string email)
        {
            Usuario usuario = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM usuarios WHERE email = @email;";
                    cmd.Parameters.AddWithValue("@email", email);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                Senha = reader["senha_hash"].ToString(),
                                Perfil = reader["perfil"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"])
                            };

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return usuario;
        }

        public List<Usuario> GetAll(string nome = null, string email = null, string perfil = null)
        {
            List<Usuario> usuarios = new List<Usuario>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    StringBuilder query = new StringBuilder("SELECT id, nome, email, perfil, data_criacao FROM usuarios WHERE 1=1");

                    if (!string.IsNullOrWhiteSpace(nome))
                        query.Append(" AND nome ILIKE @nome");

                    if (!string.IsNullOrWhiteSpace(email))
                        query.Append(" AND email ILIKE @email");

                    if (!string.IsNullOrWhiteSpace(perfil))
                        query.Append(" AND perfil = @perfil");

                    cmd.CommandText = query.ToString();

                    if(!string.IsNullOrWhiteSpace(nome))
                        cmd.Parameters.AddWithValue("@nome", $"%{nome}%");

                    if (!string.IsNullOrWhiteSpace(email))
                        cmd.Parameters.AddWithValue("@email", $"%{email}%");

                    if(!string.IsNullOrWhiteSpace (perfil))
                        cmd.Parameters.AddWithValue("@perfil", perfil);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                Perfil = reader["perfil"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return usuarios;
        }

        public bool Update(Usuario usuario)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE usuarios
                                        SET nome = @nome, 
                                        email = @email, 
                                        senha_hash = @senha_hash, 
                                        perfil = @perfil
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha_hash", usuario.Senha);
                    cmd.Parameters.AddWithValue("@perfil", usuario.Perfil);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);

                    using(cmd.Connection = dba.OpenConnection())
                    {
                        cmd.ExecuteNonQuery();
                        result = true;  
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM usuarios WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
