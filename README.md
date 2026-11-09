# 📊 Posted Journal Entries Reporting Application

An enterprise-grade **Desktop Reporting & Financial Analytics Application** built with **VB.NET (WinForms)**, **SAP Crystal Reports**, and **Microsoft SQL Server (T-SQL)**. This solution integrates directly with ERP General Ledger database structures to generate clean, formatted, and customizable **Posted Journal Entry Reports** across multiple sub-ledgers and financial modules.

---

## 🖼️ Application Interface & System Screenshots

### 1. Main Parameter & Selection Control Panel (VB.NET GUI)
The application provides an intuitive Windows Forms interface allowing financial analysts to filter transactions by date ranges, batch IDs, posting sequence numbers, account ranges, and source modules.

![Desktop Application Control Form]<img width="1371" height="752" alt="Posting-Journal-Entries" src="https://github.com/user-attachments/assets/92708c39-3c78-4410-a9e8-2234927a47fd" />


### 2. Posted Journal Entries Report Outputs (SAP Crystal Reports)

* **Report Summary Header & Multi-Batch Overview:**
  ![Posted Journal Entries Overview]<img width="932" height="563" alt="Posting-Journal-Entries6" src="https://github.com/user-attachments/assets/19134f24-2fb6-498a-889e-85ada4b691a8" />


* **Batch & Posting Sequence Breakdown:**
  ![Posting Sequence View]<img width="935" height="560" alt="Posting-Journal-Entries4" src="https://github.com/user-attachments/assets/5c70604d-4f94-4413-849d-e90b047d71af" />


* **Detailed Account Ledger & Transaction Line Comments:**
  ![Detailed Transaction Entries]<img width="936" height="563" alt="Posting-Journal-Entries2" src="https://github.com/user-attachments/assets/133ee03f-0fe6-42db-b05e-1da48c929efe" />


* **Sub-ledger Entries & Source Code Analysis:**
  ![Detailed Line Distribution]<img width="932" height="546" alt="Posting-Journal-Entries5" src="https://github.com/user-attachments/assets/d1c6c572-be19-4375-a2b2-141e70d8c6fc" />

* **Totals & Audit Summary Execution:**
  ![Report Audit Summary]<img width="935" height="553" alt="Posting-Journal-Entries3" src="https://github.com/user-attachments/assets/b454c54f-d465-4d2b-9e6c-42823af2925a" />


---

## 🌟 Key Capabilities & Features

- **Multi-Module Source Integration**: Consolidates financial postings from sub-ledgers:
  - **AP** (Accounts Payable)
  - **AR** (Accounts Receivable)
  - **BK** (Bank / Cash Management)
  - **GL** (General Ledger)
  - **IC** (Inventory Control)
  - **OE** (Order Entry)
  - **PO** (Purchase Orders)
- **Advanced Dynamic Filtering**: Filter transactions seamlessly by:
  - **Date Range** (`From Date` to `To Date`)
  - **Batch Number Range** (`From Batch` to `To Batch`)
  - **Posting Sequence Number** (`From P. Seq. No.` to `To P. Seq. No.`)
  - **Account ID Selection**
  - **Source Modules**
- **Optimized SQL Backend Architecture**: Uses a high-performance database view (`dbo.ET_GL_PostingJournal`) with `TRY_CONVERT` date sanitization and explicit column mappings for fast report generation.
- **Audit-Ready Financial Reporting**: Displays full audit trails including batch status, batch type, entry creators (`ENTEREDBY` / `AUDTUSER`), line comments (`JECOMMENT`), and reference notes.

---

## 🛠️ Technology Stack

| Component | Technology / Library |
| :--- | :--- |
| **Language** | Visual Basic .NET (VB.NET) |
| **Framework** | .NET Framework 4.8 |
| **UI Environment** | Windows Forms (WinForms) |
| **Reporting Engine** | SAP Crystal Reports (`CrystalDecisions.CrystalReports.Engine`) |
| **Database Server** | Microsoft SQL Server (T-SQL Views & Joins) |
| **IDE** | Visual Studio 2022 |

---

## 🗄️ Database View Schema (`dbo.ET_GL_PostingJournal`)

The core analytical data is exposed through the optimized SQL view shown below. It handles table joins across batch control (`GLBCTL`), journal headers (`GLJEH`), journal details (`GLJED`), account master (`GLAMF`), and journal entry comments (`GLJEC`).

```sql
USE BNBDAT;
GO

CREATE OR ALTER VIEW dbo.ET_GL_PostingJournal AS
SELECT
    b.BATCHID AS BatchNo,

    -- Translate Batch Type
    CASE b.BATCHTYPE
        WHEN 1 THEN 'Entered'
        WHEN 2 THEN 'Imported'
        WHEN 3 THEN 'Generated'
        ELSE CAST(b.BATCHTYPE AS varchar(5))
    END AS BatchType,

    -- Translate Batch Status
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

    -- Safe Date Conversion (YYYYMMDD to Date)
    TRY_CONVERT(date, CAST(CAST(h.DOCDATE AS bigint) AS varchar(8)), 112) AS DocDate,

    -- Formatted Batch-Entry Key
    CAST(CAST(b.BATCHID AS int) AS varchar(10)) + '-' + CAST(CAST(h.BTCHENTRY AS int) AS varchar(10)) AS BatchEntry,

    h.BTCHENTRY AS EntryNo,
    
    -- Source Ledger and Source Code
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

    -- Debit / Credit Split
    CASE WHEN d.TRANSAMT > 0 THEN d.TRANSAMT ELSE 0 END AS Debits,
    CASE WHEN d.TRANSAMT < 0 THEN -d.TRANSAMT ELSE 0 END AS Credits,

    RTRIM(d.TRANSDESC) AS LineDescription,
    RTRIM(d.TRANSREF) AS LineReference,

    -- Journal Entry Line Comment
    RTRIM(c.JECOMMENT) AS LineComment,

    TRY_CONVERT(date, CAST(CAST(d.TRANSDATE AS bigint) AS varchar(8)), 112) AS TransDate

FROM dbo.GLBCTL b
INNER JOIN dbo.GLJEH h ON h.BATCHID = b.BATCHID
INNER JOIN dbo.GLJED d ON d.BATCHNBR = h.BATCHID AND d.JOURNALID = h.BTCHENTRY
LEFT JOIN dbo.GLAMF a  ON a.ACCTID = d.ACCTID
LEFT JOIN dbo.GLJEC c  ON c.BATCHNBR = d.BATCHNBR AND c.JOURNALID = d.JOURNALID AND c.TRANSNBR = d.TRANSNBR;
GO
```

### Verification Query Example
```sql
-- Check Posting Sequence for specific account validation
SELECT * 
FROM dbo.ET_GL_PostingJournal 
WHERE PostingSeqNo = 7 AND AccountIDRaw = '1144011000000020';
GO
```

---

## 📁 Repository Project Structure

```text
Posting-Journal-Entries/
├── Posting Journals Reports.sln       # Visual Studio Solution File
├── README.md                           # Documentation
└── Vendor Ledger Report/
    ├── App.config                      # Application Configurations & Connection Strings
    ├── Form1.vb                        # Main Parameter Selection UI Code
    ├── Form1.Designer.vb               # WinForms UI Designer Code
    ├── frmReportViewer.vb              # Crystal Reports Viewer Control Form
    ├── Reports/
    │   └── GL_Posting.rpt              # SAP Crystal Reports Layout File
    └── SQL/
        ├── Company INFO.sql            # Database Company Information Script
        ├── Posting Jornals.sql         # View Creation Script (dbo.ET_GL_PostingJournal)
        └── Posting_Journals_with_comment_03nov.sql
```

---

## ⚙️ How to Setup and Run

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/MdAshikurRahmanShuvo200/Posting-Journal-Entries.git
   cd Posting-Journal-Entries
   ```

2. **Execute Database Script**:
   * Open Microsoft SQL Server Management Studio (SSMS).
   * Connect to your target database server (e.g., `BNBDAT`).
   * Execute the T-SQL script found under `Vendor Ledger Report/SQL/Posting Jornals.sql`.

3. **Open Project**:
   * Launch Visual Studio 2022.
   * Open `Posting Journals Reports.sln`.

4. **Prerequisites & Assemblies**:
   * Ensure **SAP Crystal Reports runtime for .NET** is installed.
   * Verify SQL Server connection strings inside `App.config`.

5. **Build & Execute**:
   * Press `F5` or click **Start** to run the application.

---

## 👨‍💻 Author
**Md. Ashikur Rahman Shuvo**  
*Data Analyst & Software Solutions Developer*  
- GitHub: [@MdAshikurRahmanShuvo200](https://github.com/MdAshikurRahmanShuvo200)
