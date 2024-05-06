namespace UdpRead;

using System;
using System.Runtime.InteropServices;
using System.Text;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct ParticipantData
{
    public byte AIControlled { get; }
    public byte DriverId { get; }
    public byte NetworkId { get; }
    public byte TeamId { get; }
    public byte MyTeam { get; }
    public byte RaceNumber { get; }
    public byte Nationality { get; }
    public string Name { get; }
    public byte YourTelemetry { get; }
    public byte ShowOnlineNames { get; }
    public byte Platform { get; }

    public ParticipantData(byte[] bytes, int startIndex = 0)
    {
        AIControlled = bytes[startIndex];
        DriverId = bytes[startIndex + 1];
        NetworkId = bytes[startIndex + 2];
        TeamId = bytes[startIndex + 3];
        MyTeam = bytes[startIndex + 4];
        RaceNumber = bytes[startIndex + 5];
        Nationality = bytes[startIndex + 6];

        byte[] nameBytes = new byte[48];
        Array.Copy(bytes, startIndex + 7, nameBytes, 0, 48);
        Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');

        YourTelemetry = bytes[startIndex + 55];
        ShowOnlineNames = bytes[startIndex + 56];
        Platform = bytes[startIndex + 57];
    }
}

public readonly struct PacketParticipantsData
{
    public PacketHeader Header { get; }
    public byte NumActiveCars { get; }
    public ParticipantData[] Participants { get; }

    public PacketParticipantsData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        NumActiveCars = bytes[24];
        Participants = new ParticipantData[22];

        for (int i = 0; i < 22; i++)
        {
            Participants[i] = new ParticipantData(bytes, 25 + i * 58);
        }
    }

    public static PacketParticipantsData FromBytes(byte[] bytes)
    {
        return new PacketParticipantsData(bytes);
    }
}
