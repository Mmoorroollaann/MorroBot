Public MustInherit Class Packet_

    Public MustOverride Sub Read(ByVal Stream As BigEndianStream)

    Public MustOverride Sub Write(ByVal stream As BigEndianStream)

    Public Sub WriteFlush(ByVal Stream As BigEndianStream)

        Write(Stream)
        Stream.Flush()

    End Sub

    Public Function GetPacketType() As PacketType_

        Return PacketMap_.GetPacketType(Me.[GetType]())

    End Function

    Public Shared Function Read(ByVal Type As PacketType_, ByVal Stream As BigEndianStream) As Packet_

        Try
            Dim PacketType As Type = PacketMap.Map(Type)
            Dim Packet As Packet_ = DirectCast(PacketType.GetConstructor(New Type(-1) {}).Invoke(New Object(-1) {}), Packet_)
            Packet.Read(Stream)
            Return Packet
        Catch ex As Exception
            Console.WriteLine("Error occured while processing a packet of type {0}: {1}", Type, ex.Message)
            Return Nothing
        End Try

    End Function

End Class

Public Class KeelAlivePacket
    Inherits Packet_

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

    End Sub

End Class

Public Class LoginRequestPacket
    Inherits Packet_

    Private m_ProtocolOrEntityID As Integer
    Private m_Username As String
    Private m_MapSeed As Long
    Private m_Dimension As SByte


    Public Property ProtocolOrEntityId() As Integer

        Get
            Return m_ProtocolOrEntityID
        End Get
        Set(ByVal value As Integer)
            m_ProtocolOrEntityId = value
        End Set

    End Property

    Public Property Username() As String

        Get
            Return m_Username
        End Get
        Set(ByVal value As String)
            m_Username = value
        End Set

    End Property

    Public Property MapSeed() As Long

        Get
            Return m_MapSeed
        End Get
        Set(ByVal value As Long)
            m_MapSeed = value
        End Set

    End Property

    Public Property Dimension() As SByte

        Get
            Return m_Dimension
        End Get
        Set(ByVal value As SByte)
            m_Dimension = value
        End Set

    End Property


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        ProtocolOrEntityId = Stream.ReadInt()
        Username = Stream.ReadString16(16)
        MapSeed = Stream.ReadLong()
        Dimension = Stream.ReadSByte()

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(ProtocolOrEntityId)
        stream.Write(Username)
        stream.Write(MapSeed)
        stream.Write(Dimension)

    End Sub

End Class

Public Class HandshakePacket
    Inherits Packet_

    Private m_UsernameOrHash As String


    Public Property UsernameOrHash() As String

        Get
            Return m_UsernameOrHash
        End Get
        Set(ByVal value As String)
            m_UsernameOrHash = value
        End Set

    End Property


    Public Overrides Sub Read(ByVal stream As BigEndianStream)

        UsernameOrHash = stream.ReadString16(16)

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)

        stream.Write(UsernameOrHash)

    End Sub

End Class

Public Class ChatMessagePacket
    Inherits Packet_
    Public Property Message() As String
        Get
            Return m_Message
        End Get
        Set(ByVal value As String)
            m_Message = Value
        End Set
    End Property
    Private m_Message As String

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Message = stream.ReadString16(100)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Message)
    End Sub
End Class

Public Class TimeUpdatePacket
    Inherits Packet_
    Public Property Time() As Long
        Get
            Return m_Time
        End Get
        Set(ByVal value As Long)
            m_Time = Value
        End Set
    End Property
    Private m_Time As Long

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Time = stream.ReadLong()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Time)
    End Sub
End Class

Public Class EntityEquipmentPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Slot() As Short
        Get
            Return m_Slot
        End Get
        Set(ByVal value As Short)
            m_Slot = Value
        End Set
    End Property
    Private m_Slot As Short
    Public Property ItemId() As Short
        Get
            Return m_ItemId
        End Get
        Set(ByVal value As Short)
            m_ItemId = Value
        End Set
    End Property
    Private m_ItemId As Short
    Public Property Durability() As Short
        Get
            Return m_Durability
        End Get
        Set(ByVal value As Short)
            m_Durability = Value
        End Set
    End Property
    Private m_Durability As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Slot = stream.ReadShort()
        ItemId = stream.ReadShort()
        Durability = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(Slot)
        stream.Write(ItemId)
        stream.Write(Durability)
    End Sub
End Class

Public Class SpawnPositionPacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As Integer
        Get
            Return m_Y
        End Get
        Set(ByVal value As Integer)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Integer
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Y = stream.ReadInt()
        Z = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
    End Sub
End Class

Public Class UseEntityPacket
    Inherits Packet_
    Public Property User() As Integer
        Get
            Return m_User
        End Get
        Set(ByVal value As Integer)
            m_User = Value
        End Set
    End Property
    Private m_User As Integer
    Public Property Target() As Integer
        Get
            Return m_Target
        End Get
        Set(ByVal value As Integer)
            m_Target = Value
        End Set
    End Property
    Private m_Target As Integer
    Public Property LeftClick() As Boolean
        Get
            Return m_LeftClick
        End Get
        Set(ByVal value As Boolean)
            m_LeftClick = Value
        End Set
    End Property
    Private m_LeftClick As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        User = stream.ReadInt()
        Target = stream.ReadInt()
        LeftClick = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(User)
        stream.Write(Target)
        stream.Write(LeftClick)
    End Sub
End Class

Public Class UpdateHealthPacket
    Inherits Packet_
    Public Property Health() As Short
        Get
            Return m_Health
        End Get
        Set(ByVal value As Short)
            m_Health = Value
        End Set
    End Property
    Private m_Health As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Health = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Health)
    End Sub
End Class

Public Class RespawnPacket
    Inherits Packet_
    Public Overrides Sub Read(ByVal stream As BigEndianStream)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
    End Sub
End Class

Public Class PlayerPacket
    Inherits Packet_
    Public Property OnGround() As Boolean
        Get
            Return m_OnGround
        End Get
        Set(ByVal value As Boolean)
            m_OnGround = Value
        End Set
    End Property
    Private m_OnGround As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        OnGround = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(OnGround)
    End Sub
End Class

Public Class PlayerPositionPacket
    Inherits Packet_
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Stance() As Double
        Get
            Return m_Stance
        End Get
        Set(ByVal value As Double)
            m_Stance = Value
        End Set
    End Property
    Private m_Stance As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property OnGround() As Boolean
        Get
            Return m_OnGround
        End Get
        Set(ByVal value As Boolean)
            m_OnGround = Value
        End Set
    End Property
    Private m_OnGround As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadDouble()
        Y = stream.ReadDouble()
        Stance = stream.ReadDouble()
        Z = stream.ReadDouble()
        OnGround = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Stance)
        stream.Write(Z)
        stream.Write(OnGround)
    End Sub
End Class

Public Class PlayerRotationPacket
    Inherits Packet_
    Public Property Yaw() As Single
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As Single)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As Single
    Public Property Pitch() As Single
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As Single)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As Single
    Public Property OnGround() As Boolean
        Get
            Return m_OnGround
        End Get
        Set(ByVal value As Boolean)
            m_OnGround = Value
        End Set
    End Property
    Private m_OnGround As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Yaw = stream.ReadFloat()
        Pitch = stream.ReadFloat()
        OnGround = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(OnGround)
    End Sub
End Class

Public Class PlayerPositionRotationPacket
    Inherits Packet_
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Stance() As Double
        Get
            Return m_Stance
        End Get
        Set(ByVal value As Double)
            m_Stance = Value
        End Set
    End Property
    Private m_Stance As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Yaw() As Single
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As Single)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As Single
    Public Property Pitch() As Single
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As Single)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As Single
    Public Property OnGround() As Boolean
        Get
            Return m_OnGround
        End Get
        Set(ByVal value As Boolean)
            m_OnGround = Value
        End Set
    End Property
    Private m_OnGround As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadDouble()
        Stance = stream.ReadDouble()
        Y = stream.ReadDouble()
        Z = stream.ReadDouble()
        Yaw = stream.ReadFloat()
        Pitch = stream.ReadFloat()
        OnGround = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Stance)
        stream.Write(Z)
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(OnGround)
    End Sub
End Class

Public Class PlayerDiggingPacket
    Inherits Packet_
    Public Property Action() As DigAction
        Get
            Return m_Action
        End Get
        Set(ByVal value As DigAction)
            m_Action = Value
        End Set
    End Property
    Private m_Action As DigAction
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As SByte
        Get
            Return m_Y
        End Get
        Set(ByVal value As SByte)
            m_Y = Value
        End Set
    End Property
    Private m_Y As SByte
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Face() As SByte
        Get
            Return m_Face
        End Get
        Set(ByVal value As SByte)
            m_Face = Value
        End Set
    End Property
    Private m_Face As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Action = DirectCast(stream.ReadByte(), DigAction)
        X = stream.ReadInt()
        Y = stream.ReadSByte()
        Z = stream.ReadInt()
        Face = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(CByte(Action))
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(Face)
    End Sub
End Class

Public Class PlayerBlockPlacementPacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As SByte
        Get
            Return m_Y
        End Get
        Set(ByVal value As SByte)
            m_Y = Value
        End Set
    End Property
    Private m_Y As SByte
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Face() As BlockFace
        Get
            Return m_Face
        End Get
        Set(ByVal value As BlockFace)
            m_Face = Value
        End Set
    End Property
    Private m_Face As BlockFace
    Public Property Item() As ItemStack
        Get
            Return m_Item
        End Get
        Set(ByVal value As ItemStack)
            m_Item = Value
        End Set
    End Property
    Private m_Item As ItemStack

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Y = stream.ReadSByte()
        Z = stream.ReadInt()
        Face = DirectCast(stream.ReadSByte(), BlockFace)
        Item = New ItemStack(stream)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(CSByte(Face))
		(If(Item, ItemStack.Void)).Write(stream)
    End Sub
End Class

Public Class HoldingChangePacket
    Inherits Packet_
    Public Property Slot() As Short
        Get
            Return m_Slot
        End Get
        Set(ByVal value As Short)
            m_Slot = Value
        End Set
    End Property
    Private m_Slot As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Slot = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Slot)
    End Sub
End Class

Public Class UseBedPacket
    Inherits Packet_
    Public Property PlayerId() As Integer
        Get
            Return m_PlayerId
        End Get
        Set(ByVal value As Integer)
            m_PlayerId = Value
        End Set
    End Property
    Private m_PlayerId As Integer
    Public Property InBed() As SByte
        Get
            Return m_InBed
        End Get
        Set(ByVal value As SByte)
            m_InBed = Value
        End Set
    End Property
    Private m_InBed As SByte
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As SByte
        Get
            Return m_Y
        End Get
        Set(ByVal value As SByte)
            m_Y = Value
        End Set
    End Property
    Private m_Y As SByte
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        PlayerId = stream.ReadInt()
        InBed = stream.ReadSByte()
        X = stream.ReadInt()
        Y = stream.ReadSByte()
        Z = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(PlayerId)
        stream.Write(InBed)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
    End Sub
End Class

Public Class AnimationPacket
    Inherits Packet_
    Public Property PlayerId() As Integer
        Get
            Return m_PlayerId
        End Get
        Set(ByVal value As Integer)
            m_PlayerId = Value
        End Set
    End Property
    Private m_PlayerId As Integer
    Public Property Animation() As SByte
        Get
            Return m_Animation
        End Get
        Set(ByVal value As SByte)
            m_Animation = Value
        End Set
    End Property
    Private m_Animation As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        PlayerId = stream.ReadInt()
        Animation = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(PlayerId)
        stream.Write(Animation)
    End Sub
End Class

Public Class EntityActionPacket
    Inherits Packet_
    Public Property PlayerId() As Integer
        Get
            Return m_PlayerId
        End Get
        Set(ByVal value As Integer)
            m_PlayerId = Value
        End Set
    End Property
    Private m_PlayerId As Integer
    Public Property Action() As SByte
        Get
            Return m_Action
        End Get
        Set(ByVal value As SByte)
            m_Action = Value
        End Set
    End Property
    Private m_Action As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        PlayerId = stream.ReadInt()
        Action = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(PlayerId)
        stream.Write(Action)
    End Sub
End Class

Public Class NamedEntitySpawnPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property PlayerName() As String
        Get
            Return m_PlayerName
        End Get
        Set(ByVal value As String)
            m_PlayerName = Value
        End Set
    End Property
    Private m_PlayerName As String
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte
    Public Property CurrentItem() As Short
        Get
            Return m_CurrentItem
        End Get
        Set(ByVal value As Short)
            m_CurrentItem = Value
        End Set
    End Property
    Private m_CurrentItem As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        PlayerName = stream.ReadString16(16)
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
        CurrentItem = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(PlayerName)
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(CurrentItem)
    End Sub
End Class

Public Class SpawnItemPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property ItemId() As Short
        Get
            Return m_ItemId
        End Get
        Set(ByVal value As Short)
            m_ItemId = Value
        End Set
    End Property
    Private m_ItemId As Short
    Public Property Count() As SByte
        Get
            Return m_Count
        End Get
        Set(ByVal value As SByte)
            m_Count = Value
        End Set
    End Property
    Private m_Count As SByte
    Public Property Durability() As Short
        Get
            Return m_Durability
        End Get
        Set(ByVal value As Short)
            m_Durability = Value
        End Set
    End Property
    Private m_Durability As Short
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte
    Public Property Roll() As SByte
        Get
            Return m_Roll
        End Get
        Set(ByVal value As SByte)
            m_Roll = Value
        End Set
    End Property
    Private m_Roll As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        ItemId = stream.ReadShort()
        Count = stream.ReadSByte()
        Durability = stream.ReadShort()
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
        Roll = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(ItemId)
        stream.Write(Count)
        stream.Write(Durability)
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(Roll)
    End Sub
End Class

Public Class CollectItemPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property PlayerId() As Integer
        Get
            Return m_PlayerId
        End Get
        Set(ByVal value As Integer)
            m_PlayerId = Value
        End Set
    End Property
    Private m_PlayerId As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        PlayerId = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(PlayerId)
    End Sub
End Class

Public Class AddObjectVehiclePacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Type() As SByte
        Get
            Return m_Type
        End Get
        Set(ByVal value As SByte)
            m_Type = Value
        End Set
    End Property
    Private m_Type As SByte
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Type = stream.ReadSByte()
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(Type)
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
    End Sub
End Class

Public Class MobSpawnPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Type() As MobType
        Get
            Return m_Type
        End Get
        Set(ByVal value As MobType)
            m_Type = Value
        End Set
    End Property
    Private m_Type As MobType
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte
    Public Property Data() As MetaData_
        Get
            Return m_Data
        End Get
        Set(ByVal value As MetaData_)
            m_Data = Value
        End Set
    End Property
    Private m_Data As MetaData_

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Type = DirectCast(stream.ReadByte(), MobType)
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
        Data = stream.ReadMetaData()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(CByte(Type))
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
        stream.Write(Yaw)
        stream.Write(Pitch)
        stream.Write(Data)
    End Sub
End Class

Public Class EntityPaintingPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(ByVal value As String)
            m_Title = Value
        End Set
    End Property
    Private m_Title As String
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As Integer
        Get
            Return m_Y
        End Get
        Set(ByVal value As Integer)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Integer
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property GraphicId() As Integer
        Get
            Return m_GraphicId
        End Get
        Set(ByVal value As Integer)
            m_GraphicId = Value
        End Set
    End Property
    Private m_GraphicId As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Title = stream.ReadString16(13)
        X = stream.ReadInt()
        Y = stream.ReadInt()
        Z = stream.ReadInt()
        GraphicId = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(Title)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(GraphicId)
    End Sub
End Class

Public Class UnknownAPacket
    Inherits Packet_
    Public Property Sink1() As Single
        Get
            Return m_Sink1
        End Get
        Set(ByVal value As Single)
            m_Sink1 = Value
        End Set
    End Property
    Private m_Sink1 As Single
    Public Property Sink2() As Single
        Get
            Return m_Sink2
        End Get
        Set(ByVal value As Single)
            m_Sink2 = Value
        End Set
    End Property
    Private m_Sink2 As Single
    Public Property Sink3() As Single
        Get
            Return m_Sink3
        End Get
        Set(ByVal value As Single)
            m_Sink3 = Value
        End Set
    End Property
    Private m_Sink3 As Single
    Public Property Sink4() As Single
        Get
            Return m_Sink4
        End Get
        Set(ByVal value As Single)
            m_Sink4 = Value
        End Set
    End Property
    Private m_Sink4 As Single
    Public Property Sink5() As Boolean
        Get
            Return m_Sink5
        End Get
        Set(ByVal value As Boolean)
            m_Sink5 = Value
        End Set
    End Property
    Private m_Sink5 As Boolean
    Public Property Sink6() As Boolean
        Get
            Return m_Sink6
        End Get
        Set(ByVal value As Boolean)
            m_Sink6 = Value
        End Set
    End Property
    Private m_Sink6 As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Sink1 = stream.ReadFloat()
        Sink2 = stream.ReadFloat()
        Sink3 = stream.ReadFloat()
        Sink4 = stream.ReadFloat()
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

Public Class EntityVelocityPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property VelocityX() As Short
        Get
            Return m_VelocityX
        End Get
        Set(ByVal value As Short)
            m_VelocityX = Value
        End Set
    End Property
    Private m_VelocityX As Short
    Public Property VelocityY() As Short
        Get
            Return m_VelocityY
        End Get
        Set(ByVal value As Short)
            m_VelocityY = Value
        End Set
    End Property
    Private m_VelocityY As Short
    Public Property VelocityZ() As Short
        Get
            Return m_VelocityZ
        End Get
        Set(ByVal value As Short)
            m_VelocityZ = Value
        End Set
    End Property
    Private m_VelocityZ As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        VelocityX = stream.ReadShort()
        VelocityY = stream.ReadShort()
        VelocityZ = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(VelocityX)
        stream.Write(VelocityY)
        stream.Write(VelocityZ)
    End Sub
End Class

Public Class DestroyEntityPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
    End Sub
End Class

Public Class CreateEntityPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
    End Sub
End Class

Public Class EntityRelativeMovePacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property DeltaX() As SByte
        Get
            Return m_DeltaX
        End Get
        Set(ByVal value As SByte)
            m_DeltaX = Value
        End Set
    End Property
    Private m_DeltaX As SByte
    Public Property DeltaY() As SByte
        Get
            Return m_DeltaY
        End Get
        Set(ByVal value As SByte)
            m_DeltaY = Value
        End Set
    End Property
    Private m_DeltaY As SByte
    Public Property DeltaZ() As SByte
        Get
            Return m_DeltaZ
        End Get
        Set(ByVal value As SByte)
            m_DeltaZ = Value
        End Set
    End Property
    Private m_DeltaZ As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        DeltaX = stream.ReadSByte()
        DeltaY = stream.ReadSByte()
        DeltaZ = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(DeltaX)
        stream.Write(DeltaY)
        stream.Write(DeltaZ)
    End Sub
End Class

Public Class EntityLookPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(Yaw)
        stream.Write(Pitch)
    End Sub
End Class

Public Class EntityLookAndRelativeMovePacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property DeltaX() As SByte
        Get
            Return m_DeltaX
        End Get
        Set(ByVal value As SByte)
            m_DeltaX = Value
        End Set
    End Property
    Private m_DeltaX As SByte
    Public Property DeltaY() As SByte
        Get
            Return m_DeltaY
        End Get
        Set(ByVal value As SByte)
            m_DeltaY = Value
        End Set
    End Property
    Private m_DeltaY As SByte
    Public Property DeltaZ() As SByte
        Get
            Return m_DeltaZ
        End Get
        Set(ByVal value As SByte)
            m_DeltaZ = Value
        End Set
    End Property
    Private m_DeltaZ As SByte
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        DeltaX = stream.ReadSByte()
        DeltaY = stream.ReadSByte()
        DeltaZ = stream.ReadSByte()
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(DeltaX)
        stream.Write(DeltaY)
        stream.Write(DeltaZ)
        stream.Write(Yaw)
        stream.Write(Pitch)
    End Sub
End Class

Public Class EntityTeleportPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Yaw() As SByte
        Get
            Return m_Yaw
        End Get
        Set(ByVal value As SByte)
            m_Yaw = Value
        End Set
    End Property
    Private m_Yaw As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        X = CDbl(stream.ReadInt()) / 32.0
        Y = CDbl(stream.ReadInt()) / 32.0
        Z = CDbl(stream.ReadInt()) / 32.0
        Yaw = stream.ReadSByte()
        Pitch = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(CInt(Math.Truncate(X * 32)))
        stream.Write(CInt(Math.Truncate(Y * 32)))
        stream.Write(CInt(Math.Truncate(Z * 32)))
        stream.Write(Yaw)
        stream.Write(Pitch)
    End Sub
End Class

Public Class EntityStatusPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property EntityStatus() As SByte
        Get
            Return m_EntityStatus
        End Get
        Set(ByVal value As SByte)
            m_EntityStatus = Value
        End Set
    End Property
    Private m_EntityStatus As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        EntityStatus = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(EntityStatus)
    End Sub
End Class

Public Class AttachEntityPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property VehicleId() As Integer
        Get
            Return m_VehicleId
        End Get
        Set(ByVal value As Integer)
            m_VehicleId = Value
        End Set
    End Property
    Private m_VehicleId As Integer

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        VehicleId = stream.ReadInt()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(VehicleId)
    End Sub
End Class

Public Class EntityMetadataPacket
    Inherits Packet_
    Public Property EntityId() As Integer
        Get
            Return m_EntityId
        End Get
        Set(ByVal value As Integer)
            m_EntityId = Value
        End Set
    End Property
    Private m_EntityId As Integer
    Public Property Data() As MetaData_
        Get
            Return m_Data
        End Get
        Set(ByVal value As MetaData_)
            m_Data = Value
        End Set
    End Property
    Private m_Data As MetaData_

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        EntityId = stream.ReadInt()
        Data = stream.ReadMetaData()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(EntityId)
        stream.Write(Data)
    End Sub
End Class

Public Class PreChunkPacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Load() As Boolean
        Get
            Return m_Load
        End Get
        Set(ByVal value As Boolean)
            m_Load = Value
        End Set
    End Property
    Private m_Load As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Z = stream.ReadInt()
        Load = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Z)
        stream.Write(Load)
    End Sub
End Class

Public Class MultiBlockChangePacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Coords() As Short()
        Get
            Return m_Coords
        End Get
        Set(ByVal value As Short())
            m_Coords = Value
        End Set
    End Property
    Private m_Coords As Short()
    Public Property Types() As SByte()
        Get
            Return m_Types
        End Get
        Set(ByVal value As SByte())
            m_Types = Value
        End Set
    End Property
    Private m_Types As SByte()
    Public Property Metadata() As SByte()
        Get
            Return m_Metadata
        End Get
        Set(ByVal value As SByte())
            m_Metadata = Value
        End Set
    End Property
    Private m_Metadata As SByte()

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Z = stream.ReadInt()
        Dim length As Short = stream.ReadShort()
        Coords = New Short(length - 1) {}
        Types = New SByte(length - 1) {}
        Metadata = New SByte(length - 1) {}
        For i As Integer = 0 To Coords.Length - 1
            Coords(i) = stream.ReadShort()
        Next
        For i As Integer = 0 To Types.Length - 1
            Types(i) = stream.ReadSByte()
        Next
        For i As Integer = 0 To Metadata.Length - 1
            Metadata(i) = stream.ReadSByte()
        Next

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Z)
        stream.Write(CShort(Coords.Length))
        For i As Integer = 0 To Coords.Length - 1
            stream.Write(Coords(i))
        Next
        For i As Integer = 0 To Types.Length - 1
            stream.Write(Types(i))
        Next
        For i As Integer = 0 To Metadata.Length - 1
            stream.Write(Metadata(i))
        Next
    End Sub
End Class

Public Class BlockChangePacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As SByte
        Get
            Return m_Y
        End Get
        Set(ByVal value As SByte)
            m_Y = Value
        End Set
    End Property
    Private m_Y As SByte
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Type() As Byte
        Get
            Return m_Type
        End Get
        Set(ByVal value As Byte)
            m_Type = Value
        End Set
    End Property
    Private m_Type As Byte
    Public Property Data() As Byte
        Get
            Return m_Data
        End Get
        Set(ByVal value As Byte)
            m_Data = Value
        End Set
    End Property
    Private m_Data As Byte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Y = stream.ReadSByte()
        Z = stream.ReadInt()
        Type = stream.ReadByte()
        Data = stream.ReadByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(Type)
        stream.Write(Data)
    End Sub
End Class

Public Class PlayNoteBlockPacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As Integer
        Get
            Return m_Y
        End Get
        Set(ByVal value As Integer)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Integer
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Instrument() As SByte
        Get
            Return m_Instrument
        End Get
        Set(ByVal value As SByte)
            m_Instrument = Value
        End Set
    End Property
    Private m_Instrument As SByte
    Public Property Pitch() As SByte
        Get
            Return m_Pitch
        End Get
        Set(ByVal value As SByte)
            m_Pitch = Value
        End Set
    End Property
    Private m_Pitch As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Y = stream.ReadInt()
        Z = stream.ReadInt()
        Instrument = stream.ReadSByte()
        Pitch = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(Instrument)
        stream.Write(Pitch)
    End Sub
End Class

Public Class ExplosionPacket
    Inherits Packet_
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = Value
        End Set
    End Property
    Private m_X As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Double
    Public Property Z() As Double
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Double
    Public Property Radius() As Single
        Get
            Return m_Radius
        End Get
        Set(ByVal value As Single)
            m_Radius = Value
        End Set
    End Property
    Private m_Radius As Single
    Public Property Offsets() As SByte(,)
        Get
            Return m_Offsets
        End Get
        Set(ByVal value As SByte(,))
            m_Offsets = Value
        End Set
    End Property
    Private m_Offsets As SByte(,)

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadDouble()
        Y = stream.ReadDouble()
        Z = stream.ReadDouble()
        Radius = stream.ReadFloat()
        Offsets = New SByte(stream.ReadInt() - 1, 2) {}
        For i As Integer = 0 To Offsets.GetLength(0) - 1
            Offsets(i, 0) = stream.ReadSByte()
            Offsets(i, 1) = stream.ReadSByte()
            Offsets(i, 2) = stream.ReadSByte()
        Next

    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        stream.Write(Radius)
        stream.Write(CInt(Offsets.GetLength(0)))
        For i As Integer = 0 To Offsets.GetLength(0) - 1
            stream.Write(Offsets(i, 0))
            stream.Write(Offsets(i, 1))
            stream.Write(Offsets(i, 2))
        Next
    End Sub
End Class

Public Class OpenWindowPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Friend Property InventoryType() As InterfaceType
        Get
            Return m_InventoryType
        End Get
        Set(ByVal value As InterfaceType)
            m_InventoryType = Value
        End Set
    End Property
    Private m_InventoryType As InterfaceType
    Public Property WindowTitle() As String
        Get
            Return m_WindowTitle
        End Get
        Set(ByVal value As String)
            m_WindowTitle = Value
        End Set
    End Property
    Private m_WindowTitle As String
    Public Property SlotCount() As SByte
        Get
            Return m_SlotCount
        End Get
        Set(ByVal value As SByte)
            m_SlotCount = Value
        End Set
    End Property
    Private m_SlotCount As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        InventoryType = DirectCast(stream.ReadSByte(), InterfaceType)
        WindowTitle = stream.ReadString8(100)
        SlotCount = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(CSByte(InventoryType))
        stream.Write8(WindowTitle)
        stream.Write(SlotCount)
    End Sub
End Class

Public Class CloseWindowPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
    End Sub
End Class

Public Class WindowClickPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Public Property Slot() As Short
        Get
            Return m_Slot
        End Get
        Set(ByVal value As Short)
            m_Slot = Value
        End Set
    End Property
    Private m_Slot As Short
    Public Property RightClick() As Boolean
        Get
            Return m_RightClick
        End Get
        Set(ByVal value As Boolean)
            m_RightClick = Value
        End Set
    End Property
    Private m_RightClick As Boolean
    Public Property Transaction() As Short
        Get
            Return m_Transaction
        End Get
        Set(ByVal value As Short)
            m_Transaction = Value
        End Set
    End Property
    Private m_Transaction As Short
    Public Property Item() As ItemStack
        Get
            Return m_Item
        End Get
        Set(ByVal value As ItemStack)
            m_Item = Value
        End Set
    End Property
    Private m_Item As ItemStack

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        Slot = stream.ReadShort()
        RightClick = stream.ReadBool()
        Transaction = stream.ReadShort()
        Item = New ItemStack(stream)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(Slot)
        stream.Write(RightClick)
        stream.Write(Transaction)
		(If(Item, ItemStack.Void)).Write(stream)
    End Sub
End Class

Public Class SetSlotPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Public Property Slot() As Short
        Get
            Return m_Slot
        End Get
        Set(ByVal value As Short)
            m_Slot = Value
        End Set
    End Property
    Private m_Slot As Short
    Public Property Item() As ItemStack
        Get
            Return m_Item
        End Get
        Set(ByVal value As ItemStack)
            m_Item = Value
        End Set
    End Property
    Private m_Item As ItemStack

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        Slot = stream.ReadShort()
        Item = New ItemStack(stream)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(Slot)
		(If(Item, ItemStack.Void)).Write(stream)
    End Sub
End Class

Public Class WindowItemsPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Public Property Items() As ItemStack()
        Get
            Return m_Items
        End Get
        Set(ByVal value As ItemStack())
            m_Items = Value
        End Set
    End Property
    Private m_Items As ItemStack()

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        Items = New ItemStack(stream.ReadShort() - 1) {}
        For i As Integer = 0 To Items.Length - 1
            Items(i) = New ItemStack(stream)
        Next
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(CShort(Items.Length))
        For i As Integer = 0 To Items.Length - 1
			(If(Items(i), ItemStack.Void)).Write(stream)
        Next
    End Sub
End Class

Public Class UpdateProgressBarPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Public Property ProgressBar() As Short
        Get
            Return m_ProgressBar
        End Get
        Set(ByVal value As Short)
            m_ProgressBar = Value
        End Set
    End Property
    Private m_ProgressBar As Short
    Public Property Value() As Short
        Get
            Return m_Value
        End Get
        Set(ByVal value As Short)
            m_Value = Value
        End Set
    End Property
    Private m_Value As Short

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        ProgressBar = stream.ReadShort()
        Value = stream.ReadShort()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(ProgressBar)
        stream.Write(Value)
    End Sub
End Class

Public Class TransactionPacket
    Inherits Packet_
    Public Property WindowId() As SByte
        Get
            Return m_WindowId
        End Get
        Set(ByVal value As SByte)
            m_WindowId = Value
        End Set
    End Property
    Private m_WindowId As SByte
    Public Property Transaction() As Short
        Get
            Return m_Transaction
        End Get
        Set(ByVal value As Short)
            m_Transaction = Value
        End Set
    End Property
    Private m_Transaction As Short
    Public Property Accepted() As Boolean
        Get
            Return m_Accepted
        End Get
        Set(ByVal value As Boolean)
            m_Accepted = Value
        End Set
    End Property
    Private m_Accepted As Boolean

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        WindowId = stream.ReadSByte()
        Transaction = stream.ReadShort()
        Accepted = stream.ReadBool()
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(WindowId)
        stream.Write(Transaction)
        stream.Write(Accepted)
    End Sub
End Class

Public Class UpdateSignPacket
    Inherits Packet_
    Public Property X() As Integer
        Get
            Return m_X
        End Get
        Set(ByVal value As Integer)
            m_X = Value
        End Set
    End Property
    Private m_X As Integer
    Public Property Y() As Short
        Get
            Return m_Y
        End Get
        Set(ByVal value As Short)
            m_Y = Value
        End Set
    End Property
    Private m_Y As Short
    Public Property Z() As Integer
        Get
            Return m_Z
        End Get
        Set(ByVal value As Integer)
            m_Z = Value
        End Set
    End Property
    Private m_Z As Integer
    Public Property Lines() As String()
        Get
            Return m_Lines
        End Get
        Set(ByVal value As String())
            m_Lines = Value
        End Set
    End Property
    Private m_Lines As String()

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        X = stream.ReadInt()
        Y = stream.ReadShort()
        Z = stream.ReadInt()
        Lines = New String(3) {}
        For i As Integer = 0 To Lines.Length - 1
            Lines(i) = stream.ReadString16(25)
        Next
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(X)
        stream.Write(Y)
        stream.Write(Z)
        For i As Integer = 0 To Lines.Length - 1
            stream.Write(Lines(i))
        Next
    End Sub
End Class

Public Class DisconnectPacket
    Inherits Packet_
    Public Property Reason() As String
        Get
            Return m_Reason
        End Get
        Set(ByVal value As String)
            m_Reason = Value
        End Set
    End Property
    Private m_Reason As String

    Public Overrides Sub Read(ByVal stream As BigEndianStream)
        Reason = stream.ReadString16(100)
    End Sub

    Public Overrides Sub Write(ByVal stream As BigEndianStream)
        stream.Write(Reason)
    End Sub
End Class