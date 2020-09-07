using System.ComponentModel;

namespace Notification.OptionEnums
{
    public enum AlertType
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
