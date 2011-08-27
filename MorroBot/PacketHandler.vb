Imports System.Threading

Public Class PacketHandler

    Public Event KeepAlive As PacketEventHandler(Of KeepAlivePacket)
    Public Event Handshake As PacketEventHandler(Of HandshakePacket)
    Public Event LoginRequest As PacketEventHandler(Of LoginRequestPacket)
    Public Event ChatMessage As PacketEventHandler(Of ChatMessagePacket)

    Private m_Net As BigEndianStream

    Private ReadOnly PacketQueue As New Queue(Of Packet)
    Private ReadOnly QueueThread As Thread

    Private Running As Boolean = True

    Private RxThread As Thread


    Public Property Net() As BigEndianStream
        Get
            Return m_Net
        End Get
        Private Set(ByVal value As BigEndianStream)
            m_Net = value
        End Set
    End Property



    Public Sub New(ByVal Stream As BigEndianStream)

        Net = Stream

        QueueThread = New Thread(AddressOf QueueProc)
        QueueThread.IsBackground = True
        QueueThread.Start()

        RxThread = New Thread(AddressOf RxProc)
        RxThread.Priority = ThreadPriority.AboveNormal
        RxThread.IsBackground = True
        RxThread.Start()

    End Sub


    Public Sub SendPacket(ByVal Packet As Packet)

        'If MorroBot.Debug = True Then Console.WriteLine("Enqueued packet of type {0}", Packet.GetPacketType)

        SyncLock PacketQueue
            PacketQueue.Enqueue(Packet)
        End SyncLock

    End Sub

    Private Sub QueueProc()

        Do While VolatileRead(Running) = True

            Do While PacketQueue.Count > 0 AndAlso VolatileRead(Running) = True

                Dim Packet As Packet

                SyncLock PacketQueue
                    Packet = PacketQueue.Dequeue
                End SyncLock

                Try

                    Net.WritePacket(Packet)

                Catch ex As Exception

                    LogParser.Parse_Errors(ex)
                    Return

                End Try

            Loop

            Thread.Sleep(10)

        Loop

    End Sub

    Public Sub Dispose()

        VolatileWrite(Running, False)

    End Sub

    Private Sub RxProc()

        Do While VolatileRead(Running) AndAlso ProcessPacket() = True
            Thread.Sleep(1)
        Loop
        'Do While True
        '    ProcessPacket()
        '    Thread.Sleep(1)
        'Loop

        Dispose()

    End Sub

    Public Function ProcessPacket() As Boolean

        Dim p As Packet
        Dim type As PacketType

        Try

            p = Net.ReadPacket
            type = p.GetPacketType

        Catch ex As Exception

            LogParser.Parse_Errors(ex)
            Return False

        End Try



        'LogParser.Parse_Received(p.GetPacketType)

        Select Case type
            Case PacketType.KeepAlive
                OnKeepAlive(DirectCast(p, KeepAlivePacket))
            Case PacketType.LoginRequest
                OnLoginRequest(DirectCast(p, LoginRequestPacket))
            Case PacketType.Handshake
                OnHandshake(DirectCast(p, HandshakePacket))
            Case PacketType.Chatmessage
                OnChatMessage(DirectCast(p, ChatMessagePacket))
            Case PacketType.MapChunk

        End Select

        Return True

    End Function


    Function VolatileRead(Of T)(ByRef Address As T) As T

        VolatileRead = Address
        Threading.Thread.MemoryBarrier()

    End Function

    Sub VolatileWrite(Of T)(ByRef Address As T, ByVal Value As T)

        Threading.Thread.MemoryBarrier()
        Address = Value

    End Sub


    Private Sub OnKeepAlive(ByVal Packet As KeepAlivePacket)

        RaiseEvent KeepAlive(Me, New PacketEventArgs(Of KeepAlivePacket)(Packet))

    End Sub

    Private Sub OnLoginRequest(ByVal Packet As LoginRequestPacket)

        RaiseEvent LoginRequest(Me, New PacketEventArgs(Of LoginRequestPacket)(Packet))

    End Sub

    Private Sub OnHandshake(ByVal Packet As HandshakePacket)

        RaiseEvent Handshake(Me, New PacketEventArgs(Of HandshakePacket)(Packet))

    End Sub

    Private Sub OnChatMessage(ByVal Packet As ChatMessagePacket)

        RaiseEvent ChatMessage(Me, New PacketEventArgs(Of ChatMessagePacket)(Packet))

    End Sub

End Class
