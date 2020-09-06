using System.ComponentModel;

namespace Notification.OptionEnums
{
    public enum ToastType
    {
        [Description("success")]
        Success,

        [Description("info")]
        Info,

        [Description("warning")]
        Warning,

        [Description("error")]
        Error,
    }
}
