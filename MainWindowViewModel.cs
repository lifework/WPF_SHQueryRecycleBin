using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SHQueryRecycleBin
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public long RecycleBinSize { get; set; }

        public MainWindowViewModel()
        {
            RecycleBinSize = GetRecycleBinSize();
        }

        // SHQueryRecycleBinW function (shellapi.h)
        // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shqueryrecyclebinw
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int SHQueryRecycleBin([MarshalAs(UnmanagedType.LPWStr), In] string? pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHQUERYRBINFO
        {
            public ulong cbSize;
            public long i64Size;
            public long i64NumItems;
        }

        // Get A Recycle Bin Size And File Count
        // https://www.dreamincode.net/forums/topic/365687-Get-a-recycle-bin-size-and-file-count/
        public long GetRecycleBinSize()
        {
            SHQUERYRBINFO query = new SHQUERYRBINFO();
            query.cbSize = (ulong) Marshal.SizeOf(query.GetType());
            SHQueryRecycleBin(null, ref query);

            Debug.WriteLine($"SHQueryRecycleBin: Size = {query.i64Size}, NumItems = {query.i64NumItems}");
            return query.i64Size;
        }
    }
}
