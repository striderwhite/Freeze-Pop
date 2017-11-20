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
    //                  CONST
    //===============================================
    const int PROCESS_WM_READ = 0x0010;         //read permissions
    const int PROCESS_VM_WRITE = 0x0020;        //write permissions
    const int PROCESS_VM_OPERATION = 0x0008;    //operation permissiosn

    //===============================================
    //                  PINVOKE
    //===============================================
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);


    #region HELPERS
    /// <summary>
    /// Update the list of processes
    /// </summary>
    private void RefreshProcList()
    {
      _procList = new List<Process>();
      listViewProc.Items.Clear();
      Process.GetProcesses().ToList().ForEach(p =>{
        _procList.Add(p);
        listViewProc.Items.Add(new ListViewItem(new String[] {p.ProcessName }));
      });
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
        proc = Process.GetProcessesByName(textBoxProcName.Text)[0];
        procHdl = OpenProcess(PROCESS_WM_READ, false, proc.Id);

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
      //vars
      int bytesWritten = 0; //tells us how much the operation wrote
      //byte[] buffer = Encoding.Unicode.GetBytes("Hello World!\0"); // '\0' marks the end of string
      byte[] buffer = BitConverter.GetBytes(int.Parse(textBoxAddrValueToForce.Text));

      while (runFreezeThread)
      {
        //perform write
        WriteProcessMemory((int)procHdl, int.Parse(textBoxAddr.Text, NumberStyles.HexNumber), buffer, buffer.Length, ref bytesWritten);
        //WriteProcessMemory((int)processHandle, 0x0046A3B8, buffer, buffer.Length, ref bytesWritten);

        //don't pin a core
        Thread.Sleep(1);
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
