using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
