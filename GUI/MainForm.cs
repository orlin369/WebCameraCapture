using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WebCamControll;

namespace GUI
{
    public partial class MainForm : Form
    {

        #region Variables

        /// <summary>
        /// Web camera control.
        /// </summary>
        private WebCamCapture webCamCapture;

        /// <summary>
        /// Shifting value.
        /// </summary>
        private int shiftValue = 8;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.InitializeWebCam();
        }

        #endregion

        /// <summary>
        /// Initialise the WEB camera.
        /// </summary>
        private void InitializeWebCam()
        {
            // 
            // WebCamCapture
            // 
            this.webCamCapture = new WebCamCapture();
            this.webCamCapture.CaptureHeight = 240;
            this.webCamCapture.CaptureWidth = 320;
            // TODO: Code generation for 'this.WebCamCapture.FrameNumber' failed because of Exception 'Invalid Primitive Type: System.UInt64. Only CLS compliant primitive types can be used. Consider using CodeObjectCreateExpression.'.
            this.webCamCapture.Location = new System.Drawing.Point(17, 17);
            this.webCamCapture.Name = "WebCamCapture";
            this.webCamCapture.Size = new System.Drawing.Size(640, 480); //342, 252
            this.webCamCapture.TabIndex = 0;
            this.webCamCapture.CaptureTime = 100;
            this.webCamCapture.ImageCaptured += new WebCamCapture.WebCamEventHandler(this.WebCamCapture_ImageCaptured);
            // 

            // set the image capture size
            this.webCamCapture.CaptureHeight = this.pbVideoView.Height;
            this.webCamCapture.CaptureWidth = this.pbVideoView.Width;
        }

        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop the video capture
            this.webCamCapture.Stop();
        }

        #endregion

        #region WEB Camera capture event.

        /// <summary>
        /// An image was capture
        /// </summary>
        /// <param name="source">control raising the event</param>
        /// <param name="e">WebCamEventArgs</param>
        private void WebCamCapture_ImageCaptured(object source, WebcamEventArgs e)
        {
            // Set the picturebox picture.
            this.pbVideoView.Image = e.Image;
        }

        #endregion

        #region Buttons

        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            // Change the capture time frame.
            this.webCamCapture.CaptureTime = (int)this.numCaptureTime.Value;

            // Start the video capture. let the control handle the frame numbers.
            this.webCamCapture.Start(0);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Stop the video capture.
            this.webCamCapture.Stop();
        }

        private void btnContinues_Click(object sender, EventArgs e)
        {
            // Change the capture time frame.
            this.webCamCapture.CaptureTime = (int)this.numCaptureTime.Value;

            // Resume the video capture from the stop.
            this.webCamCapture.Start(this.webCamCapture.FrameNumber);
        }

        #endregion

        #region Shift value changer

        private void numShiftValue_ValueChanged(object sender, EventArgs e)
        {
            this.shiftValue = Convert.ToInt32(this.numShiftValue.Value);
        }

        #endregion

    }
}
