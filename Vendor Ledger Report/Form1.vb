Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1

    ' Connection string from App.config
    Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("SageDBConn").ConnectionString

    ' Default From Date (oldest DocDate), kept for lock-state checking
    Private defaultFromDate As Date = New Date(1990, 1, 1)

    ' Source Module tracking Variables
    Private ReadOnly allModules As New List(Of String)()
    Private ReadOnly selectedModules As New List(Of String)()

    ' Flag to prevent events from firing while setting values via code
    Private isLoading As Boolean = False

    ' Accounts dropdown-er ekta row (ID + Description)
    Private Class AccountItem
        Public Property AccountID As String
        Public Property AccountDesc As String

        ' Dropdown-e "ID - Description" dekhabe
        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(AccountDesc) Then Return AccountID
            Return AccountID & " - " & AccountDesc
        End Function
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isLoading = True
        LoadPostingSeqFromView()
        LoadBatchList()
        SetDefaultDates()
        LoadSourceModules()
        LoadAccounts()
        isLoading = False

        ApplyLockState()
    End Sub

    ' ---------------------------------------------------------------------
    ' Dates: GLJEH theke MIN(DocDate) (view na, tai druto)
    ' ---------------------------------------------------------------------
    Private Sub SetDefaultDates()
        defaultFromDate = New Date(1990, 1, 1)

        Try
            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT MIN(TRY_CONVERT(date, CAST(CAST(DOCDATE AS bigint) AS varchar(8)), 112)) " &
                                      "FROM dbo.GLJEH WHERE DOCDATE > 0"
                Dim cmd As New SqlCommand(query, conn)
                cmd.CommandTimeout = 120
                conn.Open()
                Dim val As Object = cmd.ExecuteScalar()

                If val IsNot DBNull.Value AndAlso val IsNot Nothing Then
                    defaultFromDate = Convert.ToDateTime(val).Date
                End If
            End Using
        Catch ex As Exception
            defaultFromDate = New Date(1990, 1, 1)
        End Try

        dtpFromDate.Format = DateTimePickerFormat.Custom
        dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        dtpFromDate.Value = defaultFromDate

        dtpToDate.Format = DateTimePickerFormat.Custom
        dtpToDate.CustomFormat = "dd-MMM-yyyy"
        dtpToDate.Value = DateTime.Today
    End Sub

    ' ---------------------------------------------------------------------
    ' Posting Sequence load (GLBCTL theke)
    ' ---------------------------------------------------------------------
    Private Sub LoadPostingSeqFromView()
        Try
            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT DISTINCT POSTNGSEQ FROM dbo.GLBCTL " &
                                      "WHERE POSTNGSEQ IS NOT NULL AND POSTNGSEQ > 0 " &
                                      "ORDER BY POSTNGSEQ ASC"
                Dim cmd As New SqlCommand(query, conn)
                cmd.CommandTimeout = 120
                conn.Open()

                cmbFrompostSeq.Items.Clear()
                cmbTopostSeq.Items.Clear()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim seq As String = Convert.ToDecimal(reader("POSTNGSEQ")).ToString("0")
                        cmbFrompostSeq.Items.Add(seq)
                        cmbTopostSeq.Items.Add(seq)
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to load Posting Sequence: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------------------------------------------------
    ' Batch list load (GLBCTL theke)
    ' ---------------------------------------------------------------------
    Private Sub LoadBatchList()
        Try
            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT DISTINCT BATCHID FROM dbo.GLBCTL " &
                                      "WHERE BATCHID IS NOT NULL " &
                                      "ORDER BY BATCHID ASC"
                Dim cmd As New SqlCommand(query, conn)
                cmd.CommandTimeout = 120
                conn.Open()

                cmbFromBatch.Items.Clear()
                cmbToBatch.Items.Clear()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim b As String = Convert.ToDecimal(reader("BATCHID")).ToString("0")
                        cmbFromBatch.Items.Add(b)
                        cmbToBatch.Items.Add(b)
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to load Batch list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------------------------------------------------
    ' Source Modules load (GLJEH theke)
    ' ---------------------------------------------------------------------
    Private Sub LoadSourceModules()
        Try
            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT DISTINCT RTRIM(SRCELEDGER) AS SrceLedger FROM dbo.GLJEH " &
                                      "WHERE SRCELEDGER IS NOT NULL AND RTRIM(SRCELEDGER) <> '' " &
                                      "ORDER BY 1"

                Using da As New SqlDataAdapter(query, conn)
                    da.SelectCommand.CommandTimeout = 120

                    Using dt As New DataTable()
                        da.Fill(dt)

                        allModules.Clear()
                        selectedModules.Clear()

                        For Each row As DataRow In dt.Rows
                            Dim modName As String = row("SrceLedger").ToString()
                            allModules.Add(modName)
                            selectedModules.Add(modName)
                        Next
                    End Using
                End Using

                UpdateModuleTextBox()
            End Using
        Catch ex As Exception
            MessageBox.Show("Source Module Load Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------------------------------------------------
    ' Accounts load (GLAMF theke)
    ' ---------------------------------------------------------------------
    Private Sub LoadAccounts()
        Try
            Using conn As New SqlConnection(connString)
                Dim query As String = "SELECT RTRIM(ACCTFMTTD) AS AccountID, RTRIM(ACCTDESC) AS AccountDesc " &
                                      "FROM dbo.GLAMF " &
                                      "WHERE ACCTFMTTD IS NOT NULL AND RTRIM(ACCTFMTTD) <> '' " &
                                      "ORDER BY ACCTFMTTD ASC"
                Dim cmd As New SqlCommand(query, conn)
                cmd.CommandTimeout = 120
                conn.Open()

                AccountsCmb.Items.Clear()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim accId As String = reader("AccountID").ToString().Trim()
                        Dim accDesc As String = If(reader("AccountDesc") Is DBNull.Value, "", reader("AccountDesc").ToString().Trim())
                        AccountsCmb.Items.Add(New AccountItem With {.AccountID = accId, .AccountDesc = accDesc})
                    End While
                End Using
            End Using

            ' Typing korle suggestion ashbe
            AccountsCmb.DropDownStyle = ComboBoxStyle.DropDown
            AccountsCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            AccountsCmb.AutoCompleteSource = AutoCompleteSource.ListItems
            AccountsCmb.SelectedIndex = -1
            AccountsCmb.Text = String.Empty
        Catch ex As Exception
            MessageBox.Show("Failed to load Accounts: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Selected account-er shudhu ID ber kora (faka hole empty)
    Private Function GetSelectedAccountID() As String
        Dim selected As AccountItem = TryCast(AccountsCmb.SelectedItem, AccountItem)
        If selected IsNot Nothing Then Return selected.AccountID.Trim()

        Dim txt As String = AccountsCmb.Text.Trim()
        If txt = "" Then Return String.Empty

        ' Jodi "ID - Description" format-e lekha thake, shudhu ID nao
        Dim idx As Integer = txt.IndexOf(" - ")
        If idx > 0 Then txt = txt.Substring(0, idx)

        Return txt.Trim()
    End Function

    Private Sub UpdateModuleTextBox()
        If selectedModules.Count = 0 Then
            txtSelectedModules.Text = ""
        Else
            txtSelectedModules.Text = String.Join(", ", selectedModules)
        End If
    End Sub

    Private Sub btnSelectModules_Click(sender As Object, e As EventArgs) Handles btnSelectModules.Click
        Using dlg As New Form()
            dlg.Text = "Select Source Modules"
            dlg.Size = New Size(340, 430)
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False

            Dim chkList As New CheckedListBox With {
                .Location = New Point(12, 12),
                .Size = New Size(300, 290),
                .CheckOnClick = True
            }

            For Each modName As String In allModules
                Dim isChecked As Boolean = selectedModules.Contains(modName)
                chkList.Items.Add(modName, isChecked)
            Next

            Dim btnSelectAll As New Button With {
                .Text = "Select All",
                .Size = New Size(90, 28),
                .Location = New Point(12, 312)
            }
            AddHandler btnSelectAll.Click, Sub(s, ev)
                                               For i As Integer = 0 To chkList.Items.Count - 1
                                                   chkList.SetItemChecked(i, True)
                                               Next
                                           End Sub

            Dim btnUnselectAll As New Button With {
                .Text = "Unselect All",
                .Size = New Size(90, 28),
                .Location = New Point(108, 312)
            }
            AddHandler btnUnselectAll.Click, Sub(s, ev)
                                                 For i As Integer = 0 To chkList.Items.Count - 1
                                                     chkList.SetItemChecked(i, False)
                                                 Next
                                             End Sub

            Dim btnOK As New Button With {
                .Text = "Apply",
                .DialogResult = DialogResult.OK,
                .Size = New Size(90, 28),
                .Location = New Point(222, 312)
            }

            dlg.Controls.Add(chkList)
            dlg.Controls.Add(btnSelectAll)
            dlg.Controls.Add(btnUnselectAll)
            dlg.Controls.Add(btnOK)
            dlg.AcceptButton = btnOK

            If dlg.ShowDialog(Me) = DialogResult.OK Then
                selectedModules.Clear()
                For Each item In chkList.CheckedItems
                    selectedModules.Add(item.ToString())
                Next

                UpdateModuleTextBox()
            End If
        End Using
    End Sub

    Private Sub ApplyLockState()
        Dim dateUsed As Boolean = (dtpFromDate.Value.Date <> defaultFromDate) OrElse
                                  (dtpToDate.Value.Date <> DateTime.Today)

        Dim seqUsed As Boolean = (cmbFrompostSeq.Text.Trim() <> "") OrElse
                                 (cmbTopostSeq.Text.Trim() <> "")

        Dim batchUsed As Boolean = (cmbFromBatch.Text.Trim() <> "") OrElse
                                   (cmbToBatch.Text.Trim() <> "")

        Dim dateDisabled As Boolean = (seqUsed OrElse batchUsed)

        dtpFromDate.Enabled = Not dateDisabled
        dtpToDate.Enabled = Not dateDisabled

        If dateDisabled Then
            dtpFromDate.Format = DateTimePickerFormat.Custom
            dtpFromDate.CustomFormat = " "
            dtpToDate.Format = DateTimePickerFormat.Custom
            dtpToDate.CustomFormat = " "
        Else
            dtpFromDate.Format = DateTimePickerFormat.Custom
            dtpFromDate.CustomFormat = "dd-MMM-yyyy"
            dtpToDate.Format = DateTimePickerFormat.Custom
            dtpToDate.CustomFormat = "dd-MMM-yyyy"
        End If

        cmbFrompostSeq.Enabled = Not (dateUsed OrElse batchUsed)
        cmbTopostSeq.Enabled = Not (dateUsed OrElse batchUsed)

        cmbFromBatch.Enabled = Not (dateUsed OrElse seqUsed)
        cmbToBatch.Enabled = Not (dateUsed OrElse seqUsed)

        ' Accounts combo shobsomoy active thake (kono lock nei)
    End Sub

    Private Sub DatePicker_ValueChanged(sender As Object, e As EventArgs) Handles dtpFromDate.ValueChanged, dtpToDate.ValueChanged
        If isLoading Then Return
        ApplyLockState()
    End Sub

    Private Sub PostSeq_Changed(sender As Object, e As EventArgs) Handles cmbFrompostSeq.TextChanged, cmbTopostSeq.TextChanged
        If isLoading Then Return
        ApplyLockState()
    End Sub

    Private Sub Batch_Changed(sender As Object, e As EventArgs) Handles cmbFromBatch.TextChanged, cmbToBatch.TextChanged
        If isLoading Then Return
        ApplyLockState()
    End Sub

    ' ---------------------------------------------------------------------
    ' MIN / MAX (GLBCTL theke - view theke noy)
    ' ---------------------------------------------------------------------
    Private Function GetMinMax(ByVal columnName As String, ByVal isMin As Boolean, ByVal onlyPositive As Boolean) As Decimal
        ' View-er column naam -> asol table column
        Dim realCol As String = If(columnName = "PostingSeqNo", "POSTNGSEQ", "BATCHID")

        Dim aggregate As String = If(isMin, "MIN", "MAX")
        Dim query As String = $"SELECT {aggregate}({realCol}) FROM dbo.GLBCTL WHERE {realCol} IS NOT NULL"
        If onlyPositive Then query &= $" AND {realCol} > 0"

        Try
            Using conn As New SqlConnection(connString)
                Dim cmd As New SqlCommand(query, conn)
                cmd.CommandTimeout = 120
                conn.Open()
                Dim val As Object = cmd.ExecuteScalar()
                If val IsNot DBNull.Value AndAlso val IsNot Nothing Then
                    Return Convert.ToDecimal(val)
                End If
            End Using
        Catch ex As Exception
        End Try

        Return If(isMin, 0D, 999999999D)
    End Function

    Private Function GetSeqMinMax(ByVal isMin As Boolean) As Decimal
        Return GetMinMax("PostingSeqNo", isMin, True)
    End Function

    Private Function GetBatchMinMax(ByVal isMin As Boolean) As Decimal
        Return GetMinMax("BatchNo", isMin, False)
    End Function

    Private Function ParseRange(ByVal fromText As String, ByVal toText As String,
                                ByVal label As String,
                                ByVal minValue As Decimal, ByVal maxValue As Decimal,
                                ByRef fromVal As Decimal, ByRef toVal As Decimal) As Boolean

        If fromText = "" Then
            fromVal = minValue
        ElseIf Not Decimal.TryParse(fromText, fromVal) Then
            MessageBox.Show("From " & label & " must be a number.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If toText = "" Then
            toVal = maxValue
        ElseIf Not Decimal.TryParse(toText, toVal) Then
            MessageBox.Show("To " & label & " must be a number.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If fromVal > toVal Then
            MessageBox.Show("From " & label & " cannot be greater than To " & label & ".", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        isLoading = True

        cmbFrompostSeq.SelectedIndex = -1
        cmbTopostSeq.SelectedIndex = -1
        cmbFrompostSeq.Text = String.Empty
        cmbTopostSeq.Text = String.Empty

        cmbFromBatch.SelectedIndex = -1
        cmbToBatch.SelectedIndex = -1
        cmbFromBatch.Text = String.Empty
        cmbToBatch.Text = String.Empty

        AccountsCmb.SelectedIndex = -1
        AccountsCmb.Text = String.Empty

        LoadPostingSeqFromView()
        LoadBatchList()
        SetDefaultDates()
        LoadSourceModules()
        LoadAccounts()

        isLoading = False
        ApplyLockState()

        MessageBox.Show("Data Refreshed and Filters Reset Successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnShowReport_Click(sender As Object, e As EventArgs) Handles btnShowReport.Click
        If selectedModules.Count = 0 Then
            MessageBox.Show("Please select at least one Source Module.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fromSeqText As String = cmbFrompostSeq.Text.Trim()
        Dim toSeqText As String = cmbTopostSeq.Text.Trim()
        Dim fromBatchText As String = cmbFromBatch.Text.Trim()
        Dim toBatchText As String = cmbToBatch.Text.Trim()

        Dim batchMode As Boolean = (fromBatchText <> "") OrElse (toBatchText <> "")
        Dim seqMode As Boolean = (fromSeqText <> "") OrElse (toSeqText <> "")

        Dim rptForm As New frmReportViewer()

        rptForm.ParamSourceModulesList = New List(Of String)(selectedModules)
        rptForm.SourceType = String.Join(", ", selectedModules)

        ' Account (faka hole filter hobe na)
        rptForm.AccountID = GetSelectedAccountID()

        If batchMode Then
            Dim fromBatch As Decimal
            Dim toBatch As Decimal

            If Not ParseRange(fromBatchText, toBatchText, "Batch",
                            GetBatchMinMax(True), GetBatchMinMax(False),
                            fromBatch, toBatch) Then Return

            rptForm.ReportFilterMode = "B" ' Batch mode -> 'B'
            rptForm.FromDate = New Date(1990, 1, 1)
            rptForm.ToDate = New Date(2999, 12, 31)
            rptForm.FromPostingSeq = 0D
            rptForm.ToPostingSeq = 999999999D
            rptForm.FromBatch = fromBatch
            rptForm.ToBatch = toBatch

        ElseIf seqMode Then
            Dim fromSeq As Decimal
            Dim toSeq As Decimal

            If Not ParseRange(fromSeqText, toSeqText, "Posting Seq",
                            GetSeqMinMax(True), GetSeqMinMax(False),
                            fromSeq, toSeq) Then Return

            rptForm.ReportFilterMode = "P" ' Posting Seq mode -> 'P'
            rptForm.FromDate = New Date(1990, 1, 1)
            rptForm.ToDate = New Date(2999, 12, 31)
            rptForm.FromPostingSeq = fromSeq
            rptForm.ToPostingSeq = toSeq
            rptForm.FromBatch = GetBatchMinMax(True)
            rptForm.ToBatch = GetBatchMinMax(False)

        Else
            If dtpFromDate.Value.Date > dtpToDate.Value.Date Then
                MessageBox.Show("From Date cannot be greater than To Date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            rptForm.ReportFilterMode = "D" ' Date mode -> 'D'
            rptForm.FromDate = dtpFromDate.Value.Date
            rptForm.ToDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1)
            rptForm.FromPostingSeq = GetSeqMinMax(True)
            rptForm.ToPostingSeq = GetSeqMinMax(False)
            rptForm.FromBatch = GetBatchMinMax(True)
            rptForm.ToBatch = GetBatchMinMax(False)
        End If

        rptForm.ShowDialog()
    End Sub

End Class