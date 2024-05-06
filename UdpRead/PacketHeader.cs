namespace UdpRead;

public readonly struct PacketHeader(byte[] bytes)
{
    public ushort PacketFormat { get; } = BitConverter.ToUInt16(bytes, 0);
    public byte GameYear { get; } = bytes[2];
    public byte GameMajorVersion { get; } = bytes[3];
    public byte GameMinorVersion { get; } = bytes[4];
    public byte PacketVersion { get; } = bytes[5];
    public byte PacketId { get; } = bytes[6];
    public ulong SessionUID { get; } = BitConverter.ToUInt64(bytes, 7);
    public float SessionTime { get; } = BitConverter.ToSingle(bytes, 15);
    public uint FrameIdentifier { get; } = BitConverter.ToUInt32(bytes, 19);
    public uint OverallFrameIdentifier { get; } = BitConverter.ToUInt32(bytes, 23);
    public byte PlayerCarIndex { get; } = bytes[27];
    public byte SecondaryPlayerCarIndex { get; } = bytes[28];

    public static PacketHeader FromBytes(byte[] bytes)
    {
        return new PacketHeader(bytes);
    }
}