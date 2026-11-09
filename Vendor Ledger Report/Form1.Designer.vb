<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AccountsCmb = New System.Windows.Forms.ComboBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.btnSelectModules = New System.Windows.Forms.Button()
        Me.txtSelectedModules = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbFromBatch = New System.Windows.Forms.ComboBox()
        Me.cmbToBatch = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnShowReport = New System.Windows.Forms.Button()
        Me.ToCustomerIDLabel = New System.Windows.Forms.Label()
        Me.FromCustomerIDLabel = New System.Windows.Forms.Label()
        Me.cmbFrompostSeq = New System.Windows.Forms.ComboBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cmbTopostSeq = New System.Windows.Forms.ComboBox()
        Me.ToDateLabel = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.FromDateLabel = New System.Windows.Forms.Label()
        Me.MainPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MainPanel.Controls.Add(Me.Label4)
        Me.MainPanel.Controls.Add(Me.AccountsCmb)
        Me.MainPanel.Controls.Add(Me.TextBox6)
        Me.MainPanel.Controls.Add(Me.btnSelectModules)
        Me.MainPanel.Controls.Add(Me.txtSelectedModules)
        Me.MainPanel.Controls.Add(Me.Label2)
        Me.MainPanel.Controls.Add(Me.Label3)
        Me.MainPanel.Controls.Add(Me.cmbFromBatch)
        Me.MainPanel.Controls.Add(Me.cmbToBatch)
        Me.MainPanel.Controls.Add(Me.Label1)
        Me.MainPanel.Controls.Add(Me.btnShowReport)
        Me.MainPanel.Controls.Add(Me.ToCustomerIDLabel)
        Me.MainPanel.Controls.Add(Me.FromCustomerIDLabel)
        Me.MainPanel.Controls.Add(Me.cmbFrompostSeq)
        Me.MainPanel.Controls.Add(Me.dtpToDate)
        Me.MainPanel.Controls.Add(Me.btnRefresh)
        Me.MainPanel.Controls.Add(Me.cmbTopostSeq)
        Me.MainPanel.Controls.Add(Me.ToDateLabel)
        Me.MainPanel.Controls.Add(Me.dtpFromDate)
        Me.MainPanel.Controls.Add(Me.FromDateLabel)
        Me.MainPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MainPanel.Location = New System.Drawing.Point(75, 26)
        Me.MainPanel.Margin = New System.Windows.Forms.Padding(30)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Padding = New System.Windows.Forms.Padding(20)
        Me.MainPanel.Size = New System.Drawing.Size(968, 535)
        Me.MainPanel.TabIndex = 33
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Menu
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 316)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(215, 38)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Accounts"
        '
        'AccountsCmb
        '
        Me.AccountsCmb.DropDownHeight = 165
        Me.AccountsCmb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountsCmb.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.AccountsCmb.FormattingEnabled = True
        Me.AccountsCmb.IntegralHeight = False
        Me.AccountsCmb.Location = New System.Drawing.Point(228, 316)
        Me.AccountsCmb.MaxDropDownItems = 3
        Me.AccountsCmb.Name = "AccountsCmb"
        Me.AccountsCmb.Size = New System.Drawing.Size(706, 37)
        Me.AccountsCmb.TabIndex = 39
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.SystemColors.Menu
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(12, 377)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(210, 23)
        Me.TextBox6.TabIndex = 38
        Me.TextBox6.Text = "Source Modules"
        '
        'btnSelectModules
        '
        Me.btnSelectModules.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectModules.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnSelectModules.Location = New System.Drawing.Point(733, 369)
        Me.btnSelectModules.Name = "btnSelectModules"
        Me.btnSelectModules.Size = New System.Drawing.Size(200, 48)
        Me.btnSelectModules.TabIndex = 37
        Me.btnSelectModules.Text = "Modules"
        Me.btnSelectModules.UseVisualStyleBackColor = True
        '
        'txtSelectedModules
        '
        Me.txtSelectedModules.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedModules.Location = New System.Drawing.Point(228, 377)
        Me.txtSelectedModules.Name = "txtSelectedModules"
        Me.txtSelectedModules.Size = New System.Drawing.Size(483, 30)
        Me.txtSelectedModules.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Menu
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(496, 196)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 38)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "To Batch"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Menu
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 196)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(215, 38)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "From Batch"
        '
        'cmbFromBatch
        '
        Me.cmbFromBatch.DropDownHeight = 165
        Me.cmbFromBatch.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFromBatch.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmbFromBatch.FormattingEnabled = True
        Me.cmbFromBatch.IntegralHeight = False
        Me.cmbFromBatch.Location = New System.Drawing.Point(228, 196)
        Me.cmbFromBatch.MaxDropDownItems = 3
        Me.cmbFromBatch.Name = "cmbFromBatch"
        Me.cmbFromBatch.Size = New System.Drawing.Size(200, 39)
        Me.cmbFromBatch.TabIndex = 34
        '
        'cmbToBatch
        '
        Me.cmbToBatch.DropDownHeight = 165
        Me.cmbToBatch.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbToBatch.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmbToBatch.FormattingEnabled = True
        Me.cmbToBatch.IntegralHeight = False
        Me.cmbToBatch.Location = New System.Drawing.Point(733, 196)
        Me.cmbToBatch.MaxDropDownItems = 3
        Me.cmbToBatch.Name = "cmbToBatch"
        Me.cmbToBatch.Size = New System.Drawing.Size(200, 39)
        Me.cmbToBatch.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(363, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(241, 43)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Posted Journals "
        '
        'btnShowReport
        '
        Me.btnShowReport.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnShowReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReport.Location = New System.Drawing.Point(356, 449)
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(203, 51)
        Me.btnShowReport.TabIndex = 31
        Me.btnShowReport.Text = "Show Report"
        Me.btnShowReport.UseVisualStyleBackColor = True
        '
        'ToCustomerIDLabel
        '
        Me.ToCustomerIDLabel.BackColor = System.Drawing.SystemColors.Menu
        Me.ToCustomerIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToCustomerIDLabel.Location = New System.Drawing.Point(496, 253)
        Me.ToCustomerIDLabel.Name = "ToCustomerIDLabel"
        Me.ToCustomerIDLabel.Size = New System.Drawing.Size(215, 38)
        Me.ToCustomerIDLabel.TabIndex = 23
        Me.ToCustomerIDLabel.Text = "To P. Seq. No."
        '
        'FromCustomerIDLabel
        '
        Me.FromCustomerIDLabel.BackColor = System.Drawing.SystemColors.Menu
        Me.FromCustomerIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FromCustomerIDLabel.Location = New System.Drawing.Point(7, 253)
        Me.FromCustomerIDLabel.Name = "FromCustomerIDLabel"
        Me.FromCustomerIDLabel.Size = New System.Drawing.Size(215, 38)
        Me.FromCustomerIDLabel.TabIndex = 22
        Me.FromCustomerIDLabel.Text = "From P. Seq. No."
        '
        'cmbFrompostSeq
        '
        Me.cmbFrompostSeq.DropDownHeight = 165
        Me.cmbFrompostSeq.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFrompostSeq.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmbFrompostSeq.FormattingEnabled = True
        Me.cmbFrompostSeq.IntegralHeight = False
        Me.cmbFrompostSeq.Location = New System.Drawing.Point(228, 253)
        Me.cmbFrompostSeq.MaxDropDownItems = 3
        Me.cmbFrompostSeq.Name = "cmbFrompostSeq"
        Me.cmbFrompostSeq.Size = New System.Drawing.Size(200, 39)
        Me.cmbFrompostSeq.TabIndex = 24
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.dtpToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(733, 144)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(200, 35)
        Me.dtpToDate.TabIndex = 21
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.SystemColors.ActiveBorder
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(869, 35)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Padding = New System.Windows.Forms.Padding(5)
        Me.btnRefresh.Size = New System.Drawing.Size(65, 65)
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cmbTopostSeq
        '
        Me.cmbTopostSeq.DropDownHeight = 165
        Me.cmbTopostSeq.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTopostSeq.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmbTopostSeq.FormattingEnabled = True
        Me.cmbTopostSeq.IntegralHeight = False
        Me.cmbTopostSeq.Location = New System.Drawing.Point(733, 253)
        Me.cmbTopostSeq.MaxDropDownItems = 3
        Me.cmbTopostSeq.Name = "cmbTopostSeq"
        Me.cmbTopostSeq.Size = New System.Drawing.Size(200, 39)
        Me.cmbTopostSeq.TabIndex = 25
        '
        'ToDateLabel
        '
        Me.ToDateLabel.BackColor = System.Drawing.SystemColors.Menu
        Me.ToDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToDateLabel.Location = New System.Drawing.Point(496, 144)
        Me.ToDateLabel.Name = "ToDateLabel"
        Me.ToDateLabel.Size = New System.Drawing.Size(215, 38)
        Me.ToDateLabel.TabIndex = 20
        Me.ToDateLabel.Text = "To Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(228, 144)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(200, 35)
        Me.dtpFromDate.TabIndex = 18
        '
        'FromDateLabel
        '
        Me.FromDateLabel.BackColor = System.Drawing.SystemColors.Menu
        Me.FromDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FromDateLabel.Location = New System.Drawing.Point(7, 144)
        Me.FromDateLabel.Name = "FromDateLabel"
        Me.FromDateLabel.Size = New System.Drawing.Size(215, 38)
        Me.FromDateLabel.TabIndex = 19
        Me.FromDateLabel.Text = "From Date"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 590)
        Me.Controls.Add(Me.MainPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Posted Journals"
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainPanel As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents btnShowReport As Button
    Friend WithEvents ToCustomerIDLabel As Label
    Friend WithEvents FromCustomerIDLabel As Label
    Friend WithEvents cmbFrompostSeq As ComboBox
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents btnRefresh As Button
    Friend WithEvents cmbTopostSeq As ComboBox
    Friend WithEvents ToDateLabel As Label
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents FromDateLabel As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbFromBatch As ComboBox
    Friend WithEvents cmbToBatch As ComboBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents btnSelectModules As Button
    Friend WithEvents txtSelectedModules As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents AccountsCmb As ComboBox
End Class
