namespace TrainerFun
{
  partial class FormMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.buttonAttach = new System.Windows.Forms.Button();
      this.labelAttach = new System.Windows.Forms.Label();
      this.buttonRefresh = new System.Windows.Forms.Button();
      this.textBoxAddr = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.listViewProc = new System.Windows.Forms.ListView();
      this.columnHeadeProdName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.textBoxProcName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxAddrValueToForce = new System.Windows.Forms.TextBox();
      this.groupBoxProcesses = new System.Windows.Forms.GroupBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.progressBarFreezing = new System.Windows.Forms.ProgressBar();
      this.buttonStopFreeze = new System.Windows.Forms.Button();
      this.buttonFreeze = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBoxProcesses.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonAttach
      // 
      this.buttonAttach.Location = new System.Drawing.Point(8, 221);
      this.buttonAttach.Name = "buttonAttach";
      this.buttonAttach.Size = new System.Drawing.Size(310, 23);
      this.buttonAttach.TabIndex = 3;
      this.buttonAttach.Text = "Attach";
      this.buttonAttach.UseVisualStyleBackColor = true;
      this.buttonAttach.Click += new System.EventHandler(this.buttonAttach_Click);
      // 
      // labelAttach
      // 
      this.labelAttach.AutoSize = true;
      this.labelAttach.Location = new System.Drawing.Point(6, 367);
      this.labelAttach.Name = "labelAttach";
      this.labelAttach.Size = new System.Drawing.Size(97, 13);
      this.labelAttach.TabIndex = 5;
      this.labelAttach.Text = "Waiting to attach...";
      // 
      // buttonRefresh
      // 
      this.buttonRefresh.Location = new System.Drawing.Point(6, 148);
      this.buttonRefresh.Name = "buttonRefresh";
      this.buttonRefresh.Size = new System.Drawing.Size(315, 23);
      this.buttonRefresh.TabIndex = 6;
      this.buttonRefresh.Text = "Refresh";
      this.buttonRefresh.UseVisualStyleBackColor = true;
      this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
      // 
      // textBoxAddr
      // 
      this.textBoxAddr.Location = new System.Drawing.Point(6, 32);
      this.textBoxAddr.Name = "textBoxAddr";
      this.textBoxAddr.Size = new System.Drawing.Size(125, 20);
      this.textBoxAddr.TabIndex = 7;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(45, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Address";
      // 
      // listViewProc
      // 
      this.listViewProc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeadeProdName});
      this.listViewProc.Location = new System.Drawing.Point(9, 19);
      this.listViewProc.Name = "listViewProc";
      this.listViewProc.Size = new System.Drawing.Size(312, 123);
      this.listViewProc.TabIndex = 9;
      this.listViewProc.UseCompatibleStateImageBehavior = false;
      this.listViewProc.View = System.Windows.Forms.View.Details;
      this.listViewProc.Click += new System.EventHandler(this.listViewProc_Click);
      // 
      // columnHeadeProdName
      // 
      this.columnHeadeProdName.Text = "Process List";
      this.columnHeadeProdName.Width = 282;
      // 
      // textBoxProcName
      // 
      this.textBoxProcName.Location = new System.Drawing.Point(8, 195);
      this.textBoxProcName.Name = "textBoxProcName";
      this.textBoxProcName.Size = new System.Drawing.Size(310, 20);
      this.textBoxProcName.TabIndex = 10;
      this.textBoxProcName.Text = "halo2";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(5, 179);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(76, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Process Name";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(172, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "Value";
      // 
      // textBoxAddrValueToForce
      // 
      this.textBoxAddrValueToForce.Location = new System.Drawing.Point(175, 32);
      this.textBoxAddrValueToForce.Name = "textBoxAddrValueToForce";
      this.textBoxAddrValueToForce.Size = new System.Drawing.Size(135, 20);
      this.textBoxAddrValueToForce.TabIndex = 12;
      // 
      // groupBoxProcesses
      // 
      this.groupBoxProcesses.Controls.Add(this.listViewProc);
      this.groupBoxProcesses.Controls.Add(this.buttonAttach);
      this.groupBoxProcesses.Controls.Add(this.buttonRefresh);
      this.groupBoxProcesses.Controls.Add(this.label3);
      this.groupBoxProcesses.Controls.Add(this.textBoxProcName);
      this.groupBoxProcesses.Location = new System.Drawing.Point(9, 12);
      this.groupBoxProcesses.Name = "groupBoxProcesses";
      this.groupBoxProcesses.Size = new System.Drawing.Size(325, 257);
      this.groupBoxProcesses.TabIndex = 14;
      this.groupBoxProcesses.TabStop = false;
      this.groupBoxProcesses.Text = "Processes";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.progressBarFreezing);
      this.groupBox1.Controls.Add(this.buttonStopFreeze);
      this.groupBox1.Controls.Add(this.buttonFreeze);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.textBoxAddrValueToForce);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.textBoxAddr);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(9, 275);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(325, 89);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Freezer";
      // 
      // progressBarFreezing
      // 
      this.progressBarFreezing.Location = new System.Drawing.Point(175, 58);
      this.progressBarFreezing.Name = "progressBarFreezing";
      this.progressBarFreezing.Size = new System.Drawing.Size(135, 23);
      this.progressBarFreezing.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBarFreezing.TabIndex = 17;
      // 
      // buttonStopFreeze
      // 
      this.buttonStopFreeze.Location = new System.Drawing.Point(87, 58);
      this.buttonStopFreeze.Name = "buttonStopFreeze";
      this.buttonStopFreeze.Size = new System.Drawing.Size(73, 23);
      this.buttonStopFreeze.TabIndex = 16;
      this.buttonStopFreeze.Text = "Stop";
      this.buttonStopFreeze.UseVisualStyleBackColor = true;
      this.buttonStopFreeze.Click += new System.EventHandler(this.buttonStopFreeze_Click);
      // 
      // buttonFreeze
      // 
      this.buttonFreeze.Location = new System.Drawing.Point(6, 58);
      this.buttonFreeze.Name = "buttonFreeze";
      this.buttonFreeze.Size = new System.Drawing.Size(73, 23);
      this.buttonFreeze.TabIndex = 15;
      this.buttonFreeze.Text = "Freeze";
      this.buttonFreeze.UseVisualStyleBackColor = true;
      this.buttonFreeze.Click += new System.EventHandler(this.buttonFreeze_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(143, 36);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(19, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "=>";
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(344, 391);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBoxProcesses);
      this.Controls.Add(this.labelAttach);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormMain";
      this.Text = "Freeze Pop";
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.groupBoxProcesses.ResumeLayout(false);
      this.groupBoxProcesses.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button buttonAttach;
    private System.Windows.Forms.Label labelAttach;
    private System.Windows.Forms.Button buttonRefresh;
    private System.Windows.Forms.TextBox textBoxAddr;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListView listViewProc;
    private System.Windows.Forms.ColumnHeader columnHeadeProdName;
    private System.Windows.Forms.TextBox textBoxProcName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxAddrValueToForce;
    private System.Windows.Forms.GroupBox groupBoxProcesses;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button buttonFreeze;
    private System.Windows.Forms.Button buttonStopFreeze;
    private System.Windows.Forms.ProgressBar progressBarFreezing;
  }
}

