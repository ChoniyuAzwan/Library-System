Imports System.Windows.Forms
Imports LibrarySystem

Public Class ReturnBook

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        Dim StudentID As String = DGV.SelectedRows(0).Cells("studentid").Value
        Dim BookID As String = DGV.SelectedRows(0).Cells("bookid").Value

        If Not librarysystem.BorrowClass.ReturnBorrowedBook(MainWindow.DBMS, StudentID, BookID, True) Then
            MsgBox("Error while returning book", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ReturnBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not librarysystem.BorrowClass.FillDGVWithStudentBooks(DGV, MainWindow.DBMS) Then
            MsgBox("Error while display borrow information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub



    Private Sub DGV_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGV.SelectionChanged
        ' clear second dgv
        DGV2.DataSource = Nothing

        ' check if there is a row being select
        If DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' get student id
        Dim StudentID = DGV.SelectedRows(0).Cells("StudentID").Value

        ' display fines
        librarysystem.FineClass.FillDGVWithFinesForStudent(DGV2, MainWindow.DBMS, StudentID)

    End Sub
End Class
