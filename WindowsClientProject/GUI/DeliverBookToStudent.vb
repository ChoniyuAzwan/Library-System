Imports System.Windows.Forms
Imports LibrarySystem

Public Class DeliverBookToStudent

    Public BookOBJ As BookClass


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        Dim StudentID = DGV.SelectedRows(0).Cells("studentid").Value
        Dim BookID As String = BookOBJ.GetBookID

        If Not BorrowClass.PerformBorrow(MainWindow.DBMS, StudentID, BookID, True) Then
            MsgBox("Error, can't borrow book", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If


        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DeliverBookToStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        ' fill the user interface
        BTitle.Text = BookObj.GetBookTitle()
        BAuthor.Text = BookObj.GetBookAuthor()
        BYear.Text = BookObj.GetBookPublicationYear()
        BPress.Text = BookObj.GetPress
        BSubject.Text = BookObj.GetSubject
        BKeywords.Text = BookObj.GetKeywords
        BAvailableCopies.Text = BookObj.GetAvailableCopies
        BTotalCopies.Text = BookObj.GetTotalCopies
        BBarCode.Text = BookObj.GetBarcode
        TextBox1.Text = BookOBJ.GetShelfNo

        If Not QueueClass.FillDGVWithQueueInformation(DGV, MainWindow.DBMS, BookOBJ.GetBookID) Then
            MsgBox("Error while getting queue information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Exit Sub
        End If

    End Sub
End Class
