Public Class PacketMap_

    Public Shared Function GetPacketType(ByVal type As Type) As PacketType_
        For Each t As PacketType In Map.Keys
            If Map(t) = type Then
                Return t
            End If
        Next
        Throw New KeyNotFoundException()
    End Function

End Class
