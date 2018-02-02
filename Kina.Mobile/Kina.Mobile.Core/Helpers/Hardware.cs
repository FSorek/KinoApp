using Cheesebaron.MvxPlugins.DeviceInfo;
using MvvmCross.Platform;

namespace Kina.Mobile.Core.Helpers
{
    class Hardware
    {
        public static string DeviceId
        {
            get
            {
                return Mvx.Resolve<IDeviceInfo>().DeviceId;
            }
        }
    }
}
