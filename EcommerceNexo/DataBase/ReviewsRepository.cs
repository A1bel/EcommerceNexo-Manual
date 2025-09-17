using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class ReviewsRepository
    {
        public bool Add(Review review)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO reviews
                                        (nota, comentario, data_criacao, usuario_id, produto_id)
                                        VALUES(@nota, @comentario, @data_criacao, @usuario_id, @produto_id);";

                    cmd.Parameters.AddWithValue("@nota", review.Nota);
                    cmd.Parameters.AddWithValue("@comentario", review.Comentario);
                    cmd.Parameters.AddWithValue("@data_criacao", review.DataCriacao);
                    cmd.Parameters.AddWithValue("@usuario_id", review.UsuarioId);
                    cmd.Parameters.AddWithValue("@produto_id", review.ProdutoId);

                    using (cmd.Connection = dba.OpenConnection())
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

        public Review Get(int id)
        {
            Review review = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM reviews WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (cmd.Connection = dba.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            review = new Review
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nota = Convert.ToInt32(reader["nota"]),
                                Comentario = reader["comentario"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return review;
        }

        public List<Review> GetAll()
        {
            List<Review> reviews = new List<Review>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM reviews;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new Review
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nota = Convert.ToInt32(reader["nota"]),
                                Comentario = reader["comentario"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return reviews;
        }

        public List<Review> GetAllByProduto(int produtoId)
        {
            List<Review> reviews = new List<Review>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM reviews WHERE produto_id = @produto_id;";
                    cmd.Parameters.AddWithValue("@produto_id", produtoId);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new Review
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nota = Convert.ToInt32(reader["nota"]),
                                Comentario = reader["comentario"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return reviews;
        }

        public List<Review> GetAllByUsuario(int usuarioId)
        {
            List<Review> reviews = new List<Review>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM reviews WHERE usuario_id = @usuario_id;";
                    cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new Review
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nota = Convert.ToInt32(reader["nota"]),
                                Comentario = reader["comentario"].ToString(),
                                DataCriacao = Convert.ToDateTime(reader["data_criacao"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return reviews;
        }

        public bool Update(Review review)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE reviews
                                        SET nota = @nota,
                                        comentario = @comentario,
                                        usuario_id = @usuario_id,
                                        produto_id = @produto_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", review.Id);
                    cmd.Parameters.AddWithValue("@nota", review.Nota);
                    cmd.Parameters.AddWithValue("@comentario", review.Comentario);
                    cmd.Parameters.AddWithValue("@usuario_id", review.UsuarioId);
                    cmd.Parameters.AddWithValue("@produto_id", review.ProdutoId);

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

        public bool Delete(int id)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM reviews WHERE id = @id;";
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
