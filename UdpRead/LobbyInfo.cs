namespace UdpRead;

using System;
using System.Runtime.InteropServices;
using System.Text;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct LobbyInfoData
{
    public byte AIControlled { get; }
    public byte TeamId { get; }
    public byte Nationality { get; }
    public byte Platform { get; }
    public string Name { get; }
    public byte CarNumber { get; }
    public byte ReadyStatus { get; }

    public LobbyInfoData(byte[] bytes, int startIndex = 0)
    {
        AIControlled = bytes[startIndex];
        TeamId = bytes[startIndex + 1];
        Nationality = bytes[startIndex + 2];
        Platform = bytes[startIndex + 3];
        byte[] nameBytes = new byte[48];
        Array.Copy(bytes, startIndex + 4, nameBytes, 0, 48);
        Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
        CarNumber = bytes[startIndex + 52];
        ReadyStatus = bytes[startIndex + 53];
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct PacketLobbyInfoData
{
    public PacketHeader Header { get; }
    public byte NumPlayers { get; }
    public LobbyInfoData[] LobbyPlayers { get; }

    public PacketLobbyInfoData(byte[] bytes)
    {
        Header = new PacketHeader(bytes);
        NumPlayers = bytes[24];
        LobbyPlayers = new LobbyInfoData[22];
        for (int i = 0; i < 22; i++)
        {
            LobbyPlayers[i] = new LobbyInfoData(bytes, 25 + i * 54);
        }
    }

    public static PacketLobbyInfoData FromBytes(byte[] bytes)
    {
        return new PacketLobbyInfoData(bytes);
    }
}
