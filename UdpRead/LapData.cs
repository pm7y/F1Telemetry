namespace UdpRead;

using System;

public readonly struct LapData(byte[] bytes, int startIndex = 0)
{
    public uint LastLapTimeInMS { get; } = BitConverter.ToUInt32(bytes, startIndex);
    public uint CurrentLapTimeInMS { get; } = BitConverter.ToUInt32(bytes, startIndex + 4);
    public ushort Sector1TimeInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 8);
    public byte Sector1TimeMinutes { get; } = bytes[startIndex + 10];
    public ushort Sector2TimeInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 11);
    public byte Sector2TimeMinutes { get; } = bytes[startIndex + 13];
    public ushort DeltaToCarInFrontInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 14);
    public ushort DeltaToRaceLeaderInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 16);
    public float LapDistance { get; } = BitConverter.ToSingle(bytes, startIndex + 18);
    public float TotalDistance { get; } = BitConverter.ToSingle(bytes, startIndex + 22);
    public float SafetyCarDelta { get; } = BitConverter.ToSingle(bytes, startIndex + 26);
    public byte CarPosition { get; } = bytes[startIndex + 30];
    public byte CurrentLapNum { get; } = bytes[startIndex + 31];
    public byte PitStatus { get; } = bytes[startIndex + 32];
    public byte NumPitStops { get; } = bytes[startIndex + 33];
    public byte Sector { get; } = bytes[startIndex + 34];
    public byte CurrentLapInvalid { get; } = bytes[startIndex + 35];
    public byte Penalties { get; } = bytes[startIndex + 36];
    public byte TotalWarnings { get; } = bytes[startIndex + 37];
    public byte CornerCuttingWarnings { get; } = bytes[startIndex + 38];
    public byte NumUnservedDriveThroughPens { get; } = bytes[startIndex + 39];
    public byte NumUnservedStopGoPens { get; } = bytes[startIndex + 40];
    public byte GridPosition { get; } = bytes[startIndex + 41];
    public byte DriverStatus { get; } = bytes[startIndex + 42];
    public byte ResultStatus { get; } = bytes[startIndex + 43];
    public byte PitLaneTimerActive { get; } = bytes[startIndex + 44];
    public ushort PitLaneTimeInLaneInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 45);
    public ushort PitStopTimerInMS { get; } = BitConverter.ToUInt16(bytes, startIndex + 47);
    public byte PitStopShouldServePen { get; } = bytes[startIndex + 49];
}

public readonly struct PacketLapData
{
    public PacketHeader Header { get; }
    public LapData[] LapData { get; }
    public byte TimeTrialPBCarIdx { get; }
    public byte TimeTrialRivalCarIdx { get; }

    public PacketLapData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        LapData = new LapData[22];

        for (int i = 0; i < 22; i++)
        {
            LapData[i] = new LapData(bytes, 24 + i * 50);
        }

        TimeTrialPBCarIdx = bytes[24 + 22 * 50];
        TimeTrialRivalCarIdx = bytes[25 + 22 * 50];
    }

    public static PacketLapData FromBytes(byte[] bytes)
    {
        return new PacketLapData(bytes);
    }
}
