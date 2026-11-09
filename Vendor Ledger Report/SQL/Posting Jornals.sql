
USE BNBDAT;
GO

CREATE OR ALTER VIEW dbo.ET_GL_PostingJournal AS
SELECT
    b.BATCHID AS BatchNo,

    CASE b.BATCHTYPE
        WHEN 1 THEN 'Entered'
        WHEN 2 THEN 'Imported'
        WHEN 3 THEN 'Generated'
        ELSE CAST(b.BATCHTYPE AS varchar(5))
    END AS BatchType,

    CASE b.BATCHSTAT
        WHEN 1 THEN 'Open'
        WHEN 2 THEN 'Ready to Post'
        WHEN 3 THEN 'Printed'
        WHEN 4 THEN 'Posted'
        WHEN 5 THEN 'Deleted'
        ELSE CAST(b.BATCHSTAT AS varchar(5))
    END AS BatchStatus,

    b.ENTRYCNT AS NoOfEntries,
    b.ERRORCNT AS NoOfErrors,
    b.BTCHDESC AS BatchDescription,
    RTRIM(b.AUDTUSER) AS BatchEnteredBy,
    b.DEBITTOT AS BatchTotal,
    b.POSTNGSEQ AS PostingSeqNo,

    TRY_CONVERT(date,
        CAST(CAST(h.DOCDATE AS bigint) AS varchar(8)), 112
    ) AS DocDate,

    CAST(CAST(b.BATCHID AS int) AS varchar(10))
        + '-' +
    CAST(CAST(h.BTCHENTRY AS int) AS varchar(10)) AS BatchEntry,

    h.BTCHENTRY AS EntryNo,
    
    -- Ensure Source Ledger and Source Code are cleanly trimmed
    ISNULL(RTRIM(h.SRCELEDGER), '') AS SrceLedger,
    ISNULL(RTRIM(h.SRCETYPE), '') AS SrceType,
    RTRIM(h.SRCELEDGER) + '-' + RTRIM(h.SRCETYPE) AS SourceCode,
    
    h.JRNLDESC AS EntryDescription,
    RTRIM(h.ENTEREDBY) AS EntryEnteredBy,
    h.JRNLDR AS EntryTotal,

    CAST(d.TRANSNBR AS int) AS LineNumber,
    RTRIM(d.ACCTID) AS AccountIDRaw,
    RTRIM(a.ACCTFMTTD) AS AccountID,
    RTRIM(a.ACCTDESC) AS AccountDescription,

    CASE WHEN d.TRANSAMT > 0
         THEN d.TRANSAMT ELSE 0 END AS Debits,

    CASE WHEN d.TRANSAMT < 0
         THEN -d.TRANSAMT ELSE 0 END AS Credits,

    RTRIM(d.TRANSDESC) AS LineDescription,
    RTRIM(d.TRANSREF) AS LineReference,

    TRY_CONVERT(date,
        CAST(CAST(d.TRANSDATE AS bigint) AS varchar(8)), 112
    ) AS TransDate

FROM dbo.GLBCTL b
INNER JOIN dbo.GLJEH h
    ON h.BATCHID = b.BATCHID
INNER JOIN dbo.GLJED d
    ON d.BATCHNBR = h.BATCHID
   AND d.JOURNALID = h.BTCHENTRY
LEFT JOIN dbo.GLAMF a
    ON a.ACCTID = d.ACCTID;
GO

-- Check NoOfErrors for Batch 27
SELECT *
FROM dbo.ET_GL_PostingJournal
WHERE PostingSeqNo=3785;
GO
