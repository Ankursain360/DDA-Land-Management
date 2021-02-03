using Notification.OptionEnums;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Notification
{
    public static class Alert
    {
        #region HELPERS
        private static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        private static string ToLowerString(this bool value)
        {
            return value.ToString().ToLower();
        }
        #endregion

        public static string Show(string message, string title = "",
            AlertType type = AlertType.Info,
            Position position = Position.TopRight,
            int timeOut = 5000,
            bool closeButton = true,
            bool progressBar = true,
            bool newestOnTop = true,
            string onclick = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script>");
            builder.Append("toastr.options = {");
            builder.Append("'closeButton': '");
            builder.Append(closeButton.ToLowerString());
            builder.Append("','debug': false,'newestOnTop': ");
            builder.Append(newestOnTop.ToLowerString());
            builder.Append(",'progressBar': ");
            builder.Append(progressBar.ToLowerString());
            builder.Append(",'positionClass': '");
            builder.Append(StringValueOf(position));
            builder.Append("','preventDuplicates': false,'onclick': ");
            builder.Append((onclick ?? "null"));
            builder.Append(",'showDuration': '300','hideDuration': '1000','timeOut': '");
            builder.Append(timeOut);
            builder.Append("','extendedTimeOut': '"+ timeOut + "','showEasing': 'swing','hideEasing': 'linear','showMethod': 'fadeIn','hideMethod': 'fadeOut'");
            builder.Append("};");
            builder.Append("toastr['");
            builder.Append(StringValueOf(type));
            builder.Append("']('");
            builder.Append(message);
            builder.Append("', '");
            builder.Append(title);
            builder.Append("');</script>");

            return builder.ToString();
        }

      
    }
}
