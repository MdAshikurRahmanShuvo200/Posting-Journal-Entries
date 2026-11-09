Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmReportViewer

    ' Form1 theke ashbe
    Public FromDate As Date
    Public ToDate As Date
    Public FromPostingSeq As Decimal
    Public ToPostingSeq As Decimal
    Public FromBatch As Decimal
    Public ToBatch As Decimal

    ' Source Module parameters from Form1
    Public ParamSourceModulesList As New List(Of String)()
    Public SourceType As String = String.Empty
    Public ReportFilterMode As String = "D" ' "D" for Date, "B" for Batch, "P" for Posting Seq

    ' Account filter (faka thakle filter hobe na)
    Public AccountID As String = String.Empty

    ' ---------------------------------------------------------------------
    ' Account jei jei Batch+Entry te ache, shegulor "BatchEntry" list ber kora
    ' (Batch-Entry format view-er BatchEntry column-er moto, jemon "27-3")
    ' ---------------------------------------------------------------------
    Private Function GetEntriesContainingAccount(ByVal accFormatted As String) As List(Of String)
        Dim result As New List(Of String)()

        Dim connStr As String = ConfigurationManager.ConnectionStrings("SageDBConn").ConnectionString

        ' GLJED + GLAMF theke, view na, tai druto
        Dim query As String =
            "SELECT DISTINCT " &
            "CAST(CAST(d.BATCHNBR AS int) AS varchar(10)) + '-' + CAST(CAST(d.JOURNALID AS int) AS varchar(10)) AS BatchEntry " &
            "FROM dbo.GLJED d " &
            "INNER JOIN dbo.GLAMF a ON a.ACCTID = d.ACCTID " &
            "WHERE RTRIM(a.ACCTFMTTD) = @acc"

        Using conn As New SqlConnection(connStr)
            Dim cmd As New SqlCommand(query, conn)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@acc", accFormatted)
            conn.Open()

            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    result.Add(reader("BatchEntry").ToString())
                End While
            End Using
        End Using

        Return result
    End Function

    Private Sub frmReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim stepName As String = "Start"

        Try
            ' 1. Report document
            stepName = "Report document toiri"
            Dim rptDoc As New ReportDocument()

            ' 2. Report file khuje ber kora
            stepName = "Report file khuja"
            Dim reportFileName As String = "GL_Posting.rpt"

            Dim reportPath As String = IO.Path.Combine(Application.StartupPath, reportFileName)

            If Not IO.File.Exists(reportPath) Then
                reportPath = IO.Path.Combine(Application.StartupPath, "..", "..", "Reports", reportFileName)
            End If

            If Not IO.File.Exists(reportPath) Then
                reportPath = IO.Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", reportFileName)
            End If

            If Not IO.File.Exists(reportPath) Then
                MessageBox.Show("Crystal Report file not found!" & vbCrLf &
                                "Please make sure '" & reportFileName & "' is inside the 'Reports' folder or bin\Debug folder.",
                                "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' 3. Report load
            stepName = "Report load (" & reportPath & ")"
            rptDoc.Load(reportPath)

            ' 4. Database login (App.config-er SageDBConn theke, hardcode password nei)
            stepName = "Database login"
            Dim sb As New SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings("SageDBConn").ConnectionString)

            Dim connectionInfo As New ConnectionInfo()
            connectionInfo.ServerName = sb.DataSource
            connectionInfo.DatabaseName = sb.InitialCatalog
            If sb.IntegratedSecurity Then
                connectionInfo.IntegratedSecurity = True
            Else
                connectionInfo.UserID = sb.UserID
                connectionInfo.Password = sb.Password
            End If

            For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In rptDoc.Database.Tables
                stepName = "Database login - table: " & tbl.Name
                Dim tableLogOnInfo As TableLogOnInfo = tbl.LogOnInfo
                tableLogOnInfo.ConnectionInfo = connectionInfo
                tbl.ApplyLogOnInfo(tableLogOnInfo)
            Next

            ' 5. Parameters
            stepName = "Parameter list porar somoy"
            Dim existing As New List(Of String)
            For Each p As ParameterField In rptDoc.ParameterFields
                existing.Add(p.Name & " [" & p.ParameterValueType.ToString() & "]")
            Next

            stepName = "Parameter FromDate (report-e ache: " & String.Join(", ", existing) & ")"
            rptDoc.SetParameterValue("FromDate", FromDate)

            stepName = "Parameter ToDate"
            rptDoc.SetParameterValue("ToDate", ToDate)

            stepName = "Parameter FromPostingSeq"
            rptDoc.SetParameterValue("FromPostingSeq", FromPostingSeq)

            stepName = "Parameter ToPostingSeq"
            rptDoc.SetParameterValue("ToPostingSeq", ToPostingSeq)

            ' Set SourceType Parameter if exists in Crystal Report
            Try
                rptDoc.SetParameterValue("SourceType", SourceType)
            Catch ex As Exception
            End Try

            ' Set FilterMode Parameter for Group Header/Footer Suppress logic in Crystal Report
            Try
                rptDoc.SetParameterValue("FilterMode", ReportFilterMode)
            Catch ex As Exception
            End Try

            ' Set AccountID Parameter (faka hole "" jabe) - Crystal-e parameter na thakleo error dibe na
            Try
                rptDoc.SetParameterValue("AccountID", If(String.IsNullOrEmpty(AccountID), "", AccountID))
            Catch ex As Exception
            End Try

            ' Record selection formula with Module Filter included
            stepName = "Record selection formula"
            Dim inv As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

            Dim selFormula As String =
                "Date({ET_GL_PostingJournal.DocDate}) >= Date(" & FromDate.Year & "," & FromDate.Month & "," & FromDate.Day & ") and " &
                "Date({ET_GL_PostingJournal.DocDate}) <= Date(" & ToDate.Year & "," & ToDate.Month & "," & ToDate.Day & ") and " &
                "CDbl({ET_GL_PostingJournal.PostingSeqNo}) >= " & FromPostingSeq.ToString(inv) & " and " &
                "CDbl({ET_GL_PostingJournal.PostingSeqNo}) <= " & ToPostingSeq.ToString(inv) & " and " &
                "CDbl({ET_GL_PostingJournal.BatchNo}) >= " & FromBatch.ToString(inv) & " and " &
                "CDbl({ET_GL_PostingJournal.BatchNo}) <= " & ToBatch.ToString(inv)

            ' Add Source Modules (SrceLedger) filtering condition dynamically from VB
            If ParamSourceModulesList IsNot Nothing AndAlso ParamSourceModulesList.Count > 0 Then
                Dim moduleConditions As New List(Of String)()
                For Each modName As String In ParamSourceModulesList
                    moduleConditions.Add("{ET_GL_PostingJournal.SrceLedger} = '" & modName.Replace("'", "''") & "'")
                Next
                If moduleConditions.Count > 0 Then
                    selFormula &= " and (" & String.Join(" or ", moduleConditions) & ")"
                End If
            End If

            ' Account select kora thakle: oi account jei entry-gulote ache, shei entry-gulor shob line ashbe
            If Not String.IsNullOrEmpty(AccountID) Then
                stepName = "Account-er entry khuja"
                Dim entries As List(Of String) = GetEntriesContainingAccount(AccountID)

                If entries.Count = 0 Then
                    ' Kono entry nei: kichu na ashar jonno impossible condition
                    selFormula &= " and 1 = 2"
                Else
                    Dim entryConditions As New List(Of String)()
                    For Each be As String In entries
                        entryConditions.Add("{ET_GL_PostingJournal.BatchEntry} = '" & be.Replace("'", "''") & "'")
                    Next
                    selFormula &= " and (" & String.Join(" or ", entryConditions) & ")"
                End If
            End If

            rptDoc.RecordSelectionFormula = selFormula

            ' Parameter panel hide + value reuse
            stepName = "Viewer setting"
            CrystalReportViewer1.ShowParameterPanelButton = False
            CrystalReportViewer1.ReuseParameterValuesOnRefresh = True

            ' 6. Viewer-e report assign
            stepName = "Viewer-e ReportSource assign"
            CrystalReportViewer1.ReportSource = rptDoc

            stepName = "RefreshReport"
            CrystalReportViewer1.RefreshReport()

            ' 7. Maximized
            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            MessageBox.Show("Error loading report" & vbCrLf & vbCrLf &
                            "Ei step-e error hoyeche: " & stepName & vbCrLf & vbCrLf &
                            ex.Message & vbCrLf & vbCrLf & ex.StackTrace,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class