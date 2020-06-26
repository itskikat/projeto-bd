USE p2g7;

--------------------------------------------------------------------------------

CREATE Trigger checkInput ON SuperMercado.Cliente
INSTEAD OF UPDATE
AS
	BEGIN
		IF (SELECT count(*) FROM inserted) = 1
			BEGIN
				DECLARE @NIF as char(9);

				SELECT @NIF = NIF FROM inserted;

				IF len(@NIF)!=9
					RAISERROR('O NIF tem de ser composto por 9 digitos!',16,1);
				ELSE IF PATINDEX('%[^0-9]%', @NIF) !=0
					RAISERROR('O NIF tem de ser composto por 9 digitos!',16,1);
				ELSE
					UPDATE SuperMercado.Cliente SET NIF= @NIF WHERE ID IN (SELECT ID FROM inserted)
			END
	END
GO
ALTER TABLE [SuperMercado].[Cliente] ENABLE TRIGGER [checkInput]
GO

------------------------------------------------------


CREATE Trigger checkEmailTelefone ON SuperMercado.Pessoa
INSTEAD OF INSERT
AS
	BEGIN
		IF (SELECT count(*) FROM inserted) = 1
			BEGIN
				DECLARE @Email as varchar(40);
				DECLARE @Telefone as char(9);
				SELECT @Email = Email,@Telefone=Telefone FROM inserted WHERE Email LIKE '%_@_%_.__%'
				
				IF @Email is null
					BEGIN
						RAISERROR('Email invalido!',16,1)
						RETURN
					END

				IF len(@Telefone)!=9
					BEGIN
						RAISERROR('Numero telefonico invalido!',16,1)
						RETURN
					END

				IF PATINDEX('%[^0-9]%', @Telefone)!=0
					BEGIN
						RAISERROR('Numero telefonico invalido!',16,1)
						RETURN
					END

				INSERT INTO SuperMercado.Pessoa (Nome,Email,Telefone,Idade,Salario,Genero) SELECT Nome,Email,Telefone,Idade,Salario,Genero FROM inserted
			END
	END
GO
ALTER TABLE [SuperMercado].[Pessoa] ENABLE TRIGGER [checkEmailTelefone]
GO

-----------------------------------------

CREATE Trigger checkNifFornecedor ON SuperMercado.Fornecedor
INSTEAD OF INSERT
AS
	BEGIN
		IF (SELECT count(*) FROM inserted) = 1
			BEGIN
				DECLARE @NIF as char(9);
				DECLARE @Fax as char(10);

				SELECT @NIF = NIF, @Fax=Fax FROM inserted;

				
				IF len(@Fax)!=10
					BEGIN
						RAISERROR('O Fax tem de ser composto por 10 digitos!',16,1);
						RETURN
					END
				ELSE IF PATINDEX('%[^0-9]%', @Fax) !=0
					BEGIN
						RAISERROR('O Fax tem de ser composto por 10 digitos!',16,1);
						RETURN
					END
				
				IF len(@NIF)!=9
					BEGIN
						RAISERROR('O NIF tem de ser composto por 9 digitos!',16,1);
						RETURN
					END
				ELSE IF PATINDEX('%[^0-9]%', @NIF) !=0
					BEGIN
						RAISERROR('O NIF tem de ser composto por 9 digitos!',16,1);
						RETURN
					END

				INSERT INTO SuperMercado.Fornecedor SELECT * FROM inserted
			END
	END
GO
ALTER TABLE [SuperMercado].[Fornecedor] ENABLE TRIGGER [checkNifFornecedor]
GO