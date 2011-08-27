Public Class BlockSet

    Public Const SIZE As Integer = 16 * 16 * 128
    Public Delegate Sub ForEachBlock(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer)

    Protected Types() As Byte = New Byte(SIZE) {}
    Protected SkyLight() As Byte = New Byte(SIZE) {}
    Protected BlockLight() As Byte = New Byte(SIZE) {}
    Protected Data() As Byte = New Byte(SIZE) {}

    Default Public Property Item(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Get
            Return Types(Translate(x, y, z))
        End Get

        Set(ByVal value As Byte)
            Types(Translate(x, y, z)) = value
        End Set

    End Property

    Private Function Translate(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Integer

        Return x << 11 Or z << 7 Or y

    End Function

    Public Function GetBlockLight(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Return BlockLight(Translate(x, y, z))

    End Function

    Public Function GetSkyLight(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Return SkyLight(Translate(x, y, z))

    End Function

    Public Function GetData(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Return Data(Translate(x, y, z))

    End Function

    Public Function GetTotalLight(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Dim sky As Byte = GetSkyLight(x, y, z)
        Dim block As Byte = GetBlockLight(x, y, z)
        Return If(sky > block, sky, block)

    End Function

    Public Function GetLuminence(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Return BlockData.Luminence(Me(x, y, z))

    End Function

    Public Function GetOpacity(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Byte

        Return BlockData.Opacity(Me(x, y, z))

    End Function


    Public Sub SetBlockLight(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal value As Byte)

        Dim i As Integer = Translate(x, y, z)
        BlockLight(i) = value

    End Sub

    Public Sub SetSkyLight(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal value As Byte)

        Dim i As Integer = Translate(x, y, z)
        SkyLight(i) = value

    End Sub

    Public Sub SetData(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal value As Byte)

        Dim i As Integer = Translate(x, y, z)
        Data(i) = value

    End Sub

    Public Sub SetAllBlocks(ByVal Data() As Byte)

        Types = Data

    End Sub

    Public Sub SetType(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal Value As BlockData.Blocks)

        Me(x, y, z) = CByte(Value)

    End Sub


    Public Sub ForAdjacentSameChung(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal predicate As ForEachBlock)

        If x > 0 Then
            predicate(x - 1, y, z)
        ElseIf x < 15 Then
            predicate(x + 1, y, z)
        ElseIf y > 0 Then
            predicate(x, y - 1, z)
        ElseIf y < 127 Then
            predicate(x, y + 1, z)
        ElseIf z > 0 Then
            predicate(x, y, z - 1)
        ElseIf z < 15 Then
            predicate(x, y, z + 1)
        End If

    End Sub

    Public Sub ForEach(ByVal predicate As ForEachBlock)

        For x As Integer = 0 To 15
            For z As Integer = 0 To 15
                For y As Integer = 0 To 127
                    predicate(x, y, z)
                Next
            Next
        Next

    End Sub


    Public Overloads Function [GetType](ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As BlockData.Blocks

        Return DirectCast(Me(x, y, z), BlockData.Blocks)

    End Function


    Public Function IsAir(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Boolean

        BlockData.Air.Contains([GetType](x, y, z))

    End Function

End Class
