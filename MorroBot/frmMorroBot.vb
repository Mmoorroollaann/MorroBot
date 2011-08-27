Imports System.Net.Sockets
Imports System.Net
Imports System.Threading

Public Class frmMorroBot

#Const debug = False

    Private TCP As TcpClient
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend PacketHandler As PacketHandler

    Private Const Protocol As Integer = 14
    Private Const Version As Integer = 14

    Private Server As String = "localhost"
    'Private Server As String = "minecraft.lethal-zone.eu"
    Private Username As String = "Morrobot"
    Private port As Integer = 25565
    Private Password As String = "Jaheira16"

    Private Running As Boolean = False
    Private WriteAllowed As Boolean = True

    Private TimeBetweenTicks As Integer = 1000

    Private UpdateToggle As Threading.Thread
    Private UpdateEnabled As Boolean = True

    Friend ChatHandler As ChatHandler

    Private CommandHandler As CommandHandler

    Public Delegate Sub dUpdateText(ByVal Text As String)
    Public Delegate Sub dToggleUpdate()

    Public Sub Parse_Raw(ByVal Text As String)

        If WriteAllowed = False Then Return

        lstOutputRaw.Items.Add(Now.TimeOfDay.ToString & ": " & Text)
        lstOutputRaw.SelectedIndex = lstOutputRaw.Items.Count - 1

    End Sub

    Public Sub Parse_Events(ByVal Text As String)

        lstOutputEvents.Items.Add(Now.TimeOfDay.ToString & ": " & Text)

    End Sub

    Public Sub Parse_Received(ByVal Text As String)

        If WriteAllowed = False Then Return

        lstOutputReceived.Items.Add(Now.TimeOfDay.ToString & ": " & Text)
        lstOutputReceived.SelectedIndex = lstOutputReceived.Items.Count - 1

        'If Text.Contains("WindowItems") = True Then
        '    lstOutputSpecial.Items.Add("WindowItems packet received")
        'End If

        'If Text.Contains("PlayerPositionRotation") = True Then
        '    lstOutputSpecial.Items.Add("PlayerPositionRotation packet received")
        'End If


    End Sub

    Public Sub Parse_Sent(ByVal Text As String)

        'If WriteAllowed = False Then Return

        lstOutputSent.Items.Add(Now.TimeOfDay.ToString & ": " & Text)

    End Sub

    Public Sub Parse_Errors(ByVal Text As String)

        lstOutputError.Items.Add(Now.TimeOfDay.ToString & ": " & Text)

    End Sub

    Public Sub Parse_Special(ByVal Text As String)


        'TextBox1.AppendText(Text & vbCrLf)


    End Sub

    Public Sub Parse_Chat(ByVal Text As String)

        lstOutputChat.Items.Add(Text)

    End Sub

    Public Sub ToggleUpdate()

        If UpdateEnabled = True Then
            lstOutputRaw.BeginUpdate()
            lstOutputReceived.BeginUpdate()
            UpdateEnabled = False
        Else
            lstOutputRaw.EndUpdate()
            lstOutputReceived.EndUpdate()
            UpdateEnabled = True
        End If

    End Sub

    Private Sub Toggle_Update()

        Do While Running = True

            Dim f As frmMorroBot = DirectCast(My.Application.OpenForms("frmMorroBot"), frmMorroBot)

            f.Invoke(New dToggleUpdate(AddressOf f.ToggleUpdate))
            Thread.Sleep(1000)

            f.Invoke(New dToggleUpdate(AddressOf f.ToggleUpdate))
            Thread.Sleep(10)

        Loop

    End Sub


    Friend Sub run()

        Try
            Initialize_Connection()
        Catch ex As SocketException
            LogParser.Parse_Raw("Could not connect to the server at " & Server & " using port " & port)
            Exit Sub
        End Try
        Initialize_ChatHandler()
        Initialize_CommandHandler()
        Initialize_Events()
        Send_Handshake()

        Running = True
        'Console.ReadLine()
        'UpdateToggle = New Threading.Thread(AddressOf Toggle_Update)
        'UpdateToggle.Start()

    End Sub



    Private Sub Initialize_Connection()


        If TCP IsNot Nothing AndAlso TCP.Connected = True Then TCP.Close()

        TCP = New TcpClient(Server, port)
        PacketHandler = New PacketHandler(New BigEndianStream(TCP.GetStream))

    End Sub

    Private Sub Initialize_ChatHandler()

        ChatHandler = New ChatHandler(Me)

    End Sub

    Private Sub Initialize_CommandHandler()

        CommandHandler = New CommandHandler(Me)

    End Sub

    Private Sub Initialize_Events()

        AddHandler PacketHandler.KeepAlive, AddressOf Packethandler_KeepAlive
        AddHandler PacketHandler.LoginRequest, AddressOf Packethandler_LoginRequest
        AddHandler PacketHandler.Handshake, AddressOf Packethandler_Handshake

        AddHandler ChatHandler.ChatMessage, AddressOf ChatHandler_Chatmessage

        'AddHandler ChatHandler.CommandMessage, AddressOf ChatHandler_CommandMessage

    End Sub

    'Private Sub ChatHandler_CommandMessage(ByVal Command As CommandMessage)
    '    LogParser.Parse_Chat("received command")
    'End Sub



    Private Sub StartTick()

        Dim thread As New Thread(New ThreadStart(AddressOf TickProc))
        thread.IsBackground = True
        thread.Start()

    End Sub

    Private Sub TickProc()

        Do While Running = True

            Tick()

            Thread.Sleep(TimeBetweenTicks)

        Loop

    End Sub

    Private Sub Tick()

        Send_Player()
        'Send_KeepAlive()

    End Sub



    Private Sub Send_Handshake()

        Dim p As New HandshakePacket
        p.UsernameOrHash = Username

        PacketHandler.SendPacket(p)

    End Sub

    Private Sub Send_Login_Request()

        Dim p As New LoginRequestPacket

        p.Dimension = 0
        p.MapSeed = 0
        p.ProtocolOrEntityID = Protocol
        p.Username = Username

        PacketHandler.SendPacket(p)

    End Sub

    Private Sub Send_KeepAlive()

        Dim Packet As New KeepAlivePacket

        PacketHandler.SendPacket(Packet)

    End Sub

    Private Sub Send_Player()

        Dim Packet As New PlayerPacket
        Packet.OnGround = True

        PacketHandler.SendPacket(Packet)

    End Sub

    Private Sub Send_PlayerPosition()



    End Sub

    Private Sub Send_PlayerRotation()

        PacketHandler.SendPacket(New PlayerRotationPacket With {.OnGround = True, .Pitch = 0, .Yaw = 0})

    End Sub


    Friend Sub Disconnect(ByVal Reason As String)

        PacketHandler.SendPacket(New DisconnectPacket() With {.Reason = Reason})
        Thread.Sleep(10)
        PacketHandler.Dispose()
        Running = False

    End Sub


    Private Sub Packethandler_KeepAlive(ByVal sender As Object, ByVal e As PacketEventArgs(Of KeepAlivePacket))

        Send_KeepAlive()

    End Sub

    Private Sub Packethandler_LoginRequest(ByVal sender As Object, ByVal e As PacketEventArgs(Of LoginRequestPacket))

        LogParser.Parse_Events(e)

        StartTick()

        'Dim Hans As New ChatMessagePacket()
        'Hans.Message = "Hello world."
        'PacketHandler.SendPacket(Hans)

    End Sub

    Private Sub Packethandler_Handshake(ByVal sender As Object, ByVal e As PacketEventArgs(Of HandshakePacket))

        LogParser.Parse_Events(e)

        e.Packet.UsernameOrHash = e.Packet.UsernameOrHash.Remove(e.Packet.UsernameOrHash.Count - 1, 1)

        If e.Packet.UsernameOrHash <> "-" Then

            Dim web As New WebClient()

            Try

                Dim result As String = web.DownloadString("https://login.minecraft.net/" & _
                                                          "?user=" & Username & _
                                                          "&password=" & Password & _
                                                          "&version=" & Version _
                                                          )

                If result.Contains(":") = False Then
                    MsgBox("Login failed: " & result)
                    Return
                End If

                Dim Parts() As String = result.Split(CChar(":"))
                Dim session As String = Parts(3).Trim
                Username = Parts(2)

                result = web.DownloadString("http://www.minecraft.net/game/joinserver.jsp" & _
                                             "?user=" & Username & _
                                             "&sessionId=" & session & _
                                             "&serverId=" & e.Packet.UsernameOrHash)


                If result.StartsWith("OK") = False Then
                    MsgBox("Login failed: " & result)
                    Return
                    'Else
                    'MsgBox("OK")
                End If




            Catch ex As Exception

            End Try

        End If

        Send_Login_Request()

    End Sub

    Private Sub Packethandler_Chatmessage(ByVal sender As Object, ByVal e As PacketEventArgs(Of ChatMessagePacket))

        LogParser.Parse_Events(e)

        'LogParser.Parse_Chat(e.Packet)

        'ChatHandler.AddChatPacket(e.Packet)


    End Sub


    Private Sub ChatHandler_Chatmessage(ByVal Message As ChatMessage)

        LogParser.Parse_Chat("[" & TimeOfDay & "] " & Message.Player & ": " & Message.Message)

    End Sub

    Private Sub ChatHandler_Privatemessage(ByVal Message As PrivateMessage)

        LogParser.Parse_Chat(Message.Player & ": " & Message.Message)

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click

        Dim mainthread As New Thread(AddressOf run)
        mainthread.IsBackground = True
        mainthread.Start()

        'run()

        'While Running = True

        '    Application.DoEvents()

        'End While

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdToggleOutput.Click

        Select Case WriteAllowed
            Case True
                WriteAllowed = False
            Case Else
                WriteAllowed = True
        End Select

    End Sub

    Private Sub lstOutputRaw_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstOutputRaw.SelectedIndexChanged

        'txtOutputDetail.Text = lstOutputRaw.SelectedItem.ToString

    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click

        Disconnect("Disconnect.quitting")

    End Sub

    Private Sub txtChatInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChatInput.KeyDown

        If Running = False Then Return

        If e.KeyCode = Keys.Enter Then

            ChatHandler.SendChat(txtChatInput.Text)
            txtChatInput.Clear()

            ' Um den Fehlerton zu unterdrücken, der entsteht da ein Enter in einer Textbox mit multiline=false eigentlich nicht zulässig ist.
            e.SuppressKeyPress = True

        End If

    End Sub

End Class