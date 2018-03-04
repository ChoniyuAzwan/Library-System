Public Class BorrowClass
    Private StudentID As String
    Private BookID As String
    Private BorrowDate As String
    Private ReturnDate As String
    Private Notes As String
    Private Status As String

    Private OldStudentID As String
    Private OldBookID As String
    Private OldBorrowDate As String
    Private OldReturnDate As String
    Private OldNotes As String
    Private OldStatus As String

    ' the setters
    Public Sub SetStudentID(ByVal StID As String)
        StudentID = StID
    End Sub

    Public Sub SetBookID(ByVal BKID As String)
        BookID = BKID
    End Sub

    Public Sub SetBorrowDate(ByVal BRDate As String)
        BorrowDate = BRDate
    End Sub

    Public Sub SetReturnDate(ByVal RTDate As String)
        ReturnDate = RTDate
    End Sub

    Public Sub SetNotes(ByVal NT As String)
        Notes = NT
    End Sub

    Public Sub SetStatus(ByVal ST As String)
        Status = ST
    End Sub

    Public Function GetStudentID() As String
        Return Me.StudentID
    End Function

    Public Function GetBookID() As String
        Return BookID
    End Function

    Public Function GetBorrowDate() As String
        Return BorrowDate
    End Function

    Public Function GetReturnDate() As String
        Return ReturnDate
    End Function

    Public Function GetNotes() As String
        Return Notes
    End Function

    Public Function GetStatus() As String
        Return Status
    End Function


    ' insert the record into the database
    Public Function InsertIntoDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try

            ' first generate the sql command
            Dim SQL As String
            SQL = "insert into borrow(" & _
                  "Studentid, " & _
                  "bookid, " & _
                  "borrowdate, " & _
                  "returndate, " & _
                  "notes, " & _
                  "status" & _
                  ") values (@0,@1,@2,@3,@4,@5)"
            DBMS.ExecuteSQL(SQL, _
                            Me.StudentID, _
                            Me.BookID, _
                            Me.BorrowDate, _
                            Me.ReturnDate, _
                            Me.Notes, _
                            Me.Status)

            ' commit changes
            If Commit Then
                DBMS.Commit()
            End If

            ' set old =new
            Me.OldStudentID = Me.StudentID
            Me.OldBookID = Me.BookID
            Me.OldBorrowDate = Me.BorrowDate
            Me.OldReturnDate = Me.ReturnDate
            Me.OldNotes = Me.Notes
            Me.OldStatus = Me.Status

            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try

    End Function

    Public Function LoadBorrowByStudentIDAndBookID(ByVal DBMS As DBMSClass, ByVal STID As String, ByVal BKID As String)
        Try
            Dim S As String = DBMS.CreateResultSet("select * from borrow where bookid=@0 and studentid=@1", BKID, STID)
            If Not DBMS.ReadAndNotEOF(S) Then
                DBMS.CloseResultSet(S)
                DBMS.Rollback()
                Return False
            End If
            StudentID = DBMS.GetColumnValue(S, "studentid")
            BookID = DBMS.GetColumnValue(S, "bookid")
            BorrowDate = DBMS.GetColumnValue(S, "borrowdate")
            ReturnDate = DBMS.GetColumnValue(S, "returndate")
            Notes = DBMS.GetColumnValue(S, "notes")
            Status = DBMS.GetColumnValue(S, "status")

            DBMS.CloseResultSet(S)

            OldBookID = BookID
            OldBorrowDate = BorrowDate
            OldNotes = Notes
            OldReturnDate = ReturnDate
            OldStatus = Status
            OldStudentID = StudentID

            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function



    Public Function RemoveFromDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try
            DBMS.ExecuteSQL("delete from borrow where studentid=@0 and bookid=@1", Me.StudentID, Me.BookID)
            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function


    Public Shared Function ReturnBorrowedBook(ByVal DBMS As DBMSClass, ByVal StudentID As String, ByVal BookID As String, ByVal Commit As Boolean) As Boolean
        Try
            DBMS.ExecuteSQL("update books set availablecopies =availablecopies+1 where bookid=@0", BookID)
            DBMS.ExecuteSQL("delete from borrow where studentid=@0 and bookid=@1", StudentID, BookID)

            Dim OBJ As New FineClass
            If Not OBJ.LoadFromDB(DBMS, StudentID, BookID) Then
                DBMS.Rollback()
                Return False
            End If


            DBMS.ExecuteSQL("insert into payments(id,studentid,bookid,fineamount,paymentdate)" & vbNewLine & _
                            "            values  (@0,@1       ,@2    ,@3        ,@4         )", _
                            GetNextVal(), StudentID, BookID, OBJ.GetFineAmount, Format(Now, "yyyy-MM-dd"))


            DBMS.ExecuteSQL("delete from fines  where studentid=@0 and bookid=@1", StudentID, BookID)

            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            DBMS.Rollback()
            Return False
        End Try
    End Function


    Public Shared Function PerformBorrow(ByVal DBMS As DBMSClass, ByVal StudentID As String, ByVal BookID As String, ByVal Commit As Boolean) As Boolean
        Try
            DBMS.ExecuteSQL("update books set bookid=@0 where bookid=@1", BookID, BookID)

            Dim S As String = DBMS.CreateResultSet("select * from books where bookid=@0 and availablecopies>0", BookID)
            If Not DBMS.ReadAndNotEOF(S) Then
                DBMS.CloseResultSet(S)
                DBMS.Rollback()
                Return False
            End If
            DBMS.CloseResultSet(S)


            DBMS.ExecuteSQL("delete from queue where studentid=@0 and bookid=@1", StudentID, BookID)
            Dim BorrowObj As New BorrowClass
            BorrowObj.SetBookID(BookID)
            BorrowObj.SetBorrowDate(Format(Now, "yyyy-MM-dd"))
            BorrowObj.SetNotes("")
            BorrowObj.SetReturnDate(Format(Now.AddDays(7), "yyyy-MM-dd"))
            BorrowObj.SetStatus("")
            BorrowObj.SetStudentID(StudentID)
            If Not BorrowObj.InsertIntoDB(DBMS, False) Then
                DBMS.Rollback()
                Return False
            End If
            DBMS.ExecuteSQL("update books set availablecopies =availablecopies-1 where bookid=@0", BookID)
            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function




    ' this function is used to fill the dgv with students and their borrowed books
    Public Shared Function FillDGVWithStudentBooks(ByVal DGV As DataGridView, ByVal DBMS As DBMSClass) As Boolean
        Try
            ' display all the staff information in the data grid view
            If Not DBMS.FillDataGridViewWithData(DGV, "select * from studentbooks") Then
                Return False
            End If
            DGV.Columns("BookID").HeaderText = "Book ID"
            DGV.Columns("StudentID").HeaderText = "Student ID"
            DGV.Columns("BookTitle").HeaderText = "Title"
            DGV.Columns("BookAuthor").HeaderText = "Author"
            DGV.Columns("PublicationYear").HeaderText = "Year"
            DGV.Columns("ShelfNo").HeaderText = "Shelf"
            DGV.Columns("Barcode").HeaderText = "Barcode"
            DGV.Columns("StudentName").HeaderText = "Student Name"
            DGV.Columns("StudentIDENT").HeaderText = "Student Identification"
            DGV.Columns("StudentYear").HeaderText = "Student Year"
            DGV.Columns("StudentContact").HeaderText = "Contact"
            DGV.Columns("StudentEmail").HeaderText = "Email"

            DGV.Columns("bookid").Visible = False
            DGV.Columns("Studentid").Visible = False


            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function



End Class
