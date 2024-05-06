using UdpRead;
using Xunit.Abstractions;

namespace UdpTest;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var lines = await File.ReadAllLinesAsync("/Users/paul.mcilreavy/src/UdpRead/UdpRead/data.txt");
        var tp = new TelemetryProcessor();

        var lineCount = 0;
        foreach (var line in lines)
        {
            var bytes = HexStringToByteArray(line);
            tp.ProcessTelemetryData(bytes);
            lineCount++;
            //Thread.Sleep(1000/20);
        }
        
        _testOutputHelper.WriteLine($"Processed {lineCount} lines.");
    }

    public static byte[] HexStringToByteArray(string hex)
    {
        if (hex.Length % 2 != 0)
        {
            throw new ArgumentException("Hex string must have an even length.");
        }

        byte[] byteArray = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return byteArray;
    }
}