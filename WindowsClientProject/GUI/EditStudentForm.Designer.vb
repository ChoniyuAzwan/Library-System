﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditStudentForm
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
        Me.StudentYear = New System.Windows.Forms.ComboBox()
        Me.StudentEmail = New System.Windows.Forms.TextBox()
        Me.StudentContact = New System.Windows.Forms.TextBox()
        Me.IDENT = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StudentName = New System.Windows.Forms.TextBox()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.StudentLoginName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(87, 283)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 6
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 7
        Me.Cancel_Button.Text = "Cancel"
        '
        'StudentYear
        '
        Me.StudentYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StudentYear.FormattingEnabled = True
        Me.StudentYear.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.StudentYear.Location = New System.Drawing.Point(15, 114)
        Me.StudentYear.Name = "StudentYear"
        Me.StudentYear.Size = New System.Drawing.Size(216, 21)
        Me.StudentYear.TabIndex = 2
        '
        'StudentEmail
        '
        Me.StudentEmail.Location = New System.Drawing.Point(14, 201)
        Me.StudentEmail.Name = "StudentEmail"
        Me.StudentEmail.Size = New System.Drawing.Size(220, 20)
        Me.StudentEmail.TabIndex = 4
        '
        'StudentContact
        '
        Me.StudentContact.Location = New System.Drawing.Point(14, 158)
        Me.StudentContact.Name = "StudentContact"
        Me.StudentContact.Size = New System.Drawing.Size(220, 20)
        Me.StudentContact.TabIndex = 3
        '
        'IDENT
        '
        Me.IDENT.Location = New System.Drawing.Point(14, 72)
        Me.IDENT.Name = "IDENT"
        Me.IDENT.Size = New System.Drawing.Size(220, 20)
        Me.IDENT.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(220, 23)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Student Email"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 23)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Student Contact"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(220, 23)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Student Year"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(220, 23)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Student Identification"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StudentName
        '
        Me.StudentName.Location = New System.Drawing.Point(14, 29)
        Me.StudentName.Name = "StudentName"
        Me.StudentName.Size = New System.Drawing.Size(220, 20)
        Me.StudentName.TabIndex = 0
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(12, 9)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(220, 23)
        Me.UsernameLabel.TabIndex = 33
        Me.UsernameLabel.Text = "Student Name"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StudentLoginName
        '
        Me.StudentLoginName.Location = New System.Drawing.Point(14, 244)
        Me.StudentLoginName.Name = "StudentLoginName"
        Me.StudentLoginName.ReadOnly = True
        Me.StudentLoginName.Size = New System.Drawing.Size(220, 20)
        Me.StudentLoginName.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 23)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Student Login Name"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'EditStudentForm
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(245, 324)
        Me.Controls.Add(Me.StudentLoginName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.StudentYear)
        Me.Controls.Add(Me.StudentEmail)
        Me.Controls.Add(Me.StudentContact)
        Me.Controls.Add(Me.IDENT)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StudentName)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditStudentForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Student Information"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents StudentYear As System.Windows.Forms.ComboBox
    Friend WithEvents StudentEmail As System.Windows.Forms.TextBox
    Friend WithEvents StudentContact As System.Windows.Forms.TextBox
    Friend WithEvents IDENT As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StudentName As System.Windows.Forms.TextBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents StudentLoginName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
