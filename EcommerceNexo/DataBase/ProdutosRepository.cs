using EcommerceNexo.Models;
using Npgsql;

namespace EcommerceNexo.DataBase
{
    public class ProdutosRepository
    {
        
        public bool Add(Produto produto)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO produtos 
                                        (nome, descricao, preco, estoque, tamanho, cor, imagem, categoria_id)
                                        VALUES(@nome, @descricao, @preco, @estoque, @tamanho, @cor, @imagem, @categoria_id);";

                    cmd.Parameters.AddWithValue("@nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                    cmd.Parameters.AddWithValue("@preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                    cmd.Parameters.AddWithValue("@tamanho", produto.Tamanho);
                    cmd.Parameters.AddWithValue("@cor", produto.Cor);
                    cmd.Parameters.AddWithValue("@imagem", produto.Imagem);
                    cmd.Parameters.AddWithValue("@categoria_id", produto.CategoriaId);

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

        public Produto Get(int id)
        {
            Produto produto = null;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM produtos WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            produto = new Produto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                Preco = Convert.ToDecimal(reader["preco"]),
                                Estoque = Convert.ToInt32(reader["estoque"]),
                                Tamanho = reader["tamanho"].ToString(),
                                Cor = reader["cor"].ToString(),
                                Imagem = reader["imagem"].ToString(),
                                CategoriaId = Convert.ToInt32(reader["categoria_id"])

                            };
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return produto;
        }

        public List<Produto> GetAll()
        {
            List<Produto> produtos = new List<Produto>();
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM produtos;";

                    using(cmd.Connection = dba.OpenConnection())
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(new Produto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                Preco = Convert.ToDecimal(reader["preco"]),
                                Estoque = Convert.ToInt32(reader["estoque"]),
                                Tamanho = reader["tamanho"].ToString(),
                                Cor = reader["cor"].ToString(),
                                Imagem = reader["imagem"].ToString(),
                                CategoriaId = Convert.ToInt32(reader["categoria_id"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return produtos;
        }

        public bool Update(Produto produto)
        {
            bool result = false;
            DatabaseAccess dba = new DatabaseAccess();

            try
            {
                using(NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"UPDATE produtos
                                        SET nome = @nome,
                                        descricao = @descricao,
                                        preco = @preco,
                                        estoque = @estoque,
                                        tamanho = @tamanho,
                                        cor = @cor,
                                        imagem = @imagem,
                                        categoria_id = @categoria_id
                                        WHERE id = @id;";

                    cmd.Parameters.AddWithValue("@id", produto.Id);
                    cmd.Parameters.AddWithValue("@nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                    cmd.Parameters.AddWithValue("@preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                    cmd.Parameters.AddWithValue("@tamanho", produto.Tamanho);
                    cmd.Parameters.AddWithValue("@cor", produto.Cor);
                    cmd.Parameters.AddWithValue("@imagem", produto.Imagem);
                    cmd.Parameters.AddWithValue("@categoria_id", produto.CategoriaId);

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
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM produtos WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

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
    }
}
