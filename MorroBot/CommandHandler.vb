Public Class CommandHandler

    Private Client As frmMorroBot
    'Private ChatHandler As ChatHandler

    Public Sub New(ByVal Client As frmMorroBot)

        Me.Client = Client
        'Me.ChatHandler = Client.ChatHandler

        InitializeEvents()

    End Sub

    Private Sub InitializeEvents()

        AddHandler Client.ChatHandler.CommandMessage, AddressOf ChatHandler_CommandMessage
        'AddHandler ChatHandler.CommandMessage, AddressOf ChatHandler_CommandMessage

    End Sub

    Private Sub ChatHandler_CommandMessage(ByVal Command As CommandMessage)

        Dim RawCommand() As String = Command.Message.Split(CChar(" "))

        Select Case RawCommand(0)

            Case "quit"
                Command_Quit("Killed", Command)
            Case "version"
                Command_Version(Command)
            Case "slap"
                Command_Slap(Command)

        End Select

    End Sub

    Private Sub Command_Quit(ByVal Reason As String, ByVal Command As CommandMessage)

        If Command.Player = "Morrolan" Or Command.Player = "Server" Then Client.Disconnect("killed")

    End Sub

    Private Sub Command_Version(ByVal Command As CommandMessage)

        Dim text As String
        text = "MorroBot version: " & My.Application.Info.Version.ToString(3)
        Client.ChatHandler.SendChat(text)
        text = "Contact Morrolan for support."
        Client.ChatHandler.SendChat(text)

    End Sub

    Private Sub Command_Slap(ByVal Command As CommandMessage)

        Dim args() As String = Command.Message.Split(CChar(" "))
        If args.Length > 1 Then
            Dim Slapping As String = args(1)
            Client.ChatHandler.SendChat("/me slaps " & Slapping & " with a large trout")
        End If

    End Sub

End Class
