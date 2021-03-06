USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[P_NOTA_FISCAL_ITEM]    Script Date: 22/07/2017 11:48:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[P_NOTA_FISCAL_ITEM]
(
	@pId int,
    @pIdNotaFiscal int,
    @pCfop varchar(5),
    @pTipoIcms varchar(20),
    @pBaseIcms decimal(18,5),
    @pAliquotaIcms decimal(18,5),
    @pValorIcms decimal(18,5),
    @pNomeProduto varchar(50),
    @pCodigoProduto varchar(20),
	@pBaseCalculoIpi decimal(18,5),
	@pAliquotaIpi decimal(18,5),
	@pValorIpi decimal(18,5),
	@pDesconto decimal(18,5)
)
AS
BEGIN
	IF (@pId = 0)
	BEGIN 		
		INSERT INTO [dbo].[NotaFiscalItem]
           ([IdNotaFiscal]
           ,[Cfop]
           ,[TipoIcms]
           ,[BaseIcms]
           ,[AliquotaIcms]
           ,[ValorIcms]
           ,[NomeProduto]
           ,[CodigoProduto]
		   ,[BaseCalculoIpi]
		   ,[AliquotaIpi]
		   ,[ValorIpi]
		   ,[Desconto])
		VALUES
           (@pIdNotaFiscal,
			@pCfop,
			@pTipoIcms,
			@pBaseIcms,
			@pAliquotaIcms,
			@pValorIcms,
			@pNomeProduto,
			@pCodigoProduto,
			@pBaseCalculoIpi,
			@pAliquotaIpi,
			@pValorIpi,
			@pDesconto)

		SET @pId = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[NotaFiscalItem]
		SET [IdNotaFiscal] = @pIdNotaFiscal
			,[Cfop] = @pCfop
			,[TipoIcms] = @pTipoIcms
			,[BaseIcms] = @pBaseIcms
			,[AliquotaIcms] = @pAliquotaIcms
			,[ValorIcms] = @pValorIcms
			,[NomeProduto] = @pNomeProduto
			,[CodigoProduto] = @pCodigoProduto
			,[BaseCalculoIpi] = @pBaseCalculoIpi
			,[AliquotaIpi] = @pAliquotaIpi
			,[ValorIpi] = @pValorIpi
			,[Desconto] = @pDesconto
		 WHERE Id = @pId
	END	    
END


GO


