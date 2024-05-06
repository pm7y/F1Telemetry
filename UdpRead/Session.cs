namespace UdpRead;

using System;

public readonly struct MarshalZone(byte[] bytes, int startIndex = 0)
{
    public float ZoneStart { get; } = BitConverter.ToSingle(bytes, startIndex);
    public sbyte ZoneFlag { get; } = (sbyte)bytes[startIndex + 4];
}

public readonly struct WeatherForecastSample(byte[] bytes, int startIndex = 0)
{
    public byte SessionType { get; } = bytes[startIndex];
    public byte TimeOffset { get; } = bytes[startIndex + 1];
    public byte Weather { get; } = bytes[startIndex + 2];
    public sbyte TrackTemperature { get; } = (sbyte)bytes[startIndex + 3];
    public sbyte TrackTemperatureChange { get; } = (sbyte)bytes[startIndex + 4];
    public sbyte AirTemperature { get; } = (sbyte)bytes[startIndex + 5];
    public sbyte AirTemperatureChange { get; } = (sbyte)bytes[startIndex + 6];
    public byte RainPercentage { get; } = bytes[startIndex + 7];
}

public readonly struct SessionData
{
    public PacketHeader Header { get; }
    public byte Weather { get; }
    public sbyte TrackTemperature { get; }
    public sbyte AirTemperature { get; }
    public byte TotalLaps { get; }
    public ushort TrackLength { get; }
    public byte SessionType { get; }
    public sbyte TrackId { get; }
    public byte Formula { get; }
    public ushort SessionTimeLeft { get; }
    public ushort SessionDuration { get; }
    public byte PitSpeedLimit { get; }
    public byte GamePaused { get; }
    public byte IsSpectating { get; }
    public byte SpectatorCarIndex { get; }
    public byte SliProNativeSupport { get; }
    public byte NumMarshalZones { get; }
    public MarshalZone[] MarshalZones { get; }
    public byte SafetyCarStatus { get; }
    public byte NetworkGame { get; }
    public byte NumWeatherForecastSamples { get; }
    public WeatherForecastSample[] WeatherForecastSamples { get; }
    public byte ForecastAccuracy { get; }
    public byte AiDifficulty { get; }
    public uint SeasonLinkIdentifier { get; }
    public uint WeekendLinkIdentifier { get; }
    public uint SessionLinkIdentifier { get; }
    public byte PitStopWindowIdealLap { get; }
    public byte PitStopWindowLatestLap { get; }
    public byte PitStopRejoinPosition { get; }
    public byte SteeringAssist { get; }
    public byte BrakingAssist { get; }
    public byte GearboxAssist { get; }
    public byte PitAssist { get; }
    public byte PitReleaseAssist { get; }
    public byte ErsAssist { get; }
    public byte DrsAssist { get; }
    public byte DynamicRacingLine { get; }
    public byte DynamicRacingLineType { get; }
    public byte GameMode { get; }
    public byte RuleSet { get; }
    public uint TimeOfDay { get; }
    public byte SessionLength { get; }
    public byte SpeedUnitsLeadPlayer { get; }
    public byte TemperatureUnitsLeadPlayer { get; }
    public byte SpeedUnitsSecondaryPlayer { get; }
    public byte TemperatureUnitsSecondaryPlayer { get; }
    public byte NumSafetyCarPeriods { get; }
    public byte NumVirtualSafetyCarPeriods { get; }
    public byte NumRedFlagPeriods { get; }

    public SessionData(byte[] bytes)
    {
        int index = 0;

        Header = new PacketHeader(bytes);
        index += 24;

        Weather = bytes[index++];
        TrackTemperature = (sbyte)bytes[index++];
        AirTemperature = (sbyte)bytes[index++];
        TotalLaps = bytes[index++];
        TrackLength = BitConverter.ToUInt16(bytes, index);
        index += 2;
        SessionType = bytes[index++];
        TrackId = (sbyte)bytes[index++];
        Formula = bytes[index++];
        SessionTimeLeft = BitConverter.ToUInt16(bytes, index);
        index += 2;
        SessionDuration = BitConverter.ToUInt16(bytes, index);
        index += 2;
        PitSpeedLimit = bytes[index++];
        GamePaused = bytes[index++];
        IsSpectating = bytes[index++];
        SpectatorCarIndex = bytes[index++];
        SliProNativeSupport = bytes[index++];
        NumMarshalZones = bytes[index++];

        MarshalZones = new MarshalZone[21];
        for (int i = 0; i < 21; i++)
        {
            MarshalZones[i] = new MarshalZone(bytes, index);
            index += 5;
        }

        SafetyCarStatus = bytes[index++];
        NetworkGame = bytes[index++];
        NumWeatherForecastSamples = bytes[index++];

        WeatherForecastSamples = new WeatherForecastSample[56];
        for (int i = 0; i < 56; i++)
        {
            WeatherForecastSamples[i] = new WeatherForecastSample(bytes, index);
            index += 8;
        }

        ForecastAccuracy = bytes[index++];
        AiDifficulty = bytes[index++];
        SeasonLinkIdentifier = BitConverter.ToUInt32(bytes, index);
        index += 4;
        WeekendLinkIdentifier = BitConverter.ToUInt32(bytes, index);
        index += 4;
        SessionLinkIdentifier = BitConverter.ToUInt32(bytes, index);
        index += 4;
        PitStopWindowIdealLap = bytes[index++];
        PitStopWindowLatestLap = bytes[index++];
        PitStopRejoinPosition = bytes[index++];
        SteeringAssist = bytes[index++];
        BrakingAssist = bytes[index++];
        GearboxAssist = bytes[index++];
        PitAssist = bytes[index++];
        PitReleaseAssist = bytes[index++];
        ErsAssist = bytes[index++];
        DrsAssist = bytes[index++];
        DynamicRacingLine = bytes[index++];
        DynamicRacingLineType = bytes[index++];
        GameMode = bytes[index++];
        RuleSet = bytes[index++];
        TimeOfDay = BitConverter.ToUInt32(bytes, index);
        index += 4;
        SessionLength = bytes[index++];
        SpeedUnitsLeadPlayer = bytes[index++];
        TemperatureUnitsLeadPlayer = bytes[index++];
        SpeedUnitsSecondaryPlayer = bytes[index++];
        TemperatureUnitsSecondaryPlayer = bytes[index++];
        NumSafetyCarPeriods = bytes[index++];
        NumVirtualSafetyCarPeriods = bytes[index++];
        NumRedFlagPeriods = bytes[index++];
    }

    public static SessionData FromBytes(byte[] bytes)
    {
        return new SessionData(bytes);
    }
}
