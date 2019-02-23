USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[P_TOTAL_ICMS_IPI_AGRUPADO_CFOP]    Script Date: 22/07/2017 01:42:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_TOTAL_ICMS_IPI_AGRUPADO_CFOP] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		NFI.Cfop, 
		SUM(NFI.BaseIcms), 
		SUM(NFI.ValorIcms),
		SUM(NFI.BaseCalculoIpi),
		SUM(NFI.ValorIpi)
	FROM
		NotaFiscalItem NFI
	GROUP BY NFI.Cfop
END

GO


