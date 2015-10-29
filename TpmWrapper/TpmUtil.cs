using System;

namespace TpmWrapperLib
{
    public class TpmUtil
    {
        public static string GetTpmErrorMessage (UInt32 err)
        {
            const UInt32 baseErr = 0x80284001;
            string sErr = "Error code not found.";

            switch (err)
            {
                case 0:
                    sErr = "The function succeeded.";
                    break;
                case baseErr:
                    sErr = "An internal software error occurred.";
                    break;
                case baseErr + 1:
                    sErr = "One or more parameter values are not valid.";
                    break;
                case baseErr + 2:
                    sErr = "A specified output pointer is bad";
                    break;
                case baseErr + 3:
                    sErr = "The specified context handle does not refer to a valid context";
                    break;
                case baseErr + 4:
                    sErr = "The specified output buffer is too small";
                    break;
                case baseErr + 5:
                    sErr = "An error occurred while communicating with the TPM";
                    break;
                case baseErr + 6:
                    sErr = "A context parameter that is not valid was passed when attempting to create a TBS context";
                    break;
                case baseErr + 7:
                    sErr = "The TBS service is not running and could not be started";
                    break;
                case baseErr + 8:
                    sErr = "A new context could not be created because there are too many open contexts";
                    break;
                case baseErr + 9:
                    sErr = "A new virtual resource could not be created because there are too many open virtual resources";
                    break;
                case baseErr + 10:
                    sErr = "The TBS service has been started but is not yet running";
                    break;
                case baseErr + 11:
                    sErr = "The physical presence interface is not supported";
                    break;
                case baseErr + 12:
                    sErr = "The command was canceled";
                    break;
                case baseErr + 13:
                    sErr = "The input or output buffer is too large";
                    break;
                case baseErr + 14:
                    sErr = "A compatible Trusted Platform Module (TPM) Security Device cannot be found on this computer";
                    break;
                case baseErr + 15:
                    sErr = "The TBS service has been disabled";
                    break;
            }

            return sErr;
        }
    }
}
