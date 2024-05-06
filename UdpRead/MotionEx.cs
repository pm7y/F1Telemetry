// namespace UdpRead;
//
// using System;
// using System.Runtime.InteropServices;
//
// [StructLayout(LayoutKind.Sequential, Pack = 1)]
// public readonly struct PacketMotionExData
// {
//     public PacketHeader Header { get; }
//     public float[] SuspensionPosition { get; }
//     public float[] SuspensionVelocity { get; }
//     public float[] SuspensionAcceleration { get; }
//     public float[] WheelSpeed { get; }
//     public float[] WheelSlipRatio { get; }
//     public float[] WheelSlipAngle { get; }
//     public float[] WheelLatForce { get; }
//     public float[] WheelLongForce { get; }
//     public float HeightOfCOGAboveGround { get; }
//     public float LocalVelocityX { get; }
//     public float LocalVelocityY { get; }
//     public float LocalVelocityZ { get; }
//     public float AngularVelocityX { get; }
//     public float AngularVelocityY { get; }
//     public float AngularVelocityZ { get; }
//     public float AngularAccelerationX { get; }
//     public float AngularAccelerationY { get; }
//     public float AngularAccelerationZ { get; }
//     public float FrontWheelsAngle { get; }
//     public float[] WheelVertForce { get; }
//
//     public PacketMotionExData(byte[] bytes)
//     {
//         Header = new PacketHeader(bytes);
//         SuspensionPosition = new float[4];
//         SuspensionVelocity = new float[4];
//         SuspensionAcceleration = new float[4];
//         WheelSpeed = new float[4];
//         WheelSlipRatio = new float[4];
//         WheelSlipAngle = new float[4];
//         WheelLatForce = new float[4];
//         WheelLongForce = new float[4];
//         WheelVertForce = new float[4];
//
//         int offset = 24;
//
//         for (int i = 0; i < 4; i++)
//         {
//             SuspensionPosition[i] = BitConverter.ToSingle(bytes, offset + i * 4);
//             SuspensionVelocity[i] = BitConverter.ToSingle(bytes, offset + 16 + i * 4);
//             SuspensionAcceleration[i] = BitConverter.ToSingle(bytes, offset + 32 + i * 4);
//             WheelSpeed[i] = BitConverter.ToSingle(bytes, offset + 48 + i * 4);
//             WheelSlipRatio[i] = BitConverter.ToSingle(bytes, offset + 64 + i * 4);
//             WheelSlipAngle[i] = BitConverter.ToSingle(bytes, offset + 80 + i * 4);
//             WheelLatForce[i] = BitConverter.ToSingle(bytes, offset + 96 + i * 4);
//             WheelLongForce[i] = BitConverter.ToSingle(bytes, offset + 112 + i * 4);
//             WheelVertForce[i] = BitConverter.ToSingle(bytes, offset + 192 + i * 4);
//         }
//
//         offset += 128;
//         HeightOfCOGAboveGround = BitConverter.ToSingle(bytes, offset);
//         LocalVelocityX = BitConverter.ToSingle(bytes, offset + 4);
//         LocalVelocityY = BitConverter.ToSingle(bytes, offset + 8);
//         LocalVelocityZ = BitConverter.ToSingle(bytes, offset + 12);
//         AngularVelocityX = BitConverter.ToSingle(bytes, offset + 16);
//         AngularVelocityY = BitConverter.ToSingle(bytes, offset + 20);
//         AngularVelocityZ = BitConverter.ToSingle(bytes, offset + 24);
//         AngularAccelerationX = BitConverter.ToSingle(bytes, offset + 28);
//         AngularAccelerationY = BitConverter.ToSingle(bytes, offset + 32);
//         AngularAccelerationZ = BitConverter.ToSingle(bytes, offset + 36);
//         FrontWheelsAngle = BitConverter.ToSingle(bytes, offset + 40);
//     }
//
//     public static PacketMotionExData FromBytes(byte[] bytes)
//     {
//         return new PacketMotionExData(bytes);
//     }
// }
