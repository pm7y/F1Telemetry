namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct CarStatusData
{
    public byte TractionControl { get; }
    public byte AntiLockBrakes { get; }
    public byte FuelMix { get; }
    public byte FrontBrakeBias { get; }
    public byte PitLimiterStatus { get; }
    public float FuelInTank { get; }
    public float FuelCapacity { get; }
    public float FuelRemainingLaps { get; }
    public ushort MaxRPM { get; }
    public ushort IdleRPM { get; }
    public byte MaxGears { get; }
    public byte DrsAllowed { get; }
    public ushort DrsActivationDistance { get; }
    public byte ActualTyreCompound { get; }
    public byte VisualTyreCompound { get; }
    public byte TyresAgeLaps { get; }
    public sbyte VehicleFiaFlags { get; }
    public float EnginePowerICE { get; }
    public float EnginePowerMGUK { get; }
    public float ErsStoreEnergy { get; }
    public byte ErsDeployMode { get; }
    public float ErsHarvestedThisLapMGUK { get; }
    public float ErsHarvestedThisLapMGUH { get; }
    public float ErsDeployedThisLap { get; }
    public byte NetworkPaused { get; }

    public CarStatusData(byte[] bytes, int startIndex = 0)
    {
        TractionControl = bytes[startIndex];
        AntiLockBrakes = bytes[startIndex + 1];
        FuelMix = bytes[startIndex + 2];
        FrontBrakeBias = bytes[startIndex + 3];
        PitLimiterStatus = bytes[startIndex + 4];
        FuelInTank = BitConverter.ToSingle(bytes, startIndex + 5);
        FuelCapacity = BitConverter.ToSingle(bytes, startIndex + 9);
        FuelRemainingLaps = BitConverter.ToSingle(bytes, startIndex + 13);
        MaxRPM = BitConverter.ToUInt16(bytes, startIndex + 17);
        IdleRPM = BitConverter.ToUInt16(bytes, startIndex + 19);
        MaxGears = bytes[startIndex + 21];
        DrsAllowed = bytes[startIndex + 22];
        DrsActivationDistance = BitConverter.ToUInt16(bytes, startIndex + 23);
        ActualTyreCompound = bytes[startIndex + 25];
        VisualTyreCompound = bytes[startIndex + 26];
        TyresAgeLaps = bytes[startIndex + 27];
        VehicleFiaFlags = (sbyte)bytes[startIndex + 28];
        EnginePowerICE = BitConverter.ToSingle(bytes, startIndex + 29);
        EnginePowerMGUK = BitConverter.ToSingle(bytes, startIndex + 33);
        ErsStoreEnergy = BitConverter.ToSingle(bytes, startIndex + 37);
        ErsDeployMode = bytes[startIndex + 41];
        ErsHarvestedThisLapMGUK = BitConverter.ToSingle(bytes, startIndex + 42);
        ErsHarvestedThisLapMGUH = BitConverter.ToSingle(bytes, startIndex + 46);
        ErsDeployedThisLap = BitConverter.ToSingle(bytes, startIndex + 50);
        NetworkPaused = bytes[startIndex + 54];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketCarStatusData
{
    public PacketHeader Header { get; }
    public CarStatusData[] CarStatusData { get; }

    public PacketCarStatusData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarStatusData = new CarStatusData[22];
        for (int i = 0; i < 22; i++)
        {
            CarStatusData[i] = new CarStatusData(bytes, 24 + i * 55);
        }
    }

    public static PacketCarStatusData FromBytes(byte[] bytes)
    {
        return new PacketCarStatusData(bytes);
    }
}
