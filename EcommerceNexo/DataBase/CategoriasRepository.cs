using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class CategoriasRepository
    {
        public bool Add(Categoria categoria)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO categorias 
                                        (nome, descricao)
                                        VALUES(@nome, @descricao);";

                    cmd.Parameters.AddWithValue("@nome", categoria.Nome);
                    cmd.Parameters.AddWithValue("@descricao", categoria.Descricao);

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

        public Categoria Get(int id)
        {
            Categoria categoria = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM categorias WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            categoria = new Categoria
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Descricao = reader["descricao"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return categoria;
        }

        public List<Categoria> GetAll()
        {
            List<Categoria> categorias = new List<Categoria>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM categorias;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new Categoria
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Descricao = reader["descricao"].ToString()
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return categorias;
        }

        public bool Update(Categoria categoria)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE categorias
                                        SET nome = @nome,
                                        descricao = @descricao
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@nome", categoria.Nome);
                    cmd.Parameters.AddWithValue("@descricao", categoria.Descricao);
                    cmd.Parameters.AddWithValue("@id", categoria.Id);

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
                    cmd.CommandText = @"DELETE FROM categorias WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

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
    }
}
