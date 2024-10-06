SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VCO_TEST](
	[journal_type] [varchar](10) NOT NULL,
	[category] [varchar](10) NOT NULL,
	[name_fr] [varchar](150) NULL,
	[name_en] [varchar](150) NULL,
	[name_nl] [varchar](150) NULL,
	[iduser] [int] NULL,
	[idprofil] [int] NULL,
) 
GO