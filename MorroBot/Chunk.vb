Public Class Chunk
    Inherits BlockSet

    Public Property World As WorldManager
    Public Property X As Integer
    Public Property Y As Short
    Public Property Z As Integer
    Public Property SizeX As Integer
    Public Property SizeY As Integer
    Public Property SizeZ As Integer

    'Public ReadOnly Property AdjacentChunks() As Chunk

    '    Get

    '        Dim _x As Integer = X >> 4
    '        Dim _y As Integer = Y >> 4
    '        Return New Chunk() {World(X + 1, Y), World(X - 1, Y), World(X, Y + 1), World(X, Y - 1)}

    '    End Get

    'End Property

    Public Sub New(ByVal x As Integer, ByVal y As Short, ByVal z As Integer, ByVal sx As Byte, ByVal sy As Byte, ByVal sz As Byte)

        Me.X = x
        Me.Y = y
        Me.Z = z
        SizeX = sx
        SizeY = sy
        SizeZ = sz

    End Sub

    Public Sub New(ByVal x As Integer, ByVal z As Integer)

        Me.New(x, 0, z, 16, 128, 16)

    End Sub

End Class
