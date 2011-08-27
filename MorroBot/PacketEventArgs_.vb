Imports System.Linq

Public Delegate Sub PacketEventHandler(Of T As Packet_)(ByVal sender As Object, ByVal e As PacketEventArgs(Of T))

Public Class PacketEventArgs(Of T As Packet_)
    Inherits EventArgs

    Public Property Packet() As T
        Get
            Return m_Packet
        End Get
        Private Set(ByVal value As T)
            m_Packet = Value
        End Set
    End Property

    Private m_Packet As T

    Public Sub New(ByVal packet__1 As T)
        Packet = packet__1
    End Sub

End Class
