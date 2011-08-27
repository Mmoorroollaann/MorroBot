Imports System.Text.RegularExpressions

Public Class Message

    Private Const SeparatorChannelStart As Char = CChar("[")
    Private Const SeparatorChannelEnd As Char = CChar("]")
    Private Const SeparatorUserStart As Char = CChar("<")
    Private Const SeparatorUserEnd As Char = CChar(">")

    Public Property Type As MessageType
    Public Property Message As String
    Public Property Time As String
    Public Property Player As String

    Public Shared Function GetMessage(ByVal ChatPacket As ChatMessagePacket) As Message

        Dim m As String = ChatPacket.Message
        Dim messages() As String
        Dim ProcessedMessage As New Message

        m = Regex.Replace(m, "§[0-9a-f]", String.Empty)


        messages = m.Split(CChar(" "))

        If messages.Length < 2 Then Return New ChatMessage() With {.Message = "", .Player = ""}

        If messages(0) = "[Server]" Then
            ProcessedMessage = New ChatMessage
            ProcessedMessage.Player = "Server"
            ProcessedMessage.Time = DateTime.Now.TimeOfDay.ToString
            m = m.Remove(m.Length - 1)
            m = m.Replace("[Server]", String.Empty)
            ProcessedMessage.Message = m
        End If

        If messages(0).StartsWith(SeparatorChannelStart) And messages(0).EndsWith(SeparatorChannelEnd) Then
            ProcessedMessage = New ChatMessage
            ProcessedMessage.Player = messages(1)
            m = m.Remove(m.Length - 1)
            m = m.Replace(messages(0), String.Empty)
            ProcessedMessage.Message = m
        End If


        Return ProcessedMessage


    End Function

End Class

Public Class ChatMessage
    Inherits Message


    Public Sub New()
        Me.Type = MessageType.ChatMessage
    End Sub

End Class

Public Class PrivateMessage
    Inherits Message


    Public Sub New()
        Me.Type = MessageType.PrivateMessage
    End Sub

End Class

Public Class FAQMessage
    Inherits Message


    Public Sub New()
        Me.Type = MessageType.FAQMessage
    End Sub

End Class

Public Class CommandMessage
    Inherits Message


    Public Sub New()
        Me.Type = MessageType.CommandMessage
    End Sub

End Class

Public Enum MessageType

    ChatMessage
    PrivateMessage
    ServerMessage
    CommandMessage
    FAQMessage

End Enum
