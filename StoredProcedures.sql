USE p2g7;

--------------------------------------------------------------------------------

CREATE PROC SuperMercado.createNewPessoa (@Nome varchar(40),@Email varchar(40),@Telefone char(9),
							@Genero varchar(1), @Idade int,@Salario float)
AS
	INSERT INTO SuperMercado.Pessoa (Nome, Email, Telefone, Genero, Idade,Salario) VALUES (@Nome,@Email,@Telefone,@Genero,@Idade,@Salario);
GO

--------------------------------------------------------------------------------

Create PROC SuperMercado.entregarEncomenda (@Encomenda int,@Condutor int)
AS
	UPDATE SuperMercado.Encomenda SET Estado=1, Condutor=@Condutor WHERE ID=@Encomenda;
GO

---------------------------------------------------------------------------------

Create Proc SuperMercado.createNewUser (@Nome varchar(40),@Email varchar(40),@Telefone char(9),
							@Genero varchar(1), @Idade int,@Salario float,@Username varchar(40),@Senha varchar(40))
AS	
	BEGIN
		DECLARE @count int;
		DECLARE @erro varchar(400);
		SET @count = (SELECT SuperMercado.checkIfEmailUsed (@Email))
		IF(@count>=1)
			RAISERROR ('O Email ja foi usado!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					BEGIN TRAN
							DECLARE @InsertedID int;
							EXEC SuperMercado.createNewPessoa @Nome,@Email,@Telefone,@Genero,@Idade,@Salario ;
							SELECT @InsertedID=@@IDENTITY 
							INSERT INTO SuperMercado.Cliente (ID,Endereco) VALUES(@InsertedID,'')
							EXEC SuperMercado.createUtilizador @Username,@Senha,@InsertedID;
							COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro=ERROR_MESSAGE(); 
					SET @erro+=CHAR(13)+CHAR(10)+'A conta nao foi criada!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	End
GO

-----------------------------------------------------------------------------------------
Create Proc SuperMercado.createAdmin (@Nome varchar(40),@Email varchar(40),@Telefone char(9),
							@Genero varchar(1), @Idade int,@Salario float,@Username varchar(40),@Senha varchar(40))
AS	
	BEGIN
		DECLARE @count int;
		DECLARE @erro varchar(400)
		SET @count = (SELECT SuperMercado.checkIfEmailUsed (@Email))
		IF(@count>=1)
			RAISERROR ('O Email ja foi usado!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					BEGIN TRAN
							DECLARE @InsertedID int;
							EXEC SuperMercado.createNewPessoa @Nome,@Email,@Telefone,@Genero,@Idade,@Salario ;
							SELECT @InsertedID=@@IDENTITY 
							EXEC SuperMercado.createUtilizador @Username,@Senha,@InsertedID;
							INSERT INTO SuperMercado.GerenteArmazem (ID) VALUES(@InsertedID)
							COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro=ERROR_MESSAGE(); 
					SET @erro+=CHAR(13)+CHAR(10)+'A conta nao foi criada!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	End
GO

-----------------------------------------------------------------------------------

Create Proc SuperMercado.createNewFuncionario (@Nome varchar(40),@Email varchar(40),@Telefone char(9),
							@Genero varchar(1), @Idade int,@Salario float)
AS	
	BEGIN
		DECLARE @count int;
		DECLARE @erro varchar(400)
		SET @count = (SELECT SuperMercado.checkIfEmailUsed (@Email))
		IF(@count>1)
			RAISERROR ('O Email ja foi usado!', 16,1);
		ELSE
			BEGIN 
				BEGIN TRY
					BEGIN TRAN
						DECLARE @InsertedID int;
						EXEC SuperMercado.createNewPessoa @Nome,@Email,@Telefone,@Genero,@Idade,@Salario ;
						SELECT @InsertedID=@@IDENTITY 
						INSERT INTO SuperMercado.Funcionario VALUES (@InsertedID)
						COMMIT TRAN
				END TRY
				BEGIN CATCH
					ROLLBACK TRAN
					SELECT @erro=ERROR_MESSAGE(); 
					SET @erro+=CHAR(13)+CHAR(10)+'O Funcionario nao foi adicionado!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	End
GO

-----------------------------------------------------------------------------------

Create Proc SuperMercado.createNewCondutor (@Nome varchar(40), @Email varchar(40),@Telefone char(9),
							@Genero varchar(1), @Idade int,@Salario float,@Carta char(9))
AS	
	BEGIN
		DECLARE @count int;
		SET @count = (SELECT SuperMercado.checkIfEmailUsed (@Email))
		IF(@count>1)
			RAISERROR ('O Email ja foi usado!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					BEGIN TRAN
						DECLARE @InsertedID int;
						EXEC SuperMercado.createNewPessoa @Nome,@Email,@Telefone,@Genero,@Idade,@Salario;
						SELECT @InsertedID=@@IDENTITY 
						INSERT INTO SuperMercado.Condutor (ID, CartaoConducao) VALUES (@InsertedID,@Carta)
						COMMIT TRAN
				END TRY
				BEGIN CATCH
					ROLLBACK TRAN
					RAISERROR ('O Condutor nao foi adicionado!', 16,1);
				END CATCH
			END
	End
GO

-----------------------------------------------------------------------------------


CREATE PROC SuperMercado.createNewFatura @Encomenda int, @Cliente int, @Funcionario int,
							@CondicoesPagamento varchar(30), @CustoTotal int, @Data date
	AS
		INSERT INTO SuperMercado.Fatura (Encomenda,Client,Funcionario,CondicoesPagamento,CustoTotal,Data) VALUES (@Encomenda,@Cliente,@Funcionario,@CondicoesPagamento,@CustoTotal,@Data)
GO

-----------------------------------------------------------------------------


CREATE PROC SuperMercado.setEncomendaProdutos (@Quantidade int, @Encomenda int, @Codigo int, @Preco float)
AS
	INSERT INTO SuperMercado.Contem VALUES (@Quantidade, @Encomenda, @Codigo, @Preco)
GO

-----------------------------------------------------------------------------


CREATE PROC SuperMercado.createNewFornecedor (@Fax char(10), @Email varchar(30), @Nome varchar(30), @NIF char(9), @Tipo varchar(30))
AS	
	BEGIN
		DECLARE @count int;
		SELECT @count=count(1) FROM SuperMercado.Fornecedor WHERE Email=@Email
		IF(@count>1)
			RAISERROR ('O Email ja foi usado!', 16,1);
		ELSE
			BEGIN
				INSERT INTO SuperMercado.Fornecedor (Fax,Email,Nome,NIF,Tipo) VALUES (@Fax,@Email,@Nome,@NIF,@Tipo)
			END
	END
GO

--------------------------------------------------------------------------------


CREATE PROC SuperMercado.getRandomCondutor
	AS
	BEGIN
		SELECT TOP 1 * FROM SuperMercado.Condutor 
		ORDER BY NEWID()
	END
GO

--------------------------------------------------------------------------------


CREATE PROC SuperMercado.updateArmazena (@Quantidade int, @Produto int)
	AS
	BEGIN
		UPDATE SuperMercado.Armazena 
		SET Quantidade = Quantidade-@Quantidade
		WHERE Produto = @Produto
	END
GO

---------------------------------------------------------------------------------

CREATE PROC SuperMercado.getRandomFuncionario 
	AS
	BEGIN
		SELECT TOP 1 * FROM SuperMercado.Funcionario 
		ORDER BY NEWID()
	END
GO

----------------------------------------------------------------------------------

CREATE PROC SuperMercado.updateFuncionario (@Nome varchar(40), @Telefone char(9), @Salario float, @ID int)
	AS
	BEGIN
		UPDATE SuperMercado.Pessoa 
		SET Nome = @Nome, Telefone = @Telefone, Salario = @Salario
		WHERE ID = @ID
	END
GO

------------------------------------------------------------------------------------

CREATE PROC SuperMercado.fornecerProduto(@ProdutoID int,@Fornecedor varchar(50),@PrecoCompra float, @Quantidade int,@Marca varchar(50))
AS
	BEGIN
		BEGIN TRY
			BEGIN TRAN
				DECLARE @Custo float
				SET @Custo = @Quantidade*@PrecoCompra
				INSERT INTO SuperMercado.Fornece VALUES(@ProdutoID, @Fornecedor,@PrecoCompra,@Quantidade)
				UPDATE SuperMercado.Marca SET COMPRA=COMPRA+@Custo WHERE Nome=@Marca
				UPDATE SuperMercado.Armazena SET Quantidade=Quantidade+@Quantidade WHERE Produto=@ProdutoID
				COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			RAISERROR ('ERROR!', 16,1);
		END CATCH
	END
GO
	
------------------------------------------------------------------------------------------
CREATE PROC SuperMercado.payment (@Encomenda int)
As
	BEGIN
		BEGIN TRY
			BEGIN TRAN
				DECLARE @Quantidade int, @PrecoVenda float, @PrecoCompra float ,@Produto int, @Marca varchar(40)
				DECLARE C CURSOR FAST_FORWARD
				FOR SELECT Quantidade,Preco,ProdutoCodigo FROM SuperMercado.Contem WHERE Encomenda=@Encomenda; 

				OPEN C

				FETCH C INTO @Quantidade,@PrecoVenda,@Produto

				WHILE @@FETCH_STATUS = 0
					BEGIN
						SELECT @Marca=Marca FROM SuperMercado.Produto WHERE Codigo=@Produto
						SELECT @PrecoCompra=PrecoCompra FROM SuperMercado.Fornece WHERE Produto=@Produto
					
						UPDATE SuperMercado.Marca SET Venda=Venda+(@Quantidade*@PrecoVenda), Ganho=Ganho+((@PrecoVenda-@PrecoCompra)*@Quantidade)
						WHERE Nome=@Marca

						FETCH C INTO @Quantidade,@PrecoVenda,@Produto
					END
				CLOSE C
				DEALLOCATE C

				UPDATE SuperMercado.Encomenda SET Pago=1 WHERE ID = @Encomenda
				COMMIT
					
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			RAISERROR ('O Pagamento nao foi realizado!Tente mais tarde!', 16,1);
		END CATCH
	END
GO

----------------------------------------------------------------------------------

CREATE PROC SuperMercado.UpdateClient(@ID int, @NIF char(9)) 
	AS
	BEGIN
		UPDATE SuperMercado.Cliente SET NIF=@NIF WHERE ID=@ID
	END
GO

-----------------------------------------------------------------------------------

CREATE PROC SuperMercado.createUtilizador(@Username varchar(40), @Senha varchar(40), @InsertedID int)
AS 
	BEGIN
		DECLARE @Pwd varbinary(20)
		SET @Pwd = HASHBYTES('SHA1', @Senha)
		INSERT INTO SuperMercado.Utilizador VALUES (@Username,@Pwd,@InsertedID)
	END
GO