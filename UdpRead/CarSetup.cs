namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct CarSetupData(byte[] bytes, int startIndex = 0)
{
    public byte FrontWing { get; } = bytes[startIndex];
    public byte RearWing { get; } = bytes[startIndex + 1];
    public byte OnThrottle { get; } = bytes[startIndex + 2];
    public byte OffThrottle { get; } = bytes[startIndex + 3];
    public float FrontCamber { get; } = BitConverter.ToSingle(bytes, startIndex + 4);
    public float RearCamber { get; } = BitConverter.ToSingle(bytes, startIndex + 8);
    public float FrontToe { get; } = BitConverter.ToSingle(bytes, startIndex + 12);
    public float RearToe { get; } = BitConverter.ToSingle(bytes, startIndex + 16);
    public byte FrontSuspension { get; } = bytes[startIndex + 20];
    public byte RearSuspension { get; } = bytes[startIndex + 21];
    public byte FrontAntiRollBar { get; } = bytes[startIndex + 22];
    public byte RearAntiRollBar { get; } = bytes[startIndex + 23];
    public byte FrontSuspensionHeight { get; } = bytes[startIndex + 24];
    public byte RearSuspensionHeight { get; } = bytes[startIndex + 25];
    public byte BrakePressure { get; } = bytes[startIndex + 26];
    public byte BrakeBias { get; } = bytes[startIndex + 27];
    public float RearLeftTyrePressure { get; } = BitConverter.ToSingle(bytes, startIndex + 28);
    public float RearRightTyrePressure { get; } = BitConverter.ToSingle(bytes, startIndex + 32);
    public float FrontLeftTyrePressure { get; } = BitConverter.ToSingle(bytes, startIndex + 36);
    public float FrontRightTyrePressure { get; } = BitConverter.ToSingle(bytes, startIndex + 40);
    public byte Ballast { get; } = bytes[startIndex + 44];
    public float FuelLoad { get; } = BitConverter.ToSingle(bytes, startIndex + 45);
}

public readonly struct PacketCarSetupData
{
    public PacketHeader Header { get; }
    public CarSetupData[] CarSetups { get; }

    public PacketCarSetupData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarSetups = new CarSetupData[22];
        for (int i = 0; i < 22; i++)
        {
            CarSetups[i] = new CarSetupData(bytes, 24 + i * 49);
        }
    }

    public static PacketCarSetupData FromBytes(byte[] bytes)
    {
        return new PacketCarSetupData(bytes);
    }
}
