namespace UdpRead;

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct CarTelemetryData
{
    public ushort Speed { get; }
    public float Throttle { get; }
    public float Steer { get; }
    public float Brake { get; }
    public byte Clutch { get; }
    public sbyte Gear { get; }
    public ushort EngineRPM { get; }
    public byte Drs { get; }
    public byte RevLightsPercent { get; }
    public ushort RevLightsBitValue { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ushort[] BrakesTemperature { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] TyresSurfaceTemperature { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] TyresInnerTemperature { get; }
    public ushort EngineTemperature { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyresPressure { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] SurfaceType { get; }

    public CarTelemetryData(byte[] bytes, int startIndex = 0)
    {
        Speed = BitConverter.ToUInt16(bytes, startIndex);
        Throttle = BitConverter.ToSingle(bytes, startIndex + 2);
        Steer = BitConverter.ToSingle(bytes, startIndex + 6);
        Brake = BitConverter.ToSingle(bytes, startIndex + 10);
        Clutch = bytes[startIndex + 14];
        Gear = (sbyte)bytes[startIndex + 15];
        EngineRPM = BitConverter.ToUInt16(bytes, startIndex + 16);
        Drs = bytes[startIndex + 18];
        RevLightsPercent = bytes[startIndex + 19];
        RevLightsBitValue = BitConverter.ToUInt16(bytes, startIndex + 20);

        BrakesTemperature = new ushort[4];
        for (int i = 0; i < 4; i++)
        {
            BrakesTemperature[i] = BitConverter.ToUInt16(bytes, startIndex + 22 + i * 2);
        }

        TyresSurfaceTemperature = new byte[4];
        Array.Copy(bytes, startIndex + 30, TyresSurfaceTemperature, 0, 4);

        TyresInnerTemperature = new byte[4];
        Array.Copy(bytes, startIndex + 34, TyresInnerTemperature, 0, 4);

        EngineTemperature = BitConverter.ToUInt16(bytes, startIndex + 38);

        TyresPressure = new float[4];
        for (int i = 0; i < 4; i++)
        {
            TyresPressure[i] = BitConverter.ToSingle(bytes, startIndex + 40 + i * 4);
        }

        SurfaceType = new byte[4];
        Array.Copy(bytes, startIndex + 56, SurfaceType, 0, 4);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketCarTelemetryData
{
    public PacketHeader Header { get; }
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
    public CarTelemetryData[] CarTelemetryData { get; }
    public byte MfdPanelIndex { get; }
    public byte MfdPanelIndexSecondaryPlayer { get; }
    public sbyte SuggestedGear { get; }

    public PacketCarTelemetryData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        CarTelemetryData = new CarTelemetryData[22];
        for (int i = 0; i < 22; i++)
        {
            CarTelemetryData[i] = new CarTelemetryData(bytes, 24 + i * 60);
        }

        MfdPanelIndex = bytes[24 + 22 * 60];
        MfdPanelIndexSecondaryPlayer = bytes[25 + 22 * 60];
        SuggestedGear = (sbyte)bytes[26 + 22 * 60];
    }

    public static PacketCarTelemetryData FromBytes(byte[] bytes)
    {
        return new PacketCarTelemetryData(bytes);
    }
}
