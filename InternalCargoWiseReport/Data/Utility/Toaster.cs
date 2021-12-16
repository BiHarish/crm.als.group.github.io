using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ICWR.Data.Utility
{
    public static class Toastr
    {
        public enum ToastType
        {
            Success,
            Info,
            Warning,
            Error
            //Reserved word
        }

        public enum ToastPosition
        {
            TopRight,
            TopLeft,
            TopCenter,
            TopStretch,
            BottomRight,
            BottomLeft,
            BottomCenter,
            BottomStretch
        }
        public static void ShowToast(Page Page, ToastType Type, string Msg, string Title = "", ToastPosition Position = ToastPosition.BottomRight, bool ShowCloseButton = true)
        {
            dynamic strType = "";
            dynamic strPosition = "";

            switch (Type)
            {
                case ToastType.Success:
                    strType = "success";
                    break;
                case ToastType.Info:
                    strType = "info";
                    break;
                case ToastType.Warning:
                    strType = "warning";
                    break;
                case ToastType.Error:
                    strType = "error";
                    break;
            }

            //Set the position based on selected and change value to match toastr plugin
            switch (Position)
            {
                case ToastPosition.TopRight:
                    strPosition = "toast-top-right";
                    break;
                case ToastPosition.TopLeft:
                    strPosition = "toast-top-left";
                    break;
                case ToastPosition.TopCenter:
                    strPosition = "toast-top-center";
                    break;
                case ToastPosition.TopStretch:
                    strPosition = "toast-top-full-width";
                    break;
                case ToastPosition.BottomRight:
                    strPosition = "toast-bottom-right";
                    break;
                case ToastPosition.BottomLeft:
                    strPosition = "toast-bottom-left";
                    break;
                case ToastPosition.BottomCenter:
                    strPosition = "toast-bottom-center";
                    break;
                case ToastPosition.BottomStretch:
                    strPosition = "toast-bottom-full-width";
                    break;
            }

            //Call the toastify() function in toaster_manualscript.js
            dynamic script = "toastify('" + strType + "', '" + CleanStr(Msg) + "', '" + CleanStr(Title) + "', '" + strPosition + "', '" + ShowCloseButton.ToString() + "');";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "toastedMsg", script, true);
        }
        private static string CleanStr(string Text)
        {
            return Text.Replace("'", "&#39;");
        }
    }
}
