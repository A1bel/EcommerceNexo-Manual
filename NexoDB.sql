DROP TABLE IF EXISTS itens_pedido;
DROP TABLE IF EXISTS pedidos;
DROP TABLE IF EXISTS itens_carrinho;
DROP TABLE IF EXISTS carrinhos;
DROP TABLE IF EXISTS reviews;
DROP TABLE IF EXISTS produtos;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS categorias;

Create table IF NOT EXISTS categorias(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(100) NOT NULL,
	descricao TEXT
);

CREATE TABLE IF NOT EXISTS produtos(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(100) NOT NULL,
	descricao TEXT,
	preco NUMERIC(10,2) NOT NULL,
	estoque INT NOT NULL,
	tamanho VARCHAR(20),
	cor VARCHAR(50),
	imagem VARCHAR(300),
	categoria_id INT NOT NULL,
	CONSTRAINT fk_produto_categoria FOREIGN KEY (categoria_id) REFERENCES categorias(id)
);

CREATE TABLE IF NOT EXISTS usuarios(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(100) NOT NULL,
	email VARCHAR(150) NOT NULL,
	senha_hash VARCHAR(255) NOT NULL,
	perfil VARCHAR(20) NOT NULL DEFAULT 'cliente',
	data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS pedidos( 
	id SERIAL PRIMARY KEY,
	usuario_id INT NOT NULL,
	data TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	status VARCHAR(20) NOT NULL DEFAULT 'Pendente',
	total DECIMAL(10,2) NOT NULL,
	CONSTRAINT fk_pedido_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

CREATE TABLE IF NOT EXISTS itens_pedido(
	id SERIAL PRIMARY KEY,
	pedido_id INT NOT NULL,
	produto_id INT NOT NULL,
	quantidade INT NOT NULL CHECK (quantidade > 0),
	preco_unitario DECIMAL(10,2) NOT NULL,
	CONSTRAINT fk_itempedido_pedido FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
	CONSTRAINT fk_itempedido_produto FOREIGN KEY (produto_id) REFERENCES produtos(id)
);

CREATE TABLE IF NOT EXISTS carrinhos(
	id SERIAL PRIMARY KEY,
	usuario_id INT NOT NULL,
	data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	ativo BOOLEAN DEFAULT TRUE,
	CONSTRAINT fk_carrinho_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

CREATE TABLE IF NOT EXISTS itens_carrinho(
	id SERIAL PRIMARY KEY,
	carrinho_id INT NOT NULL,
	produto_id INT NOT NULL,
	quantidade INT NOT NULL CHECK(quantidade > 0),
	preco_unitario DECIMAL(10,2) NOT NULL,
	CONSTRAINT fk_carrinhoitens_carrinho FOREIGN KEY (carrinho_id) REFERENCES carrinhos(id),
	CONSTRAINT fk_carrinhoitens_produto FOREIGN KEY (produto_id) REFERENCES produtos(id)
);

CREATE TABLE IF NOT EXISTS reviews(
	id SERIAL PRIMARY KEY,
	usuario_id INT NOT NULL,
	produto_id INT NOT NULL,
	nota INT NOT NULL CHECK(nota >= 1 AND nota <=5),
	comentario TEXT,
	data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT fk_review_produto FOREIGN KEY (produto_id) REFERENCES produtos(id),
	CONSTRAINT fk_review_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);
/*INSERT INTO Categoria(Nome, Descricao)
VALUES('Camisetas', 'camisetas masculinas e femininas');*/


SELECT * FROM categorias;
SELECT * FROM produtos; 
SELECT * FROM usuarios;
SELECT * FROM pedidos;
SELECT * FROM itens_pedido;
SELECT * FROM carrinhos;
SELECT * FROM itens_carrinho;
SELECT * FROM reviews;

/*
Produto → Id, Nome, Descrição, Preço, Estoque, CategoriaId, Tamanho, Cor, ImagemUrl 
Categoria → Id, Nome, Descrição 
Usuário → Id, Nome, Email, SenhaHash (SQL manual) ou PasswordHash (Identity), Perfil/Roles (Cliente/Admin) 
Pedido → Id, UsuarioId, Data, Status, Total 
ItemPedido → Id, PedidoId, ProdutoId, Quantidade, PreçoUnitário 
Carrinho → Id, UsuarioId, Lista de Itens (ProdutoId + Quantidade) 
Review → Id, ProdutoId, UsuarioId, Nota, Comentário, Data Perfis/roles de usuário 
Cliente: cadastrado via formulário público. 
Admin: criado manual ou via seed, nunca pelo formulário público. 
Diferenciação de acesso é feita verificando o perfil/role após login.*/