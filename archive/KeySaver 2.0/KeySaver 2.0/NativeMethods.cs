using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeySaver
{
    internal static class NativeMethods
    {
        /// <summary>
        /// Enthält den hexadezimalen Wert für die native SendMessageW() Methode zum Setzen des CueBanners.
        /// </summary>
        private static UInt32 EM_SETCUEBANNER = 0x1501;

        /// <summary>
        /// Import einer Windows eigenen Funktion.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// Setzt oder entfernt den CueBanner bei der angegebenen TextBox.
        /// </summary>
        /// <param name="textBox">Die betreffende TextBox.</param>
        /// <param name="cueBanner">Der CueBanner.</param>
        /// <param name="hide">true, wenn der CueBanner entfernt werden soll, sonst false.</param>
        public static void SetCueBanner(this TextBox textBox, string cueBanner, bool hide)
        {
            if (!string.IsNullOrWhiteSpace(cueBanner) || hide)
            {
                NativeMethods.SendMessageW(textBox.Handle, NativeMethods.EM_SETCUEBANNER, default(IntPtr), cueBanner);
            }
        }
    }
}