namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct FinalClassificationData
{
    public byte Position { get; }
    public byte NumLaps { get; }
    public byte GridPosition { get; }
    public byte Points { get; }
    public byte NumPitStops { get; }
    public byte ResultStatus { get; }
    public uint BestLapTimeInMS { get; }
    public double TotalRaceTime { get; }
    public byte PenaltiesTime { get; }
    public byte NumPenalties { get; }
    public byte NumTyreStints { get; }
    public byte[] TyreStintsActual { get; }
    public byte[] TyreStintsVisual { get; }
    public byte[] TyreStintsEndLaps { get; }

    public FinalClassificationData(byte[] bytes, int startIndex = 0)
    {
        Position = bytes[startIndex];
        NumLaps = bytes[startIndex + 1];
        GridPosition = bytes[startIndex + 2];
        Points = bytes[startIndex + 3];
        NumPitStops = bytes[startIndex + 4];
        ResultStatus = bytes[startIndex + 5];
        BestLapTimeInMS = BitConverter.ToUInt32(bytes, startIndex + 6);
        TotalRaceTime = BitConverter.ToDouble(bytes, startIndex + 10);
        PenaltiesTime = bytes[startIndex + 18];
        NumPenalties = bytes[startIndex + 19];
        NumTyreStints = bytes[startIndex + 20];

        TyreStintsActual = new byte[8];
        TyreStintsVisual = new byte[8];
        TyreStintsEndLaps = new byte[8];
        Array.Copy(bytes, startIndex + 21, TyreStintsActual, 0, 8);
        Array.Copy(bytes, startIndex + 29, TyreStintsVisual, 0, 8);
        Array.Copy(bytes, startIndex + 37, TyreStintsEndLaps, 0, 8);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketFinalClassificationData
{
    public PacketHeader Header { get; }
    public byte NumCars { get; }
    public FinalClassificationData[] ClassificationData { get; }

    public PacketFinalClassificationData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        NumCars = bytes[24];
        ClassificationData = new FinalClassificationData[22];
        for (int i = 0; i < 22; i++)
        {
            ClassificationData[i] = new FinalClassificationData(bytes, 25 + i * 45);
        }
    }

    public static PacketFinalClassificationData FromBytes(byte[] bytes)
    {
        return new PacketFinalClassificationData(bytes);
    }
}
