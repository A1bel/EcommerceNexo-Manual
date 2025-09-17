using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class ItensPedidoRepository
    {
        public bool Add(ItemPedido item)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO itens_pedido
                                        (quantidade, preco_unitario, pedido_id, produto_id)
                                        VALUES(@quantidade, @preco_unitario, @pedido_id, @produto_id);";

                    cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@preco_unitario", item.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@pedido_id", item.PedidoId);
                    cmd.Parameters.AddWithValue("@produto_id", item.ProdutoId);

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

        public ItemPedido Get(int id)
        {
            ItemPedido item = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_pedido WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection()) 
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ItemPedido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                PedidoId = Convert.ToInt32(reader["pedido_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }

        public List<ItemPedido> GetAll()
        {
            List<ItemPedido> itens = new List<ItemPedido>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_pedido;";

                    using(cmd.Connection =  dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            itens.Add(new ItemPedido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                PedidoId = Convert.ToInt32(reader["pedido_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
            

            }
            return itens;
        }

        public List<ItemPedido> GetAllByPedido(int pedidoId)
        {
            List<ItemPedido> itens = new List<ItemPedido>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_pedido WHERE pedido_id = @pedido_id;";
                    cmd.Parameters.AddWithValue("@pedido_id", pedidoId);

                    using(cmd.Connection =  dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            itens.Add(new ItemPedido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                PedidoId = Convert.ToInt32(reader["pedido_id"]),
                                ProdutoId = Convert.ToInt32(reader["produto_id"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
            

            }
            return itens;
        }

        public bool Update(ItemPedido item)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE itens_pedido
                                        SET quantidade = @quantidade,
                                        preco_unitario = @preco_unitario,
                                        pedido_id = @pedido_id,
                                        produto_id = @produto_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@preco_unitario", item.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@pedido_id", item.PedidoId);
                    cmd.Parameters.AddWithValue("@produto_id", item.ProdutoId);

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
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM itens_pedido WHERE id = @id;";
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
