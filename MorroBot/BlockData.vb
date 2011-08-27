Public Class BlockData

    '0    1    2    3    4    5    6    7    8    9    a    b    c    d    e    f
    Public Shared ReadOnly Opacity As Byte() = New Byte() _
    {&H0, &HF, &HF, &HF, &HF, &HF, &H0, &HF, &H2, &H2, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &H1, &HF, &H0, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &H0, &H0, &H0, &H0, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &H0, &H0, &H0, &HF, &HF, &H0, &HF, &HF, &HF, &H0, &HF, &HF, &HF, &H0,
     &H0, &H0, &H0, &HF, &H0, &H0, &H0, &H0, &H0, &HF, &HF, &H0, &H0, &H0, &HF, &H2,
     &HF, &H0, &H0, &H0, &HF, &H0, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &H0, &H0, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF,
     &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF, &HF}


    '0    1    2    3    4    5    6    7    8    9    a    b    c    d    e    f
    Public Shared ReadOnly Luminence As Byte() = New Byte() _
    {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HF, &HF, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &HE, &HF, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H9, &H0, &H0, &H7, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &HF, &H0, &HF, &H0, &H0, &H7, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0,
     &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Public Shared ReadOnly Air As Blocks() = New Blocks() _
                                             {Blocks.Crops,
                                              Blocks.Redstone_Repeater,
                                              Blocks.Redstone_Repeater_On,
                                              Blocks.Redstone_Torch,
                                              Blocks.Redstone_Torch_On,
                                              Blocks.Redstone_Wire,
                                              Blocks.Reed,
                                              Blocks.Sapling,
                                              Blocks.Torch,
                                              Blocks.Air,
                                              Blocks.Brown_Mushroom,
                                              Blocks.Red_Mushroom,
                                              Blocks.Red_Rose,
                                              Blocks.Sapling,
                                              Blocks.Yellow_Flower,
                                              Blocks.Water,
                                              Blocks.Still_Water,
                                              Blocks.Rails,
                                              Blocks.Sign_Post,
                                              Blocks.Wall_Sign,
                                              Blocks.Iron_Door,
                                              Blocks.Wooden_Pressure_Plate,
                                              Blocks.Stone_Pressure_Plate,
                                              Blocks.Fire}

    Public Enum Blocks As Byte
        'Invalid = 255,

        'Block Tiles
        Air = 0
        Stone = 1
        Grass = 2
        Dirt = 3
        Cobblestone = 4
        Wood = 5
        Sapling = 6
        Bedrock = 7
        Adminium = 7
        Water = 8
        Stationary_Water = 9
        Still_Water = 9
        Lava = 10
        Stationary_Lava = 11
        Still_Lava = 11
        Sand = 12
        Gravel = 13
        Gold_Ore = 14
        Iron_Ore = 15
        Coal_Ore = 16
        Log = 17
        Leaves = 18
        Sponge = 19
        Glass = 20
        Lapis_Lazuli_Ore = 21
        Lapis_Lazuli_Block = 22
        Dispenser = 23
        Sandstone = 24
        Note_Block = 25
        Bed = 26

        Cloth = 35
        Wool = 35
        Yellow_Flower = 37
        Flower = 37
        Red_Rose = 38
        Rose = 38
        Brown_Mushroom = 39
        Red_Mushroom = 40
        Gold_Block = 41
        Iron_Block = 42
        Double_Stair = 43
        Double_Stone_Slab = 43
        Stair = 44
        Slab = 44
        Brick = 45
        TNT = 46
        Bookcase = 47
        Bookshelf = 47
        Mossy_Cobblestone = 48
        Obsidian = 49
        Torch = 50
        Fire = 51
        Mob_Spawner = 52
        Wooden_Stairs = 53
        Chest = 54
        Redstone_Wire = 55
        Diamond_Ore = 56
        Diamond_Block = 57
        Workbench = 58
        Crops = 59
        Soil = 60
        Furnace = 61
        Burning_Furnace = 62
        Sign_Post = 63
        Ladder = 65
        Minecart_Rail = 66
        Rails = 66
        Track = 66
        Tracks = 66
        Cobblestone_Stairs = 67
        Stone_Stairs = 67
        Wall_Sign = 68
        Lever = 69
        Stone_Pressure_Plate = 70
        Iron_Door = 71
        Wooden_Pressure_Plate = 72
        Redstone_Ore = 73
        Redstone_Ore_Glowing = 74
        Redstone_Torch = 75
        Redstone_Torch_On = 76
        Stone_Button = 77
        Snow = 78
        Ice = 79
        Snow_Block = 80
        Cactus = 81
        Clay = 82
        Reed = 83
        Jukebox = 84
        Fence = 85
        Pumpkin = 86
        Bloodstone = 87
        Netherrack = 87
        Slow_Sand = 88
        Soul_Sand = 88
        Lightstone = 89
        Glowstone = 89
        Portal = 90
        Jack_O_Lantern = 91
        Pumpkin_Lantern = 91
        Cake = 92
        Redstone_Repeater = 93
        Redstone_Repeater_On = 94
    End Enum

End Class
