Public Enum PacketType_ As Byte
    ' All directions have been verified in the sourcecode
    KeepAlive = &H0
    ' c <-> s
    LoginRequest = &H1
    '   <->
    Handshake = &H2
    '   <->
    ChatMessage = &H3
    '   <->
    TimeUpdate = &H4
    '   <--
    EntityEquipment = &H5
    '   <--
    SpawnPosition = &H6
    '   <--
    UseEntity = &H7
    '   -->
    UpdateHealth = &H8
    '   <--
    Respawn = &H9
    '   <->
    Player = &HA
    '   <->
    PlayerPosition = &HB
    '   <->
    PlayerRotation = &HC
    '   <->
    PlayerPositionRotation = &HD
    '   <->
    PlayerDigging = &HE
    '   -->
    PlayerBlockPlacement = &HF
    '   -->
    HoldingChange = &H10
    '   -->
    UseBed = &H11
    '   <--
    Animation = &H12
    '   <->
    EntityAction = &H13
    '   -->
    NamedEntitySpawn = &H14
    '   <--
    PickupSpawn = &H15
    '   <->
    CollectItem = &H16
    '   <--
    AddObjectVehicle = &H17
    '   <--
    MobSpawn = &H18
    '   <--
    EntityPainting = &H19
    '   <--
    UnknownA = &H1B
    '   -->
    EntityVelocity = &H1C
    '   <--
    DestroyEntity = &H1D
    '   <--
    Entity = &H1E
    '   <--
    EntityRelativeMove = &H1F
    '   <--
    EntityLook = &H20
    '   <--
    EntityLookAndRelativeMove = &H21
    '   <--
    EntityTeleport = &H22
    '   <--
    EntityStatus = &H26
    '   <--
    AttachEntity = &H27
    '   <--
    EntityMetadata = &H28
    '   <--
    PreChunk = 50
    '   <--
    MapChunk = 51
    '   <--
    MultiBlockChange = 52
    '   <--
    BlockChange = 53
    '   <--
    PlayNoteBlock = 54
    '   <--
    Explosion = 60
    '   <--
    InvalidBed = 70
    '   <--
    Weather = 71
    '   <--
    OpenWindow = &H64
    '   <--
    CloseWindow = &H65
    '   <->
    WindowClick = &H66
    '   -->
    SetSlot = &H67
    '   <--
    WindowItems = &H68
    '   <--
    UpdateProgressBar = &H69
    '   <--
    Transaction = &H6A
    '   <->
    UpdateSign = &H82
    '   <->
    IncrementStatistic = 200
    '   <--
    Disconnect = &HFF
    '   <->
End Enum
