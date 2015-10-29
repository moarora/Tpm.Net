using System;
using System.Runtime.InteropServices;

namespace TpmWrapperLib
{
    public class Tpm
    {
        private static Tpm _tpm;
        private IntPtr _hContext = IntPtr.Zero;

        [DllImport("tbs.dll")]
        private unsafe static extern UInt32 Tbsi_Context_Create(UInt32* version, IntPtr* hContext);

        [DllImport("tbs.dll")]
        private unsafe static extern UInt32 Tbsip_Context_Close(IntPtr hContext);

        [DllImport("tbs.dll")]
        private unsafe static extern UInt32 Tbsip_Submit_Command(IntPtr hContext, UInt32 Locality, UInt32 Priority, byte* pCommandBuf, UInt32 CommandBufLen, byte* pResultBuf, UInt32* pResultBufLen);

        private Tpm()
        {
            unsafe
            {
                UInt32 version = 1;
                IntPtr handle = _hContext;
                UInt32 result = Tbsi_Context_Create(&version, &handle);

                if (result == 0)
                {
                    _hContext = handle;                    
                }
                else
                {
                    _hContext = IntPtr.Zero;
                    throw new TpmGenericException(result, "Error connecting to TPM, see hardware error for details.");
                }
            }
        }

        ~Tpm()
        {
            if (_hContext != IntPtr.Zero)
            {
                unsafe
                {
                    UInt32 result = Tbsip_Context_Close(_hContext);
                    if (result == 0)
                    {
                        _hContext = IntPtr.Zero;                        
                    }
                    else
                    {
                        throw new TpmGenericException(result, "Error connecting to TPM, see hardware error for details.");
                    }
                }
            }
        }

        public byte[] SubmitCommand(byte[] command, UInt32 responeSize)
        {
            byte[] response = null;

            if (_hContext != IntPtr.Zero)
            {
                unsafe
                {
                    UInt32 result = 0;
                    response = new byte[responeSize];
                    uint cmdSize = (uint)command.Length;
                    uint resSize = responeSize;

                    fixed (byte* pCmd = command, pRes = response)
                    {
                        result = Tbsip_Submit_Command(_hContext, 0, 200, pCmd, cmdSize, pRes, &resSize);
                    }

                    if (result != 0)
                    {
                        _hContext = IntPtr.Zero;
                        throw new TpmGenericException(result, "Error sending command to TPM, see hardware error for details.");
                    }
                }
            }

            return response;
        }

        public static Tpm Instance
        {
            get 
            {
                if (_tpm == null)
                    _tpm = new Tpm();
                return _tpm;
            }
        }
    }
}
