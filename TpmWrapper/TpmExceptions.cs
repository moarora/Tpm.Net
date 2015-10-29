using System;

namespace TpmWrapperLib
{
    public class TpmGenericException : Exception
    {
        public UInt32 TpmHardwareError = 0;
        public string TpmHardwareErrorMsg = TpmUtil.GetTpmErrorMessage(0);

        public TpmGenericException(UInt32 TpmErrorCode) : base() 
        { 
            TpmHardwareError = TpmErrorCode;
            TpmHardwareErrorMsg = TpmUtil.GetTpmErrorMessage(TpmErrorCode);
        }

        public TpmGenericException(UInt32 TpmErrorCode, string message) : base(message) 
        {
            TpmHardwareError = TpmErrorCode;
            TpmHardwareErrorMsg = TpmUtil.GetTpmErrorMessage(TpmErrorCode);
        }
    }

   
}
