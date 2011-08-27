Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports Earth2Me.Kreate.Net


Public Class BigEndianStream
    Inherits Stream

    Public Net As NetworkStream


    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return Net.CanRead
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return Net.CanSeek
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return Net.CanWrite
        End Get
    End Property

    Public Overrides ReadOnly Property Length As Long
        Get
            Return Net.Length
        End Get
    End Property

    Public Overrides Property Position As Long
        Get
            Return Net.Position
        End Get
        Set(ByVal value As Long)
            Net.Position = value
        End Set
    End Property



    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer

        Return Net.Read(buffer, offset, count)

    End Function

    Public Overrides Function Seek(ByVal offset As Long, ByVal origin As System.IO.SeekOrigin) As Long

        Return Net.Seek(offset, origin)

    End Function

    Public Overrides Sub SetLength(ByVal value As Long)

        Net.SetLength(value)

    End Sub

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)

        Net.Write(buffer, offset, count)

    End Sub

    Public Overrides Sub Flush()

        Net.Flush()

    End Sub



    Public Sub New(ByVal Stream As NetworkStream)

        Net = Stream

    End Sub



    Public Function ReadPacket() As Packet

        Dim Type As PacketType = CType(ReadByte(), PacketType)

        If Type = &H33 Then
            Dim libPacket As New libMapChunkPacket(Net)
            libPacket.Read()

            Dim p As New MapChunkPacket()
            Return p
        Else
            Dim Packet As Packet = Packet.Read(Type, Me)
            Return Packet
        End If



    End Function

    Public Shadows Function ReadByte() As Byte

        Dim b As Integer = Net.ReadByte

        If b >= Byte.MinValue AndAlso b <= Byte.MaxValue Then
            Return CByte(b)
        End If
        Throw New EndOfStreamException("End of stream, invalid byte length")

    End Function

    Public Function ReadString16(ByVal maxLenght As Short) As String

        Dim Length As Integer = ReadShort()

        If Length > maxLenght Then Throw New IOException("String too long!")

        Dim b As Byte() = New Byte(Length * 2) {}

        For i As Integer = 0 To Length * 2 - 1
            b(i) = CByte(ReadByte())
        Next
        Return ASCIIEncoding.BigEndianUnicode.GetString(b)

    End Function

    Public Function ReadString8(ByVal maxLength As Short) As String

        Dim len As Integer = ReadShort()

        If len > maxLength Then Throw New IOException("String field too long")

        Dim b() As Byte = New Byte(len) {}

        For i As Integer = 0 To len - 1
            b(i) = CByte(ReadByte())
        Next

        Return ASCIIEncoding.UTF8.GetString(b)

    End Function

    Public Function ReadShort() As Short

        'Dim z As Short
        'Dim x As Byte = ReadByte()
        'Dim y As Byte = ReadByte()

        'Dim w() As Byte = New Byte(2) {}
        'w(1) = x
        'w(0) = y

        'z = BitConverter.ToInt16(w, 0)

        'z = ((x << 8) Or y)

        'Return z

        Return CShort((ReadByte() << 8) Or ReadByte())

    End Function

    Public Function ReadInt() As Integer

        Dim a As Byte = ReadByte()
        Dim b As Byte = ReadByte()
        Dim c As Byte = ReadByte()
        Dim d As Byte = ReadByte()

        Dim w As Integer = a << 24
        Dim x As Integer = b << 16
        Dim y As Integer = c << 8
        Dim z As Integer = d

        Dim f As Integer = w + x + y + z
        Return f

        'Dim f As Integer = (a << 24) Or (b << 16) Or (c << 8) Or (d)
        Return f

        Return (ReadByte() << 24) Or (ReadByte() << 16) Or (ReadByte() << 8) Or ReadByte()

    End Function

    Public Function ReadLong() As Long

        Return (ReadByte() << 56) Or (ReadByte() << 48) Or (ReadByte() << 40) Or (ReadByte() << 32) Or (ReadByte() << 24) Or (ReadByte() << 16) Or (ReadByte() << 8) Or (ReadByte())

    End Function

    Public Function ReadSByte() As SByte

        Return CSByte(ReadByte())

    End Function

    Public Function ReadBool() As Boolean

        Return ReadByte() = 1

    End Function

    Public Function ReadSingle() As Single

        Dim i As Integer = ReadInt()
        Return CType(i, Single)

    End Function

    Public Function ReadDouble() As Double

        Dim r As Byte() = New Byte(7) {}
        For i As Integer = 7 To 0 Step -1
            r(i) = CByte(ReadByte())
        Next
        Return BitConverter.ToDouble(r, 0)

    End Function

    Public Function ReadDoublePacked() As Double

        Return CDbl(ReadInt()) / 32

    End Function

    Public Function ReadMetaData() As MetaData

        Return New MetaData(Me)

    End Function


    Public Sub WritePacket(ByVal Packet As Packet)

        ' If frmMorroBot.Debug = True Then LogParser.p("Writing packet of type " & Packet.GetPacketType)

        Write(CByte(Packet.GetPacketType))
        Packet.WriteFlush(Me)

#If Debug = True Then
        LogParser.Parse_Sent(Packet.GetPacketType)
#End If

    End Sub

    Public Overloads Sub Write(ByVal Data As String)

        Dim b() As Byte = ASCIIEncoding.BigEndianUnicode.GetBytes(Data)
        Write(CShort(Data.Length))
        Write(b, 0, b.Length)

    End Sub

    Public Sub Write8(ByVal Data As String)

        Dim b() As Byte = ASCIIEncoding.UTF8.GetBytes(Data)
        Write(CShort(b.Length))
        Write(b, 0, b.Length)

    End Sub

    Public Overloads Sub Write(ByVal Data As Short)

        Write(CByte(Data >> 8))
        Write(CByte(Data))

    End Sub

    Public Overloads Sub Write(ByVal Data As Byte)

        Net.WriteByte(Data)

    End Sub

    Public Overloads Sub Write(ByVal Data As Integer)

        Write(CByte(Data >> 24))
        Write(CByte(Data >> 16))
        Write(CByte(Data >> 8))
        Write(CByte(Data))

    End Sub

    Public Overloads Sub Write(ByVal Data As Long)
        Write(CByte(Data >> 56))
        Write(CByte(Data >> 48))
        Write(CByte(Data >> 40))
        Write(CByte(Data >> 32))
        Write(CByte(Data >> 24))
        Write(CByte(Data >> 16))
        Write(CByte(Data >> 8))
        Write(CByte(Data))
    End Sub

    Public Overloads Sub Write(ByVal Data As SByte)

        Write(CByte(Data))

    End Sub

    Public Overloads Sub Write(ByVal Data As Boolean)

        Write(CByte(If(Data = True, 1, 0)))

    End Sub

    Public Overloads Sub Write(ByVal Data As Single)

        Write(CInt(Data))

    End Sub

    Public Overloads Sub Write(ByVal Data As Double)

        Write(CType(Data, Long))

    End Sub

    Public Overloads Sub Write(ByVal Data As MetaData)

        Data.Write(Me)

    End Sub

End Class
