Partial Public Class SelectBook
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StudentOBJ As librarysystem.StudentClass
        StudentOBJ = Session("student")
        If StudentOBJ Is Nothing Then
            Server.Transfer("default.aspx")
            Exit Sub
        End If
        WelcomeMessage.Text = "Welcome " & StudentOBJ.GetStudentName
    End Sub

    Protected Sub SignOut_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SignOut.Click
        Session.Abandon()
        Server.Transfer("default.aspx")
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
      
        If e.CommandName = "request" Then
            Dim RowNumber = e.CommandArgument

            Try
                Dim DBMS As New LibrarySystem.DBMSClass
                If DBMS.OpenDB() <> "OK" Then
                    ErrorMSG.Text = "Error opening the database"
                    ErrorMSG.Visible = True
                    Exit Sub
                End If



                ' get the book barcode
                Dim Barcode As String = GridView1.Rows(RowNumber).Cells(6).Text

                ' get the student id
                Dim StudentObject As LibrarySystem.StudentClass
                StudentObject = Session("student")


                If Not LibrarySystem.QueueClass.InsertQueueEntry(DBMS, Barcode, StudentObject.GetStudentID) Then
                    'ErrorMSG.Text = "Error requesting the book"
                    ErrorMSG.Text = "The book has being requesting"
                    ErrorMSG.Visible = True
                    DBMS.CloseDB()
                    Exit Sub
                End If

                DBMS.CloseDB()

                Server.Transfer("StudentWorkspace.aspx")


            Catch ex As Exception
                ErrorMSG.Text = "Unexpected error"
                ErrorMSG.Visible = True
            End Try

        End If
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class