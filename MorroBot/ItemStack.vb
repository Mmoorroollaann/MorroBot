Public Class ItemStack

    Friend Property Slot As Short

    Public Shared ReadOnly Property Void As ItemStack

        Get
            Return New ItemStack(-1, 0, 0)
        End Get

    End Property

    Private m_Type As Short
    Public Property type As Short

        Get
            Return m_Type
        End Get

        Set(ByVal value As Short)
            m_Type = value
        End Set

    End Property

    Private m_Count As SByte
    Public Property Count As SByte

        Get
            Return m_Count
        End Get

        Set(ByVal value As SByte)
            m_Count = value
        End Set

    End Property

    Private m_Durability As Short
    Public Property Durability As Short

        Get
            Return m_Durability
        End Get

        Set(ByVal value As Short)
            m_Durability = value
        End Set

    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal Type As Short)

        Me.type = Type
        Count = 1
        Durability = 0

    End Sub

    Public Sub New(ByVal Type As Short, ByVal count As SByte, ByVal durability As Short)

        Me.type = Type
        Me.Count = count
        Me.Durability = durability

    End Sub

    Friend Sub New(ByVal Stream As BigEndianStream)

        type = Stream.ReadShort
        'Stream.ReadShort()
        'type = -1
        If type <> -1 And type <> 255 Then
            Count = Stream.ReadSByte
            Durability = Stream.ReadShort
        End If

    End Sub


    Friend Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(type)
        If type >= 0 Then
            Stream.Write(Count)
            Stream.Write(Durability)
        End If

    End Sub

End Class
