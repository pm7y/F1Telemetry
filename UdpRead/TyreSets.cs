namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct TyreSetData
{
    public byte ActualTyreCompound { get; }
    public byte VisualTyreCompound { get; }
    public byte Wear { get; }
    public byte Available { get; }
    public byte RecommendedSession { get; }
    public byte LifeSpan { get; }
    public byte UsableLife { get; }
    public short LapDeltaTime { get; }
    public byte Fitted { get; }

    public TyreSetData(byte[] bytes, int startIndex = 0)
    {
        ActualTyreCompound = bytes[startIndex];
        VisualTyreCompound = bytes[startIndex + 1];
        Wear = bytes[startIndex + 2];
        Available = bytes[startIndex + 3];
        RecommendedSession = bytes[startIndex + 4];
        LifeSpan = bytes[startIndex + 5];
        UsableLife = bytes[startIndex + 6];
        LapDeltaTime = BitConverter.ToInt16(bytes, startIndex + 7);
        Fitted = bytes[startIndex + 9];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketTyreSetsData
{
    public PacketHeader Header { get; }
    public byte CarIdx { get; }
    public TyreSetData[] TyreSetData { get; }
    public byte FittedIdx { get; }

    public PacketTyreSetsData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarIdx = bytes[24];
        TyreSetData = new TyreSetData[20];
        for (int i = 0; i < 20; i++)
        {
            TyreSetData[i] = new TyreSetData(bytes, 25 + i * 10);
        }
        FittedIdx = bytes[225];
    }

    public static PacketTyreSetsData FromBytes(byte[] bytes)
    {
        return new PacketTyreSetsData(bytes);
    }
}
