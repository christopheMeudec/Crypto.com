namespace Crypto.WinUI;

partial class MainMenuForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        btnLoad = new Button();
        cbInstruments = new ComboBox();
        lblInstruments = new Label();
        lblTrend = new Label();
        cbPoints = new ComboBox();
        lblPoints = new Label();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Location = new Point(0, 15);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(732, 435);
        formsPlot1.TabIndex = 0;
        // 
        // btnLoad
        // 
        btnLoad.Location = new Point(347, 12);
        btnLoad.Name = "btnLoad";
        btnLoad.Size = new Size(75, 23);
        btnLoad.TabIndex = 1;
        btnLoad.Text = "Load";
        btnLoad.UseVisualStyleBackColor = true;
        btnLoad.Click += btnLoad_Click;
        // 
        // cbInstruments
        // 
        cbInstruments.FormattingEnabled = true;
        cbInstruments.Items.AddRange(new object[] { "CKBUSD-INDEX" });
        cbInstruments.Location = new Point(83, 12);
        cbInstruments.Name = "cbInstruments";
        cbInstruments.Size = new Size(121, 23);
        cbInstruments.TabIndex = 2;
        cbInstruments.SelectedIndexChanged += cbInstruments_SelectedIndexChanged;
        // 
        // lblInstruments
        // 
        lblInstruments.AutoSize = true;
        lblInstruments.Location = new Point(7, 15);
        lblInstruments.Name = "lblInstruments";
        lblInstruments.Size = new Size(70, 15);
        lblInstruments.TabIndex = 3;
        lblInstruments.Text = "Instruments";
        // 
        // lblTrend
        // 
        lblTrend.AutoSize = true;
        lblTrend.Location = new Point(453, 15);
        lblTrend.Name = "lblTrend";
        lblTrend.Size = new Size(36, 15);
        lblTrend.TabIndex = 4;
        lblTrend.Text = "Trend";
        // 
        // cbPoints
        // 
        cbPoints.FormattingEnabled = true;
        cbPoints.Items.AddRange(new object[] { "100", "200", "500" });
        cbPoints.Location = new Point(269, 12);
        cbPoints.Name = "cbPoints";
        cbPoints.Size = new Size(58, 23);
        cbPoints.TabIndex = 2;
        cbPoints.SelectedIndexChanged += cbPoints_SelectedIndexChanged;
        // 
        // lblPoints
        // 
        lblPoints.AutoSize = true;
        lblPoints.Location = new Point(223, 15);
        lblPoints.Name = "lblPoints";
        lblPoints.Size = new Size(40, 15);
        lblPoints.TabIndex = 3;
        lblPoints.Text = "Points";
        // 
        // MainMenuForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(732, 450);
        Controls.Add(lblTrend);
        Controls.Add(lblPoints);
        Controls.Add(lblInstruments);
        Controls.Add(cbPoints);
        Controls.Add(cbInstruments);
        Controls.Add(btnLoad);
        Controls.Add(formsPlot1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainMenuForm";
        Text = "#";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private Button btnLoad;
    private ComboBox cbInstruments;
    private Label lblInstruments;
    private Label lblTrend;
    private ComboBox cbPoints;
    private Label lblPoints;
}
