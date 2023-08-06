namespace ReMix
{
    partial class Form1
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
            button1 = new Button();
            checkBoxLead = new CheckBox();
            checkBoxLoop = new CheckBox();
            checkBoxBeat = new CheckBox();
            checkBoxBass = new CheckBox();
            musicBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            btnPlay = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(56, 363);
            button1.Name = "button1";
            button1.Size = new Size(103, 23);
            button1.TabIndex = 0;
            button1.Text = "Click To Shift";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBoxLead
            // 
            checkBoxLead.AutoSize = true;
            checkBoxLead.Location = new Point(295, 162);
            checkBoxLead.Name = "checkBoxLead";
            checkBoxLead.Size = new Size(51, 19);
            checkBoxLead.TabIndex = 1;
            checkBoxLead.Text = "Lead";
            checkBoxLead.UseVisualStyleBackColor = true;
            checkBoxLead.CheckedChanged += checkBoxLead_CheckedChanged;
            // 
            // checkBoxLoop
            // 
            checkBoxLoop.AutoSize = true;
            checkBoxLoop.Location = new Point(352, 162);
            checkBoxLoop.Name = "checkBoxLoop";
            checkBoxLoop.Size = new Size(53, 19);
            checkBoxLoop.TabIndex = 2;
            checkBoxLoop.Text = "Loop";
            checkBoxLoop.UseVisualStyleBackColor = true;
            checkBoxLoop.CheckedChanged += checkBoxLoop_CheckedChanged;
            // 
            // checkBoxBeat
            // 
            checkBoxBeat.AutoSize = true;
            checkBoxBeat.Location = new Point(411, 162);
            checkBoxBeat.Name = "checkBoxBeat";
            checkBoxBeat.Size = new Size(49, 19);
            checkBoxBeat.TabIndex = 3;
            checkBoxBeat.Text = "Beat";
            checkBoxBeat.UseVisualStyleBackColor = true;
            checkBoxBeat.CheckedChanged += checkBoxBeat_CheckedChanged;
            // 
            // checkBoxBass
            // 
            checkBoxBass.AutoSize = true;
            checkBoxBass.Location = new Point(466, 162);
            checkBoxBass.Name = "checkBoxBass";
            checkBoxBass.Size = new Size(49, 19);
            checkBoxBass.TabIndex = 4;
            checkBoxBass.Text = "Bass";
            checkBoxBass.UseVisualStyleBackColor = true;
            checkBoxBass.CheckedChanged += checkBoxBass_CheckedChanged;
            // 
            // musicBackgroundWorker
            // 
            musicBackgroundWorker.DoWork += backgroundWorker1_DoWork;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(365, 74);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 23);
            btnPlay.TabIndex = 5;
            btnPlay.Text = "Play/Pause";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPlay);
            Controls.Add(checkBoxBass);
            Controls.Add(checkBoxBeat);
            Controls.Add(checkBoxLoop);
            Controls.Add(checkBoxLead);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private CheckBox checkBoxLead;
        private CheckBox checkBoxLoop;
        private CheckBox checkBoxBeat;
        private CheckBox checkBoxBass;
        private System.ComponentModel.BackgroundWorker musicBackgroundWorker;
        private Button btnPlay;
    }
}