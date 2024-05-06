namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct CarDamageData
{
    public float[] TyresWear { get; }
    public byte[] TyresDamage { get; }
    public byte[] BrakesDamage { get; }
    public byte FrontLeftWingDamage { get; }
    public byte FrontRightWingDamage { get; }
    public byte RearWingDamage { get; }
    public byte FloorDamage { get; }
    public byte DiffuserDamage { get; }
    public byte SidepodDamage { get; }
    public byte DrsFault { get; }
    public byte ErsFault { get; }
    public byte GearBoxDamage { get; }
    public byte EngineDamage { get; }
    public byte EngineMGUHWear { get; }
    public byte EngineESWear { get; }
    public byte EngineCEWear { get; }
    public byte EngineICEWear { get; }
    public byte EngineMGUKWear { get; }
    public byte EngineTCWear { get; }
    public byte EngineBlown { get; }
    public byte EngineSeized { get; }

    public CarDamageData(byte[] bytes, int startIndex = 0)
    {
        TyresWear = new float[4];
        TyresDamage = new byte[4];
        BrakesDamage = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            TyresWear[i] = BitConverter.ToSingle(bytes, startIndex + i * 4);
            TyresDamage[i] = bytes[startIndex + 16 + i];
            BrakesDamage[i] = bytes[startIndex + 20 + i];
        }

        FrontLeftWingDamage = bytes[startIndex + 24];
        FrontRightWingDamage = bytes[startIndex + 25];
        RearWingDamage = bytes[startIndex + 26];
        FloorDamage = bytes[startIndex + 27];
        DiffuserDamage = bytes[startIndex + 28];
        SidepodDamage = bytes[startIndex + 29];
        DrsFault = bytes[startIndex + 30];
        ErsFault = bytes[startIndex + 31];
        GearBoxDamage = bytes[startIndex + 32];
        EngineDamage = bytes[startIndex + 33];
        EngineMGUHWear = bytes[startIndex + 34];
        EngineESWear = bytes[startIndex + 35];
        EngineCEWear = bytes[startIndex + 36];
        EngineICEWear = bytes[startIndex + 37];
        EngineMGUKWear = bytes[startIndex + 38];
        EngineTCWear = bytes[startIndex + 39];
        EngineBlown = bytes[startIndex + 40];
        EngineSeized = bytes[startIndex + 41];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketCarDamageData
{
    public PacketHeader Header { get; }
    public CarDamageData[] CarDamageData { get; }

    public PacketCarDamageData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarDamageData = new CarDamageData[22];
        for (int i = 0; i < 22; i++)
        {
            CarDamageData[i] = new CarDamageData(bytes, 24 + i * 42);
        }
    }

    public static PacketCarDamageData FromBytes(byte[] bytes)
    {
        return new PacketCarDamageData(bytes);
    }
}
