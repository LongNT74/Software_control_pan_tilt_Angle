using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanTilt123.Class
{
    public class BootloaderPacket
    {
        private const int DEVICE_UNIQUE_IDENTIFIER_NUMBER_FIELD_LEN = 16;
        private const int BOOTLOADER_RX_PACKET_DATA_MAX_LEN = 200;
        private const int BOOTLOADER_RX_PACKET_VERISON_MAX_LEN = 4;

        private int deviceUidFieldLen = DEVICE_UNIQUE_IDENTIFIER_NUMBER_FIELD_LEN;
        private int versionFieldMaxLen = BOOTLOADER_RX_PACKET_VERISON_MAX_LEN;

        private int packetLenght = 0;
        private byte[] deviceUid = new byte[DEVICE_UNIQUE_IDENTIFIER_NUMBER_FIELD_LEN];
        private byte[] fwVersion = new byte[BOOTLOADER_RX_PACKET_VERISON_MAX_LEN];
        private UInt16 totalPacket = 0;
        private UInt16 packetNo = 0;
        private int dataLenght = 0;
        private byte[] data = new byte[BOOTLOADER_RX_PACKET_DATA_MAX_LEN];

        public BootloaderPacket(byte[] uid = null, byte[] version = null, ushort total = 0, ushort no = 0, byte[] packeData = null)
        {
            if (uid != null)
            {
                for (int i = 0; i < deviceUid.Length; i++)
                {
                    deviceUid[i] = uid[i];
                }
            }
            else
            {
                for (int i = 0; i < deviceUid.Length; i++)
                {
                    deviceUid[i] = 0;
                }
            }
            if (version != null)
            {
                for (int i = 0; i < fwVersion.Length; i++)
                {
                    fwVersion[i] = version[i];
                }
            }
            else
            {
                for (int i = 0; i < fwVersion.Length; i++)
                {
                    fwVersion[i] = 0xff;
                }
            }
            if (packeData != null)
            {
                for (int i = 0; i < packeData.Length; i++)
                {
                    data[i] = packeData[i];
                }
                dataLenght = packeData.Length;
            }
            else
            {
                dataLenght = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = 0;
                }
            }
            totalPacket = total;
            packetNo = no;
            // Calculate packet lenght
            packetLenght = (byte)(DEVICE_UNIQUE_IDENTIFIER_NUMBER_FIELD_LEN + BOOTLOADER_RX_PACKET_VERISON_MAX_LEN + 2 + 2 + 1 + dataLenght);
        }


        public byte[] DeviceUid { get => deviceUid; set => deviceUid = value; }
        public byte[] FwVersion { get => fwVersion; set => fwVersion = value; }
        public ushort TotalPacket { get => totalPacket; set => totalPacket = value; }
        public ushort PacketNo { get => packetNo; set => packetNo = value; }
        public int DataLenght { get => dataLenght; set => dataLenght = value; }
        public byte[] Data { get => data; set => data = value; }
        public int PacketLenght { get => packetLenght; set => packetLenght = value; }
        public int DeviceUidFieldLen { get => deviceUidFieldLen; set => deviceUidFieldLen = value; }
        public int VersionFieldMaxLen { get => versionFieldMaxLen; set => versionFieldMaxLen = value; }
    }

    public enum BootloaderProcessingState
    {
        SendDfuCommand,
        WaitingDeviceBootup,
        EraseFlash,
        WaitForErasing,
        SendNextDatapacket,
        WaitForWritingPacket,
        SendRebootCommand,
        Finish,
        Error
    }

    public enum BootloaderResultCode
    {
        Nothing,
        Done = 0x10,
        Fault = 0x20,
        Ok,
        Error,
        UserCancel,
    }
}
