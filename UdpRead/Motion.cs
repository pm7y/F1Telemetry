namespace UdpRead;

public readonly struct CarMotionData(byte[] bytes, int startIndex = 0)
{
    public float WorldPositionX { get; } = BitConverter.ToSingle(bytes, startIndex);
    public float WorldPositionY { get; } = BitConverter.ToSingle(bytes, startIndex + 4);
    public float WorldPositionZ { get; } = BitConverter.ToSingle(bytes, startIndex + 8);
    public float WorldVelocityX { get; } = BitConverter.ToSingle(bytes, startIndex + 12);
    public float WorldVelocityY { get; } = BitConverter.ToSingle(bytes, startIndex + 16);
    public float WorldVelocityZ { get; } = BitConverter.ToSingle(bytes, startIndex + 20);
    public short WorldForwardDirX { get; } = BitConverter.ToInt16(bytes, startIndex + 24);
    public short WorldForwardDirY { get; } = BitConverter.ToInt16(bytes, startIndex + 26);
    public short WorldForwardDirZ { get; } = BitConverter.ToInt16(bytes, startIndex + 28);
    public short WorldRightDirX { get; } = BitConverter.ToInt16(bytes, startIndex + 30);
    public short WorldRightDirY { get; } = BitConverter.ToInt16(bytes, startIndex + 32);
    public short WorldRightDirZ { get; } = BitConverter.ToInt16(bytes, startIndex + 34);
    public float GForceLateral { get; } = BitConverter.ToSingle(bytes, startIndex + 36);
    public float GForceLongitudinal { get; } = BitConverter.ToSingle(bytes, startIndex + 40);
    public float GForceVertical { get; } = BitConverter.ToSingle(bytes, startIndex + 44);
    public float Yaw { get; } = BitConverter.ToSingle(bytes, startIndex + 48);
    public float Pitch { get; } = BitConverter.ToSingle(bytes, startIndex + 52);
    public float Roll { get; } = BitConverter.ToSingle(bytes, startIndex + 56);
}

public readonly struct MotionData
{
    public PacketHeader Header { get; }
    public CarMotionData[] CarMotionData { get; }

    public MotionData(PacketHeader header, byte[] bytes)
    {
        Header = header;

        CarMotionData = new CarMotionData[22];
        for (int i = 0; i < 22; i++)
        {
            CarMotionData[i] = new CarMotionData(bytes, 24 + i * 60);
        }
    }

    public static MotionData FromBytes(PacketHeader header, byte[] bytes)
    {
        return new MotionData(header, bytes);
    }
}
