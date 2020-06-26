USE p2g7;
CREATE SCHEMA SuperMercado;
GO

------------------------------------------------------------
-- PESSOA
create table SuperMercado.Pessoa(
	ID int							not null Identity(1,1),
	Nome varchar(40)				not null,
	Email varchar(30)				not null,
	Telefone char(9),
	Idade int						not null,
	Salario float					not null DEFAULT -1,
	Genero varchar(1)			    not null,

	Primary key (ID),
	Unique(Email,Telefone)
)

-- UTILIZADOR
create table SuperMercado.Utilizador(
	Nome varchar(40)				not null,
	Senha varbinary(20)				not null,
	ID 	 int						not null,

	Primary key(ID),
	Foreign key(ID) references SuperMercado.Pessoa(ID)
)

-- GERENTE ARMAZEM
create table SuperMercado.GerenteArmazem(
	ID int							not null,

	Foreign key (ID) references SuperMercado.Utilizador(ID)
)

-- ARMAZEM
create table SuperMercado.Armazem(
	ID int							not null Identity(1,1) ,
	Endereco varchar(50)			not null,
	Primary key (ID)
)

-- CONDUTOR
create table SuperMercado.Condutor(
	ID int							not null,
	Disponivel int					not null DEFAULT 1,
	CartaoConducao char(9)			not null,

	Primary key (ID),
	Foreign key (ID) references SuperMercado.Pessoa(ID),
	Unique(CartaoConducao)
)	

-- FUNCIONARIO
create table SuperMercado.Funcionario(
	ID int							not null,

	Primary key(ID),
	Foreign key (ID) references SuperMercado.Pessoa(ID)
)

-- CLIENTE
create table SuperMercado.Cliente(
	ID int							not null,
	NIF char(9)						not null DEFAULT '999999999',
	Endereco varchar(50)			not null,

	Primary key(ID),
	Foreign key (ID) references SuperMercado.Pessoa(ID),
)

-- IVA
create table SuperMercado.Iva(
	Codigo	int						not null,
	Imposto	int						not null,

	Primary key (Codigo),
	Unique(Imposto)
)

-- MARCA
create table SuperMercado.Marca(
	Nome	varchar(30)				not null,
	Compra	float						not null DEFAULT 0,
	Venda	float						not null DEFAULT 0,
	Ganho	float						not null DEFAULT 0,

	Primary key(Nome)
)

-- TIPO
create table SuperMercado.Tipo(
	Nome varchar(30)				not null
	
    Primary key(Nome)
)

-- PRODUTO
create table SuperMercado.Produto(
	Codigo	int						not null Identity(1,1),
	IvaCodigo int					not null,
	Nome	varchar(30)				not null,
	Preco	float					not null  DEFAULT 0,
	Descricao text					not null,
	Disponivel int					not null DEFAULT 0,
	Armazem int						not null,
	Marca	varchar(30)				not null,
	Tipo	varchar(30)				not null,

	Primary key(Codigo),
	Foreign key (IvaCodigo) references SuperMercado.IVA(Codigo),
	Foreign key (Armazem) references SuperMercado.Armazem(ID),
	Foreign key (Marca) references SuperMercado.Marca(Nome),
	Foreign key (Tipo) references SuperMercado.Tipo(Nome)
)

-- ARMAZENA
create table SuperMercado.Armazena(
	Armazem	int						not null,
	Produto int						not null,
	Quantidade int					not null check(Quantidade>=0),

	Primary key(Armazem,Produto),
	Foreign key (Armazem) references SuperMercado.Armazem(ID),
	Foreign key (Produto) references SuperMercado.Produto(Codigo)
)

-- FORNECEDOR
create table SuperMercado.Fornecedor(
	Fax char(10)						not null,
	Email varchar(30)				not null,
	Nome varchar(30)				not null,
	NIF char(9)						not null,
	Tipo varchar(30)				not null,

	Primary key(Nome),
	Unique(Email),
	Unique(NIF),
	Foreign Key (Tipo) references SuperMercado.Tipo(Nome)
)

-- FORNECE
create table SuperMercado.Fornece(
	Produto	int						not null,
	Fornecedor	varchar(30)			not null,
	PrecoCompra float				not null,
	Quantidade	int					not null,

	Foreign key (Fornecedor) references SuperMercado.Fornecedor(Nome),
	Foreign key (Produto) references SuperMercado.Produto(Codigo)
)

-- TRANSPORTE
create table SuperMercado.Transporte(
	ID  int							not null Identity(1,1),
	Matricula char(6)				not null,
	Marca varchar(20)				not null,
	Disponivel int					not null DEFAULT 1,
	Capacidade int					not null,

	Primary key(ID),
	Unique(Matricula)
)

-- CONDUZ
create table SuperMercado.Conduz(
	Condutor int					not null,
	Transporte int					not null,

	Foreign key (Condutor) references SuperMercado.Condutor(ID),
	Foreign key (Transporte) references SuperMercado.Transporte(ID)
)

-- ENCOMENDA
create table SuperMercado.Encomenda(
	ID int							not null Identity(1,1),
	Client int						not null,
	Funcionario int					not null,
	Condutor int 					not null DEFAULT -1,
	CondicoesPagamento varchar(30)	not null,
	Destino varchar(50)				not null,
	CustoTotal float				not null DEFAULT 0,
	Estado int						not null DEFAULT 0,
	Pago 	int  					not null DEFAULT 0,
	Data date						not null,

	Primary key (ID),
	Foreign key (Client) references SuperMercado.Cliente(ID),
	Foreign key (Funcionario) references SuperMercado.Funcionario(ID)
)

-- FATURA
create table SuperMercado.Fatura(
	ID int							not null Identity(1,1),
	Encomenda int					not null,
	Client int						not null,
	Funcionario int					not null,
	CondicoesPagamento varchar(30)  not null,
	CustoTotal int					not null,
	Data date						not null,

	Primary key (ID),
	Foreign key (Encomenda) references SuperMercado.Encomenda(ID),
	Foreign key (Client) references SuperMercado.Cliente(ID),
	Foreign key (Funcionario) references SuperMercado.Funcionario(ID)
)

-- PAGAMENTO
create table SuperMercado.Pagamento(
	ID int							not null Identity(1,1), 
	Cliente int					not null,
	Fatura int					not null,
	CondicoesPagamento varchar(30)  not null,
	CustoTotal int					not null,
	Data date						not null,

	Primary key (ID),
	Foreign key (Cliente) references SuperMercado.Cliente(ID),
	Foreign key (Fatura) references SuperMercado.Fatura(ID)
)

-- CONTEM
create table SuperMercado.Contem(
	Quantidade int					not null,
	Encomenda	int					not null,
	ProdutoCodigo int				not null,
	Preco 		float				not null,

	Foreign key (Encomenda) references SuperMercado.Encomenda(ID),
	Foreign key (ProdutoCodigo) references SuperMercado.Produto(Codigo)
)