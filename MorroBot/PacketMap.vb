Imports System.Linq
Imports Morrolan.MorroBot.Packet

Public Class PacketMap

    '   Private Shared ReadOnly _Map As New Dictionary(Of PacketType, Type)() From { _
    ' {PacketType.AddObjectVehicle, GetType(AddObjectVehiclePacket)}, _
    ' {PacketType.Animation, GetType(AnimationPacket)}, _
    ' {PacketType.AttachEntity, GetType(AttachEntityPacket)}, _
    ' {PacketType.BlockChange, GetType(BlockChangePacket)}, _
    ' {PacketType.Chatmessage, GetType(ChatMessagePacket)}, _
    ' {PacketType.CloseWindow, GetType(CloseWindowPacket)}, _
    ' {PacketType.CollectItem, GetType(CollectItemPacket)}, _
    ' {PacketType.DestroyEntity, GetType(DestroyEntityPacket)}, _
    ' {PacketType.Disconnect, GetType(DisconnectPacket)}, _
    ' {PacketType.Entity, GetType(CreateEntityPacket)}, _
    ' {PacketType.EntityAction, GetType(EntityactionPacket)}, _
    ' {PacketType.EntityEquipment, GetType(EntityEquipmentPacket)}, _
    ' {PacketType.EntityLook, GetType(EntityLookPacket)}, _
    ' {PacketType.EntityLookAndRelativeMove, GetType(EntityLookAndRelativeMovePacket)}, _
    ' {PacketType.EntityMetaData, GetType(EntityMetaDataPacket)}, _
    ' {PacketType.EntityPainting, GetType(EntityPaintingPacket)}, _
    ' {PacketType.EntityRelativeMove, GetType(EntityRelativeMovePacket)}, _
    ' {PacketType.EntityStatus, GetType(EntityStatuspacket)}, _
    ' {PacketType.EntityTeleport, GetType(EntityTeleportPacket)}, _
    ' {PacketType.EntityVelocity, GetType(EntityVelocityPacket)}, _
    ' {PacketType.Explosion, GetType(ExplosionPacket)}, _
    ' {PacketType.Handshake, GetType(HandshakePacket)}, _
    ' {PacketType.HoldingChange, GetType(HoldingChangePacket)}, _
    ' {PacketType.IncrementStatistic, GetType(IncrementStatisticPacket)}, _
    ' {PacketType.Invalidbed, GetType(InvalidStatePacket)}, _
    ' {PacketType.KeepAlive, GetType(KeepAlivePacket)}, _
    ' {PacketType.LoginRequest, GetType(LoginRequestPacket)}, _
    ' {PacketType.MapChunk, GetType(MapChunkPacket)}, _
    ' {PacketType.MobSpawn, GetType(MobSpawnPacket)}, _
    ' {PacketType.MultiBlockChange, GetType(MultiBlockChangePacket)}, _
    ' {PacketType.NamedEntitySpawn, GetType(NamedEntitySpawnPacket)}, _
    ' {PacketType.OpenWindow, GetType(OpenWindowPacket)}, _
    ' {PacketType.PickupSpawn, GetType(SpawnItemPacket)}, _
    ' {PacketType.Player, GetType(PlayerPacket)}, _
    ' {PacketType.PlayerBlockPlacement, GetType(PlayerBlockPlacementPacket)}, _
    ' {PacketType.PlayerDigging, GetType(PlayerDiggingPacket)}, _
    ' {PacketType.PlayerPosition, GetType(PlayerPositionPacket)}, _
    ' {PacketType.PlayerPositionRotation, GetType(PlayerPositionRotationPacket)}, _
    ' {PacketType.PlayerRotation, GetType(PlayerRotationPacket)}, _
    ' {PacketType.PlayNoteBlock, GetType(PlayNoteBlockPacket)}, _
    ' {PacketType.PreChunk, GetType(PreChunkPacket)}, _
    ' {PacketType.Respawn, GetType(RespawnPacket)}, _
    ' {PacketType.SetSlot, GetType(SetSlotPacket)}, _
    ' {PacketType.SpawnPosition, GetType(SpawnPositionPacket)}, _
    ' {PacketType.TimeUpdate, GetType(TimeUpdatePacket)}, _
    ' {PacketType.Transaction, GetType(TransactionPacket)}, _
    ' {PacketType.UnknownA, GetType(UnknownAPacket)}, _
    ' {PacketType.UpdateHealth, GetType(UpdateHealthPacket)}, _
    ' {PacketType.UpdateProgressBar, GetType(UpdateProgressBarPacket)}, _
    ' {PacketType.UpdateSign, GetType(UpdateSignPacket)}, _
    ' {PacketType.UseBed, GetType(UseBedPacket)}, _
    ' {PacketType.UseEntity, GetType(UseEntityPacket)}, _
    ' {PacketType.Weather, GetType(WeatherPacket)}, _
    ' {PacketType.WindowClick, GetType(WindowClickPacket)}, _
    ' {PacketType.WindowItems, GetType(WindowItemsPacket)} _
    '}


    Private Shared ReadOnly _Map As New Dictionary(Of PacketType, Type)() From { _
  {PacketType.AddObjectVehicle, GetType(AddObjectVehiclePacket)}, _
  {PacketType.Animation, GetType(AnimationPacket)}, _
  {PacketType.AttachEntity, GetType(AttachEntityPacket)}, _
  {PacketType.BlockChange, GetType(BlockChangePacket)}, _
  {PacketType.Chatmessage, GetType(ChatMessagePacket)}, _
  {PacketType.CloseWindow, GetType(CloseWindowPacket)}, _
  {PacketType.CollectItem, GetType(CollectItemPacket)}, _
  {PacketType.DestroyEntity, GetType(DestroyEntityPacket)}, _
  {PacketType.Disconnect, GetType(DisconnectPacket)}, _
  {PacketType.DoorChange, GetType(DoorChangePacket)}, _
  {PacketType.Entity, GetType(CreateEntityPacket)}, _
  {PacketType.EntityAction, GetType(EntityactionPacket)}, _
  {PacketType.EntityEquipment, GetType(EntityEquipmentPacket)}, _
  {PacketType.EntityLook, GetType(EntityLookPacket)}, _
  {PacketType.EntityLookAndRelativeMove, GetType(EntityLookAndRelativeMovePacket)}, _
  {PacketType.EntityMetaData, GetType(EntityMetaDataPacket)}, _
  {PacketType.EntityPainting, GetType(EntityPaintingPacket)}, _
  {PacketType.EntityRelativeMove, GetType(EntityRelativeMovePacket)}, _
  {PacketType.EntityStatus, GetType(EntityStatuspacket)}, _
  {PacketType.EntityTeleport, GetType(EntityTeleportPacket)}, _
  {PacketType.EntityVelocity, GetType(EntityVelocityPacket)}, _
  {PacketType.Explosion, GetType(ExplosionPacket)}, _
  {PacketType.Handshake, GetType(HandshakePacket)}, _
  {PacketType.HoldingChange, GetType(HoldingChangePacket)}, _
  {PacketType.IncrementStatistic, GetType(IncrementStatisticPacket)}, _
  {PacketType.Invalidbed, GetType(InvalidStatePacket)}, _
  {PacketType.KeepAlive, GetType(KeepAlivePacket)}, _
  {PacketType.LoginRequest, GetType(LoginRequestPacket)}, _
  {PacketType.MapData, GetType(MapDataPacket)}, _
  {PacketType.MapChunk, GetType(MapChunkPacket)}, _
  {PacketType.MobSpawn, GetType(MobSpawnPacket)}, _
  {PacketType.MultiBlockChange, GetType(MultiBlockChangePacket)}, _
  {PacketType.NamedEntitySpawn, GetType(NamedEntitySpawnPacket)}, _
  {PacketType.OpenWindow, GetType(OpenWindowPacket)}, _
  {PacketType.PickupSpawn, GetType(SpawnItemPacket)}, _
  {PacketType.Player, GetType(PlayerPacket)}, _
  {PacketType.PlayerBlockPlacement, GetType(PlayerBlockPlacementPacket)}, _
  {PacketType.PlayerDigging, GetType(PlayerDiggingPacket)}, _
  {PacketType.PlayerPosition, GetType(PlayerPositionPacket)}, _
  {PacketType.PlayerPositionRotation, GetType(PlayerPositionRotationPacket)}, _
  {PacketType.PlayerRotation, GetType(PlayerRotationPacket)}, _
  {PacketType.PlayNoteBlock, GetType(PlayNoteBlockPacket)}, _
  {PacketType.PreChunk, GetType(PreChunkPacket)}, _
  {PacketType.Respawn, GetType(RespawnPacket)}, _
  {PacketType.SetSlot, GetType(SetSlotPacket)}, _
  {PacketType.SpawnPosition, GetType(SpawnPositionPacket)}, _
  {PacketType.TimeUpdate, GetType(TimeUpdatePacket)}, _
  {PacketType.Transaction, GetType(TransactionPacket)}, _
  {PacketType.UnknownA, GetType(UnknownAPacket)}, _
  {PacketType.UpdateHealth, GetType(UpdateHealthPacket)}, _
  {PacketType.UpdateProgressBar, GetType(UpdateProgressBarPacket)}, _
  {PacketType.UpdateSign, GetType(UpdateSignPacket)}, _
  {PacketType.UseBed, GetType(UseBedPacket)}, _
  {PacketType.UseEntity, GetType(UseEntityPacket)}, _
  {PacketType.Weather, GetType(WeatherPacket)}, _
  {PacketType.WindowClick, GetType(WindowClickPacket)}, _
  {PacketType.WindowItems, GetType(WindowItemsPacket)} _
 }




    Public Shared ReadOnly Property Map() As Dictionary(Of PacketType, Type)
        Get
            Return _Map
        End Get
    End Property

    Public Shared Function GetPacketType(ByVal Type As Type) As PacketType

        For Each t As PacketType In Map.Keys

            If Map(t) = Type Then Return t

        Next

        Throw New KeyNotFoundException("Key not found: " & Type.ToString)

    End Function

End Class
