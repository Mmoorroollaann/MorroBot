Public Class LogParser

    Public Delegate Sub Parse(ByVal Text As String)
    Public Delegate Sub ParseMultiline(ByVal Text As String)

    Public Shared Sub Parse_Raw(ByVal Text As String)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Raw), New Object() {Text})

    End Sub

    Public Shared Sub Parse_Events(ByVal RaisedEvent As EventArgs)

        Dim Text As String

        Text = "Event raised: " & RaisedEvent.ToString

        Parse_Raw(Text)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Events), New Object() {Text})

    End Sub

    Public Shared Sub Parse_Received(ByVal pType As PacketType)

        Dim Text As String

        Text = "Received packet with id " & pType.ToString & " : " & [Enum].GetName(GetType(PacketType), pType)

        Parse_Raw(Text)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Received), New Object() {Text})

    End Sub

    Public Shared Sub Parse_Sent(ByVal pType As PacketType)

        Dim Text As String

        Text = "Sent packet with id " & pType.ToString & " : " & [Enum].GetName(GetType(PacketType), pType)

        Parse_Raw(Text)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Sent), New Object() {Text})

    End Sub

    Public Shared Sub Parse_Errors(ByVal Ex As Exception)

        Dim text As String

        text = "Exception occurred: " & vbCrLf & Ex.Message

        Parse_Raw(text)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Errors), New Object() {Text})

    End Sub

    Public Shared Sub Parse_Special(ByVal Text As String)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New ParseMultiline(AddressOf f.Parse_Special), New Object() {Text})

    End Sub

    Public Overloads Shared Sub Parse_Chat(ByVal ChatMessage As ChatMessagePacket)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Chat), New Object() {ChatMessage.Message})

    End Sub

    Public Overloads Shared Sub Parse_Chat(ByVal ChatMessage As String)

        Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

        f.Invoke(New Parse(AddressOf f.Parse_Chat), New Object() {ChatMessage})

    End Sub

End Class
