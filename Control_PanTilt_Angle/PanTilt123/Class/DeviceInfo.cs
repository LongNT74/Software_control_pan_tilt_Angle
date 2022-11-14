using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
 

namespace PanTilt123.Class
{
    public class DeviceInfo : ViewModelBase
    {
        private Byte[] address = null;
        private Byte[] serialNumber = null;
        private Byte[] hardwareVerison = null;
        private Byte[] firmwareVerison = null;
        private Byte[] uid = null;
        private bool isValid = false;

        public DeviceInfo()
        {
            address = new Byte[4];
            address[0] = 192;
            address[1] = 168;
            address[2] = 1;
            address[3] = 1;
            serialNumber = new Byte[16];
            for (int i = 0; i < serialNumber.Length; i++)
            {
                serialNumber[i] = 0;
            }
            hardwareVerison = new Byte[4];
            for (int i = 0; i < hardwareVerison.Length; i++)
            {
                hardwareVerison[i] = 0;
            }
            firmwareVerison = new Byte[4];
            for (int i = 0; i < firmwareVerison.Length; i++)
            {
                firmwareVerison[i] = 0;
            }
            uid = new Byte[16];
            for (int i = 0; i < uid.Length; i++)
            {
                uid[i] = 0;
            }
            isValid = false;
        }

        public byte[] Address { get => address; set { SetProperty(ref address, value); } }
        public byte[] SerialNumber { get => serialNumber; set { SetProperty(ref serialNumber, value); } }
        public byte[] HardwareVerison { get => hardwareVerison; set { SetProperty(ref hardwareVerison, value); } }
        public byte[] FirmwareVerison { get => firmwareVerison; set { SetProperty(ref firmwareVerison, value); } }

        public byte[] Uid { get => uid; set { SetProperty(ref uid, value); } }

        public bool IsValid { get => isValid; set => isValid = value; }

    }
    public class SerialNumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String retVal = String.Empty;

            if (value != null && value is Byte[])
            {
                byte[] rxBuffer = (byte[])value;
                retVal = Encoding.ASCII.GetString(rxBuffer, 0, rxBuffer.Length);
            }

            return retVal;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var snString = (string)value;

            var snBuffer = ASCIIEncoding.ASCII.GetBytes(snString);

            byte[] retBuffer = new byte[16];
            int copyLenght = snBuffer.Length;
            if (copyLenght > retBuffer.Length)
            {
                copyLenght = retBuffer.Length;
            }
            for (int i = 0; i < copyLenght; i++)
            {
                retBuffer[i] = snBuffer[i];
            }
            return retBuffer;
        }
    }

    public class HardwareVersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String retVal = String.Empty;
            if (value != null && value is Byte[])
            {
                Byte[] buf = (Byte[])value;

                retVal = buf[0].ToString() + ".";
                retVal += buf[1].ToString() + ".";
                retVal += buf[2].ToString() + ".";
                retVal += buf[3].ToString();
            }
            return retVal;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new Exception("Can't convert back");
        }
    }

    public class FirmwareVersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String retVal = String.Empty;
            if (value != null && value is Byte[])
            {
                Byte[] buf = (Byte[])value;
                retVal = buf[0].ToString() + ".";
                retVal += buf[1].ToString() + ".";
                retVal += buf[2].ToString() + ".";
                retVal += buf[3].ToString();
            }
            return retVal;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new Exception("Can't convert back");
        }
    }

    public class DeviceAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String retVal = String.Empty;
            if (value != null && value is Byte[])
            {
                Byte[] buf = (Byte[])value;

                retVal = buf[0].ToString() + ".";
                retVal += buf[1].ToString() + ".";
                retVal += buf[2].ToString() + ".";
                retVal += buf[3].ToString();
            }
            return retVal;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var addrString = (string)value;
            byte[] addrBuffer = new byte[4];
            string[] addrFeild = addrString.Split('.');
            int i = 0;
            foreach (string field in addrFeild)
            {
                Byte.TryParse(field, out addrBuffer[i++]);
                if (i == 4)
                {
                    break;
                }
            }
            return addrBuffer;
            //throw new Exception("Can't convert back");
        }
    }
}
