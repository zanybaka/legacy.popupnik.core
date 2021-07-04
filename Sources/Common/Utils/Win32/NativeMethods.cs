using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Common.Utils.Win32
{
    internal class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            uint SecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            int hTemplateFile
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        public static extern SafeFileHandle CreateMailslot(string lpName, uint nMaxMessageSize,
                                                           int lReadTimeout, IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        public static extern bool GetMailslotInfo(SafeFileHandle hMailslot,
                                                  out IntPtr lpMaxMessageSize,
                                                  out IntPtr lpNextSize,
                                                  out IntPtr lpMessageCount,
                                                  out IntPtr lpReadTimeout);

        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer,
                                            uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
                                            [In] ref NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(SafeFileHandle hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer,
                                           uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);
    }
}