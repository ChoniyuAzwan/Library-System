Imports LibrarySystem

Public Class MainWindow
    ' define the DBMS object
    Public DBMS As New DBMSClass

    ' define the staff object
    Public StaffObject As StaffClass

    ' this function executes when the form first appears
    Private Sub MainWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' open the db
        Dim result As String = DBMS.OpenDB()
        If result <> "OK" Then
            MsgBox("Error openining the database." & vbNewLine & result, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End
        End If

        ' create login form
        Dim MyLoginForm As New LoginForm
        MyLoginForm.ShowDialog()
        StaffObject = StaffClass.Login(DBMS, MyLoginForm.UsernameTextBox.Text, MyLoginForm.PasswordTextBox.Text)
        If StaffObject Is Nothing Then
            MsgBox("Invalid username or password", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End
        End If

        'if the admin just logged in, then we should remove other tabs
        If StaffObject.GetEmployeeLoginName = "admin" Then


            ' remove the tab related to normal employees
            TabControl1.TabPages.RemoveAt(1)
            TabControl1.TabPages.RemoveAt(2)
            ShelfToolStripMenuItem.Visible = False
            BookToolStripMenuItem.Visible = False
            StudentToolStripMenuItem.Visible = False

            ' display all staff information
            If Not StaffClass.FillDGVWithStaffInfo(DGV, DBMS) Then
                MsgBox("Error while displaying staff information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If


        Else


            ' remove tabs and menus for admin
            TabControl1.TabPages.RemoveAt(0)
            Me.StaffToolStripMenuItem.Visible = False


            ' fill the dgv with the shelf info
            If Not ShelfClass.FillDGVWithShelfInfo(DGV2, DBMS) Then
                MsgBox("Error while displaying shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If

            ' fill the dgv with the book info
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error while displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If

            ' fill the combobox for search books 
            Dim I As Integer
            For I = 0 To DGV3.ColumnCount - 1
                If DGV3.Columns(I).Visible Then
                    SearchBookCB.Items.Add(DGV3.Columns(I).HeaderText)
                End If
            Next
            SearchBookCB.SelectedIndex = 0

            ' fill the dgv4 with student info
            If Not StudentClass.FillDGVWithStudentInfo(DGV4, DBMS) Then
                MsgBox("Error while displaying student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If
        End If


        If Not FineClass.CalculateFinesForAllStudents(DBMS, True) Then
            MsgBox("Error while calculating fines", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End
        End If

    End Sub

    ' this is used to add a new staff member
    Private Sub AddNewStaffMemberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewStaffMemberToolStripMenuItem.Click
        Dim Tmp As New AddStaffForm
        If Tmp.ShowDialog() = Windows.Forms.DialogResult.OK Then
            StaffClass.FillDGVWithStaffInfo(DGV, DBMS)
        End If
    End Sub

    ' this one is used to edit staff member information
    Private Sub EditStaffMemberInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditStaffMemberInformationToolStripMenuItem.Click
        If DGV.SelectedRows.Count = 0 Then
            MsgBox("You should select a staff member", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' load the object from db
        Dim StaffName As String = DGV.SelectedRows(0).Cells("EmployeeLoginName").Value
        If StaffName = "admin" Then
            MsgBox("admin account can't be edited", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        Dim TMP As New LibrarySystem.StaffClass
        If Not TMP.LoadFromDB(DBMS, StaffName) Then
            MsgBox("Unable of loading staff information from db", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' edit the information
        Dim EditStaffObject As New EditStaffForm
        EditStaffObject.StaffObj = TMP
        If EditStaffObject.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' display all staff information
            If Not StaffClass.FillDGVWithStaffInfo(DGV, DBMS) Then
                MsgBox("Error while displaying staff information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    ' this function is used to remove staff member
    Private Sub RemoveStaffMemberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveStaffMemberToolStripMenuItem.Click
        If DGV.SelectedRows.Count = 0 Then
            MsgBox("You should select a staff member", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' get the staff member name
        Dim StaffName As String = DGV.SelectedRows(0).Cells("EmployeeLoginName").Value
        If StaffName = "admin" Then
            MsgBox("admin account can't be removed", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        Dim TMP As New LibrarySystem.StaffClass
        If Not TMP.LoadFromDB(DBMS, StaffName) Then
            MsgBox("Unable of loading staff information from db", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' remove the staff information from db
        If Not TMP.RemoveFromDB(DBMS, True) Then
            MsgBox("Unable of removing staff member from db", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Else
            ' display all staff information
            If Not StaffClass.FillDGVWithStaffInfo(DGV, DBMS) Then
                MsgBox("Error while displaying staff information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub AddNewShelfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewShelfToolStripMenuItem.Click
        Dim AddShelfObj As New AddShelfForm
        If AddShelfObj.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' fill the dgv with the shelf info
            If Not ShelfClass.FillDGVWithShelfInfo(DGV2, DBMS) Then
                MsgBox("Error while displaying shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If

        End If
    End Sub

    ' used to edit shelf information
    Private Sub EditShelfToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditShelfToolStripMenuItem.Click
        ' make sure shelf is being selected
        If DGV2.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' load the object
        Dim ShelfObj As New ShelfClass
        Dim ShelfNo As String = DGV2.SelectedRows(0).Cells("shelfno").Value
        If Not ShelfObj.LoadFromDB(DBMS, ShelfNo) Then
            MsgBox("Error while loading shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Else
            Dim EditShelfFormObject As New EditShelfForm
            EditShelfFormObject.OriginalShelfInfo = ShelfObj
            If EditShelfFormObject.ShowDialog = Windows.Forms.DialogResult.OK Then

                ' fill the dgv with the shelf info
                If Not ShelfClass.FillDGVWithShelfInfo(DGV2, DBMS) Then
                    MsgBox("Error while displaying shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End If

            End If
        End If
    End Sub

    ' this is used to remove a shelf 
    Private Sub RemoveShelfToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveShelfToolStripMenuItem.Click
        ' make sure shelf is being selected
        If DGV2.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' load the object
        Dim ShelfObj As New ShelfClass
        Dim ShelfNo As String = DGV2.SelectedRows(0).Cells("shelfno").Value
        If Not ShelfObj.LoadFromDB(DBMS, ShelfNo) Then
            MsgBox("Error while loading shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' delete the object
        If Not ShelfObj.RemoveFromDB(DBMS, True) Then
            MsgBox("Error while deleting shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Else

            ' fill the dgv with the shelf info
            If Not ShelfClass.FillDGVWithShelfInfo(DGV2, DBMS) Then
                MsgBox("Error while displaying shelf information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If

        End If

    End Sub

    Private Sub AddNewBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewBookToolStripMenuItem.Click
        Dim BK As New AddBookForm
        If BK.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' fill the dgv with the book info
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error while displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If
        End If
    End Sub

    ' used to edit a book
    Private Sub EditBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBookToolStripMenuItem.Click
        If DGV3.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        Dim BookID As String = DGV3.SelectedRows(0).Cells("BookID").Value

        ' load the book object
        Dim BookObj As New BookClass
        If Not BookObj.LoadFromDB(DBMS, BookID) Then
            MsgBox("Unable of loading book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' create the edit book window
        Dim Win As New EditBookForm
        Win.BookObj = BookObj

        If Win.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' fill the dgv with the book info
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error while displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If
        End If
    End Sub

    ' this function is used to remove a book
    Private Sub RemoveBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveBookToolStripMenuItem.Click
        ' make sure a book is being selected
        If DGV3.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' load the object
        Dim BookObj As New BookClass
        Dim BookID As String = DGV3.SelectedRows(0).Cells("BookID").Value
        If Not BookObj.LoadFromDB(DBMS, BookID) Then
            MsgBox("Error while loading book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' delete the object
        If Not BookObj.RemoveFromDB(DBMS, True) Then
            MsgBox("Error while deleting book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Else

            ' fill the dgv with the shelf info
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error while displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If

        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        ' create the search condition
        Dim FilterStr As String

        ' identify which column the search should work on
        Dim I As Integer
        For I = 0 To DGV3.Columns.Count - 1
            If DGV3.Columns(I).Visible Then
                If DGV3.Columns(I).HeaderText = SearchBookCB.SelectedItem Then

                    If DGV3.Columns(I).ValueType.ToString = "System.String" Then
                        FilterStr = DGV3.Columns(I).Name & " like '%" + SearchBookTX.Text + "%'"
                    Else
                        FilterStr = DGV3.Columns(I).Name & " = " & SearchBookTX.Text
                    End If

                    Dim BS As BindingSource = DGV3.DataSource
                    Try
                        BS.Filter = FilterStr
                    Catch ex As Exception
                        BS.Filter = ""
                    End Try

                End If
            End If
        Next
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim BS As BindingSource = DGV3.DataSource
        BS.Filter = ""
    End Sub

    Private Sub AddNewStudentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewStudentToolStripMenuItem.Click
        Dim Win As New AddStudentForm

        ' insert the student
        If Win.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' if the insert process is successful then display the info again
            If Not StudentClass.FillDGVWithStudentInfo(DGV4, DBMS) Then
                MsgBox("Error while displaying student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If
        End If
    End Sub

    ' used to edit student info
    Private Sub EditStudentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditStudentToolStripMenuItem.Click
        ' check if student is selected
        If DGV4.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' get student id
        Dim StudentID As String = DGV4.SelectedRows(0).Cells("StudentID").Value

        ' load the student info from db
        Dim StudentObj As New StudentClass
        If Not StudentObj.LoadFromDBByID(DBMS, StudentID) Then
            MsgBox("Error while loading student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' create the edit window
        Dim WindowObj As New EditStudentForm
        WindowObj.StudentObj = StudentObj

        If WindowObj.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' if the update process is successful then display the info again
            If Not StudentClass.FillDGVWithStudentInfo(DGV4, DBMS) Then
                MsgBox("Error while displaying student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If

        End If
    End Sub

    ' remove student info
    Private Sub RemoveStudentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveStudentToolStripMenuItem.Click
        ' make sure a student is being selected
        If DGV4.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' load the object
        Dim StudentObj As New StudentClass
        Dim StudentID As String = DGV4.SelectedRows(0).Cells("StudentID").Value
        If Not StudentObj.LoadFromDBByID(DBMS, StudentID) Then
            MsgBox("Error while loading student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' delete the object

        If Not StudentObj.RemoveFromDB(DBMS, True) Then
            MsgBox("Error while deleting student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Else

            ' fill the dgv with the shelf info
            If Not StudentClass.FillDGVWithStudentInfo(DGV4, DBMS) Then
                MsgBox("Error while displaying studnet information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            End If

        End If

    End Sub

    ''' <summary>
    ''' this method is used to update password
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPasswordToolStripMenuItem.Click
        ' check if student is selected
        If DGV4.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        ' get student id
        Dim StudentID As String = DGV4.SelectedRows(0).Cells("StudentID").Value

        ' load the student info from db
        Dim StudentObj As New StudentClass
        If Not StudentObj.LoadFromDBByID(DBMS, StudentID) Then
            MsgBox("Error while loading student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        ' create the edit window
        Dim WindowObj As New SetStudentPasswordForm
        WindowObj.StudentObj = StudentObj

        If WindowObj.ShowDialog = Windows.Forms.DialogResult.OK Then

            ' if the update process is successful then display the info again
            If Not StudentClass.FillDGVWithStudentInfo(DGV4, DBMS) Then
                MsgBox("Error while displaying student information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                End
            End If

        End If
    End Sub

    ''' <summary>
    ''' this method is used to display all students who want to borrow the book
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DGV3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGV3.DoubleClick
        If DGV3.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        Dim BookID As String = DGV3.SelectedRows(0).Cells("bookid").Value

        Dim BorrowDetails = DetailedQueueEntryClass.GetBookingListForBook(DBMS, BookID)
        If BorrowDetails Is Nothing Then
            MsgBox("Error while loading booking details", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim DetailsWindow As New DisplayBookingDetailsForm
        DetailsWindow.DetailsList = BorrowDetails
        DetailsWindow.ShowDialog()
    End Sub


    Private Sub DeliverToStudentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeliverToStudentToolStripMenuItem.Click
        If DGV3.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        Dim BookID As String = DGV3.SelectedRows(0).Cells("BookID").Value
        Dim BookOBJ As New BookClass
        If Not BookOBJ.LoadFromDB(DBMS, BookID) Then
            MsgBox("Can't load book", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        Dim TMP As New WindowsClientProject.DeliverBookToStudent
        TMP.BookOBJ = BookOBJ
        If TMP.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ReturnBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnBookToolStripMenuItem.Click
        Dim TMP As New ReturnBook
        If TMP.ShowDialog Then
            If Not BookClass.FillDGVWithBookInfo(DGV3, DBMS) Then
                MsgBox("Error displaying book information", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub BackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupToolStripMenuItem.Click
        Dim SFD As New SaveFileDialog
        SFD.Filter = "*.bku|*.bku"
        If SFD.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            BackupDB(SFD.FileName, DBMS)
            MsgBox("Backup completed successfully", MsgBoxStyle.OkOnly, "Success")
        Catch ex As Exception
            MsgBox("unexpected error" & vbNewLine & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreToolStripMenuItem.Click
        Dim OFD As New OpenFileDialog
        OFD.Filter = "*.bku|*.bku"
        If OFD.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            RestoreDB(OFD.FileName, DBMS)
            MsgBox("DB restored successfully", MsgBoxStyle.OkOnly, "Success")
            End
        Catch ex As Exception
            MsgBox("unexpected error" & vbNewLine & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

End Class