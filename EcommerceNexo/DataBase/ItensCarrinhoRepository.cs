using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class ItensCarrinhoRepository
    {
        
        public bool Add(ItemCarrinho item)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO itens_carrinho 
                                        (quantidade, preco_unitario, carrinho_id, produto_id)
                                        VALUES(@quantidade, @preco_unitario, @carrinho_id, @produto_id);";

                    cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@preco_unitario", item.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@carrinho_id", item.CarrinhoId);
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

        public ItemCarrinho Get(int id)
        {
            ItemCarrinho item = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_carrinho WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ItemCarrinho
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                CarrinhoId = Convert.ToInt32(reader["carrinho_id"]),
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

        public List<ItemCarrinho> GetAll()
        {
            List<ItemCarrinho> itens = new List<ItemCarrinho>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_carrinho;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            itens.Add(new ItemCarrinho
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                CarrinhoId = Convert.ToInt32(reader["carrinho_id"]),
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

        public List<ItemCarrinho> GetAllByCarrinho(int carrinhoId)
        {
            List<ItemCarrinho> itens = new List<ItemCarrinho>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM itens_carrinho WHERE carrinho_id = @carrinho_id;";
                    cmd.Parameters.AddWithValue("@carrinho_id", carrinhoId);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            itens.Add(new ItemCarrinho
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Quantidade = Convert.ToInt32(reader["quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["preco_unitario"]),
                                CarrinhoId = Convert.ToInt32(reader["carrinho_id"]),
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

        public bool Update(ItemCarrinho item)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE itens_carrinho
                                        SET quantidade = @quantidade,
                                        preco_unitario = @preco_unitario,
                                        carrinho_id = @carrinho_id,
                                        produto_id = @produto_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@preco_unitario", item.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@carrinho_id", item.CarrinhoId);
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
                    cmd.CommandText = @"DELETE FROM itens_carrinho WHERE id = @id;";
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
