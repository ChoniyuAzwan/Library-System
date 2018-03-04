Public Class FineClass

    Private StudentID As Integer
    Private BookID As String
    Private Details As String
    Private FineAmount As Decimal
    Private Status As String

    Public Function GetStudentID() As String
        Return StudentID
    End Function

    Public Function GetBookID() As String
        Return BookID
    End Function

    Public Function GetDetails() As String
        Return Details
    End Function

    Public Function GetFineAmount() As Decimal
        Return FineAmount
    End Function

    Public Function GetStatus() As String
        Return Status
    End Function

    Public Sub SetStudentID(ByVal ID As Long)
        Me.StudentID = ID
    End Sub

    Public Sub SetBookID(ByVal ID As String)
        Me.BookID = ID
    End Sub

    Public Sub SetDetails(ByVal DTL As String)
        Me.Details = DTL
    End Sub

    Public Sub SetFineAmount(ByVal AM As Decimal)
        Me.FineAmount = AM
    End Sub

    Public Sub SetStatus(ByVal ST As String)
        Me.Status = ST
    End Sub

    Public Function InsertIntoDB(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try
            DBMS.ExecuteSQL("insert into fines(studentid,bookid,details,fineamount,status) vales (@0,@1,@2,@3,@4)", Me.StudentID, Me.BookID, Me.Details, Me.FineAmount, Me.Status)
            If Commit Then
                DBMS.Commit()
            End If
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

    Public Function LoadFromDB(ByVal DBMS As DBMSClass, ByVal STID As String, ByVal BKID As String) As Boolean
        Try
            Dim S As String = DBMS.CreateResultSet("select * from fines where studentid=@0 and bookid=@1", STID, BKID)
            If Not DBMS.ReadAndNotEOF(S) Then
                DBMS.CloseResultSet(S)
                DBMS.Rollback()
                Return False
            End If

            Me.StudentID = DBMS.GetColumnValue(S, "studentid")
            Me.BookID = DBMS.GetColumnValue(S, "bookid")
            Me.Details = DBMS.GetColumnValue(S, "details")
            Me.FineAmount = DBMS.GetColumnValue(S, "fineamount")
            Me.Status = DBMS.GetColumnValue(S, "status")

            DBMS.CloseResultSet(S)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            DBMS.Rollback()
            Return False
        End Try
    End Function

    Public Shared Function SetFineStatus(ByVal DBMS As DBMSClass, ByVal STID As String, ByVal BKID As String, ByVal Status As String, ByVal Commit As Boolean) As Boolean
        Try
            DBMS.ExecuteSQL("update fines set status=@0 where studnetid=@1 and bookid=@2", Status, STID, BKID)
            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function


    Public Shared Function CalculateFinesForAllStudents(ByVal DBMS As DBMSClass, ByVal Commit As Boolean) As Boolean
        Try

            Dim LastCalc As String = ""

            Dim S As String = DBMS.CreateResultSet("select * from vars where varname=@0", "calc")
            If DBMS.ReadAndNotEOF(S) Then
                LastCalc = DBMS.GetColumnValue(S, "varvalue")
            Else
                DBMS.ExecuteSQL("insert into vars values (@0,@1)", "calc", Format(Now.AddDays(-1), "yyyy-MM-dd"))
                LastCalc = Format(Now.AddDays(-1), "yyyy-MM-dd")
            End If
            DBMS.CloseResultSet(S)

            Dim CurrentCalc As String = Format(Now, "yyyy-MM-dd")

            If CurrentCalc = LastCalc Then
                Return True
            End If

            ' start looping on borrow table
            S = DBMS.CreateResultSet("select studentid,Bookid from borrow")
            Dim TMP As New BorrowClass
            Do While DBMS.ReadAndNotEOF(S)

                Dim STID As String = DBMS.GetColumnValue(S, "studentid")
                Dim BKID As String = DBMS.GetColumnValue(S, "Bookid")

                If Not TMP.LoadBorrowByStudentIDAndBookID(DBMS, STID, BKID) Then
                    DBMS.CloseResultSet(S)
                    DBMS.Rollback()
                    Return False
                End If

                If TMP.GetReturnDate < CurrentCalc Then

                    ' update the fines calculated for the student
                    Dim Y1 As String = TMP.GetReturnDate.Substring(0, 4)
                    Dim M1 As String = TMP.GetReturnDate.Substring(5, 2)
                    Dim D1 As String = TMP.GetReturnDate.Substring(8, 2)

                    Dim Y2 As String = CurrentCalc.Substring(0, 4)
                    Dim M2 As String = CurrentCalc.Substring(5, 2)
                    Dim D2 As String = CurrentCalc.Substring(8, 2)

                    Dim DT1 As New Date(Y1, M1, D1)
                    Dim DT2 As New Date(Y2, M2, D2)

                    Dim DayDiff = DT2 - DT1
                    Dim Fine As Decimal = DayDiff.Days * 1

                    DBMS.ExecuteSQL("delete from fines where studentid=@0 and bookid=@1", STID, BKID)
                    DBMS.ExecuteSQL("insert into fines (studentid,bookid,details,fineamount,status) values (@0,@1,@2,@3,@4)", STID, BKID, "overdue", Fine, "unpaid")


                End If

            Loop
            DBMS.CloseResultSet(S)


            DBMS.ExecuteSQL("update vars set varvalue=@0 where varname=@1", CurrentCalc, "calc")


            If Commit Then
                DBMS.Commit()
            End If
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function


    ' this function is used to fill the dgv with fines
    Public Shared Function FillDGVWithFinesForStudent(ByVal DGV As DataGridView, ByVal DBMS As DBMSClass, ByVal StudentID As String) As Boolean
        Try
            ' display all the staff information in the data grid view
            If Not DBMS.FillDataGridViewWithData(DGV, "select * from StudentFines where StudentID=@0", StudentID) Then
                Return False
            End If
            DGV.Columns("BookTitle").HeaderText = "Title"
            DGV.Columns("Barcode").HeaderText = "Barcode"
            DGV.Columns("Details").HeaderText = "Details"
            DGV.Columns("FineAmount").HeaderText = "Amount Due"
            DGV.Columns("Status").HeaderText = "Status"

            DGV.Columns("StudnetID").Visible = False
            Return True
        Catch ex As Exception
            DBMS.Rollback()
            Return False
        End Try
    End Function

End Class
