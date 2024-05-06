using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace UdpRead;

class Program
{
    static async Task Main()
    {
        var udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 20777));
        var tp = new TelemetryProcessor();
        Console.WriteLine($"Listening for F1 telemetry data...");

        // /Users/paul.mcilreavy/src/UdpRead/UdpRead/data.txt

        while (true)
        {
            try
            {
                var result = await udpClient.ReceiveAsync();
                var hex = BitConverter.ToString(result.Buffer).Replace("-", "");
                // await File.AppendAllLinesAsync("./data.txt", new[] { hex });

                tp.ProcessTelemetryData(result.Buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
            }
        }
    }
}

public class TelemetryProcessor
{
    public void ProcessTelemetryData(byte[] data)
    {
        var header = PacketHeader.FromBytes(data);
        Console.WriteLine($"Received packet ID: {header.PacketId}, Frame: {header.FrameIdentifier}");

        switch (header.PacketId)
        {
            case (byte)PacketType.Motion:
                var motionPacket = MotionData.FromBytes(header, data);
                Debug.WriteLine($"Motion packet: {motionPacket}");
                break;
            case (byte)PacketType.Session:
                var sessionPacket = SessionData.FromBytes(data);
                Debug.WriteLine($"Session packet: {sessionPacket}");
                break;
            case (byte)PacketType.LapData:
                var lapDataPacket = PacketLapData.FromBytes(data);
                Debug.WriteLine($"Lap packet: {lapDataPacket}");
                break;
            case (byte)PacketType.Event:
                var eventDataPacket = PacketEventData.FromBytes(data);
                Debug.WriteLine($"Event packet: {eventDataPacket}");
                break;
            case (byte)PacketType.Participants:
                var participantsDataPacket = PacketParticipantsData.FromBytes(data);
                Debug.WriteLine($"Participants packet: {participantsDataPacket}");
                break;
            case (byte)PacketType.CarSetups:
                var carSetupsDataPacket = PacketCarSetupData.FromBytes(data);
                Debug.WriteLine($"Car Setups packet: {carSetupsDataPacket}");
                break;
            case (byte)PacketType.CarTelemetry:
                var carTelemetryDataPacket = PacketCarTelemetryData.FromBytes(data);
                Debug.WriteLine($"Car Telemetry packet: {carTelemetryDataPacket}");
                break;
            case (byte)PacketType.CarStatus:
                var carStatusDataPacket = PacketCarStatusData.FromBytes(data);
                Debug.WriteLine($"Car Status packet: {carStatusDataPacket}");
                break;
            case (byte)PacketType.FinalClassification:
                var finalClassificationDataPacket = PacketFinalClassificationData.FromBytes(data);
                Debug.WriteLine($"Final Classification packet: {finalClassificationDataPacket}");
                break;
            case (byte)PacketType.LobbyInfo:
                var lobbyDataPacket = PacketLobbyInfoData.FromBytes(data);
                Debug.WriteLine($"Lobby Info packet: {lobbyDataPacket}");
                break;
            case (byte)PacketType.CarDamage:
                var carDamageDataPacket = PacketCarDamageData.FromBytes(data);
                Debug.WriteLine($"Car Damage packet: {carDamageDataPacket}");
                break;
            case (byte)PacketType.SessionHistory:
                var sessionHistoryDataPacket = PacketSessionHistoryData.FromBytes(data);
                Debug.WriteLine($"Session History packet: {sessionHistoryDataPacket}");
                break;
            case (byte)PacketType.TyreSets:
                var tyreSetsDataPacket = PacketTyreSetsData.FromBytes(data);
                Debug.WriteLine($"Tyre Sets packet: {tyreSetsDataPacket}");
                break;
            case (byte)PacketType.MotionEx:
                //var motionExDataPacket = PacketMotionExData.FromBytes(data);
                Debug.WriteLine($"MotionEx packet");
                break;
            default:
                throw new InvalidOperationException($"Unknown packet ID: {header.PacketId}");
        }
    }
}

public enum PacketType : byte
{
    Motion = 0,
    Session = 1,
    LapData = 2,
    Event = 3,
    Participants = 4,
    CarSetups = 5,
    CarTelemetry = 6,
    CarStatus = 7,
    FinalClassification = 8,
    LobbyInfo = 9,
    CarDamage = 10,
    SessionHistory = 11,
    TyreSets = 12,
    MotionEx = 13
}

//[StructLayout(LayoutKind.Sequential, Pack = 1)]