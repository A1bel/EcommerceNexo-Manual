using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class CarrinhosRepository
    {

        public bool Add(Carrinho carrinho)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO carrinhos
                                        (data_criacao, ativo, usuario_id)
                                        VALUES(@data_criacao, @ativo, @usuario_id);";

                    cmd.Parameters.AddWithValue("@data_criacao", carrinho.DataCriacao);
                    cmd.Parameters.AddWithValue("@ativo", carrinho.Ativo);
                    cmd.Parameters.AddWithValue("@usuario_id", carrinho.UsuarioId);

                    using(cmd.Connection = dba.OpenConnection())
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public Carrinho Get(int id)
        {
            Carrinho carrinho = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM carrinhos WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            carrinho = new Carrinho
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                Ativo = Convert.ToBoolean(reader["ativo"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return carrinho;
        }

        public List<Carrinho> GetAll()
        {
            List<Carrinho> carrinhos = new List<Carrinho>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM carrinhos;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carrinhos.Add(new Carrinho
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                Ativo = Convert.ToBoolean(reader["ativo"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return carrinhos;
        }

        public bool Update(Carrinho carrinho)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE carrinhos
                                        SET data_criacao = @data_criacao,
                                        ativo = @ativo,
                                        usuario_id = @usuario_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", carrinho.Id);
                    cmd.Parameters.AddWithValue("@data_criacao", carrinho.DataCriacao);
                    cmd.Parameters.AddWithValue("@ativo", carrinho.Ativo);
                    cmd.Parameters.AddWithValue("@usuario_id", carrinho.UsuarioId);

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
                    cmd.CommandText = @"DELETE FROM carrinhos WHERE id = @id;";
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
