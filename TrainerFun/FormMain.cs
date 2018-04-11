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
    IntPtr hProc;             //ptr to proc
    Thread FreezeThread = null; //thread that freezes var
    bool runFreezeThread = true;   //ctrl flag for freeze thread
    bool attached = false;      //flag if we are attached to proc


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

    public static int WriteMem(IntPtr hProc, IntPtr address, long v)
    {
      var val = new byte[] { (byte)v };
      int bytesWritten = 0;
      WriteProcessMemory(hProc, address, val, (UInt32)val.LongLength, out bytesWritten);
      return bytesWritten;
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
        attached = false;
        proc = Process.GetProcessesByName(textBoxProcName.Text).FirstOrDefault(); //process object
        hProc = OpenProcess(ProcessAccessFlags.All, false, (int)proc.Id);         //process handle
        if (proc == null || textBoxProcName.Text == ""){ throw new Exception("Failed to attach to process");}
      }
      catch(Exception ex)
      {
        labelAttach.Text = ex.ToString();
        MessageBox.Show("Failed to attach to process due to: " + ex.Message, "Failed to attach to process");
        return;
      }
      attached = true;
      labelAttach.BackColor = Color.LightGreen;
      labelAttach.Text = "Attached to " + proc.ProcessName.ToString();
    }

    /// <summary>
    /// Create a new thread that continuously writes to the address with the specified value
    /// Kills any other thread
    /// Error if not attached to any processes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonFreeze_Click(object sender, EventArgs e)
    {
      if (!attached){
        MessageBox.Show("Attach to a process before freezing", "Not attached to process");
        return;
      }
      //kill any existing processes
      if(FreezeThread != null)
      {
        runFreezeThread = false;
        Thread.Sleep(100);        //wait.. let the thread die (bad way of doing this, but whatever)
      }
      FreezeThread = new Thread(FreezerThread) { IsBackground = true };
      FreezeThread.Start();
      progressBarFreezing.Style = ProgressBarStyle.Marquee;
      progressBarFreezing.MarqueeAnimationSpeed = 30;
      //MessageBox.Show("FreezeThread is alive and not null: " + FreezeThread.IsAlive);
    }

    private void FreezerThread()
    {
      //TODO: catch exceptions and field validation
      IntPtr address = new IntPtr(int.Parse(textBoxAddr.Text,System.Globalization.NumberStyles.HexNumber)); //Address to modify
      int value = int.Parse(textBoxAddrValueToForce.Text);  //TODO parse from textbox
      int bytesWritten = 0;
      runFreezeThread = true;

      while (runFreezeThread)
      {
        bytesWritten = WriteMem(hProc, address, value);
        //Console.WriteLine("Wrote " + bytesWritten + " bytes"); 
        Thread.Sleep(1); //Dont pin
      }
      attached = false;
      CloseHandle(hProc);
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

    private void buttonStopFreeze_Click(object sender, EventArgs e)
    {
      runFreezeThread = false;
      while (FreezeThread.IsAlive)
      {
        Thread.Sleep(50); //Hold on until we know its dead
      }
      labelAttach.BackColor = Color.Red;
      labelAttach.Text = "Unattached";
      progressBarFreezing.Style = ProgressBarStyle.Continuous;
      progressBarFreezing.MarqueeAnimationSpeed = 0;
    }
  }
}
