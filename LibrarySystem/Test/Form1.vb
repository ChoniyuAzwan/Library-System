Imports LibrarySystem

Public Class Form1

    ' START ALL TESTS
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        ' =========================================
        '   test 01 :DBMS class open connection method
        ' =========================================

        Dim OBJ As New DBMSClass
        If OBJ.OpenDB = "OK" Then
            ListBox1.Items.Add("test 01 OK")
        Else
            ListBox2.Items.Add("test 01 failed")
        End If

        '====================================
        '   test 02 :DBMS class exequte sql
        '====================================
        Try
            OBJ.ExecuteSQL("insert into vars values (@0, @1)", "hello1", "everyone1")
            OBJ.ExecuteSQL("insert into vars values (@0, @1)", "hello2", "everyone2")
            OBJ.ExecuteSQL("insert into vars values (@0, @1)", "hello3", "everyone3")
            ListBox1.Items.Add("test 02 OK")
        Catch ex As Exception
            ListBox2.Items.Add("test 02 failed")
        End Try

        '=============================================
        ' test 03 : DBMS class read result set
        '=============================================
        Try
            Dim L = OBJ.CreateResultSet("select * from vars")
            Do While OBJ.ReadAndNotEOF(L)
                Dim V = OBJ.GetColumnValue(L, "varname")
            Loop
            OBJ.CloseResultSet(L)
            ListBox1.Items.Add("test 03 OK")
        Catch ex As Exception
            ListBox2.Items.Add("test 03 failed")
        End Try

        '==============================================
        ' test 04 : staff insert record
        '==============================================
        Dim StaffObj As New LibrarySystem.StaffClass
        StaffObj.SetEmployeeLoginName("smith")
        StaffObj.SetEmployeeName("john smith")
        StaffObj.SetEmployeePassword("123")

        If StaffObj.InsertIntoDB(OBJ, False) Then
            ListBox1.Items.Add("test 04 OK")
        Else
            ListBox2.Items.Add("test 04 failed")
        End If


        ' ==========================================
        ' test 05 : staff load record
        ' ==========================================
        Dim StaffObj2 As New LibrarySystem.StaffClass
        If StaffObj2.LoadFromDB(OBJ, "smith") Then
            ListBox1.Items.Add("test 05 OK")
        Else
            ListBox2.Items.Add("test 05 failed")
        End If

        ' ==========================================
        ' test 06 : staff update
        ' ==========================================
        StaffObj2.SetEmployeeLoginName("smith2")
        If StaffObj2.UpdateDB(OBJ, False) Then
            ListBox1.Items.Add("test 06 OK")
        Else
            ListBox2.Items.Add("test 06 failed")
        End If

        ' ==========================================
        ' test 07 : staff remove
        ' ==========================================
        If StaffObj2.RemoveFromDB(OBJ, False) Then
            ListBox1.Items.Add("test 07 OK")
        Else
            ListBox2.Items.Add("test 07 failed")
        End If


        ' ==========================================
        ' test 08 : fill datagridview
        ' ==========================================
        Dim TMPDGV As New DataGridView
        If OBJ.FillDataGridViewWithData(TMPDGV, "select * from vars") Then
            ListBox1.Items.Add("test 08 OK")
        Else
            ListBox2.Items.Add("test 08 failed")
        End If

        ' ==========================================
        ' test 09 staff login
        ' ==========================================
        Dim TmpStaffObj As New LibrarySystem.StaffClass
        TmpStaffObj.SetEmployeeLoginName("A")
        TmpStaffObj.SetEmployeeName("ABC")
        TmpStaffObj.SetEmployeePassword("B")
        TmpStaffObj.InsertIntoDB(OBJ, False)

        Dim TmpStaff = LibrarySystem.StaffClass.Login(OBJ, "A", "B")
        If TmpStaff IsNot Nothing Then
            ListBox1.Items.Add("test 09 OK")
        Else
            ListBox2.Items.Add("test 09 failed")
        End If

        ' ==========================================
        ' test 10 insert new shelf
        ' ==========================================
        Dim SHF As New ShelfClass
        SHF.SetShelfNo("abc")
        SHF.SetSection("A")
        SHF.SetFloor("3")
        If SHF.InsertIntoDB(OBJ, False) Then
            ListBox1.Items.Add("test 10 OK")
        Else
            ListBox2.Items.Add("test 10 failed")
        End If

        ' ==========================================
        ' test 11 load the shelf
        ' ==========================================
        Dim SHF2 As New ShelfClass
        If SHF2.LoadFromDB(OBJ, "abc") Then
            ListBox1.Items.Add("test 11 OK")
        Else
            ListBox2.Items.Add("test 11 failed")
        End If

        ' ==========================================
        ' test 12 edit  the shelf
        ' ==========================================
        SHF2.SetFloor("vfneruinhuier")
        If SHF2.UpdateDB(OBJ, False) Then
            ListBox1.Items.Add("test 12 OK")
        Else
            ListBox2.Items.Add("test 12 failed")
        End If

        ' ==========================================
        ' test 13 remove the shelf
        ' ==========================================
        If SHF2.RemoveFromDB(OBJ, False) Then
            ListBox1.Items.Add("test 13 OK")
        Else
            ListBox2.Items.Add("test 13 failed")
        End If



        ' ==========================================
        ' test 14 add new book
        ' ==========================================
        Dim BK As New BookClass
        BK.SetBookID(123)
        BK.SetBookTitle("introduction to vb programming")
        BK.SetBookAuthor("someone")
        BK.SetBarcode("3843826728")
        BK.SetBookPress("super kitty")
        BK.SetShelfNO("wxyz")
        BK.SetBookPublicationYear(1999)
        BK.SetBookSubject("programming")
        BK.SetBookKeywords("programming tutorial how to")
        BK.SetAvailableCopies(9)
        BK.SetTotalCopies(9)


        Dim SHF3 As New ShelfClass
        SHF3.SetShelfNo("wxyz")
        SHF3.SetSection("programming")
        SHF3.SetFloor("1")
        If Not SHF3.InsertIntoDB(OBJ, False) Then
            ListBox2.Items.Add("test 14 failed")
        End If

        If BK.InsertIntoDB(OBJ, False) Then
            ListBox1.Items.Add("test 14 OK")
        Else
            ListBox2.Items.Add("test 14 failed")
        End If

        ' ==========================================
        ' test 15 load book
        ' ==========================================
        Dim BK2 As New BookClass
        If BK2.LoadFromDB(OBJ, BK.GetBookID) Then
            ListBox1.Items.Add("test 15 OK")
        Else
            ListBox2.Items.Add("test 15 failed")
        End If


        ' ==========================================
        ' test 16 modify or edit book
        ' ==========================================
        BK2.SetAvailableCopies(22)
        BK2.SetBookTitle("the amazing cat")
        If BK2.UpdateDB(OBJ, False) Then
            ListBox1.Items.Add("test 16 OK")
        Else
            ListBox2.Items.Add("test 16 failed")
        End If

        ' ==========================================
        ' test 17 remove book
        ' ==========================================
        If BK2.RemoveFromDB(OBJ, False) Then
            ListBox1.Items.Add("test 17 OK")
        Else
            ListBox2.Items.Add("test 17 failed")
        End If

        ' ==========================================
        ' test 18 insert new student
        ' ==========================================
        Dim ST As New StudentClass
        ST.SetStudentName("todd")
        ST.SetStudentContact("7483979")
        ST.SetStudentEmail("todd@yahoo.com")
        ST.SetStudentIDENT("988928282")
        ST.SetStudentYear("2011")
        ST.SetStudentPassword("")
        ST.SetStudentLoginName("todd")

        If ST.InsertIntoDB(OBJ, False) Then
            ListBox1.Items.Add("test 18 OK")
        Else
            ListBox2.Items.Add("test 18 failed")
        End If

        ' ==========================================
        ' test 19 load student
        ' ==========================================
        Dim ST2 As New StudentClass

        If ST2.LoadFromDBByID(OBJ, ST.GetStudentID) Then
            ListBox1.Items.Add("test 19 OK")
        Else
            ListBox2.Items.Add("test 19 failed")
        End If

        ' ==========================================
        ' test 19.5 load student
        ' ==========================================
        Dim ST3 As New StudentClass

        If ST2.LoadFromDBByLoginName(OBJ, ST.GetStudentLoginName) Then
            ListBox1.Items.Add("test 19.5 OK")
        Else
            ListBox2.Items.Add("test 19.5 failed")
        End If


        ' ==========================================
        ' test 20 modify the infro
        ' ==========================================
        ST2.SetStudentName("meyao")
        If ST2.UpdateDB(OBJ, False) Then
            ListBox1.Items.Add("test 20 OK")
        Else
            ListBox2.Items.Add("test 20 failed")
        End If

        ' ==========================================
        ' test 21 remove student info
        ' ==========================================
        If ST2.RemoveFromDB(OBJ, False) Then
            ListBox1.Items.Add("test 21 OK")
        Else
            ListBox2.Items.Add("test 21 failed")
        End If

        ' ==========================================
        ' test 22 insert booking information
        ' ==========================================

        Dim BK3 As New BookClass
        BK3.SetBookID("bk3")
        BK3.SetBookTitle("a book")
        BK3.SetBookAuthor("mr. x")
        BK3.SetBarcode("22873")
        BK3.SetBookPress("super kitty")
        BK3.SetShelfNO("wxyz")
        BK3.SetBookPublicationYear(1999)
        BK3.SetBookSubject("programming")
        BK3.SetBookKeywords("programming tutorial how to")
        BK3.SetAvailableCopies(9)
        BK3.SetTotalCopies(9)

        Dim FLG As Boolean = True
        If Not BK3.InsertIntoDB(OBJ, False) Then
            FLG = False
        End If

        If FLG Then
            Dim Queue As New QueueClass
            Queue.SetBookID(BK3.GetBookID)
            Queue.SetStudentID(ST2.GetStudentID)
            Queue.SetBookingOrder(9)

            If Not Queue.InsertIntoDB(OBJ, False) Then
                FLG = False
            End If
        End If

        If FLG Then
            ListBox1.Items.Add("test 22 OK:insert item to queue")
        Else
            ListBox2.Items.Add("test 22 failed:insert item to queue")
        End If


        ' ==========================================
        ' test 23 loading queue item
        ' ==========================================
        Dim Queue2 As New QueueClass
        If Queue2.LoadFromDB(OBJ, BK3.GetBookID, ST2.GetStudentID) Then
            ListBox1.Items.Add("test 23 OK:load queue item")
        Else
            ListBox2.Items.Add("test 23 failed:load queue item")
        End If

        ' ==========================================
        ' test 24 update
        ' ==========================================
        Queue2.SetBookingOrder(9999)
        If Queue2.UpdateDB(OBJ, False) Then
            ListBox1.Items.Add("test 24 OK:update queue item")
        Else
            ListBox2.Items.Add("test 24 failed:update queue item")
        End If

        ' ==========================================
        ' test 25 remove queue item
        ' ==========================================

        If Queue2.RemoveFromDB(OBJ, False) Then
            ListBox1.Items.Add("test 25 OK:remove queue item")
        Else
            ListBox2.Items.Add("test 25 failed:remove queue item")
        End If

        ' ==========================================
        ' test 26 load details from database
        ' ==========================================

        Dim ST4 As New StudentClass
        ST4.SetStudentName("todd4")
        ST4.SetStudentContact("7483979")
        ST4.SetStudentEmail("todd@yahoo.com")
        ST4.SetStudentIDENT("988928282")
        ST4.SetStudentYear("2011")
        ST4.SetStudentPassword("")
        ST4.SetStudentLoginName("todd4")

        FLG = True
        If Not ST4.InsertIntoDB(OBJ, False) Then
            ListBox2.Items.Add("test 26 failed: can't create student record")
            FLG = False
        End If

        If FLG Then
            BK3 = New BookClass
            BK3.SetBookID("bk3")
            BK3.SetBookTitle("a book")
            BK3.SetBookAuthor("mr. x")
            BK3.SetBarcode("22873")
            BK3.SetBookPress("super kitty")
            BK3.SetShelfNO("wxyz")
            BK3.SetBookPublicationYear(1999)
            BK3.SetBookSubject("programming")
            BK3.SetBookKeywords("programming tutorial how to")
            BK3.SetAvailableCopies(9)
            BK3.SetTotalCopies(9)
        End If

        If Not BK3.InsertIntoDB(OBJ, False) Then
            FLG = False
        End If

        Dim QueueOBJ As New QueueClass
        If FLG Then
            QueueOBJ.SetBookID(BK3.GetBookID)
            QueueOBJ.SetStudentID(ST4.GetStudentID)
            QueueOBJ.SetBookingOrder(9)

            If Not QueueOBJ.InsertIntoDB(OBJ, False) Then
                FLG = False
            End If
        End If

        If Not FLG Then
            ListBox2.Items.Add("test 26 failed:insert item to queue")
        Else

            Dim BookingDetails As New DetailedQueueEntryClass
            If Not BookingDetails.LoadBookingRecord(OBJ, QueueOBJ.GetStudentID, QueueOBJ.GetBookID) Then
                ListBox2.Items.Add("test 26 failed:can't load details")
            Else
                ListBox1.Items.Add("test 26 success:load booking details")
            End If

        End If

        ' ==========================================
        ' test 27 load details list from database
        ' ==========================================
        Dim DetailsList As List(Of DetailedQueueEntryClass)
        DetailsList = DetailedQueueEntryClass.GetBookingListForBook(OBJ, QueueOBJ.GetBookID)

        If DetailsList Is Nothing Then
            ListBox2.Items.Add("test 27 failed:can't load list of booking details")
        Else
            ListBox1.Items.Add("test 27 success:loading list of booking details")
        End If

        ' ==========================================
        ' test 28 insert borrow information into db
        ' ==========================================
        Dim BorrowObj As New BorrowClass
        BorrowObj.SetBookID("bkid")
        BorrowObj.SetStudentID("stid")
        BorrowObj.SetBorrowDate("2013")
        BorrowObj.SetReturnDate("2019")
        BorrowObj.SetNotes("why")
        BorrowObj.SetStatus("not returned")
        If Not BorrowObj.InsertIntoDB(OBJ, False) Then
            ListBox2.Items.Add("test 28 failed:can't insert borrow record")
        Else
            ListBox1.Items.Add("test 28 success:inserting borrow record")
        End If

        ' ==========================================
        ' test 29 load borrow information into db
        ' ==========================================
        Dim BorrowObj2 As New BorrowClass
        If Not BorrowObj2.LoadBorrowByStudentIDAndBookID(OBJ, "stid", "bkid") Then
            ListBox2.Items.Add("test 29 failed:can't load borrow record")
        Else
            ListBox1.Items.Add("test 28 success:loaded borrow record")
        End If

        ' ==========================================
        ' test 30 remove borrow information
        ' ==========================================

        If Not BorrowObj2.RemoveFromDB(OBJ, False) Then
            ListBox2.Items.Add("test 30 failed:can't remove borrow record")
        Else
            ListBox1.Items.Add("test 28 success:removed borrow record")
        End If


        OBJ.Rollback()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim OBJ As New DBMSClass
        OBJ.OpenDB()
        BackupDB("d:\test.txt", OBJ)
        OBJ.CloseDB()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim OBJ As New DBMSClass
        OBJ.OpenDB()
        RestoreDB("d:\test.txt", OBJ)
        OBJ.CloseDB()
    End Sub

End Class
