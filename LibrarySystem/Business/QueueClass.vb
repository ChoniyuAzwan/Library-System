''' <summary>
''' this class is used to manage booking queues
''' </summary>
''' <remarks></remarks>
Public Class QueueClass


    ' define the members of the class
    Private BookID As String
    Private StudentID As Long
    Private BookingOrder As Long

    Private OldBookID As String
    Private OldStudentID As Long
    Private OldBookingOrder As Long

    ' setters and getters for the methods
    Public Sub SetBookID(ByVal vBookID As String)
        Me.BookID = vBookID
    End Sub

    Public Sub SetStudentID(ByVal vStudentID As Long)
        Me.StudentID = vStudentID
    End Sub

    Public Sub SetBookingOrder(ByVal vBookingOrder As Long)
        Me.BookingOrder = vBookingOrder
    End Sub

    Public Function GetBookID() As String
        Return Me.BookID
    End Function

    Public Function GetStudentID() As Long
        Return Me.StudentID
    End Function

    Public Function GetBookingOrder() As Long
        Return Me.BookingOrder
    End Function

    ' this method is used to insert an object into the database
    Public Function InsertIntoDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try
            ' insert the record
            Dim SQL As String
            SQL = "insert into queue(bookid,studentid,bookingorder) values (@0,@1,@2)"
            DBMS.ExecuteSQL(SQL, Me.BookID, Me.StudentID, Me.BookingOrder)

            ' save the changes
            If Commit Then
                DBMS.Commit()
            End If

            Me.OldStudentID = Me.StudentID
            Me.OldBookingOrder = Me.BookingOrder
            Me.OldBookID = Me.BookID

            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    ' this function is used to load shelf information from db
    Public Function LoadFromDB(ByVal DBMS As DBMSClass, ByVal vBookID As String, ByVal vStudentID As Long) As Boolean
        Try
            ' get the record
            Dim S As String
            S = DBMS.CreateResultSet("select * from queue where bookid=@0 and studentid=@1", vBookID, vStudentID)
            If DBMS.ReadAndNotEOF(S) Then

                ' fill the object
                Me.BookID = DBMS.GetColumnValue(S, "bookid")
                Me.StudentID = DBMS.GetColumnValue(S, "studentid")
                Me.BookingOrder = DBMS.GetColumnValue(S, "bookingorder")

                ' set old =new
                Me.OldBookID = Me.BookID
                Me.OldStudentID = Me.StudentID
                Me.OldBookingOrder = Me.BookingOrder

                ' close recordset
                DBMS.CloseResultSet(S)

                Return True
            Else

                ' no record was found, so close result set
                DBMS.CloseResultSet(S)

                ' rollback
                DBMS.Rollback()

                Return False

            End If
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    ' this function checks to see if the record was changed or not
    Public Function WasRecordChanged(ByVal DBMS As DBMSClass) As String
        Try
            ' load the record from db
            Dim TMP As New QueueClass
            If Not TMP.LoadFromDB(DBMS, Me.OldBookID, Me.OldStudentID) Then
                DBMS.Rollback()
                Return "error"
            End If

            ' check if any value was changed
            If TMP.OldStudentID <> Me.OldStudentID Then Return "yes"
            If TMP.OldBookID <> Me.OldBookID Then Return "yes"
            If TMP.OldBookingOrder <> Me.OldBookingOrder Then Return "yes"

            Return "no"
        Catch ex As Exception
            DBMS.Rollback()
            Return "error"
        End Try
    End Function


    ' this function is used to edit the record in the db
    Public Function UpdateDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try
            ' first lock this record
            DBMS.ExecuteSQL("update queue set bookid=@0, studentid=@1 where bookid=@0 and studentid=@1", Me.OldBookID, Me.OldStudentID)

            ' check if record was changed
            Dim R = Me.WasRecordChanged(DBMS)
            If R = "error" Or R = "yes" Then
                DBMS.Rollback()
                Return False
            End If

            ' update the db
            DBMS.ExecuteSQL("update queue set bookid      =@0 where bookid=@1 and studentid=@2", Me.BookID, Me.OldBookID, Me.OldStudentID)
            DBMS.ExecuteSQL("update queue set studentid   =@0 where bookid=@1 and studentid=@2", Me.StudentID, Me.BookID, Me.OldStudentID)
            DBMS.ExecuteSQL("update queue set bookingorder=@0 where bookid=@1 and studentid=@2", Me.BookingOrder, Me.BookID, Me.StudentID)

            ' commit the changes
            If Commit Then
                DBMS.Commit()
            End If

            ' set old values = new
            Me.OldBookID = Me.BookID
            Me.OldStudentID = Me.StudentID
            Me.OldBookingOrder = Me.BookingOrder

            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    ' this function is used to remove the object from db
    Public Function RemoveFromDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try
            ' first lock this record
            DBMS.ExecuteSQL("update queue set bookid=@0 where bookid=@0 and studentid=@1", Me.OldBookID, Me.OldStudentID)

            ' check if record was changed
            Dim R = Me.WasRecordChanged(DBMS)
            If R = "error" Or R = "yes" Then
                DBMS.Rollback()
                Return False
            End If

            ' remove the record
            DBMS.ExecuteSQL("delete from queue where studentid=@0 and bookid=@1", Me.OldStudentID, Me.OldBookID)

            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' this method is used to insert new queue entry
    ''' </summary>
    ''' <param name="DBMS"></param>
    ''' <param name="BookBarcode"></param>
    ''' <param name="StudentID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertQueueEntry(ByVal DBMS As DBMSClass, ByVal BookBarcode As String, ByVal StudentID As String) As Boolean
        Try
            Dim BookID As String = ""
            If Not BookClass.GetBookIDFromBarcode(DBMS, BookBarcode, BookID) Then
                DBMS.Rollback()
                Return False
            End If

            Dim BookingOrder = CommonDBOperations.GetNextVal

            Dim QueueOBJ As New QueueClass
            QueueOBJ.SetBookID(BookID)
            QueueOBJ.SetBookingOrder(BookingOrder)
            QueueOBJ.SetStudentID(StudentID)

            If Not QueueOBJ.InsertIntoDB(DBMS, True) Then
                DBMS.Rollback()
                Return False
            End If

            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' this method is used to insert new queue entry
    ''' </summary>
    ''' <param name="DBMS"></param>
    ''' <param name="BookBarcode"></param>
    ''' <param name="StudentID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RemoveQueueEntry(ByVal DBMS As DBMSClass, ByVal BookBarcode As String, ByVal StudentID As String) As Boolean
        Try
            Dim BookID As String = ""
            If Not BookClass.GetBookIDFromBarcode(DBMS, BookBarcode, BookID) Then
                DBMS.Rollback()
                Return False
            End If

            DBMS.ExecuteSQL("delete from queue where studentid=@0 and bookid=@1", StudentID, BookID)


            DBMS.Commit()
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function


    ' this function is used to fill the dgv with books info
    Public Shared Function FillDGVWithQueueInformation(ByVal DGV As DataGridView, ByVal DBMS As DBMSClass, ByVal BookID As String) As Boolean
        Try
            ' display all the staff information in the data grid view
            If Not DBMS.FillDataGridViewWithData(DGV, "select Students.StudentID,StudentName,StudentIDENT from queue,students where queue.studentid=students.studentid  and bookid=@0 order by bookingorder", BookID) Then
                Return False
            End If
            DGV.Columns("StudentName").HeaderText = "Studnet Name"
            DGV.Columns("StudentIDENT").HeaderText = "Identification"

            DGV.Columns("StudentID").Visible = False
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function


End Class
