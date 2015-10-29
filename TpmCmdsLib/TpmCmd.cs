using TpmWrapperLib;

namespace TpmCmdsLib
{
    public class TpmCmd
    {
        Tpm _TpmHw;

        public TpmCmd()
        {
            _TpmHw = Tpm.Instance;
        }
    }
}
