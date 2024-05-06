namespace UdpRead;

using System.Runtime.InteropServices;

public enum EventType : uint
{
    SessionStarted = 0x53535441,        // "SSTA"
    SessionEnded = 0x53454E44,          // "SEND"
    FastestLap = 0x46544C50,            // "FTLP"
    Retirement = 0x52544D54,            // "RTMT"
    DRSEnabled = 0x44525345,            // "DRSE"
    DRSDisabled = 0x44525344,           // "DRSD"
    TeamMateInPits = 0x544D5054,        // "TMPT"
    ChequeredFlag = 0x43485146,         // "CHQF"
    RaceWinner = 0x5243574E,            // "RCWN"
    PenaltyIssued = 0x50454E41,         // "PENA"
    SpeedTrapTriggered = 0x53505450,    // "SPTP"
    StartLights = 0x53544C47,           // "STLG"
    LightsOut = 0x4C474F54,             // "LGOT"
    DriveThroughServed = 0x44545356,    // "DTSV"
    StopGoServed = 0x53534756,          // "SGSV"
    Flashback = 0x464C4253,             // "FLBK"
    ButtonStatus = 0x4255544E,          // "BUTN"
    RedFlag = 0x5244464C,               // "RDFL"
    Overtake = 0x4F56544B               // "OVTK"
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct FastestLapData
{
    public byte VehicleIdx { get; }
    public float LapTime { get; }

    public FastestLapData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
        LapTime = BitConverter.ToSingle(bytes, startIndex + 1);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct RetirementData
{
    public byte VehicleIdx { get; }

    public RetirementData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct TeamMateInPitsData
{
    public byte VehicleIdx { get; }

    public TeamMateInPitsData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct RaceWinnerData
{
    public byte VehicleIdx { get; }

    public RaceWinnerData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PenaltyData
{
    public byte PenaltyType { get; }
    public byte InfringementType { get; }
    public byte VehicleIdx { get; }
    public byte OtherVehicleIdx { get; }
    public byte Time { get; }
    public byte LapNum { get; }
    public byte PlacesGained { get; }

    public PenaltyData(byte[] bytes, int startIndex = 0)
    {
        PenaltyType = bytes[startIndex];
        InfringementType = bytes[startIndex + 1];
        VehicleIdx = bytes[startIndex + 2];
        OtherVehicleIdx = bytes[startIndex + 3];
        Time = bytes[startIndex + 4];
        LapNum = bytes[startIndex + 5];
        PlacesGained = bytes[startIndex + 6];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct SpeedTrapData
{
    public byte VehicleIdx { get; }
    public float Speed { get; }
    public byte IsOverallFastestInSession { get; }
    public byte IsDriverFastestInSession { get; }
    public byte FastestVehicleIdxInSession { get; }
    public float FastestSpeedInSession { get; }

    public SpeedTrapData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
        Speed = BitConverter.ToSingle(bytes, startIndex + 1);
        IsOverallFastestInSession = bytes[startIndex + 5];
        IsDriverFastestInSession = bytes[startIndex + 6];
        FastestVehicleIdxInSession = bytes[startIndex + 7];
        FastestSpeedInSession = BitConverter.ToSingle(bytes, startIndex + 8);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct StartLightsData
{
    public byte NumLights { get; }

    public StartLightsData(byte[] bytes, int startIndex = 0)
    {
        NumLights = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct DriveThroughPenaltyServedData
{
    public byte VehicleIdx { get; }

    public DriveThroughPenaltyServedData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct StopGoPenaltyServedData
{
    public byte VehicleIdx { get; }

    public StopGoPenaltyServedData(byte[] bytes, int startIndex = 0)
    {
        VehicleIdx = bytes[startIndex];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct FlashbackData
{
    public uint FlashbackFrameIdentifier { get; }
    public float FlashbackSessionTime { get; }

    public FlashbackData(byte[] bytes, int startIndex = 0)
    {
        FlashbackFrameIdentifier = BitConverter.ToUInt32(bytes, startIndex);
        FlashbackSessionTime = BitConverter.ToSingle(bytes, startIndex + 4);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct ButtonsData
{
    public uint ButtonStatus { get; }

    public ButtonsData(byte[] bytes, int startIndex = 0)
    {
        ButtonStatus = BitConverter.ToUInt32(bytes, startIndex);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct OvertakeData
{
    public byte OvertakingVehicleIdx { get; }
    public byte BeingOvertakenVehicleIdx { get; }

    public OvertakeData(byte[] bytes, int startIndex = 0)
    {
        OvertakingVehicleIdx = bytes[startIndex];
        BeingOvertakenVehicleIdx = bytes[startIndex + 1];
    }
}

public readonly struct EventDataDetails
{
    public EventType Type { get; }
    public object Details { get; }

    public EventDataDetails(byte[] bytes, EventType eventType)
    {
        Type = eventType;

        switch (eventType)
        {
            case EventType.FastestLap:
                Details = new FastestLapData(bytes, 5);
                break;
            case EventType.Retirement:
                Details = new RetirementData(bytes, 5);
                break;
            case EventType.TeamMateInPits:
                Details = new TeamMateInPitsData(bytes, 5);
                break;
            case EventType.RaceWinner:
                Details = new RaceWinnerData(bytes, 5);
                break;
            case EventType.PenaltyIssued:
                Details = new PenaltyData(bytes, 5);
                break;
            case EventType.SpeedTrapTriggered:
                Details = new SpeedTrapData(bytes, 5);
                break;
            case EventType.StartLights:
                Details = new StartLightsData(bytes, 5);
                break;
            // case EventType.LightsOut:
            //     // No additional data
            //     Details = null;
            //     break;
            case EventType.DriveThroughServed:
                Details = new DriveThroughPenaltyServedData(bytes, 5);
                break;
            case EventType.StopGoServed:
                Details = new StopGoPenaltyServedData(bytes, 5);
                break;
            case EventType.Flashback:
                Details = new FlashbackData(bytes, 5);
                break;
            case EventType.ButtonStatus:
                Details = new ButtonsData(bytes, 5);
                break;
            // case EventType.RedFlag:
            //     // No additional data
            //     Details = null;
            //     break;
            case EventType.Overtake:
                Details = new OvertakeData(bytes, 5);
                break;
            default:
                throw new ArgumentException($"Unsupported event type: {eventType}");
        }
    }
}

public readonly struct PacketEventData
{
    public PacketHeader Header { get; }
    public EventType EventType { get; }
    public EventDataDetails EventDetails { get; }

    public PacketEventData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        EventType = (EventType)BitConverter.ToUInt32(bytes, 24);
        EventDetails = new EventDataDetails(bytes, EventType);
    }

    public static PacketEventData FromBytes(byte[] bytes)
    {
        return new PacketEventData(bytes);
    }
}
