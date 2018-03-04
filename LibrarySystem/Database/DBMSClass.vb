' THIS CLASS WILL USED TO MANAGE CONNECTIVITY WITH THE DATABASE
Imports System.Data.SqlClient

Public Class DBMSClass
    ' DEFINE THE CONNECTION STRING
    Private DBMSConnectionstring = "Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True; MultipleActiveResultSets=true"

    ' DEFINE ThE CONNECTION
    Private DBMSConnectionObj As New SqlConnection

    ' DEFINE THE TRANSACTION
    Private DBMSTransactionObj As SqlTransaction

    ' DEFINE THE COMMANDS OBJECT and result sets
    Private DBMSCommands As List(Of SqlCommand)
    Private DBMSCommandCodes As List(Of Long)
    Private DBMSResultSets As List(Of Object)

    ' command counter
    Private DBMSCommandCounter As Long

    ' OPEN DATABASE CONNECTION
    Public Function OpenDB() As String
        Try
            ' OPEN THE CONNECITON
            DBMSConnectionObj = New SqlConnection(DBMSConnectionstring)
            DBMSConnectionObj.Open()

            ' CREATE THE TRANSACTION
            DBMSTransactionObj = DBMSConnectionObj.BeginTransaction

            ' PREPARE THE COMMANDS LIST
            DBMSCommands = New List(Of SqlCommand)
            DBMSCommandCodes = New List(Of Long)
            DBMSResultSets = New List(Of Object)

            ' prepare the command counter
            DBMSCommandCounter = 0

            ' RETURN OK
            Return "OK"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ' this is used to run sql commands
    Public Sub ExecuteSQL(ByVal SQL As String, ByVal ParamArray Obj() As Object)

        ' build the command object
        Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, Me.DBMSConnectionObj, Me.DBMSTransactionObj)

        ' add the parameters to the sql command
        Dim I As Integer
        For I = 0 To Obj.Length - 1
            CMD.Parameters.AddWithValue("@" & I, Obj(I))
        Next

        ' run the sql
        CMD.ExecuteNonQuery()

    End Sub

    ' this function is used to commit a transaction
    Public Sub Commit()
        Me.DBMSTransactionObj.Commit()
        Me.DBMSTransactionObj = Me.DBMSConnectionObj.BeginTransaction
    End Sub

    ' this function is used to rollback a transaction
    Public Sub Rollback()
        Me.DBMSTransactionObj.Rollback()
        Me.DBMSTransactionObj = Me.DBMSConnectionObj.BeginTransaction
    End Sub

    ' this function is used to create a result set
    Public Function CreateResultSet(ByVal SQL As String, ByVal ParamArray OBJ() As Object) As Long
        DBMSCommandCounter += 1

        ' build the command object
        Dim CMD As New System.Data.SqlClient.SqlCommand(SQL, Me.DBMSConnectionObj, Me.DBMSTransactionObj)

        ' add the parameters to the sql command
        Dim I As Integer
        For I = 0 To OBJ.Length - 1
            CMD.Parameters.AddWithValue("@" & I, OBJ(I))
        Next

        ' read the data
        Dim RS = CMD.ExecuteReader(CommandBehavior.Default)

        ' store objects in list
        Me.DBMSCommandCodes.Add(DBMSCommandCounter)
        Me.DBMSCommands.Add(CMD)
        Me.DBMSResultSets.Add(RS)

        Return DBMSCommandCounter
    End Function

    ' this function is used to close a result set
    Public Sub CloseResultSet(ByVal Nmbr As Long)
        Dim I As Integer
        For I = 0 To Me.DBMSCommandCodes.Count - 1

            ' find the command and result set
            If DBMSCommandCodes(I) = Nmbr Then

                ' get the objects
                Dim R = Me.DBMSResultSets(I)
                Dim C = Me.DBMSCommands(I)

                ' remove the objects from the list
                Me.DBMSResultSets.RemoveAt(I)
                Me.DBMSCommands.RemoveAt(I)
                Me.DBMSCommandCodes.RemoveAt(I)

                ' return the resources
                R.Close()
                R.Dispose()
                C.Dispose()

                Return

            End If
        Next

        Throw New Exception("the command or result set does not exist")
    End Sub

    ' this function is used to read a single record from db
    Public Function ReadAndNotEOF(ByVal Code As Long) As Boolean
        ' do a search
        Dim I As Long
        For i = 0 To Me.DBMSCommandCodes.Count - 1
            If DBMSCommandCodes(i) = Code Then
                Return DBMSResultSets(i).Read
            End If
        Next
        Throw New Exception("Command or Resultset does not exist")
    End Function

    ''' <summary>
    ''' this function gets the number of columns
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumnCount(ByVal Code As Long) As Integer
        ' do a search
        Dim I As Long
        For I = 0 To Me.DBMSCommandCodes.Count - 1
            If DBMSCommandCodes(I) = Code Then
                Dim TMP As System.Data.SqlClient.SqlDataReader = DBMSResultSets(I)
                Return TMP.FieldCount
            End If
        Next
        Throw New Exception("Command or Resultset does not exist")
    End Function

    ' this function is used to get a column value from db
    Public Function GetColumnValueByNo(ByVal Code As Long, ByVal ColumnID As Integer) As Object
        Dim I As Long
        For I = 0 To Me.DBMSCommands.Count - 1
            If DBMSCommandCodes(I) = Code Then
                Dim TMP As System.Data.SqlClient.SqlDataReader = DBMSResultSets(I)
                Return TMP.Item(ColumnID)
            End If
        Next
        Throw New Exception("Command or Resultset does not exist")
    End Function


    ' this function is used to get a column value from db
    Public Function GetColumnValue(ByVal Code As Long, ByVal ColumnName As String) As Object
        Dim I As Long
        For I = 0 To Me.DBMSCommands.Count - 1
            If DBMSCommandCodes(I) = Code Then
                Return DBMSResultSets(I).Item(ColumnName)
            End If
        Next
        Throw New Exception("Command or Resultset does not exist")
    End Function

    ' this function is used to fill the datagridview with data
    Public Function FillDataGridViewWithData(ByVal DGV As DataGridView, ByVal SQL As String, ByVal ParamArray SqlParams() As Object) As Boolean
        Try
            ' create the command object
            Dim TmpCMD As New System.Data.SqlClient.SqlCommand
            TmpCMD.CommandText = SQL
            TmpCMD.Connection = Me.DBMSConnectionObj
            TmpCMD.Transaction = Me.DBMSTransactionObj

            Dim I As Integer
            For I = 0 To SqlParams.Count - 1
                TmpCMD.Parameters.AddWithValue("@" & I, SqlParams(I))
            Next


            ' create a data adapter
            Dim TmpDataAdapter As New System.Data.SqlClient.SqlDataAdapter(TmpCMD)

            ' next fill the datatable
            Dim TmpDataTable As New DataTable
            TmpDataAdapter.Fill(TmpDataTable)

            ' next create a binding source
            Dim TmpBindingSouce As New BindingSource
            TmpBindingSouce.DataSource = TmpDataTable

            ' display the info
            DGV.DataSource = TmpBindingSouce

            ' destroy the data adapter
            TmpDataAdapter.Dispose()
            TmpCMD.Dispose()

            Return True
        Catch ex As Exception
            Me.Rollback()
            DGV.DataSource = Nothing
            Return False
        End Try
    End Function

    ' this function is used to close db
    Public Function CloseDB() As Boolean
        Try
            Me.Rollback()
            For Each rs In DBMSResultSets
                rs.Close()
                rs.Dispose()
            Next
            For Each cmd In DBMSCommands
                cmd.Dispose()
            Next
            DBMSTransactionObj.Dispose()
            DBMSConnectionObj.Close()
            DBMSConnectionObj.Dispose()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
