Friend Class MapChunkPacket
    Inherits Packet


    Public ReadOnly Property X As Integer

        Get
            Return Chunk.X
        End Get

    End Property

    Public ReadOnly Property Y As Short

        Get
            Return Chunk.Y
        End Get

    End Property

    Public ReadOnly Property Z As Integer

        Get
            Return Chunk.Z
        End Get

    End Property

    Public ReadOnly Property SizeX As Byte

        Get
            Return CByte(Chunk.SizeX - 1)
        End Get

    End Property

    Public ReadOnly Property SizeY As Byte

        Get
            Return CByte(Chunk.SizeY - 1)
        End Get

    End Property

    Public ReadOnly Property SizeZ As Byte

        Get
            Return CByte(Chunk.SizeZ - 1)
        End Get

    End Property

    Public Property Chunk As Chunk


    Public Overloads Overrides Sub Read(ByVal Stream As BigEndianStream)

        Stream.ReadByte()
        Stream.ReadByte()
        Stream.ReadByte()
        Stream.ReadByte()

        Stream.ReadByte()
        Stream.ReadByte()

        Stream.ReadByte()
        Stream.ReadByte()
        Stream.ReadByte()
        Stream.ReadByte()

        Stream.ReadByte()

        Stream.ReadByte()

        Stream.ReadByte()

        Dim length As Integer
        length = Stream.ReadInt

        For i = 0 To length - 1
            Stream.ReadByte()
        Next

        LogParser.Parse_Special(TimeOfDay.TimeOfDay.ToString & " Received MapChunk packet, length=" & length.ToString)

        'Dim posX As Integer = Stream.ReadInt
        'Dim posY As Short = Stream.ReadShort
        'Dim posZ As Integer = Stream.ReadInt

        'Dim _sizeX As Byte = CByte(Stream.ReadByte + 1)
        'Dim _sizeY As Byte = CByte(Stream.ReadByte + 1)
        'Dim _sizeZ As Byte = CByte(Stream.ReadByte + 1)

        'Dim o As Integer = _sizeX * _sizeY * _sizeZ
        'Chunk = New Chunk(posX, posY, posZ, _sizeX, _sizeX, _sizeZ)

        'Dim len As Integer = Stream.ReadInt
        'Dim comp As Byte() = New Byte(len - 1) {}
        ''Dim data As Byte() = New Byte((o * 5 \ 2)) {}
        'len = Stream.Read(comp, 0, len)

        '     	Inflater inflater = new Inflater();
        '     Try
        '{
        '	inflater.setInput(comp);
        '	inflater.inflate(data);
        '}
        '     Finally
        '{
        '	inflater.end();
        '}

        '     		int i = 0;
        'for (int x = 0; x < Chunk.SizeX; x++)
        '{
        '	for (int z = 0; z < Chunk.SizeZ; z++)
        '	{
        '		for (int y = 0; y < Chunk.SizeY; y++)
        '		{
        '			int s = ((i + 1) & 1) * 4;
        '			int ofst = i;
        '			Chunk[x, y, z] = data[ofst];
        '			ofst = i / 2 + o;
        '			Chunk.SetData(x, y, z, unchecked((byte)((data[ofst] >> s) & 0xf)));
        '			ofst = i / 2 + o * 3 / 2;
        '			Chunk.SetBlockLight(x, y, z, unchecked((byte)((data[ofst] >> s) & 0xf)));
        '			ofst = i / 2 + o * 2;
        '			// For some reason, this next line doesn't work right.
        '			//Chunk.SetSkyLight(x, y, z, unchecked((byte)((data[ofst] >> s) & 0xf)));
        '			Chunk.SetSkyLight(x, y, z, (byte)0xf);
        '			i++;
        '		}
        '	}
        '}



    End Sub

    Public Overrides Sub Write(ByVal Stream As BigEndianStream)

        Stream.Write(X)
        Stream.Write(Y)
        Stream.Write(Z)
        Stream.Write(SizeX)
        Stream.Write(SizeY)
        Stream.Write(SizeZ)

        ' Much more, including a Deflater. Most likely a part of that DLL it requires, will not use this here, cause I don't need to be able to write it.

    End Sub

End Class
