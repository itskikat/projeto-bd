USE p2g7;

-------------------------------------------------------

CREATE FUNCTION SuperMercado.getAllTransporte() RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=count(1) FROM SuperMercado.Transporte
		RETURN @res
	END
GO

-------------------------------------------------------

CREATE FUNCTION SuperMercado.getAllFornecedores() RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=count(1) FROM SuperMercado.Fornecedor
		RETURN @res
	END
GO

-------------------------------------------------------

CREATE FUNCTION SuperMercado.getAllCondutor() RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=count(1) FROM SuperMercado.Pessoa AS Pessoa 
		JOIN SuperMercado.Condutor AS CONDUTOR on Pessoa.ID = Condutor.ID
		WHERE Salario != -1
		RETURN @res
	END
GO

------------------------------------------------------

CREATE FUNCTION SuperMercado.checkStocked (@Produto int) RETURNS TABLE
AS
	RETURN (SELECT Quantidade FROM SuperMercado.Armazena
		WHERE Produto = @Produto)
GO

-----------------------------------------------------

CREATE FUNCTION SuperMercado.getFuncionarioByID (@ID int) RETURNS TABLE
AS
	RETURN	(SELECT PESSOA.ID,Nome,Email,Telefone,Idade,Salario,Genero FROM SuperMercado.Pessoa AS PESSOA 
	JOIN (SELECT * FROM SuperMercado.Funcionario WHERE ID= @ID) AS Funcionario ON Pessoa.ID = Funcionario.ID)
GO

---------------------------------------------------

CREATE FUNCTION SuperMercado.getAllFuncionario() RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=COUNT(1) FROM SuperMercado.Pessoa AS Pessoa 
		JOIN SuperMercado.Funcionario AS Funcionario ON Pessoa.ID = Funcionario.ID
		WHERE Salario != -1
		RETURN @res
	END
GO

--------------------------------------------------------

CREATE FUNCTION SuperMercado.getCountFaturas (@ID int) RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=COUNT(1) FROM SuperMercado.Fatura 
		WHERE Client = @ID
		RETURN @res
	END
GO

-------------------------------------------------------

CREATE FUNCTION SuperMercado.getCountEncomendas (@ID int) RETURNS int
AS
	BEGIN
		DECLARE @countEnc int
		IF (@ID != -1)
				SELECT @countEnc=COUNT(1) FROM SuperMercado.Encomenda
				WHERE Client = @ID
		ELSE
				SELECT @countEnc=COUNT(1) FROM SuperMercado.Encomenda
		RETURN @countEnc
	END
GO


-----------------------------------------------------

CREATE FUNCTION SuperMercado.getClientByID (@ID int) RETURNS TABLE
AS 
	RETURN	(SELECT Pessoa.ID,Nome,Email,Telefone,Idade,Salario,Genero,NIF,Endereco FROM SuperMercado.Pessoa AS Pessoa
		INNER JOIN (SELECT * FROM SuperMercado.Cliente WHERE ID = @ID) AS Cliente ON Pessoa.ID = Cliente.ID)
GO

-------------------------------------------------------------

CREATE FUNCTION SuperMercado.getPessoaByEmail (@Email varchar(30)) RETURNS TABLE
AS
	RETURN	(SELECT * FROM SuperMercado.Pessoa
		WHERE Email = @Email)
GO

----------------------------------------------------------------

CREATE FUNCTION SuperMercado.checkIfUserExists (@Email varchar(30), @Senha varchar(40)) RETURNS @table Table (ID int, Name varchar(50))
AS
	BEGIN
		DECLARE @ID int
		Set @ID =0
		SELECT @ID = ID FROM SuperMercado.Pessoa WHERE Email = @Email;
		IF (@ID = 0)
			INSERT @table SELECT @ID,'unkown'
		ELSE
			 INSERT @table SELECT ID,Nome FROM SuperMercado.Utilizador WHERE ID = @ID AND Senha = HASHBYTES('SHA1', @Senha)
		RETURN;
	END
GO

----------------------------------------------------------------

CREATE FUNCTION SuperMercado.checkIfEmailUsed (@Email varchar(40)) RETURNS int
AS
	BEGIN
		DECLARE @count int
		SELECT @count=count(1) FROM SuperMercado.Pessoa WHERE Email=@Email
		RETURN @count
	END
GO

----------------------------------------------------------------

CREATE FUNCTION SuperMercado.initIvas() RETURNS TABLE
AS
	RETURN	SELECT * FROM SuperMercado.Iva
GO

------------------------------------------------------------------

CREATE FUNCTION SuperMercado.getCountProdutos () RETURNS int
AS
	BEGIN
		DECLARE @res int 
		SELECT @res=COUNT(1) FROM SuperMercado.Produto 
		RETURN @res
	END
GO

----------------------------------------------------------------

CREATE FUNCTION SuperMercado.getProdutoByCodigo (@Codigo int) RETURNS TABLE
AS 
	RETURN	(SELECT * FROM SuperMercado.Produto WHERE Codigo = @Codigo)
GO

------------------------------------------------------------------


CREATE FUNCTION SuperMercado.searchProdutoAndGetQuantity (@Size int = 10,@Marca varchar(40),@Min int,@Max int,@Name varchar(40),@Tipo varchar(40)) RETURNS TABLE
AS 
	RETURN (Select Top (@size) Totalcount,Quantidade,Codigo,IvaCodigo,Tipo,Nome,Descricao,Marca,result.Preco,Disponivel,result.Armazem From(
							Select COUNT(1) OVER() AS TotalCount,Codigo,IvaCodigo,Nome,Descricao,Tipo,Marca,Produto.Preco,Disponivel,Produto.Armazem From
							SuperMercado.Produto as Produto Where Marca like '%'+@Marca+'%' and Preco Between @Min and @Max and Nome like '%'+@Name+ '%' and Tipo like '%'+@Tipo+ '%') as result 
							join (Select Armazena.Produto, SUM(Quantidade) As Quantidade from SuperMercado.Armazena as Armazena group by Armazena.Produto) as Armazena on result.Codigo = Armazena.Produto)
GO

-------------------------------------------------------------------

Create FUNCTION SuperMercado.searchProduct (@Size int = 10,@StartLine int, @Marca varchar(40),@Min int,@Max int,@Name varchar(40),@Tipo varchar(40)) RETURNS TABLE
As
	RETURN	(Select Top (@size) Codigo,IvaCodigo,Nome,Descricao,Tipo,Marca,Quantidade,Produto.Preco,Disponivel,Produto.Armazem From
		                     SuperMercado.Produto as Produto join (Select Armazena.Produto, sum(quantidade) as quantidade From SuperMercado.Armazena as Armazena group by Armazena.Produto) as result
		                     on Produto.Codigo = result.Produto where Codigo not in (Select Top (@StartLine) Codigo from SuperMercado.Produto ) 
		                     and Marca like '%'+@Marca+'%' and Preco Between @Min and @Max and Nome like '%'+@Name+ '%' and Tipo like '%'+@Tipo+ '%')
GO

-----------------------------------------------------------------------

Create FUNCTION SuperMercado.getProdutoList (@Size int, @StartLine int) RETURNS TABLE
As
	RETURN (Select Top (@Size) Codigo,IvaCodigo,Nome,Descricao,Tipo,Marca,Quantidade,Produto.Preco,Disponivel,Produto.Armazem From
		                     SuperMercado.Produto as Produto join (Select Armazena.Produto, sum(quantidade) as quantidade From SuperMercado.Armazena as Armazena group by Armazena.Produto) as result
		                     on Produto.Codigo = result.Produto where Codigo not in (Select Top (@StartLine) Codigo from SuperMercado.Produto ))
GO
-----------------------------------------------------------------------

Create FUNCTION SuperMercado.getProdutosByEncomendaId(@ID int) RETURNS TABLE
AS
	RETURN ( SELECT Codigo,IvaCodigo,Nome,Produto.Preco,Descricao,Marca,Disponivel,Quantidade From SuperMercado.Produto as Produto
				join SuperMercado.Contem on Codigo=ProdutoCodigo Where Encomenda = @ID)
GO
-------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getEncomendaOfClient (@Size int, @StartLine int, @ClientID int) 
RETURNS @table TABLE (ID int, Client int, Funcionario int,
Condutor int, CondicoesPagamento varchar(50),Destino varchar(50),CustoTotal float,Estado int,Pago int, Data date)
As
	Begin
		If(@ClientID=-1)
			INSERT @table Select Top (@Size) * from SuperMercado.Encomenda where ID not in (Select Top (@StartLine) ID from SuperMercado.Encomenda)
		Else
			INSERT @table Select Top (@Size) * from SuperMercado.Encomenda where Client=@ClientID and ID not in 
			(Select Top (@StartLine) ID from SuperMercado.Encomenda where Client=@ClientID)
		RETURN;
	End
GO

--------------------------------------------------------------------------

Create FUNCTION SuperMercado.getEncomandasByEstadoAndGetQuantity (@Size int,@Estado int, @ClientID int)
	RETURNS @table TABLE (TotalCount int, ID int, Client int, Funcionario int, CondicoesPagamento varchar(50),Destino varchar(50),CustoTotal float,Estado int, Data date,Pago int)
AS
	BEGIN
		If(@ClientID=-1)
			INSERT @table Select Top (@Size) TotalCount,ID,Client, Funcionario,CondicoesPagamento,Destino,CustoTotal,Estado,Data,Pago From 
            (Select COUNT(1) OVER() AS TotalCount,ID,Client, Funcionario,CondicoesPagamento,Destino,CustoTotal,Estado,Data,Pago  From SuperMercado.Encomenda Where Estado=@Estado) as reulst
		Else
			INSERT @table Select Top (@Size) TotalCount,ID, Client,Funcionario,CondicoesPagamento,Destino,CustoTotal,Estado,Data,Pago  From 
                    (Select COUNT(1) OVER() AS TotalCount,ID,Client,Funcionario,CondicoesPagamento,Destino,CustoTotal,Estado,Data,Pago  From SuperMercado.Encomenda Where Estado=@Estado and Client=@ClientID) as reulst
		RETURN
	END
GO

------------------------------------------------------------------------

Create FUNCTION SuperMercado.getEncomandasByEstado (@Size int,@StartLine int, @Estado int, @ClientID int) 
RETURNS @table TABLE (ID int, Client int, Funcionario int, Condutor int, CondicoesPagamento varchar(50),
    Destino varchar(50),CustoTotal float,Estado int, Pago int, Data date)
As
	BEGIN
		If(@ClientID=-1)
			INSERT @table Select Top (@Size) * From SuperMercado.Encomenda Where Estado=@Estado and ID not in (Select TOP (@StartLine) ID from SuperMercado.Encomenda)
		Else
			INSERT @table Select Top (@Size) * From SuperMercado.Encomenda Where Estado=@Estado and Client=@ClientID and ID not in (Select TOP (@StartLine) ID from SuperMercado.Encomenda)
		RETURN
	END
GO

---------------------------------------------------------------------------

Create FUNCTION SuperMercado.getFaturaByPageAndGetQuantity (@Size int, @ClientID int) RETURNS TABLE
As
	RETURN Select Top (@Size) TotalCount,ID,Funcionario,Encomenda,CondicoesPagamento,CustoTotal,Data From 
       (Select COUNT(1) OVER() AS TotalCount,ID,Funcionario,CondicoesPagamento,Encomenda,CustoTotal,Data From SuperMercado.Fatura Where Client=@ClientID) as reulst
GO
	
-----------------------------------------------------------------------------

Create FUNCTION SuperMercado.getFaturaByPage (@Size int,@StartLine int, @ClientID int) RETURNS TABLE
As
	RETURN	Select Top (@Size) * From SuperMercado.Fatura Where Client=@ClientID and ID not in (select TOP (@StartLine) ID from SuperMercado.Fatura)
GO

-----------------------------------------------------------------------------

Create FUNCTION SuperMercado.getFaturaOfClient (@ClientID int) RETURNS TABLE
AS
	RETURN (SELECT * FROM SuperMercado.Fatura WHERE Client = @ClientID)
GO
------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getFuncionariosList (@Size int,@StartLine int) RETURNS TABLE
AS
	RETURN Select Top (@Size) Pessoa.ID,Nome,Email,Telefone,Idade,Salario,Genero From  SuperMercado.Pessoa as Pessoa
	join SuperMercado.Funcionario as Funcionario on Pessoa.ID = Funcionario.ID Where Salario!=-1 and 
    Pessoa.ID not in (Select Top (@StartLine) ID From SuperMercado.Funcionario)
GO

--------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchFuncionarioAndGetQuantity (@Size int,@Name varchar(50),@Email varchar(50),@Min int, @Max int) RETURNS TABLE
As
	RETURN Select Top (@Size) TotalCount,Funcionario.ID,Nome,Email,Telefone,Idade,Salario,Genero From (Select COUNT(1) OVER() AS TotalCount,* From SuperMercado.Pessoa 
    Where Salario Between @Min and @Max and Nome like '%'+@Name+'%' and Email like '%'+@Email+'%') as Result
	join SuperMercado.Funcionario as Funcionario on Result.ID = Funcionario.ID
GO

------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchFuncionario (@Size int,@StartLine int, @Name varchar(50),@Email varchar(50),@Min int, @Max int) RETURNS TABLE
As
	RETURN Select Top (@Size) Funcionario.ID,Nome,Email,Telefone,Idade,Salario,Genero From SuperMercado.Pessoa as Pessoa 
    join SuperMercado.Funcionario as Funcionario on Pessoa.ID = Funcionario.ID Where Salario Between @Min and @Max 
	and Nome like '%'+@Name+'%' and Email like '%'+@Email+'%' and Pessoa.ID not in (Select Top (@StartLine) ID From SuperMercado.Funcionario) 
GO

------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getCondutoresList (@Size int,@StartLine int) RETURNS TABLE
As
	RETURN Select Top (@Size) Pessoa.ID,Nome,Email,Telefone,Idade,Salario,Genero,Disponivel,CartaoConducao From  SuperMercado.Pessoa as Pessoa
    join SuperMercado.Condutor as Condutor on Pessoa.ID = Condutor.ID Where Salario!=-1 and 
	Pessoa.ID not in (Select Top (@StartLine)  ID From SuperMercado.Condutor)
GO

------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchCondutorAndGetQuantity (@Size int,@Name varchar(50),@Email varchar(50),@Min int, @Max int) RETURNS TABLE
As
	RETURN Select Top (@Size) TotalCount,Result.ID,Nome,Email,Telefone,Idade,Salario,Genero,Disponivel,CartaoConducao From (Select COUNT(1) OVER() AS TotalCount,* From SuperMercado.Pessoa 
    Where Salario Between @Min and @Max and Nome like '%'+@Name+'%' and Email like '%'+@Email+'%') as Result 
	join SuperMercado.Condutor as Condutor on Result.ID = Condutor.ID
GO

-----------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchCondutor (@Size int,@StartLine int, @Name varchar(50),@Email varchar(50),@Min int, @Max int) RETURNS TABLE
As
	RETURN Select Top (@Size) Pessoa.ID,Nome,Email,Telefone,Idade,Salario,Genero,Disponivel,CartaoConducao From SuperMercado.Pessoa as Pessoa 
	join SuperMercado.Condutor as Condutor on Pessoa.ID = Condutor.ID Where Salario Between @Min and @Max 
	and Nome like '%'+@Name+'%' and Email like '%'+@Email+'%' and Pessoa.ID not in (Select Top (@StartLine) ID From SuperMercado.Condutor) 
GO

------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getFornecedoresList (@Size int,@StartLine int) RETURNS TABLE
As
	RETURN SELECT Top (@Size) * FROM SuperMercado.Fornecedor Where NIF not in (Select Top (@StartLine) NIF From SuperMercado.Fornecedor)
GO

-----------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchFornecedorAndGetQuantity (@Size int,@Name varchar(50),@Email varchar(50),@Fax varchar(9), @NIF varchar(9)) RETURNS TABLE
As
	RETURN Select Top (@Size) * From (Select COUNT(1) OVER() AS TotalCount,* From SuperMercado.Fornecedor 
	Where Fax like '%'+@Fax+'%' and NIF like '%'+@NIF+'%' and Nome like '%'+@Name+'%' and Email like '%'+@Email+'%') as Result

GO

------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.searchFornecedor (@Size int, @StartLine int, @Name varchar(50),@Email varchar(50),@Fax varchar(9), @NIF varchar(9)) RETURNS TABLE
As
	RETURN Select Top (@Size) * From SuperMercado.Fornecedor 
	Where Fax like '%' + @Fax + '%' and NIF like '%' + @NIF + '%' and Nome like '%' + @Name + '%' and Email like '%' + @Email + '%'
	and NIF not in (Select Top (@StartLine) NIF From SuperMercado.Fornecedor) 
GO

--------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getTransportesList (@Size int,@StartLine int) RETURNS TABLE
As
	RETURN Select Top (@Size) * From  SuperMercado.Transporte as Transporte Where ID not in (Select Top (@StartLine)  ID From SuperMercado.Transporte)
GO

--------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getAllMarcaList () RETURNS TABLE
AS
	RETURN SELECT * FROM  SuperMercado.Marca
GO

--------------------------------------------------------------------------------------

CREATE FUNCTION SuperMercado.getAllMarca() RETURNS int
AS
	BEGIN
		DECLARE @res int
		SELECT @res=count(1) FROM SuperMercado.Marca
		RETURN @res
	END
GO

-------------------------------------------------------------------------------------------

CREATE Function SuperMercado.getMarcaList (@Size int,@StartLine int) RETURNS TABLE
AS
		RETURN SELECT TOP (@Size) * FROM  SuperMercado.Marca AS Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca)
GO

-------------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getAllTipoList () RETURNS TABLE
As
	RETURN SELECT * FROM  SuperMercado.Tipo
GO

---------------------------------------------------------------------------------------------

Create FUNCTION SuperMercado.getProdutosByFornecedorName (@Name varchar(50)) RETURNS @table TABLE (Codigo int,IvaCodigo int,Nome varchar(50),Descricao text,
Tipo varchar(30),Marca varchar(30),Preco float)
As
	BEGIN
		DECLARE @Tipo varchar(30)
		SELECT @Tipo=Tipo FROM SuperMercado.Fornecedor WHERE Nome=@Name
		INSERT @table SELECT Codigo,IvaCodigo,Nome,Descricao,Tipo,Marca,Preco FROM SuperMercado.Produto WHERE Tipo=@Tipo
		RETURN;
	END	
GO

----------------------------------------------------------------------------------------------

CREATE FUNCTION SuperMercado.getMarcaOrderByCompra(@Size int, @StartLine int,@Asc int) RETURNS @table TABLE (Nome varchar(30),Compra float,Venda float, Ganho float)
AS
	BEGIN
		IF @Asc=-1
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Compra) DESC) ORDER BY (Compra) DESC
		ELSE
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Compra)) ORDER BY (Compra)
		RETURN;
	END
GO
	
------------------------------------------------------------------------------------------------

CREATE FUNCTION SuperMercado.getMarcaOrderByVenda(@Size int, @StartLine int,@Desc int) RETURNS @table TABLE (Nome varchar(30),Compra float,Venda float, Ganho float)
AS
	BEGIN
		IF @Desc=-1
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Venda)) ORDER BY (Venda)
		ELSE
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Venda) DESC) ORDER BY (Venda) DESC
		RETURN;
	END
GO

----------------------------------------------------------------------------------------------------

CREATE FUNCTION SuperMercado.getMarcaOrderByGanho(@Size int, @StartLine int,@Asc int) RETURNS @table TABLE (Nome varchar(30),Compra float,Venda float, Ganho float)
AS
	BEGIN
		IF @Asc=-1
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Ganho) DESC) ORDER BY (Ganho) DESC
		ELSE
			INSERT @table SELECT TOP (@Size) * FROM SuperMercado.Marca WHERE Nome NOT IN (SELECT TOP (@StartLine) Nome FROM SuperMercado.Marca ORDER BY (Ganho)) ORDER BY (Ganho)
		RETURN;
	END
GO