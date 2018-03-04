Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StudentOBJ As LibrarySystem.StudentClass
        StudentOBJ = Session("student")
        If StudentOBJ Is Nothing Then
            Server.Transfer("default.aspx")
            Exit Sub
        End If
        WelcomeMessage.Text = "Welcome " & StudentOBJ.GetStudentName
    End Sub

    Protected Sub SignOut_Click(sender As Object, e As EventArgs) Handles SignOut.Click
        Session.Abandon()
        Server.Transfer("default.aspx")
    End Sub

    Private Sub ChangePassword1_ChangingPassword(sender As Object, e As LoginCancelEventArgs) Handles ChangePassword1.ChangingPassword
        Dim PSW As String = ChangePassword1.NewPassword
        Dim CPS As String = ChangePassword1.ConfirmNewPassword
        Dim OLD As String = ChangePassword1.CurrentPassword

        Dim StudentOBJ As LibrarySystem.StudentClass
        StudentOBJ = Session("student")

        If StudentOBJ.GetStudentPassword <> OLD Then
            ChangePassword1.ChangePasswordFailureText = "The old password was not entered correctly."
            Exit Sub
        End If

        If CPS <> PSW Then
            ChangePassword1.ChangePasswordFailureText = "The new password and its confirmation don't match."
            Exit Sub
        End If


        ' open db
        Dim DBMS As New LibrarySystem.DBMSClass
        If DBMS.OpenDB() <> "OK" Then
            ChangePassword1.ChangePasswordFailureText = "Can't open database."
            Exit Sub
        End If

        StudentOBJ.SetStudentPassword(PSW)
        If Not StudentOBJ.UpdateDB(DBMS, True) Then
            ChangePassword1.ChangePasswordFailureText = "Can't change password."
            Exit Sub
        End If

        DBMS.CloseDB()
        Server.Transfer("studentworkspace.aspx")
    End Sub
End Class