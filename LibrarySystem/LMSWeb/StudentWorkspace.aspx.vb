Imports LibrarySystem

Public Class StudentWorkspace
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StudentOBJ As LibrarySystem.StudentClass
        StudentOBJ = Session("student")
        If StudentOBJ Is Nothing Then
            Server.Transfer("Default.aspx")
            Exit Sub
        End If
        WelcomeMessage.Text = "Welcome " & StudentOBJ.GetStudentName
    End Sub

    Protected Sub SignOut_Click(sender As Object, e As EventArgs) Handles SignOut.Click
        Session.Abandon()
        Server.Transfer("Default.aspx")
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "bkcancel" Then
            Dim RowID = e.CommandArgument
            Dim Barcode As String = GridView1.Rows(RowID).Cells(6).Text
            Dim StudentOBJ As StudentClass
            StudentOBJ = Session("student")

            Dim DBMS As New DBMSClass
            Dim Result = DBMS.OpenDB()
            If Result <> "OK" Then
                ' error message
                Exit Sub
            End If
            If Not QueueClass.RemoveQueueEntry(DBMS, Barcode, StudentOBJ.GetStudentID) Then
                ' error message
                Exit Sub
            End If
            DBMS.CloseDB()

            GridView1.DataBind()

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class