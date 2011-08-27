Public Module ChatColors

    Public Black As New ChatColor With {.ChatCode = "§0", .Color = Color.Black}
    Public DarkBlue As New ChatColor With {.ChatCode = "§1", .Color = Color.DarkBlue}
    Public DarkGreen As New ChatColor With {.ChatCode = "§2", .Color = Color.DarkGreen}
    Public DarkCyan As New ChatColor With {.ChatCode = "§3", .Color = Color.DarkCyan}
    Public DarkRed As New ChatColor With {.ChatCode = "§4", .Color = Color.DarkRed}
    Public Purple As New ChatColor With {.ChatCode = "§5", .Color = Color.Purple}
    Public Gold As New ChatColor With {.ChatCode = "§6", .Color = Color.Gold}
    Public Gray As New ChatColor With {.ChatCode = "§7", .Color = Color.Gray}
    Public DarkGray As New ChatColor With {.ChatCode = "§8", .Color = Color.DarkGray}
    Public Blue As New ChatColor With {.ChatCode = "§9", .Color = Color.Blue}
    Public BrightGreen As New ChatColor With {.ChatCode = "§a", .Color = Color.LightGreen}
    Public Cyan As New ChatColor With {.ChatCode = "§b", .Color = Color.Cyan}
    Public Red As New ChatColor With {.ChatCode = "§c", .Color = Color.Red}
    Public Pink As New ChatColor With {.ChatCode = "§d", .Color = Color.Pink}
    Public Yellow As New ChatColor With {.ChatCode = "§e", .Color = Color.Yellow}
    Public White As New ChatColor With {.ChatCode = "§f", .Color = Color.White}

End Module

Public Class ChatColor

    Public ChatCode As String
    Public Color As Color

End Class