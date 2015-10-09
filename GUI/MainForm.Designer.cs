namespace GUI
{
    partial class MainForm
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
            this.pbVideoView = new System.Windows.Forms.PictureBox();
            this.btnStartCapture = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnContinues = new System.Windows.Forms.Button();
            this.numCaptureTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCaptureTime)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideoView
            // 
            this.pbVideoView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVideoView.Location = new System.Drawing.Point(12, 12);
            this.pbVideoView.Name = "pbVideoView";
            this.pbVideoView.Size = new System.Drawing.Size(640, 480);
            this.pbVideoView.TabIndex = 0;
            this.pbVideoView.TabStop = false;
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.Location = new System.Drawing.Point(12, 498);
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(75, 23);
            this.btnStartCapture.TabIndex = 1;
            this.btnStartCapture.Text = "Start";
            this.btnStartCapture.UseVisualStyleBackColor = true;
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(255, 498);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnContinues
            // 
            this.btnContinues.Location = new System.Drawing.Point(93, 498);
            this.btnContinues.Name = "btnContinues";
            this.btnContinues.Size = new System.Drawing.Size(75, 23);
            this.btnContinues.TabIndex = 3;
            this.btnContinues.Text = "Continues";
            this.btnContinues.UseVisualStyleBackColor = true;
            this.btnContinues.Click += new System.EventHandler(this.btnContinues_Click);
            // 
            // numCaptureTime
            // 
            this.numCaptureTime.Location = new System.Drawing.Point(174, 501);
            this.numCaptureTime.Name = "numCaptureTime";
            this.numCaptureTime.Size = new System.Drawing.Size(75, 20);
            this.numCaptureTime.TabIndex = 4;
            this.numCaptureTime.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 528);
            this.Controls.Add(this.numCaptureTime);
            this.Controls.Add(this.btnContinues);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStartCapture);
            this.Controls.Add(this.pbVideoView);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "WEB Camera 3D";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCaptureTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVideoView;
        private System.Windows.Forms.Button btnStartCapture;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnContinues;
        private System.Windows.Forms.NumericUpDown numCaptureTime;
    }
}

