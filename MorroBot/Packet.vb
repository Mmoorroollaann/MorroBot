Imports System.Reflection
Imports System.Runtime.InteropServices


Public MustInherit Class Packet

    Public MustOverride Sub Read(ByVal Stream As BigEndianStream)
    Public MustOverride Sub Write(ByVal Stream As BigEndianStream)

    Public Sub WriteFlush(ByVal Stream As BigEndianStream)

        Write(Stream)
        Stream.Flush()

    End Sub

    Public Function GetPacketType() As PacketType

        Return (PacketMap.GetPacketType(Me.GetType))

    End Function

    Public Shared Function Read(ByVal Type As PacketType, ByVal Stream As BigEndianStream) As Packet

        Try

            Dim pType As Type = PacketMap.Map(Type)
            Dim packet As Packet = DirectCast(pType.GetConstructor(New Type(-1) {}).Invoke(New Object(-1) {}), Packet)
            packet.Read(Stream)
            Return packet

        Catch ex As Exception

            LogParser.Parse_Errors(ex)
            Return Nothing

        End Try

    End Function

End Class

Public Class KeepAlivePacket
    Inherits Packet

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

    End Sub
End Class

Public Class LoginRequestPacket
    Inherits Packet

    Public Property ProtocolOrEntityID As Integer
    Public Property Username As String
    Public Property MapSeed As Long
    Public Property Dimension As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        ProtocolOrEntityID = Stream.readint()
        Username = Stream.ReadString16(16)
        MapSeed = Stream.ReadLong()
        Dimension = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(ProtocolOrEntityID)
        Stream.Write(Username)
        Stream.Write(MapSeed)
        Stream.Write(Dimension)

    End Sub


End Class

Public Class HandshakePacket
    Inherits Packet

    Public Property UsernameOrHash As String

    Public Overrides Sub Read(ByVal Stream As BigEndianStream)

        UsernameOrHash = Stream.ReadString16(16)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(UsernameOrHash)

    End Sub

End Class

Public Class PlayerPacket
    Inherits Packet

    Public Property OnGround As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        OnGround = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(OnGround)

    End Sub

End Class

Public Class PlayerPositionRotationPacket
    Inherits Packet

    Public Property X As Double
    Public Property Y As Double
    Public Property Stance As Double
    Public Property Z As Double
    Public Property Yaw As Single
    Public Property Pitch As Single
    Public Property OnGround As Boolean


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadDouble
        Stance = Stream.ReadDouble
        Y = Stream.ReadDouble
        Z = Stream.ReadDouble
        Yaw = Stream.ReadSingle
        Pitch = Stream.ReadSingle
        OnGround = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Stance)
        Stream.Write(Z)
        Stream.Write(Yaw)
        Stream.Write(Pitch)
        Stream.Write(OnGround)

    End Sub

End Class

Public Class SpawnPositionPacket
    Inherits Packet

    Public Property X As Integer
    Public Property Y As Integer
    Public Property Z As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Y = Stream.ReadInt
        Z = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)

    End Sub

End Class

Public Class MapDataPacket
    Inherits Packet

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Stream.ReadShort()
        Stream.ReadShort()
        Dim b As Byte = Stream.ReadByte
        For i = 0 To b - 1
            Stream.ReadByte()
        Next

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

    End Sub

End Class

Public Class MobSpawnPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Type As MobType
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    Public Property Yaw As SByte
    Public Property Pitch As SByte
    Public Property Data As MetaData

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Type = DirectCast(CByte(Stream.ReadByte), MobType)

        X = CDbl(Stream.ReadInt()) / 32.0
        Y = CDbl(Stream.ReadInt()) / 32.0
        Z = CDbl(Stream.ReadInt()) / 32.0
        Yaw = Stream.ReadSByte()
        Pitch = Stream.ReadSByte()
        Data = Stream.ReadMetaData()

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(CByte(Type))
        Stream.Write(CInt(X * 32))
        Stream.Write(CInt(Y * 32))
        Stream.Write(CInt(Z * 32))
        Stream.Write(Yaw)
        Stream.Write(Pitch)
        Stream.Write(Data)

    End Sub

End Class

Public Class EntityVelocityPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property VelocityX As Short
    Public Property VelocityY As Short
    Public Property VelocityZ As Short

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        VelocityX = Stream.ReadShort
        VelocityY = Stream.ReadShort
        VelocityZ = Stream.ReadShort

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(VelocityX)
        Stream.Write(VelocityY)
        Stream.Write(VelocityZ)

    End Sub

End Class

Public Class EntityMetaDataPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Data As MetaData

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Data = Stream.ReadMetaData()

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(Data)

    End Sub


End Class

Public Class AddObjectVehiclePacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Type As SByte
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    Public Property UnknownFlag As Integer
    Public Property Unknown1 As Short
    Public Property Unknown2 As Short
    Public Property Unknown3 As Short


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Type = Stream.ReadSByte
        X = CDbl(Stream.ReadInt) / 32
        Y = CDbl(Stream.ReadInt) / 32
        Z = CDbl(Stream.ReadInt) / 32
        UnknownFlag = Stream.ReadInt
        If UnknownFlag > 0 Then
            Unknown1 = Stream.ReadShort
            Unknown2 = Stream.ReadShort
            Unknown3 = Stream.ReadShort
        End If

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(Type)
        Stream.Write(CInt(X * 32))
        Stream.Write(CInt(Y * 32))
        Stream.Write(CInt(Z * 32))
        Stream.Write(UnknownFlag)
        If UnknownFlag > 0 Then
            Stream.Write(Unknown1)
            Stream.Write(Unknown2)
            Stream.Write(Unknown3)
        End If

    End Sub

End Class

Public Class AnimationPacket
    Inherits Packet

    Public Property PlayerID As Integer
    Public Property Animation As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        PlayerID = Stream.ReadInt
        Animation = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(PlayerID)
        Stream.Write(Animation)

    End Sub

End Class

Public Class AttachEntityPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property VehicleID As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        VehicleID = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(VehicleID)

    End Sub

End Class

Public Class BlockChangePacket
    Inherits Packet

    Public Property X As Integer
    Public Property Y As SByte
    Public Property Z As Integer
    Public Property Type As Byte
    Public Property Data As Byte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt()
        Y = Stream.ReadSByte
        Z = Stream.ReadInt
        Type = Stream.ReadByte
        Data = Stream.ReadByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(Type)
        Stream.Write(Data)

    End Sub


End Class

Public Class ChatMessagePacket
    Inherits Packet

    Public Property Message As String

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Message = Stream.ReadString16(100)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Message)

    End Sub

End Class

Public Class CloseWindowPacket
    Inherits Packet

    Public Property WindowID As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        WindowID = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(WindowID)

    End Sub

End Class

Public Class CollectItemPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property PlayerID As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        PlayerID = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(PlayerID)

    End Sub

End Class

Public Class DestroyEntityPacket
    Inherits Packet

    Public Property EntityID As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)

    End Sub

End Class

Public Class DisconnectPacket
    Inherits Packet

    Public Property Reason As String

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Reason = Stream.ReadString16(300)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Reason)

    End Sub

End Class

Public Class DoorChangePacket
    Inherits Packet

    Public Property EffectID As Integer
    Public Property X As Integer
    Public Property Y As Byte
    Public Property Z As Integer
    Public Property SoundData As Integer


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Stream.ReadInt()
        Stream.ReadInt()
        Stream.ReadByte()
        Stream.ReadInt()
        Stream.ReadInt()

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

    End Sub

End Class

Public Class CreateEntityPacket
    Inherits Packet

    Public Property EntityID As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)

    End Sub
End Class

Public Class EntityactionPacket
    Inherits Packet

    Public Property PlayerId As Integer
    Public Property Action As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        PlayerId = Stream.ReadInt
        Action = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(PlayerId)
        Stream.Write(Action)

    End Sub

End Class

Public Class EntityEquipmentPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Slot As Short
    Public Property ItemID As Short
    Public Property Durability As Short

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Slot = Stream.ReadShort
        ItemID = Stream.ReadShort
        Durability = Stream.ReadShort

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(Slot)
        Stream.Write(ItemID)
        Stream.Write(Durability)

    End Sub

End Class

Public Class EntityLookPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Yaw As SByte
    Public Property Pitch As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Yaw = Stream.ReadSByte
        Pitch = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(Yaw)
        Stream.Write(Pitch)

    End Sub

End Class

Public Class EntityLookAndRelativeMovePacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property DeltaX As SByte
    Public Property DeltaY As SByte
    Public Property DeltaZ As SByte
    Public Property Yaw As SByte
    Public Property Pitch As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        DeltaX = Stream.ReadSByte
        DeltaY = Stream.ReadSByte
        DeltaZ = Stream.ReadSByte
        Yaw = Stream.ReadSByte
        Pitch = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(DeltaX)
        Stream.Write(DeltaY)
        Stream.Write(DeltaZ)
        Stream.Write(Yaw)
        Stream.Write(Pitch)

    End Sub

End Class

Public Class EntityPaintingPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Title As String
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Z As Integer
    Public Property GraphicID As Integer

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        Title = Stream.ReadString16(13) '13=Length of longest image title.
        X = Stream.ReadInt
        Y = Stream.ReadInt
        Z = Stream.ReadInt
        GraphicID = Stream.ReadInt

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(Title)
        Stream.Write(X)
        Stream.Write(X)
        Stream.Write(Z)
        Stream.Write(GraphicID)

    End Sub

End Class

Public Class EntityRelativeMovePacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property DeltaX As SByte
    Public Property DeltaY As SByte
    Public Property DeltaZ As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        DeltaX = Stream.ReadSByte
        DeltaY = Stream.ReadSByte
        DeltaZ = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(DeltaX)
        Stream.Write(DeltaY)
        Stream.Write(DeltaZ)

    End Sub

End Class

Public Class EntityStatuspacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property EntityStatus As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        EntityStatus = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(EntityStatus)

    End Sub

End Class

Public Class EntityTeleportPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Z As Integer
    Public Property Yaw As SByte
    Public Property Pitch As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        EntityID = Stream.ReadInt
        X = Stream.ReadInt
        Y = Stream.ReadInt
        Z = Stream.ReadInt
        Yaw = Stream.ReadSByte
        Pitch = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(EntityID)
        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(Yaw)
        Stream.Write(Pitch)

    End Sub

End Class

Public Class ExplosionPacket
    Inherits Packet

    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    Public Property Radius As Single
    Public Property Offsets() As SByte(,)

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadDouble()
        Y = Stream.ReadDouble()
        Z = Stream.ReadDouble()
        Radius = Stream.ReadSingle
        Offsets = New SByte(Stream.ReadInt() - 1, 2) {}

        For i As Integer = 0 To Offsets.GetLength(0) - 1
            Offsets(i, 0) = Stream.ReadSByte
            Offsets(i, 1) = Stream.ReadSByte
            Offsets(i, 2) = Stream.ReadSByte
        Next


    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(Radius)
        Stream.Write(CInt(Offsets.GetLength(0)))
        For i As Integer = 0 To Offsets.GetLength(0) - 1
            Stream.Write(Offsets(i, 0))
            Stream.Write(Offsets(i, 1))
            Stream.Write(Offsets(i, 2))
        Next

    End Sub

End Class

Public Class HoldingChangePacket
    Inherits Packet

    Public Property Slot As Short

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Slot = Stream.ReadShort

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Slot)

    End Sub

End Class

Public Class MultiBlockChangePacket
    Inherits Packet

    Public Property X As Integer
    Public Property Z As Integer
    ' Was not able to use types=new sbyte(length) {} with the properties
    Public Coords() As Short
    Public Types() As SByte
    Public MetaData() As SByte


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Z = Stream.ReadInt
        Dim length As Short = Stream.ReadShort

        Coords = New Short(length - 1) {}
        Types = New SByte(length - 1) {}
        MetaData = New SByte(length - 1) {}

        For i As Integer = 0 To Coords.Length - 1
            Coords(i) = Stream.ReadShort()
        Next

        For i As Integer = 0 To Types.Length - 1
            Types(i) = Stream.ReadSByte()
        Next

        For i As Integer = 0 To MetaData.Length - 1
            MetaData(i) = Stream.ReadSByte()
        Next

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Z)
        Stream.Write(CShort(Coords.Length))

        For i As Integer = 0 To Coords.Length - 1
            Stream.Write(Coords(i))
        Next
        For i As Integer = 0 To Types.Length - 1
            Stream.Write(Types(i))
        Next
        For i As Integer = 0 To MetaData.Length - 1
            Stream.Write(MetaData(i))
        Next

    End Sub

End Class

Public Class NamedEntitySpawnPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property PlayerName As String
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    Public Property Yaw As SByte
    Public Property Pitch As SByte
    Public Property CurrentItem As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        EntityID = stream.ReadInt()
        PlayerName = stream.ReadString16(16)
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
        CurrentItem = stream.ReadShort()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(EntityID)
        stream.Write(PlayerName)
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(CurrentItem)

    End Sub

End Class

Public Class OpenWindowPacket
    Inherits Packet

    Public Property WindowID As SByte
    Friend Property InventoryType As InterfaceType
    Public Property WindowTitle As String
    Public Property SlotCount As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        WindowID = Stream.ReadSByte
        InventoryType = DirectCast(Stream.ReadSByte, InterfaceType)
        WindowTitle = Stream.ReadString8(100)
        SlotCount = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(WindowID)
        Stream.Write(CByte(InventoryType))
        Stream.Write8(WindowTitle)
        Stream.Write(SlotCount)

    End Sub


End Class

Public Class SpawnItemPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property ItemID As Short
    Public Property Count As SByte
    Public Property Durability As Short
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    Public Property Yaw As SByte
    Public Property Pitch As SByte
    Public Property Roll As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        EntityID = stream.ReadInt()
        ItemID = stream.ReadShort
        Count = stream.ReadSByte
        Durability = stream.ReadShort
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
        Roll = stream.ReadSByte()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(EntityID)
        stream.Write(ItemID)
        stream.Write(Count)
        stream.Write(Durability)
        stream.Write(CInt(X * 32))
        stream.Write(CInt(Y * 32))
        stream.Write(CInt(Z * 32))
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(Roll)

    End Sub

End Class

Public Class PlayerBlockPlacementPacket
    Inherits Packet

    Public Property X As Integer
    Public Property Y As SByte
    Public Property Z As Integer
    Public Property Face As BlockFace
    Public Property Item As ItemStack

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Y = Stream.ReadSByte
        Z = Stream.ReadInt
        Face = DirectCast(Stream.ReadSByte, BlockFace)
        Item = New ItemStack(Stream)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(CByte(Face))
        ' Need to insert valid code here to write the item.

    End Sub

End Class

Public Class PlayerDiggingPacket
    Inherits Packet

    Public Property Action As DigAction
    Public Property X As Integer
    Public Property Y As SByte
    Public Property Z As Integer
    Public Property Face As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Action = DirectCast(Stream.ReadByte, DigAction)
        X = Stream.ReadInt
        Y = Stream.ReadSByte
        Z = Stream.ReadInt
        Face = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(CByte(Action))
        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(Face)

    End Sub

End Class

Public Class PlayerPositionPacket
    Inherits Packet

    Public Property X As Double
    Public Property Y As Double
    Public Property Stance As Double
    Public Property Z As Double
    Public Property OnGround As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadDouble
        Y = Stream.ReadDouble
        Stance = Stream.ReadDouble
        Z = Stream.ReadDouble
        OnGround = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Stance)
        Stream.Write(Z)
        Stream.Write(OnGround)

    End Sub


End Class

Public Class PlayerRotationPacket
    Inherits Packet

    Public Property Yaw As Single
    Public Property Pitch As Single
    Public Property OnGround As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Yaw = Stream.ReadSingle
        Pitch = Stream.ReadSingle
        OnGround = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Yaw)
        Stream.Write(Pitch)
        Stream.Write(OnGround)

    End Sub

End Class

Public Class PlayNoteBlockPacket
    Inherits Packet

    Public Property X As Integer
    Public Property Y As SByte
    Public Property Z As Integer
    Public Property Instrument As SByte
    Public Property Pitch As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Y = Stream.ReadSByte
        Z = Stream.ReadInt
        Instrument = Stream.ReadSByte
        Pitch = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(Instrument)
        Stream.Write(Pitch)

    End Sub

End Class

Public Class PreChunkPacket
    Inherits Packet

    Public Property X As Integer
    Public Property Z As Integer
    Public Property Load As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Z = Stream.ReadInt
        Load = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Z)
        Stream.Write(Load)

    End Sub

End Class

Public Class RespawnPacket
    Inherits Packet

    Public Property World As SByte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        World = Stream.ReadSByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(World)

    End Sub


End Class

Public Class SetSlotPacket
    Inherits Packet

    Public Property WindowID As SByte
    Public Property Slot As Short
    Public Property Item As ItemStack

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        WindowID = Stream.ReadSByte
        Slot = Stream.ReadShort
        Item = New ItemStack(Stream)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(WindowID)
        Stream.Write(Slot)
        ' Add proper code to write the item data.

    End Sub

End Class

Public Class TimeUpdatePacket
    Inherits Packet

    Public Property Time As Long

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Time = Stream.ReadLong

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Time)

    End Sub

End Class

Public Class TransactionPacket
    Inherits Packet

    Public Property WindowID As SByte
    Public Property Transaction As Short
    Public Property Accepted As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        WindowID = Stream.ReadSByte
        Transaction = Stream.ReadShort
        Accepted = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(WindowID)
        Stream.Write(Transaction)
        Stream.Write(Accepted)

    End Sub

End Class

Public Class UnknownAPacket
    Inherits Packet

    Public Property Sink1 As Single
    Public Property Sink2 As Single
    Public Property Sink3 As Single
    Public Property Sink4 As Single
    Public Property Sink5 As Boolean
    Public Property Sink6 As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        Sink1 = stream.ReadSingle()
        Sink2 = stream.ReadSingle()
        Sink3 = stream.ReadSingle()
        Sink4 = stream.ReadSingle()
        Sink5 = stream.ReadBool()
        Sink6 = stream.ReadBool()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(Sink1)
        stream.Write(Sink2)
        stream.Write(Sink3)
        stream.Write(Sink4)
        stream.Write(Sink5)
        stream.Write(Sink6)

    End Sub

End Class

Public Class UpdateHealthPacket
    Inherits Packet

    Public Property Health As Short

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Health = Stream.ReadShort

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Health)

    End Sub

End Class

Public Class UpdateSignPacket
    Inherits Packet

    Public Property X As Integer
    Public Property Y As Short
    Public Property Z As Integer
    Public Lines() As String

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        X = Stream.ReadInt
        Y = Stream.ReadShort
        Z = Stream.ReadInt
        Lines = New String(4) {}
        For i As Integer = 0 To Lines.Length - 1
            Lines(i) = Stream.ReadString16(25)
        Next

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        For i As Integer = 0 To Lines.Length - 1
            Stream.Write(Lines(i))
        Next

    End Sub

End Class

Public Class UpdateProgressBarPacket
    Inherits Packet

    Public Property WindowID As SByte
    Public Property ProgressBar As Short
    Public Property Value As Short

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        WindowID = Stream.ReadSByte
        ProgressBar = Stream.ReadShort
        Value = Stream.ReadShort

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(WindowID)
        Stream.Write(ProgressBar)
        Stream.Write(Value)

    End Sub

End Class

Public Class UseBedPacket
    Inherits Packet

    Public Property PlayerID As Integer
    Public Property InBed As SByte
    Public Property X As Integer
    Public Property Y As SByte
    Public Property Z As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        PlayerID = stream.ReadInt()
        InBed = stream.ReadSByte()
        X = stream.ReadInt()
        Y = stream.ReadSByte()
        Z = stream.ReadInt()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(PlayerID)
        stream.Write(InBed)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)

    End Sub

End Class

Public Class UseEntityPacket
    Inherits Packet

    Public Property User As Integer
    Public Property Target As Integer
    Public Property LeftClick As Boolean

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        User = Stream.ReadInt
        Target = Stream.ReadInt
        LeftClick = Stream.ReadBool

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(User)
        Stream.Write(Target)
        Stream.Write(LeftClick)

    End Sub

End Class

Public Class WeatherPacket
    Inherits Packet

    Public Property EntityID As Integer
    Public Property Unknown As Boolean
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        EntityID = stream.ReadInt()
        Unknown = stream.ReadBool()
        X = stream.ReadDoublePacked()
        Y = stream.ReadDoublePacked()
        Z = stream.ReadDoublePacked()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(EntityID)
        stream.Write(Unknown)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)

    End Sub

End Class

Public Class WindowClickPacket
    Inherits Packet

    Public Property WindowID As SByte
    Public Property Slot As Short
    Public Property RightClick As Boolean
    Public Property Transaction As Short
    Public Property Item As ItemStack

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        WindowID = stream.ReadSByte()
        Slot = stream.ReadShort()
        RightClick = stream.ReadBool()
        Transaction = stream.ReadShort()
        Item = New ItemStack(stream)

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(WindowID)
        stream.Write(Slot)
        stream.Write(RightClick)
        stream.Write(Transaction)
        ' Add proper code for writing the item data.

    End Sub

End Class

Public Class WindowItemsPacket
    Inherits Packet

    Public Property WindowID As SByte
    Public Property ItemCount As Short

    Public Items() As ItemStack

    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        Dim y As String

        WindowID = stream.ReadSByte()

        Items = New ItemStack(stream.ReadShort - 1) {}

        For i = 0 To Items.Length - 1
            Items(i) = New ItemStack(stream)
            'y &= "Type: " & Items(i).type & vbCrLf
            'y &= "Item Count: " & Items(i).Count & vbCrLf
            'y &= "Item Durability: " & Items(i).Durability & vbCrLf
            'LogParser.Parse_Special(y)
        Next

        'Threading.Thread.CurrentThread.Sleep(5)
        ''Items = New ItemStack(stream.ReadShort() - 1) {}
        ''Items = New ItemStack(stream.ReadShort()) {}
        'Dim count As Short = stream.ReadShort
        'Threading.Thread.CurrentThread.Sleep(5)
        ''For i As Integer = 0 To Items.Length - 1
        ''Items(i) = New ItemStack(stream)
        ''Next
        '' Strange thing is that I haave to use "To count", and not "To count-1". It seems count is actually the LENGTH of the array, not the COUNT.""""
        'y &= "WindowID: " & WindowID & vbCrLf
        'y &= "Count: " & count & vbCrLf

        'For i = 0 To count ' - 1
        '    x = New ItemStack(stream)
        '    Threading.Thread.CurrentThread.Sleep(5)
        '    y &= "Type: " & x.type & vbCrLf
        '    y &= "Item Count: " & x.Count & vbCrLf
        '    y &= "Item Durability: " & x.Durability & vbCrLf
        '    LogParser.Parse_Special(y)
        '    y = ""
        'Next

    End Sub


    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(WindowID)
        stream.Write(CShort(Items.Length))
        'For i As Integer = 0 To Items.Length - 1
        '    ' Add proper code to write the item data.
        'Next

    End Sub

End Class