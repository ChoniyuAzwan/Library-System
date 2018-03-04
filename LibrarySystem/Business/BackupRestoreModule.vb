''' <summary>
''' this module is used to backup and restore the database
''' </summary>
''' <remarks></remarks>
Public Module BackupRestoreModule

    ''' <summary>
    ''' backup the db
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks></remarks>
    Public Sub BackupDB(ByVal FileName As String, ByVal DBMS As DBMSClass)
        Try
            ' create list of tables
            Dim TableList As New List(Of String)

            TableList.Add("books")
            TableList.Add("borrow")
            TableList.Add("fines")
            TableList.Add("payments")
            TableList.Add("queue")
            TableList.Add("shelves")
            TableList.Add("staff")
            TableList.Add("students")
            TableList.Add("vars")


            Dim I As Integer
            'For I = 0 To TableList.Count - 1
            '    DBMS.ExecuteSQL("select * from " & TableList(I) & " for update")
            'Next

            Dim BackupFileStream As New System.IO.FileStream(FileName, IO.FileMode.Create)
            For I = 0 To TableList.Count - 1
                BackupTable(BackupFileStream, TableList(I), DBMS)
            Next

            BackupFileStream.Close()
            DBMS.Commit()
            MsgBox("Backup data successfully", vbInformation)
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            DBMS.Rollback()
        End Try

     


    End Sub


    ''' <summary>
    ''' restore the db
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks></remarks>
    Public Sub RestoreDB(ByVal FileName As String, ByVal DBMS As DBMSClass)

        Try
            ' create list of tables
            Dim TableList As New List(Of String)

            TableList.Add("books")
            TableList.Add("borrow")
            TableList.Add("fines")
            TableList.Add("payments")
            TableList.Add("queue")
            TableList.Add("shelves")
            TableList.Add("staff")
            TableList.Add("students")
            TableList.Add("vars")


            Dim I As Integer
            For I = 0 To TableList.Count - 1
                DBMS.ExecuteSQL("delete from  " & TableList(I) & "")
            Next

            Dim BackupFileStream As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            For I = 0 To TableList.Count - 1
                RestoreTable(BackupFileStream, TableList(I), DBMS)
            Next

            BackupFileStream.Close()
            DBMS.Commit()
            MsgBox("Restore data successfully", vbInformation)
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            DBMS.Rollback()
        End Try

    End Sub

    Public Sub AddValueToFile(ByVal BackupFileStream As System.IO.FileStream, ByVal Val As String)
        Dim B() As Byte = System.Text.Encoding.GetEncoding("windows-1256").GetBytes(Val)
        Dim B1 As Byte
        Dim B2 As Byte
        Dim B3 As Byte
        Dim B4 As Byte
        B1 = B.Length Mod 256
        B2 = (B.Length \ 256) Mod 256
        B3 = (B.Length \ (256 * 256)) Mod 256
        B4 = (B.Length \ (256 * 256 * 256)) Mod 256

        BackupFileStream.WriteByte(B1)
        BackupFileStream.WriteByte(B2)
        BackupFileStream.WriteByte(B3)
        BackupFileStream.WriteByte(B4)
        BackupFileStream.Write(B, 0, B.Length)
    End Sub


    Public Function GetValueFromFile(ByVal BackupFileStream As System.IO.FileStream) As String

        Dim B1 As Byte
        Dim B2 As Byte
        Dim B3 As Byte
        Dim B4 As Byte
        B1 = BackupFileStream.ReadByte
        B2 = BackupFileStream.ReadByte
        B3 = BackupFileStream.ReadByte
        B4 = BackupFileStream.ReadByte

        Dim STRLEN As Integer = B1 + B2 * 256.0 + B3 * 256 * 256 + B4 * 256 * 256 * 256

        Dim B(0 To STRLEN - 1) As Byte

        Dim Start As Integer = 0
        Do While STRLEN > 0
            Dim BR = BackupFileStream.Read(B, Start, STRLEN)
            Start = Start + BR
            STRLEN = STRLEN - BR
        Loop

        Dim S As String = System.Text.Encoding.GetEncoding("windows-1256").GetString(B)
        Return S
    End Function

    Public Sub RestoreRow(ByVal BackupFileStream As System.IO.FileStream, ByVal ColumnCount As Long, ByVal TableName As String, ByVal DBMS As DBMSClass)
        Dim p(0 To ColumnCount - 1) As Object
        Dim I As Integer
        Dim ParameterNames As String = ""
        For I = 0 To ColumnCount - 1
            p(I) = GetValueFromFile(BackupFileStream)
            ParameterNames = ParameterNames & "@" & I.ToString
            If I < ColumnCount - 1 Then
                ParameterNames = ParameterNames & ","
            End If
        Next

        Dim SQL As String = "insert into " & TableName & " values (" & ParameterNames & ")"

        DBMS.ExecuteSQL(SQL, p)
    End Sub


    Public Sub BackupRow(ByVal BackupFileStream As System.IO.FileStream, ByVal Code As Long, ByVal DBMS As DBMSClass)
        Dim I As Integer
        For I = 0 To DBMS.GetColumnCount(Code) - 1
            Dim V As String = DBMS.GetColumnValueByNo(Code, I) & ""
            AddValueToFile(BackupFileStream, V)
        Next
    End Sub



    Public Sub RestoreTable(ByVal BackupFileStream As System.IO.FileStream, ByVal TableName As String, ByVal DBMS As DBMSClass)
        Dim TestTableName As String = GetValueFromFile(BackupFileStream)
        If TestTableName <> TableName Then
            Throw New Exception("error in backup file")
        End If

        Dim S As String = DBMS.CreateResultSet("select * from " & TableName & " where 1=2")
        Dim ColumnCount As Integer = DBMS.GetColumnCount(S)
        DBMS.CloseResultSet(S)

        Do While True
            Dim B As Byte = BackupFileStream.ReadByte
            If B = 0 Then
                Exit Do
            End If

            RestoreRow(BackupFileStream, ColumnCount, TableName, DBMS)
        Loop



    End Sub


    Public Sub BackupTable(ByVal BackupFileStream As System.IO.FileStream, ByVal TableName As String, ByVal DBMS As DBMSClass)

        ' adds the table name to file
        AddValueToFile(BackupFileStream, TableName)


        Dim S As String = DBMS.CreateResultSet("select * from " & TableName)
        Do While DBMS.ReadAndNotEOF(S)
            BackupFileStream.WriteByte(1)
            BackupRow(BackupFileStream, S, DBMS)
        Loop
        BackupFileStream.WriteByte(0)

    End Sub


End Module
