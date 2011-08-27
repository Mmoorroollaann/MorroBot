Public Class InvalidStatePacket
    Inherits Packet

    Public Property Reason As InvalidReason

    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Reason = CType(Stream.ReadByte, InvalidReason)

    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.WriteByte(CByte(Reason))

    End Sub


    Public Enum InvalidReason As Byte

        InvalidBed = 0
        NullA = 1
        NullB = 2

    End Enum

End Class