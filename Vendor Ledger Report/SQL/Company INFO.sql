
USE BNBDAT;
GO

CREATE OR ALTER VIEW dbo.ET_GL_COMPANY_INFO
AS
SELECT TOP (1)
    comp.CONAME AS [Company Name],
    comp.LEGALNAME AS [Company Legal Name],

    CONCAT(
        LTRIM(RTRIM(comp.ADDR01)),
        CASE WHEN NULLIF(LTRIM(RTRIM(comp.ADDR02)), '') IS NOT NULL
             THEN ' ' + LTRIM(RTRIM(comp.ADDR02)) ELSE '' END,
        CASE WHEN NULLIF(LTRIM(RTRIM(comp.ADDR03)), '') IS NOT NULL
             THEN ' ' + LTRIM(RTRIM(comp.ADDR03)) ELSE '' END
    ) AS [Company Address]

FROM dbo.CSCOM AS comp;
GO

SELECT *
FROM dbo.ET_GL_COMPANY_INFO;
GO
