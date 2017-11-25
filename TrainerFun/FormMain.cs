using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;

namespace TrainerFun
{
  public partial class FormMain : Form
  {

    //===============================================
    //                  VARS
    //===============================================
    List<Process> _procList;    //list of procs
    Process proc = null;        //proc we want to attach to
    IntPtr procHdl;             //ptr to proc
    Thread FreezeThread = null; //thread that freezes var
    bool runFreezeThread = true;   //ctrl flag for freeze thread

    //===============================================
    //                  DELEGATES
    //===============================================
    delegate String updateLabelDelegate();

    //===============================================
    //                  CONST
    //===============================================
    public static uint PROCESS_VM_READ = 0x0010;
    public static uint PROCESS_VM_WRITE = 0x0020;
    public static uint PROCESS_VM_OPERATION = 0x0008;
    public static uint PAGE_READWRITE = 0x0004;


    //===============================================
    //                  PINVOKE
    //===============================================

    [DllImport("kernel32.dll")]
    static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern Int32 CloseHandle(IntPtr hProcess);

    //[DllImport("kernel32.dll")]
    //public static extern IntPtr OpenProcess(UInt32 dwAccess, bool inherit, int pid);

    ////[DllImport("kernel32.dll")]
    ////public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    //[DllImport("kernel32.dll")]
    //public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    ////[DllImport("kernel32.dll", SetLastError = true)]
    ////static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

    //[DllImport("kernel32.dll")]
    //public static extern bool WriteProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, [In, Out] byte[] lpBuffer, UInt64 dwSize, out IntPtr lpNumberOfBytesWritten);

    //[DllImport("kernel32.dll", SetLastError = true)]
    //public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UInt32 dwSize, uint flNewProtect, out uint lpflOldProtect);

    //===============================================
    //                  FLAGS
    //===============================================

    [Flags]
    public enum ProcessAccessFlags : uint
    {
      All = 0x001F0FFF,
      Terminate = 0x00000001,
      CreateThread = 0x00000002,
      VMOperation = 0x00000008,
      VMRead = 0x00000010,
      VMWrite = 0x00000020,
      DupHandle = 0x00000040,
      SetInformation = 0x00000200,
      QueryInformation = 0x00000400,
      Synchronize = 0x00100000
    }

    #region HELPERS
    /// <summary>
    /// Update the list of processes
    /// </summary>
    private void RefreshProcList()
    {
      _procList = new List<Process>();
      listViewProc.Items.Clear();
      Process.GetProcesses().ToList().ForEach(p =>{_procList.Add(p);});
      _procList.Sort((a, b) => a.ProcessName.CompareTo(b.ProcessName));
      _procList.ForEach(p => listViewProc.Items.Add(new ListViewItem(new String[] { p.ProcessName }))); 
    }

    public static void WriteMem(Process p, int address, long v)
    {
      var hProc = OpenProcess(ProcessAccessFlags.All, false, (int)p.Id);
      var val = new byte[] { (byte)v };

      int wtf = 0;
      WriteProcessMemory(hProc, new IntPtr(address), val, (UInt32)val.LongLength, out wtf);

      CloseHandle(hProc);
    }

    #endregion

    #region EVENTS

    private void buttonRefresh_Click(object sender, EventArgs e)
    {
      RefreshProcList();
    }

    private void listViewProc_Click(object sender, EventArgs e)
    {
      try
      {
        var selection = listViewProc.SelectedItems[0].SubItems[0];
        textBoxProcName.Text = selection.Text;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void buttonAttach_Click(object sender, EventArgs e)
    {
      try
      {
        //proc = Process.GetProcessesByName(textBoxProcName.Text)[0];
        //procHdl = OpenProcess(PROCESS_WM_READ, false, proc.Id);
        //procHdl = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, proc.Id);

        if (proc == null || textBoxProcName.Text == ""){ throw new Exception("Failed to attach to process");}
      }
      catch(Exception ex)
      {
        labelAttach.Text = ex.ToString();
        MessageBox.Show("Failed to attach to process due to: " + ex.Message, "Failed to attach to process");
        return;
      }

      labelAttach.BackColor = Color.LightGreen;
      labelAttach.Text = "Attached to " + proc.ProcessName.ToString();

    }

    private void buttonFreeze_Click(object sender, EventArgs e)
    {
      //create a new thread that continuously writes to the address with the specified value
      if(FreezeThread != null)
      {
        runFreezeThread = false;
        Thread.Sleep(100);        //wait.. let the thread die (bad way of doing this, but whatever)
      }
      FreezeThread = new Thread(FreezerThread) { IsBackground = true };
      FreezeThread.Start();
    }

    private void FreezerThread()
    {
      //labelAttach.Text = "Freeze Thread Start!";
      //labelAttach.BackColor = Color.LightBlue;
      //vars
      //IntPtr bytesWritten = (IntPtr)0; //tells us how much the operation wrote
      //byte[] buffer = Encoding.Unicode.GetBytes("Hello World!\0"); // '\0' marks the end of string
      //byte[] buffer = BitConverter.GetBytes(int.Parse(textBoxAddrValueToForce.Text));
      //IntPtr address = (IntPtr)0x036B43EC;
      ///Console.WriteLine("Value in buffer: " + buffer.ToString());

      var proc = Process.GetProcessesByName(textBoxProcName.Text).FirstOrDefault();

      while (runFreezeThread)
      {


        WriteMem(proc, 0x036B43EC, 99);
        //Console.WriteLine("Wrote " + bytesWritten + " bytes");
        //don't pin a core
        Thread.Sleep(25);
      }
    }
    #endregion

    #region WINFORMS
    //===============================================
    //                  WIN FORMS STUFF
    //===============================================
    public FormMain()
    {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      RefreshProcList();
    }

    #endregion
  }
}
