<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditBookForm
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.BShelfNo = New System.Windows.Forms.ComboBox()
        Me.BKeywords = New System.Windows.Forms.TextBox()
        Me.BAvailableCopies = New System.Windows.Forms.TextBox()
        Me.BYear = New System.Windows.Forms.TextBox()
        Me.BBarCode = New System.Windows.Forms.TextBox()
        Me.BSubject = New System.Windows.Forms.TextBox()
        Me.BAuthor = New System.Windows.Forms.TextBox()
        Me.BTotalCopies = New System.Windows.Forms.TextBox()
        Me.BPress = New System.Windows.Forms.TextBox()
        Me.BTitle = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(326, 246)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'BShelfNo
        '
        Me.BShelfNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BShelfNo.FormattingEnabled = True
        Me.BShelfNo.Location = New System.Drawing.Point(251, 207)
        Me.BShelfNo.Name = "BShelfNo"
        Me.BShelfNo.Size = New System.Drawing.Size(220, 21)
        Me.BShelfNo.TabIndex = 48
        '
        'BKeywords
        '
        Me.BKeywords.Location = New System.Drawing.Point(251, 29)
        Me.BKeywords.Name = "BKeywords"
        Me.BKeywords.Size = New System.Drawing.Size(220, 20)
        Me.BKeywords.TabIndex = 44
        '
        'BAvailableCopies
        '
        Me.BAvailableCopies.Location = New System.Drawing.Point(251, 72)
        Me.BAvailableCopies.Name = "BAvailableCopies"
        Me.BAvailableCopies.Size = New System.Drawing.Size(220, 20)
        Me.BAvailableCopies.TabIndex = 45
        '
        'BYear
        '
        Me.BYear.Location = New System.Drawing.Point(12, 121)
        Me.BYear.Name = "BYear"
        Me.BYear.Size = New System.Drawing.Size(220, 20)
        Me.BYear.TabIndex = 41
        '
        'BBarCode
        '
        Me.BBarCode.Location = New System.Drawing.Point(251, 164)
        Me.BBarCode.Name = "BBarCode"
        Me.BBarCode.Size = New System.Drawing.Size(220, 20)
        Me.BBarCode.TabIndex = 47
        '
        'BSubject
        '
        Me.BSubject.Location = New System.Drawing.Point(14, 207)
        Me.BSubject.Name = "BSubject"
        Me.BSubject.Size = New System.Drawing.Size(220, 20)
        Me.BSubject.TabIndex = 43
        '
        'BAuthor
        '
        Me.BAuthor.Location = New System.Drawing.Point(14, 72)
        Me.BAuthor.Name = "BAuthor"
        Me.BAuthor.Size = New System.Drawing.Size(220, 20)
        Me.BAuthor.TabIndex = 40
        '
        'BTotalCopies
        '
        Me.BTotalCopies.Location = New System.Drawing.Point(251, 121)
        Me.BTotalCopies.Name = "BTotalCopies"
        Me.BTotalCopies.Size = New System.Drawing.Size(220, 20)
        Me.BTotalCopies.TabIndex = 46
        '
        'BPress
        '
        Me.BPress.Location = New System.Drawing.Point(14, 164)
        Me.BPress.Name = "BPress"
        Me.BPress.Size = New System.Drawing.Size(220, 20)
        Me.BPress.TabIndex = 42
        '
        'BTitle
        '
        Me.BTitle.Location = New System.Drawing.Point(14, 29)
        Me.BTitle.Name = "BTitle"
        Me.BTitle.Size = New System.Drawing.Size(220, 20)
        Me.BTitle.TabIndex = 39
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(251, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(220, 23)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "Shelf"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(251, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 23)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Keywords"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(251, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(220, 23)
        Me.Label8.TabIndex = 54
        Me.Label8.Text = "Availalble Copies"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(251, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(220, 23)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "Barcode"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(220, 23)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Publication Year"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 187)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(220, 23)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Subject"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(251, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(220, 23)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Total Copies"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(220, 23)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Author"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 23)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Press"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(12, 9)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(220, 23)
        Me.UsernameLabel.TabIndex = 51
        Me.UsernameLabel.Text = "Book Title"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'EditBookForm
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(484, 287)
        Me.Controls.Add(Me.BShelfNo)
        Me.Controls.Add(Me.BKeywords)
        Me.Controls.Add(Me.BAvailableCopies)
        Me.Controls.Add(Me.BYear)
        Me.Controls.Add(Me.BBarCode)
        Me.Controls.Add(Me.BSubject)
        Me.Controls.Add(Me.BAuthor)
        Me.Controls.Add(Me.BTotalCopies)
        Me.Controls.Add(Me.BPress)
        Me.Controls.Add(Me.BTitle)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditBookForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EditBookForm"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents BShelfNo As System.Windows.Forms.ComboBox
    Friend WithEvents BKeywords As System.Windows.Forms.TextBox
    Friend WithEvents BAvailableCopies As System.Windows.Forms.TextBox
    Friend WithEvents BYear As System.Windows.Forms.TextBox
    Friend WithEvents BBarCode As System.Windows.Forms.TextBox
    Friend WithEvents BSubject As System.Windows.Forms.TextBox
    Friend WithEvents BAuthor As System.Windows.Forms.TextBox
    Friend WithEvents BTotalCopies As System.Windows.Forms.TextBox
    Friend WithEvents BPress As System.Windows.Forms.TextBox
    Friend WithEvents BTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label

End Class
