Imports System.Linq
Imports System.Net.Sockets
Imports System.Threading

Friend Class PacketHandler_
    Implements IDisposable
    Public Property Net() As BigEndianStream
        Get
            Return m_Net
        End Get
        Private Set(ByVal value As BigEndianStream)
            m_Net = Value
        End Set
    End Property
    Private m_Net As BigEndianStream

    Public Event AddObjectVehicle As PacketEventHandler(Of AddObjectVehiclePacket)
    Public Event Animation As PacketEventHandler(Of AnimationPacket)
    Public Event AttachEntity As PacketEventHandler(Of AttachEntityPacket)
    Public Event BlockChange As PacketEventHandler(Of BlockChangePacket)
    Public Event ChatMessage As PacketEventHandler(Of ChatMessagePacket)
    Public Event CloseWindow As PacketEventHandler(Of CloseWindowPacket)
    Public Event CollectItem As PacketEventHandler(Of CollectItemPacket)
    Public Event DestroyEntity As PacketEventHandler(Of DestroyEntityPacket)
    Public Event Disconnect As PacketEventHandler(Of DisconnectPacket)
    Public Event Entity As PacketEventHandler(Of CreateEntityPacket)
    Public Event EntityAction As PacketEventHandler(Of EntityActionPacket)
    Public Event EntityEquipment As PacketEventHandler(Of EntityEquipmentPacket)
    Public Event EntityLook As PacketEventHandler(Of EntityLookPacket)
    Public Event EntityLookAndRelativeMove As PacketEventHandler(Of EntityLookAndRelativeMovePacket)
    Public Event EntityMetadata As PacketEventHandler(Of EntityMetadataPacket)
    Public Event EntityPainting As PacketEventHandler(Of EntityPaintingPacket)
    Public Event EntityRelativeMove As PacketEventHandler(Of EntityRelativeMovePacket)
    Public Event EntityStatus As PacketEventHandler(Of EntityStatusPacket)
    Public Event EntityTeleport As PacketEventHandler(Of EntityTeleportPacket)
    Public Event EntityVelocity As PacketEventHandler(Of EntityVelocityPacket)
    Public Event Explosion As PacketEventHandler(Of ExplosionPacket)
    Public Event Handshake As PacketEventHandler(Of HandshakePacket)
    Public Event HoldingChange As PacketEventHandler(Of HoldingChangePacket)
    Public Event IncrementStatistic As PacketEventHandler(Of IncrementStatisticPacket)
    Public Event InvalidState As PacketEventHandler(Of InvalidStatePacket)
    Public Event KeepAlive As PacketEventHandler(Of KeepAlivePacket)
    Public Event LoginRequest As PacketEventHandler(Of LoginRequestPacket)
    Public Event MapChunk As PacketEventHandler(Of MapChunkPacket)
    Public Event MobSpawn As PacketEventHandler(Of MobSpawnPacket)
    Public Event MultiBlockChange As PacketEventHandler(Of MultiBlockChangePacket)
    Public Event NamedEntitySpawn As PacketEventHandler(Of NamedEntitySpawnPacket)
    Public Event OpenWindow As PacketEventHandler(Of OpenWindowPacket)
    Public Event PickupSpawn As PacketEventHandler(Of SpawnItemPacket)
    Public Event Player As PacketEventHandler(Of PlayerPacket)
    Public Event PlayerBlockPlacement As PacketEventHandler(Of PlayerBlockPlacementPacket)
    Public Event PlayerDigging As PacketEventHandler(Of PlayerDiggingPacket)
    Public Event PlayerPosition As PacketEventHandler(Of PlayerPositionPacket)
    Public Event PlayerPositionRotation As PacketEventHandler(Of PlayerPositionRotationPacket)
    Public Event PlayerRotation As PacketEventHandler(Of PlayerRotationPacket)
    Public Event PlayNoteBlock As PacketEventHandler(Of PlayNoteBlockPacket)
    Public Event PreChunk As PacketEventHandler(Of PreChunkPacket)
    Public Event Respawn As PacketEventHandler(Of RespawnPacket)
    Public Event SetSlot As PacketEventHandler(Of SetSlotPacket)
    Public Event SpawnPosition As PacketEventHandler(Of SpawnPositionPacket)
    Public Event TimeUpdate As PacketEventHandler(Of TimeUpdatePacket)
    Public Event Transaction As PacketEventHandler(Of TransactionPacket)
    Public Event UnknownA As PacketEventHandler(Of UnknownAPacket)
    Public Event UpdateHealth As PacketEventHandler(Of UpdateHealthPacket)
    Public Event UpdateProgressBar As PacketEventHandler(Of UpdateProgressBarPacket)
    Public Event UpdateSign As PacketEventHandler(Of UpdateSignPacket)
    Public Event UseBed As PacketEventHandler(Of UseBedPacket)
    Public Event UseEntity As PacketEventHandler(Of UseEntityPacket)
    Public Event Weather As PacketEventHandler(Of WeatherPacket)
    Public Event WindowClick As PacketEventHandler(Of WindowClickPacket)
    Public Event WindowItems As PacketEventHandler(Of WindowItemsPacket)

    Private ReadOnly PacketQueue As New Queue(Of Packet_)()
    Private ReadOnly QueueThread As Thread
    Private Running As Boolean = True
    Private RxThread As Thread

    Public Sub New(ByVal stream As BigEndianStream)
        Net = stream

        QueueThread = New Thread(AddressOf QueueProc)
        QueueThread.IsBackground = True
        QueueThread.Start()

        RxThread = New Thread(AddressOf RxProc)
        RxThread.IsBackground = True
        RxThread.Start()
    End Sub

    Public Sub New(ByVal tcp As TcpClient)
        Me.New(New BigEndianStream(tcp.GetStream()))
    End Sub

    Public Sub SendPacket(ByVal packet As Packet_)
        SyncLock PacketQueue
            PacketQueue.Enqueue(packet)
        End SyncLock
    End Sub

    Private Sub QueueProc()
        While Running
            While PacketQueue.Count > 0 AndAlso Running
                Dim packet As Packet_
                SyncLock PacketQueue
                    packet = PacketQueue.Dequeue()
                End SyncLock
                Try
                    Net.WritePacket(packet)
                Catch ex As Exception
                    Dispose()
                    Console.WriteLine("Disconnecting due to Tx failure: " + ex.Message)
                    Return
                End Try
            End While
            Thread.Sleep(1)
        End While
    End Sub

    Public Sub Dispose()
        Running = False
    End Sub

    Private Sub RxProc()
        While Running AndAlso ProcessPacket()
            Thread.Sleep(1)
        End While
        Dispose()
    End Sub

    Public Function ProcessPacket() As Boolean
        Dim p As Packet_
        Dim type As PacketType_
        Try
            p = Net.ReadPacket()
            type = p.GetPacketType()
        Catch ex As Exception
            Console.WriteLine("Disconnecting: " & ex)
            Return False
        End Try

        Select Case type
            Case PacketType_.AddObjectVehicle
                OnAddObjectVehicle(DirectCast(p, AddObjectVehiclePacket))
                Exit Select
            Case PacketType_.Animation
                OnAnimation(DirectCast(p, AnimationPacket))
                Exit Select
            Case PacketType_.AttachEntity
                OnAttachEntity(DirectCast(p, AttachEntityPacket))
                Exit Select
            Case PacketType_.BlockChange
                OnBlockChange(DirectCast(p, BlockChangePacket))
                Exit Select
            Case PacketType_.ChatMessage
                OnChatMessage(DirectCast(p, ChatMessagePacket))
                Exit Select
            Case PacketType_.CloseWindow
                OnCloseWindow(DirectCast(p, CloseWindowPacket))
                Exit Select
            Case PacketType_.CollectItem
                OnCollectItem(DirectCast(p, CollectItemPacket))
                Exit Select
            Case PacketType_.DestroyEntity
                OnDestroyEntity(DirectCast(p, DestroyEntityPacket))
                Exit Select
            Case PacketType_.Disconnect
                OnDisconnect(DirectCast(p, DisconnectPacket))
                Console.WriteLine("Disconnecting: {0}", DirectCast(p, DisconnectPacket).Reason)
                Return False
            Case PacketType_.Entity
                OnEntity(DirectCast(p, CreateEntityPacket))
                Exit Select
            Case PacketType_.EntityAction
                OnEntityAction(DirectCast(p, EntityActionPacket))
                Exit Select
            Case PacketType_.EntityEquipment
                OnEntityEquipment(DirectCast(p, EntityEquipmentPacket))
                Exit Select
            Case PacketType_.EntityLook
                OnEntityLook(DirectCast(p, EntityLookPacket))
                Exit Select
            Case PacketType_.EntityLookAndRelativeMove
                OnEntityLookAndRelativeMove(DirectCast(p, EntityLookAndRelativeMovePacket))
                Exit Select
            Case PacketType_.EntityMetadata
                OnEntityMetadata(DirectCast(p, EntityMetadataPacket))
                Exit Select
            Case PacketType_.EntityPainting
                OnEntityPainting(DirectCast(p, EntityPaintingPacket))
                Exit Select
            Case PacketType_.EntityRelativeMove
                OnEntityRelativeMove(DirectCast(p, EntityRelativeMovePacket))
                Exit Select
            Case PacketType_.EntityStatus
                OnEntityStatus(DirectCast(p, EntityStatusPacket))
                Exit Select
            Case PacketType_.EntityTeleport
                OnEntityTeleport(DirectCast(p, EntityTeleportPacket))
                Exit Select
            Case PacketType_.EntityVelocity
                OnEntityVelocity(DirectCast(p, EntityVelocityPacket))
                Exit Select
            Case PacketType_.Explosion
                OnExplosion(DirectCast(p, ExplosionPacket))
                Exit Select
            Case PacketType_.Handshake
                OnHandshake(DirectCast(p, HandshakePacket))
                Exit Select
            Case PacketType_.HoldingChange
                OnHoldingChange(DirectCast(p, HoldingChangePacket))
                Exit Select
            Case PacketType_.KeepAlive
                OnKeepAlive(DirectCast(p, KeepAlivePacket))
                Exit Select
            Case PacketType_.LoginRequest
                OnLoginRequest(DirectCast(p, LoginRequestPacket))
                Exit Select
            Case PacketType_.MapChunk
                OnMapChunk(DirectCast(p, MapChunkPacket))
                Exit Select
            Case PacketType_.MobSpawn
                OnMobSpawn(DirectCast(p, MobSpawnPacket))
                Exit Select
            Case PacketType_.MultiBlockChange
                OnMultiBlockChange(DirectCast(p, MultiBlockChangePacket))
                Exit Select
            Case PacketType_.NamedEntitySpawn
                OnNamedEntitySpawn(DirectCast(p, NamedEntitySpawnPacket))
                Exit Select
            Case PacketType_.OpenWindow
                OnOpenWindow(DirectCast(p, OpenWindowPacket))
                Exit Select
            Case PacketType_.PickupSpawn
                OnPickupSpawn(DirectCast(p, SpawnItemPacket))
                Exit Select
            Case PacketType_.Player
                OnPlayer(DirectCast(p, PlayerPacket))
                Exit Select
            Case PacketType_.PlayerBlockPlacement
                OnPlayerBlockPlacement(DirectCast(p, PlayerBlockPlacementPacket))
                Exit Select
            Case PacketType_.PlayerDigging
                OnPlayerDigging(DirectCast(p, PlayerDiggingPacket))
                Exit Select
            Case PacketType_.PlayerPosition
                OnPlayerPosition(DirectCast(p, PlayerPositionPacket))
                Exit Select
            Case PacketType_.PlayerPositionRotation
                OnPlayerPositionRotation(DirectCast(p, PlayerPositionRotationPacket))
                Exit Select
            Case PacketType_.PlayerRotation
                OnPlayerRotation(DirectCast(p, PlayerRotationPacket))
                Exit Select
            Case PacketType_.PlayNoteBlock
                OnPlayNoteBlock(DirectCast(p, PlayNoteBlockPacket))
                Exit Select
            Case PacketType_.PreChunk
                OnPreChunk(DirectCast(p, PreChunkPacket))
                Exit Select
            Case PacketType_.Respawn
                OnRespawn(DirectCast(p, RespawnPacket))
                Exit Select
            Case PacketType_.SetSlot
                OnSetSlot(DirectCast(p, SetSlotPacket))
                Exit Select
            Case PacketType_.SpawnPosition
                OnSpawnPosition(DirectCast(p, SpawnPositionPacket))
                Exit Select
            Case PacketType_.TimeUpdate
                OnTimeUpdate(DirectCast(p, TimeUpdatePacket))
                Exit Select
            Case PacketType_.Transaction
                OnTransaction(DirectCast(p, TransactionPacket))
                Exit Select
            Case PacketType_.UnknownA
                OnUnknownA(DirectCast(p, UnknownAPacket))
                Exit Select
            Case PacketType_.UpdateHealth
                OnUpdateHealth(DirectCast(p, UpdateHealthPacket))
                Exit Select
            Case PacketType_.UpdateProgressBar
                OnUpdateProgressBar(DirectCast(p, UpdateProgressBarPacket))
                Exit Select
            Case PacketType_.UpdateSign
                OnUpdateSign(DirectCast(p, UpdateSignPacket))
                Exit Select
            Case PacketType_.UseBed
                OnUseBed(DirectCast(p, UseBedPacket))
                Exit Select
            Case PacketType_.UseEntity
                OnUseEntity(DirectCast(p, UseEntityPacket))
                Exit Select
            Case PacketType_.WindowClick
                OnWindowClick(DirectCast(p, WindowClickPacket))
                Exit Select
            Case PacketType_.WindowItems
                OnWindowItems(DirectCast(p, WindowItemsPacket))
                Exit Select
        End Select
        Return True
    End Function

    Private Sub OnAddObjectVehicle(ByVal p As AddObjectVehiclePacket)
        If AddObjectVehicle IsNot Nothing Then
            AddObjectVehicle.Invoke(Me, New PacketEventArgs(Of AddObjectVehiclePacket)(p))
        End If
    End Sub
    Private Sub OnAnimation(ByVal p As AnimationPacket)
        If Animation IsNot Nothing Then
            Animation.Invoke(Me, New PacketEventArgs(Of AnimationPacket)(p))
        End If
    End Sub
    Private Sub OnAttachEntity(ByVal p As AttachEntityPacket)
        If AttachEntity IsNot Nothing Then
            AttachEntity.Invoke(Me, New PacketEventArgs(Of AttachEntityPacket)(p))
        End If
    End Sub
    Private Sub OnBlockChange(ByVal p As BlockChangePacket)
        If BlockChange IsNot Nothing Then
            BlockChange.Invoke(Me, New PacketEventArgs(Of BlockChangePacket)(p))
        End If
    End Sub
    Private Sub OnChatMessage(ByVal p As ChatMessagePacket)
        If ChatMessage IsNot Nothing Then
            ChatMessage.Invoke(Me, New PacketEventArgs(Of ChatMessagePacket)(p))
        End If
    End Sub
    Private Sub OnCloseWindow(ByVal p As CloseWindowPacket)
        If CloseWindow IsNot Nothing Then
            CloseWindow.Invoke(Me, New PacketEventArgs(Of CloseWindowPacket)(p))
        End If
    End Sub
    Private Sub OnCollectItem(ByVal p As CollectItemPacket)
        If CollectItem IsNot Nothing Then
            CollectItem.Invoke(Me, New PacketEventArgs(Of CollectItemPacket)(p))
        End If
    End Sub
    Private Sub OnDestroyEntity(ByVal p As DestroyEntityPacket)
        If DestroyEntity IsNot Nothing Then
            DestroyEntity.Invoke(Me, New PacketEventArgs(Of DestroyEntityPacket)(p))
        End If
    End Sub
    Private Sub OnDisconnect(ByVal p As DisconnectPacket)
        If Disconnect IsNot Nothing Then
            Disconnect.Invoke(Me, New PacketEventArgs(Of DisconnectPacket)(p))
        End If
    End Sub
    Private Sub OnEntity(ByVal p As CreateEntityPacket)
        If Entity IsNot Nothing Then
            Entity.Invoke(Me, New PacketEventArgs(Of CreateEntityPacket)(p))
        End If
    End Sub
    Private Sub OnEntityAction(ByVal p As EntityActionPacket)
        If EntityAction IsNot Nothing Then
            EntityAction.Invoke(Me, New PacketEventArgs(Of EntityActionPacket)(p))
        End If
    End Sub
    Private Sub OnEntityEquipment(ByVal p As EntityEquipmentPacket)
        If EntityEquipment IsNot Nothing Then
            EntityEquipment.Invoke(Me, New PacketEventArgs(Of EntityEquipmentPacket)(p))
        End If
    End Sub
    Private Sub OnEntityLook(ByVal p As EntityLookPacket)
        If EntityLook IsNot Nothing Then
            EntityLook.Invoke(Me, New PacketEventArgs(Of EntityLookPacket)(p))
        End If
    End Sub
    Private Sub OnEntityLookAndRelativeMove(ByVal p As EntityLookAndRelativeMovePacket)
        If EntityLookAndRelativeMove IsNot Nothing Then
            EntityLookAndRelativeMove.Invoke(Me, New PacketEventArgs(Of EntityLookAndRelativeMovePacket)(p))
        End If
    End Sub
    Private Sub OnEntityMetadata(ByVal p As EntityMetadataPacket)
        If EntityMetadata IsNot Nothing Then
            EntityMetadata.Invoke(Me, New PacketEventArgs(Of EntityMetadataPacket)(p))
        End If
    End Sub
    Private Sub OnEntityPainting(ByVal p As EntityPaintingPacket)
        If EntityPainting IsNot Nothing Then
            EntityPainting.Invoke(Me, New PacketEventArgs(Of EntityPaintingPacket)(p))
        End If
    End Sub
    Private Sub OnEntityRelativeMove(ByVal p As EntityRelativeMovePacket)
        If EntityRelativeMove IsNot Nothing Then
            EntityRelativeMove.Invoke(Me, New PacketEventArgs(Of EntityRelativeMovePacket)(p))
        End If
    End Sub
    Private Sub OnEntityStatus(ByVal p As EntityStatusPacket)
        If EntityStatus IsNot Nothing Then
            EntityStatus.Invoke(Me, New PacketEventArgs(Of EntityStatusPacket)(p))
        End If
    End Sub
    Private Sub OnEntityTeleport(ByVal p As EntityTeleportPacket)
        If EntityTeleport IsNot Nothing Then
            EntityTeleport.Invoke(Me, New PacketEventArgs(Of EntityTeleportPacket)(p))
        End If
    End Sub
    Private Sub OnEntityVelocity(ByVal p As EntityVelocityPacket)
        If EntityVelocity IsNot Nothing Then
            EntityVelocity.Invoke(Me, New PacketEventArgs(Of EntityVelocityPacket)(p))
        End If
    End Sub
    Private Sub OnExplosion(ByVal p As ExplosionPacket)
        If Explosion IsNot Nothing Then
            Explosion.Invoke(Me, New PacketEventArgs(Of ExplosionPacket)(p))
        End If
    End Sub
    Private Sub OnHandshake(ByVal p As HandshakePacket)
        If Handshake IsNot Nothing Then
            Handshake.Invoke(Me, New PacketEventArgs(Of HandshakePacket)(p))
        End If
    End Sub
    Private Sub OnHoldingChange(ByVal p As HoldingChangePacket)
        If HoldingChange IsNot Nothing Then
            HoldingChange.Invoke(Me, New PacketEventArgs(Of HoldingChangePacket)(p))
        End If
    End Sub
    Private Sub OnKeepAlive(ByVal p As KeepAlivePacket)
        If KeepAlive IsNot Nothing Then
            KeepAlive.Invoke(Me, New PacketEventArgs(Of KeepAlivePacket)(p))
        End If
    End Sub
    Private Sub OnLoginRequest(ByVal p As LoginRequestPacket)
        If LoginRequest IsNot Nothing Then
            LoginRequest.Invoke(Me, New PacketEventArgs(Of LoginRequestPacket)(p))
        End If
    End Sub
    Private Sub OnMapChunk(ByVal p As MapChunkPacket)
        If MapChunk IsNot Nothing Then
            MapChunk.Invoke(Me, New PacketEventArgs(Of MapChunkPacket)(p))
        End If
    End Sub
    Private Sub OnMobSpawn(ByVal p As MobSpawnPacket)
        If MobSpawn IsNot Nothing Then
            MobSpawn.Invoke(Me, New PacketEventArgs(Of MobSpawnPacket)(p))
        End If
    End Sub
    Private Sub OnMultiBlockChange(ByVal p As MultiBlockChangePacket)
        If MultiBlockChange IsNot Nothing Then
            MultiBlockChange.Invoke(Me, New PacketEventArgs(Of MultiBlockChangePacket)(p))
        End If
    End Sub
    Private Sub OnNamedEntitySpawn(ByVal p As NamedEntitySpawnPacket)
        If NamedEntitySpawn IsNot Nothing Then
            NamedEntitySpawn.Invoke(Me, New PacketEventArgs(Of NamedEntitySpawnPacket)(p))
        End If
    End Sub
    Private Sub OnOpenWindow(ByVal p As OpenWindowPacket)
        If OpenWindow IsNot Nothing Then
            OpenWindow.Invoke(Me, New PacketEventArgs(Of OpenWindowPacket)(p))
        End If
    End Sub
    Private Sub OnPickupSpawn(ByVal p As SpawnItemPacket)
        If PickupSpawn IsNot Nothing Then
            PickupSpawn.Invoke(Me, New PacketEventArgs(Of SpawnItemPacket)(p))
        End If
    End Sub
    Private Sub OnPlayer(ByVal p As PlayerPacket)
        If Player IsNot Nothing Then
            Player.Invoke(Me, New PacketEventArgs(Of PlayerPacket)(p))
        End If
    End Sub
    Private Sub OnPlayerBlockPlacement(ByVal p As PlayerBlockPlacementPacket)
        If PlayerBlockPlacement IsNot Nothing Then
            PlayerBlockPlacement.Invoke(Me, New PacketEventArgs(Of PlayerBlockPlacementPacket)(p))
        End If
    End Sub
    Private Sub OnPlayerDigging(ByVal p As PlayerDiggingPacket)
        If PlayerDigging IsNot Nothing Then
            PlayerDigging.Invoke(Me, New PacketEventArgs(Of PlayerDiggingPacket)(p))
        End If
    End Sub
    Private Sub OnPlayerPosition(ByVal p As PlayerPositionPacket)
        If PlayerPosition IsNot Nothing Then
            PlayerPosition.Invoke(Me, New PacketEventArgs(Of PlayerPositionPacket)(p))
        End If
    End Sub
    Private Sub OnPlayerPositionRotation(ByVal p As PlayerPositionRotationPacket)
        If PlayerPositionRotation IsNot Nothing Then
            PlayerPositionRotation.Invoke(Me, New PacketEventArgs(Of PlayerPositionRotationPacket)(p))
        End If
    End Sub
    Private Sub OnPlayerRotation(ByVal p As PlayerRotationPacket)
        If PlayerRotation IsNot Nothing Then
            PlayerRotation.Invoke(Me, New PacketEventArgs(Of PlayerRotationPacket)(p))
        End If
    End Sub
    Private Sub OnPlayNoteBlock(ByVal p As PlayNoteBlockPacket)
        If PlayNoteBlock IsNot Nothing Then
            PlayNoteBlock.Invoke(Me, New PacketEventArgs(Of PlayNoteBlockPacket)(p))
        End If
    End Sub
    Private Sub OnPreChunk(ByVal p As PreChunkPacket)
        If PreChunk IsNot Nothing Then
            PreChunk.Invoke(Me, New PacketEventArgs(Of PreChunkPacket)(p))
        End If
    End Sub
    Private Sub OnRespawn(ByVal p As RespawnPacket)
        If Respawn IsNot Nothing Then
            Respawn.Invoke(Me, New PacketEventArgs(Of RespawnPacket)(p))
        End If
    End Sub
    Private Sub OnSetSlot(ByVal p As SetSlotPacket)
        If SetSlot IsNot Nothing Then
            SetSlot.Invoke(Me, New PacketEventArgs(Of SetSlotPacket)(p))
        End If
    End Sub
    Private Sub OnSpawnPosition(ByVal p As SpawnPositionPacket)
        If SpawnPosition IsNot Nothing Then
            SpawnPosition.Invoke(Me, New PacketEventArgs(Of SpawnPositionPacket)(p))
        End If
    End Sub
    Private Sub OnTimeUpdate(ByVal p As TimeUpdatePacket)
        If TimeUpdate IsNot Nothing Then
            TimeUpdate.Invoke(Me, New PacketEventArgs(Of TimeUpdatePacket)(p))
        End If
    End Sub
    Private Sub OnTransaction(ByVal p As TransactionPacket)
        If Transaction IsNot Nothing Then
            Transaction.Invoke(Me, New PacketEventArgs(Of TransactionPacket)(p))
        End If
    End Sub
    Private Sub OnUnknownA(ByVal p As UnknownAPacket)
        If UnknownA IsNot Nothing Then
            UnknownA.Invoke(Me, New PacketEventArgs(Of UnknownAPacket)(p))
        End If
    End Sub
    Private Sub OnUpdateHealth(ByVal p As UpdateHealthPacket)
        If UpdateHealth IsNot Nothing Then
            UpdateHealth.Invoke(Me, New PacketEventArgs(Of UpdateHealthPacket)(p))
        End If
    End Sub
    Private Sub OnUpdateProgressBar(ByVal p As UpdateProgressBarPacket)
        If UpdateProgressBar IsNot Nothing Then
            UpdateProgressBar.Invoke(Me, New PacketEventArgs(Of UpdateProgressBarPacket)(p))
        End If
    End Sub
    Private Sub OnUpdateSign(ByVal p As UpdateSignPacket)
        If UpdateSign IsNot Nothing Then
            UpdateSign.Invoke(Me, New PacketEventArgs(Of UpdateSignPacket)(p))
        End If
    End Sub
    Private Sub OnUseBed(ByVal p As UseBedPacket)
        If UseBed IsNot Nothing Then
            UseBed.Invoke(Me, New PacketEventArgs(Of UseBedPacket)(p))
        End If
    End Sub
    Private Sub OnUseEntity(ByVal p As UseEntityPacket)
        If UseEntity IsNot Nothing Then
            UseEntity.Invoke(Me, New PacketEventArgs(Of UseEntityPacket)(p))
        End If
    End Sub
    Private Sub OnWindowClick(ByVal p As WindowClickPacket)
        If WindowClick IsNot Nothing Then
            WindowClick.Invoke(Me, New PacketEventArgs(Of WindowClickPacket)(p))
        End If
    End Sub
    Private Sub OnWindowItems(ByVal p As WindowItemsPacket)
        If WindowItems IsNot Nothing Then
            WindowItems.Invoke(Me, New PacketEventArgs(Of WindowItemsPacket)(p))
        End If
    End Sub
End Class
