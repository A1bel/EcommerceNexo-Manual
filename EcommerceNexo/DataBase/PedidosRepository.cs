using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class PedidosRepository
    {

        public bool Add(Pedido pedido)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO pedidos
                                        (data, status, total, usuario_id)
                                        VALUES(@data, @status, @total, @usuario_id);";

                    cmd.Parameters.AddWithValue("@data", pedido.Data);
                    cmd.Parameters.AddWithValue("@status", pedido.Status);
                    cmd.Parameters.AddWithValue("@total", pedido.Total);
                    cmd.Parameters.AddWithValue("@usuario_id", pedido.UsuarioId);

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

        public Pedido Get(int id)
        {
            Pedido pedido = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM pedidos WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido = new Pedido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Data = Convert.ToDateTime(reader["data"]),
                                Status = reader["status"].ToString(),
                                Total = Convert.ToDecimal(reader["total"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return pedido;
        }

        public List<Pedido> GetAll()
        {
            List<Pedido> pedidos = new List<Pedido>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM pedidos;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidos.Add(new Pedido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Data= Convert.ToDateTime(reader["data"]),
                                Status = reader["status"].ToString(),
                                Total = Convert.ToDecimal(reader["total"]),
                                UsuarioId = Convert.ToInt32(reader["usuario_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return pedidos;
        }

        public bool Update(Pedido pedido)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE pedidos
                                        SET data = @data,
                                        status = @status,
                                        total = @total,
                                        usuario_id = @usuario_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", pedido.Id);
                    cmd.Parameters.AddWithValue("@data", pedido.Data);
                    cmd.Parameters.AddWithValue("@status", pedido.Status);
                    cmd.Parameters.AddWithValue("@total", pedido.Total);
                    cmd.Parameters.AddWithValue("@usuario_id", pedido.UsuarioId);

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
                    cmd.CommandText = @"DELETE FROM pedidos WHERE id = @id;";
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
