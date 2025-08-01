﻿using System.ComponentModel;

namespace Notification.OptionEnums
{
    public enum Position
    {
        [Description("toast-top-right")]
        TopRight,

        [Description("toast-bottom-righ")]
        BottomRight,

        [Description("toast-bottom-left")]
        BottomLeft,

        [Description("toast-top-left")]
        TopLeft,

        [Description("toast-top-full-width")]
        TopFullWidth,

        [Description("toast-bottom-full-width")]
        BottomFullWidth,

        [Description("toast-top-center")]
        TopCenter,

        [Description("toast-bottom-center")]
        BottomCenter
    }
}
