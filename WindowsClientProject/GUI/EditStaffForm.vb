Imports System.Windows.Forms

Public Class EditStaffForm
    ' define the object that holds the staff information
    Public StaffObj As LibrarySystem.StaffClass

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ' this event is used to fill the display
    Private Sub EditStaffForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' fill the textboxes with the values from the object
        UsernameTextBox.Text = StaffObj.GetEmployeeLoginName
        StaffFullNameTextBox.Text = StaffObj.GetEmployeeName
    End Sub

    Private Sub Cancel_Button_Click_1(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub OK_Button_Click_1(sender As Object, e As EventArgs) Handles OK_Button.Click
        ' fill the object
        StaffObj.SetEmployeeLoginName(UsernameTextBox.Text)
        StaffObj.SetEmployeeName(StaffFullNameTextBox.Text)

        ' check the password
        If PasswordTextBox.Text = ConfirmPasswordTextBox.Text Then
            If PasswordTextBox.Text <> "" Then
                StaffObj.SetEmployeePassword(PasswordTextBox.Text)
            End If
        Else
            MsgBox("The password and its confirmation does not match", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' save the changes
        If Not StaffObj.UpdateDB(MainWindow.DBMS, True) Then
            MsgBox("Unable of updating the staff information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub
End Class
