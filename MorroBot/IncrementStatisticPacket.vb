Public Class IncrementStatisticPacket
    Inherits Packet

    Public Property Statistic As Integer
    Public Property Amount As Byte

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Statistic = Stream.ReadInt
        Amount = Stream.ReadByte

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(Statistic)
        Stream.Write(Amount)

    End Sub

    Public Enum Statistics As Integer

        StartGame = 1000
        CreateWorld = 1001
        LoadWorld = 1002
        JoinMultiplayer = 1003
        LeaveGame = 1004
        PlayOneMinute = 1100
        WalkOneCm = 2000
        SwimOneCm = 2001
        FallOneCm = 2002
        ClimbOneCm = 2003
        FlyOneCm = 2004
        DiveOneCm = 2005
        MinecartOneCm = 2006
        BoatOneCm = 2007
        PigOneCm = 2008
        Jump = 2010
        Drop = 2011
        DamageDealt = 2020
        DamageTaken = 2021
        Deaths = 2022
        MobKills = 2023
        PlayerKills = 2024
        FishCaught = 2025
        MineBlock = 16777216    ' Note: Add an item ID to this value
        CraftItem = 16842752    ' Note: Add an item ID to this value
        UseItem = 16908288      ' Note: Add an item ID to this value
        BreakItem = 16973824    ' Note: Add an item ID to this value

    End Enum

End Class
