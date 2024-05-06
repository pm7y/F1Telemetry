namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct LapHistoryData
{
    public uint LapTimeInMS { get; }
    public ushort Sector1TimeInMS { get; }
    public byte Sector1TimeMinutes { get; }
    public ushort Sector2TimeInMS { get; }
    public byte Sector2TimeMinutes { get; }
    public ushort Sector3TimeInMS { get; }
    public byte Sector3TimeMinutes { get; }
    public byte LapValidBitFlags { get; }

    public LapHistoryData(byte[] bytes, int startIndex = 0)
    {
        LapTimeInMS = BitConverter.ToUInt32(bytes, startIndex);
        Sector1TimeInMS = BitConverter.ToUInt16(bytes, startIndex + 4);
        Sector1TimeMinutes = bytes[startIndex + 6];
        Sector2TimeInMS = BitConverter.ToUInt16(bytes, startIndex + 7);
        Sector2TimeMinutes = bytes[startIndex + 9];
        Sector3TimeInMS = BitConverter.ToUInt16(bytes, startIndex + 10);
        Sector3TimeMinutes = bytes[startIndex + 12];
        LapValidBitFlags = bytes[startIndex + 13];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct TyreStintHistoryData(byte[] bytes, int startIndex = 0)
{
    public byte EndLap { get; } = bytes[startIndex];
    public byte TyreActualCompound { get; } = bytes[startIndex + 1];
    public byte TyreVisualCompound { get; } = bytes[startIndex + 2];
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketSessionHistoryData
{
    public PacketHeader Header { get; }
    public byte CarIdx { get; }
    public byte NumLaps { get; }
    public byte NumTyreStints { get; }
    public byte BestLapTimeLapNum { get; }
    public byte BestSector1LapNum { get; }
    public byte BestSector2LapNum { get; }
    public byte BestSector3LapNum { get; }
    public LapHistoryData[] LapHistoryData { get; }
    public TyreStintHistoryData[] TyreStintsHistoryData { get; }

    public PacketSessionHistoryData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarIdx = bytes[24];
        NumLaps = bytes[25];
        NumTyreStints = bytes[26];
        BestLapTimeLapNum = bytes[27];
        BestSector1LapNum = bytes[28];
        BestSector2LapNum = bytes[29];
        BestSector3LapNum = bytes[30];
        
        LapHistoryData = new LapHistoryData[100];
        for (int i = 0; i < 100; i++)
        {
            LapHistoryData[i] = new LapHistoryData(bytes, 31 + i * 14);
        }

        TyreStintsHistoryData = new TyreStintHistoryData[8];
        for (int i = 0; i < 8; i++)
        {
            TyreStintsHistoryData[i] = new TyreStintHistoryData(bytes, 1431 + i * 3);
        }
    }

    public static PacketSessionHistoryData FromBytes(byte[] bytes)
    {
        return new PacketSessionHistoryData(bytes);
    }
}
