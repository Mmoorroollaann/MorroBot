Public Class MetaData

    Private Data As Dictionary(Of Int32, Object) = New Dictionary(Of Int32, Object)

    Public Property Sheared() As Boolean

        Get
            Return (CByte(Data(16)) And &H10) <> 0
        End Get

        Set(ByVal value As Boolean)
            Data(16) = If(Data.ContainsKey(16), (CByte(Data(16)) And &HEF) Or (If(value, &H10, 0)), (If(value, &H10, 0)))
        End Set

    End Property

    Public Property WoolColor As WoolColor

        Get
            Return DirectCast(CByte(CByte(Data(16)) And &HF), WoolColor)
        End Get

        Set(ByVal value As WoolColor)
            Data(16) = If(Data.ContainsKey(16), (CByte(Data(16)) And &HF) Or CByte(value), CByte(value))
        End Set
    End Property

    Public Property IsOnFire() As Boolean

        Get
            Return (CByte(Data(0)) And &H1) <> 0
        End Get

        Set(ByVal value As Boolean)
            Data(0) = CByte(CByte(Data(0)) And &HFE Or (If(value, &H1, 0)))
        End Set

    End Property

    Public Property IsCrouched() As Boolean

        Get
            Return (CByte(Data(0)) And &H2) <> 0
        End Get

        Set(ByVal value As Boolean)
            Data(0) = CByte(CByte(Data(0)) And &HFD Or (If(value, &H2, 0)))
        End Set

    End Property

    Public Property IsRiding() As Boolean

        Get
            Return (CByte(Data(0)) And &H4) <> 0
        End Get

        Set(ByVal value As Boolean)
            Data(0) = CByte(CByte(Data(0)) And &HFB Or (If(value, &H4, 0)))
        End Set

    End Property


    Public Sub New()

        If Not Data.ContainsKey(0) Then
            Data.Add(0, CByte(0))
        End If

    End Sub

    Public Sub New(ByVal rx As BigEndianStream)

        Dim x As Byte

        Do While True
            x = CByte(rx.ReadByte)
            If x = &H7F Then Exit Do

            Select Case x >> 5
                Case 0
                    Data(x And &H1F) = rx.ReadByte()
                    Exit Select
                Case 1
                    Data(x And &H1F) = rx.ReadShort()
                    Exit Select
                Case 2
                    Data(x And &H1F) = rx.ReadInt()
                    Exit Select
                Case 3
                    Data(x And &H1F) = rx.ReadSingle()
                    Exit Select
                Case 4
                    Data(x And &H1F) = rx.ReadString16(64)
                    Exit Select
                Case Else
                    Data(x And &H1F) = Nothing
                    Exit Select
            End Select

        Loop


    End Sub

    Public Sub Write(ByVal tx As BigEndianStream)

        For Each k As Integer In Data.Values

            Dim type As Type = Data(k).GetType

            If type = GetType(Byte) Then
                tx.WriteByte(CByte(k))
                tx.Write(CByte(Data(k)))
            ElseIf type = GetType(Short) Then
                tx.WriteByte(CByte(&H20 Or k))
                tx.Write(CShort(Data(k)))
            ElseIf type = GetType(Integer) Then
                tx.WriteByte(CByte(&H40 Or k))
                tx.Write(CInt(Data(k)))
            ElseIf type = GetType(Single) Then
                tx.WriteByte(CByte(&H60 Or k))
                tx.Write(CSng(Data(k)))
            ElseIf type = GetType(String) Then
                tx.WriteByte(CByte(&H80 Or k))
                tx.Write(DirectCast(Data(k), String))
            End If

        Next

        tx.WriteByte(&H7F)

    End Sub


    Public Enum Wood As Byte
        Normal
        Redwood
        Birch
    End Enum

    Public Enum Slab As Byte
        Stone
        Sandstone
        Wooden
        Cobblestone
    End Enum

    Public Enum Liquid As Byte
        Full = &H0
        LavaMax = &H3
        WaterMax = &H7
        Falling = &H8
    End Enum

    Public Enum Wool As Byte
        White
        Orange
        Magenta
        LightBlue
        Yellow
        LightGreen
        Pink
        Gray
        LightGray
        Cyan
        Purple
        Blue
        Brown
        DarkGreen
        Red
        Black
    End Enum

    Public Enum Dyes As Byte
        InkSack
        RoseRed
        CactusGreen
        CocoBeans
        LapisLazuli
        Purple
        Cyan
        LightGray
        Gray
        Pink
        Lime
        DandelionYellow
        LightBlue
        Magenta
        Orange
        BoneMeal
    End Enum

    Public Enum Torch As Byte
        South = &H1
        North
        East
        West
        Standing
    End Enum

    Public Enum Bed As Byte
        West
        North
        East
        South
        BedFoot
    End Enum

    Public Enum Tracks As Byte
        EastWest
        NorthSouth
        RiseSouth
        RiseNorth
        RiseEast
        RiseWest
        NECorner
        SECorner
        SWCorner
        NWCorner
    End Enum

    Public Enum Ladders As Byte
        East = 2
        West
        North
        South
    End Enum

    Public Enum Stairs As Byte
        South
        North
        West
        East
    End Enum

    Public Enum Lever As Byte
        SouthWall = 1
        NorthWall
        WestWall
        EastWall
        EWGround
        NSGround
        IsFlipped = 8
    End Enum

    Public Enum Door As Byte
        Northeast
        Southeast
        Southwest
        Northwest
        IsOpen = 4
        IsTopHalf = 8
    End Enum

    Public Enum Button As Byte
        SouthWall = &H1
        NorthWall
        WestWall
        EastWall
        IsPressed = &H8
    End Enum

    Public Enum SignPost As Byte
        West = &H0
        WestNorthwest
        Northwest
        NorthNorthwest
        North
        NorthNortheast
        Northeast
        EastNortheast
        East
        EastSoutheast
        Southeast
        SouthSoutheast
        South
        SouthSouthwest
        Southwest
        WestSouthwest
    End Enum

    Public Enum SignWall As Byte
        East = &H2
        West
        North
        South
    End Enum

    Public Enum Dispenser As Byte
        East = &H2
        West
        North
        South
    End Enum

    Public Enum Furnace As Byte
        East = &H2
        West
        North
        South
    End Enum

    Public Enum Pumpkin As Byte
        East
        South
        West
        North
    End Enum

    Public Enum RedstoneRepeater As Byte
        East
        South
        West
        North
        Tick1
        Tick2
        Tick3
        Tick4
    End Enum

End Class
