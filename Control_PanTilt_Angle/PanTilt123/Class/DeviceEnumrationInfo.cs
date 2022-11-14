using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanTilt123.Class
{
    public class DeviceEnumrationInfo : ViewModelBase
    {
        private int deviceNo;
        private byte[] address = new byte[4];
        private byte[] serialNumber = new byte[16];
        private byte[] hwVersion = new byte[4];
        private byte[] fwVersion = new byte[4];
        private byte[] uid = new byte[16];

        // string for display on table
        private string addressString;
        private string serialNumberString;
        private string hwVersionString;
        private string fwVersionString;
        private string uidString;

        public int DeviceNo { get => deviceNo; set { SetProperty(ref deviceNo, value); } }

        public string CtrAddressString { get => addressString; set { SetProperty(ref addressString, value); } }
        public string SerialNumberString { get => serialNumberString; set { SetProperty(ref serialNumberString, value); } }
        public string HwVersionString { get => hwVersionString; set { SetProperty(ref hwVersionString, value); } }
        public string FwVersionString { get => fwVersionString; set { SetProperty(ref fwVersionString, value); } }
        public string UidString { get => uidString; set { SetProperty(ref uidString, value); } }

        public byte[] Address { get => address; set => address = value; }
        public byte[] SerialNumber { get => serialNumber; set => serialNumber = value; }
        public byte[] HwVersion { get => hwVersion; set => hwVersion = value; }
        public byte[] FwVersion { get => fwVersion; set => fwVersion = value; }
        public byte[] Uid { get => uid; set => uid = value; }

        private string ByteArrayToHex(byte[] barray)
        {
            char[] c = new char[barray.Length * 2];
            byte b;
            for (int i = 0; i < barray.Length; ++i)
            {
                b = ((byte)(barray[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(barray[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }

        public void ConvertToString()
        {
            // Convert address
            CtrAddressString = string.Empty;
            CtrAddressString = Address[0].ToString() + ".";
            CtrAddressString += Address[1].ToString() + ".";
            CtrAddressString += Address[2].ToString() + ".";
            CtrAddressString += Address[3].ToString();

            // Convert serial number
            SerialNumberString = Encoding.ASCII.GetString(SerialNumber, 0, SerialNumber.Length);

            // Convert HwVersion
            HwVersionString = string.Empty;
            HwVersionString = HwVersion[0].ToString() + ".";
            HwVersionString += HwVersion[1].ToString() + ".";
            HwVersionString += HwVersion[2].ToString() + ".";
            HwVersionString += HwVersion[3].ToString();

            // Convert FwVersion
            FwVersionString = string.Empty;
            FwVersionString = FwVersion[0].ToString() + ".";
            FwVersionString += FwVersion[1].ToString() + ".";
            FwVersionString += FwVersion[2].ToString() + ".";
            FwVersionString += FwVersion[3].ToString();

            // Convert Uid
            UidString = string.Empty;
            UidString = ByteArrayToHex(Uid);
        }
    }
}
