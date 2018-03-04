Imports System.Windows.Forms

Public Class DisplayBookingDetailsForm

    Public DetailsList As List(Of LibrarySystem.DetailedQueueEntryClass)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub DisplayBookingDetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ITM In DetailsList

            DGV3.Rows.Add(ITM.StudentOBJ.GetStudentName, ITM.StudentOBJ.GetStudentYear, ITM.BookingOrder)

        Next
    End Sub
End Class
