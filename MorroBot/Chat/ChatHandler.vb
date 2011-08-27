Public Class ChatHandler

    Public Event ChatMessage(ByVal Message As ChatMessage)
    Public Event PrivateMessage(ByVal Message As PrivateMessage)
    Public Event CommandMessage(ByVal Message As CommandMessage)
    Public Event FAQMessage(ByVal Message As FAQMessage)

    Private Const MaxChatLength As Integer = 90

    Private Client As frmMorroBot

    Public Sub New(ByVal Client As frmMorroBot)

        Me.Client = Client

        InitializeEvents()

    End Sub

    Private Sub InitializeEvents()

        AddHandler Client.PacketHandler.ChatMessage, AddressOf PacketHandler_ChatMessage

    End Sub


    Public Sub SendChat(ByVal Text As String)

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim lines As New List(Of String)

        Do While i < Text.Length

            If Text.Length - i > MaxChatLength Then
                lines.Add(Text.Substring(i, i + MaxChatLength))
                i += MaxChatLength
                j += 1
            Else
                lines.Add(Text.Substring(i, Text.Length - i))
                i += 90
            End If

        Loop

        For Each l As String In lines

            Client.PacketHandler.SendPacket(New ChatMessagePacket() With {.Message = l})

        Next


    End Sub


    Private Sub PacketHandler_ChatMessage(ByVal sender As Object, ByVal e As PacketEventArgs(Of ChatMessagePacket))

        'LogParser.Parse_Chat(e.Packet)

        Dim Message As Message = Message.GetMessage(e.Packet)

        Select Case Message.Type

            Case MessageType.ChatMessage
                'MsgBox("1")
                onChatMessage(DirectCast(Message, ChatMessage))
            Case MessageType.PrivateMessage
                'MsgBox("2")
                onPrivateMessage(DirectCast(Message, PrivateMessage))
            Case MessageType.CommandMessage
                'MsgBox("3")
                onChatMessage(New ChatMessage() With {.Player = Message.Player, .Message = "!" & Message.Message})
                onCommandMessage(DirectCast(Message, CommandMessage))
            Case MessageType.FAQMessage
                'MsgBox("4")
                onChatMessage(New ChatMessage() With {.Player = Message.Player, .Message = "??" & Message.Message})
                onFAQMessage(DirectCast(Message, FAQMessage))
        End Select

    End Sub


    Private Sub onChatMessage(ByVal Message As ChatMessage)

        RaiseEvent ChatMessage(Message)

    End Sub

    Private Sub onPrivateMessage(ByVal Message As PrivateMessage)

        RaiseEvent PrivateMessage(Message)

    End Sub

    Private Sub onCommandMessage(ByVal Message As CommandMessage)

        RaiseEvent CommandMessage(Message)

    End Sub

    Private Sub onFAQMessage(ByVal Message As FAQMessage)

        RaiseEvent FAQMessage(Message)

    End Sub

End Class
