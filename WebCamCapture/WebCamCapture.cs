using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WebCamControll
{
    /// <summary>
    /// Summary description for WebCamControll.
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(WebCamCapture), "CAMERA.ICO")] // toolbox bitmap
    [Designer("Sytem.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))] // make composite
    public class WebCamCapture : UserControl
    {
        /*
         * If you want to allow the user to change the display size and 
         * color format of the video capture, call:
         * SendMessage (mCapHwnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0);
         * You will need to requery the capture device to get the new settings
         */

        #region Variables

        /// <summary>
        /// Component container.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Capture timer.
        /// </summary>
        private System.Windows.Forms.Timer captureTimer;

        /// <summary>
        /// Capture time in [mm].
        /// </summary>
        private int captureTime = 100;

        /// <summary>
        /// Image widthe.
        /// </summary>
        private int imageWidth = 320;

        /// <summary>
        /// Image height.
        /// </summary>
        private int imageHeight = 240;

        /// <summary>
        /// Capture handle.
        /// </summary>
        private int captureHandle;

        /// <summary>
        /// Frae number.
        /// </summary>
        private ulong frameNumber = 0;

        /// <summary>
        /// Event object.
        /// </summary>
        private WebcamEventArgs capturedImageEventArg = new WebcamEventArgs();
        
        /// <summary>
        /// Captured data.
        /// </summary>
        private IDataObject tempImageDataObject;
        
        /// <summary>
        /// Captured data goes in to the image.
        /// </summary>
        private Image tempImage;

        /// <summary>
        /// Request to stop.
        /// </summary>
        private bool bStopped = true;

        #endregion

        #region Properties

        /// <summary>
        /// The time intervale between frame captures in [ms].
        /// </summary>
        public int CaptureTime
        {
            get
            {
                return captureTime;
            }

            set
            {
                captureTime = value;
            }
        }

        /// <summary>
        /// The height of the video capture image in [pix].
        /// </summary>
        public int CaptureHeight
        {
            get
            {
                return imageHeight;
            }

            set
            {
                imageHeight = value;
            }
        }

        /// <summary>
        /// The width of the video capture image in [pix].
        /// </summary>
        public int CaptureWidth
        {
            get
            {
                return imageWidth;
            }

            set
            {
                imageWidth = value;
            }
        }

        /// <summary>
        /// The sequence number to start at for the frame number. OPTIONAL
        /// </summary>
        public ulong FrameNumber
        {
            get
            {
                return frameNumber;
            }

            set
            {
                frameNumber = value;
            }
        }

        #endregion

        /// <summary>
        /// event delegate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public delegate void WebCamEventHandler(object source, WebcamEventArgs e);

        /// <summary>
        /// Fired when a new image is captured.
        /// </summary>
        public event WebCamEventHandler ImageCaptured;

        #region API Declarations

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

        [DllImport("user32", EntryPoint = "OpenClipboard")]
        public static extern int OpenClipboard(int hWnd);

        [DllImport("user32", EntryPoint = "EmptyClipboard")]
        public static extern int EmptyClipboard();

        [DllImport("user32", EntryPoint = "CloseClipboard")]
        public static extern int CloseClipboard();

        #endregion

        #region API Constants

        public const int WM_USER = 1024;

        public const int WM_CAP_CONNECT = 1034;
        public const int WM_CAP_DISCONNECT = 1035;
        public const int WM_CAP_GET_FRAME = 1084;
        public const int WM_CAP_COPY = 1054;

        public const int WM_CAP_START = WM_USER;

        public const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
        public const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
        public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;
        public const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebCamCapture()
        {
            // This call is required by the Windows.Forms Form Designer.
            this.InitializeComponent();
        }

        /// <summary>
        /// Override the class's finalize method, so we can stop
        /// the video capture on exit
        /// </summary>
        ~WebCamCapture()
        {
            this.Stop();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.captureTimer = new System.Windows.Forms.Timer(this.components);
            // 
            // timer1
            // 
            this.captureTimer.Tick += new System.EventHandler(this.captureTimer_Tick);
            // 
            // WebCamCapture
            // 
            this.Name = "WebCamCapture";
            this.Size = new System.Drawing.Size(342, 252);

        }

        #endregion

        #region Start and Stop Capture Functions

        /// <summary>
        /// Starts the video capture
        /// </summary>
        /// <param name="FrameNumber">the frame number to start at. 
        /// Set to 0 to let the control allocate the frame number</param>
        public void Start(ulong FrameNum)
        {
            try
            {
                // for safety, call stop, just in case we are already running
                this.Stop();

                // setup a capture window
                captureHandle = capCreateCaptureWindowA("WebCap", 0, 0, 0, imageWidth, imageHeight, this.Handle.ToInt32(), 0);

                // connect to the capture device
                Application.DoEvents();
                SendMessage(captureHandle, WM_CAP_CONNECT, 0, 0);
                SendMessage(captureHandle, WM_CAP_SET_PREVIEW, 0, 0);

                // set the frame number
                frameNumber = FrameNum;

                // set the timer information
                this.captureTimer.Interval = captureTime;
                bStopped = false;
                this.captureTimer.Start();
            }

            catch (Exception excep)
            {
                MessageBox.Show("An error ocurred while starting the video capture. Check that your webcamera is connected properly and turned on.\r\n\n" + excep.Message);
                this.Stop();
            }
        }

        /// <summary>
        /// Stops the video capture
        /// </summary>
        public void Stop()
        {
            try
            {
                // stop the timer
                bStopped = true;
                this.captureTimer.Stop();

                // disconnect from the video source
                Application.DoEvents();
                SendMessage(captureHandle, WM_CAP_DISCONNECT, 0, 0);
            }

            catch (Exception exception)
            { // don't raise an error here.
            }

        }

        #endregion

        #region Video Capture Code

        /// <summary>
        /// Capture the next frame from the video feed
        /// </summary>
        private void captureTimer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                // pause the timer
                this.captureTimer.Stop();

                // get the next frame;
                SendMessage(captureHandle, WM_CAP_GET_FRAME, 0, 0);

                // copy the frame to the clipboard
                SendMessage(captureHandle, WM_CAP_COPY, 0, 0);

                // paste the frame into the event args image
                if (ImageCaptured != null)
                {
                    // get from the clipboard
                    tempImageDataObject = Clipboard.GetDataObject();
                    tempImage = (System.Drawing.Bitmap)tempImageDataObject.GetData(System.Windows.Forms.DataFormats.Bitmap);

                    /*
                    * For some reason, the API is not resizing the video
                    * feed to the width and height provided when the video
                    * feed was started, so we must resize the image here
                    */
                    capturedImageEventArg.Image = tempImage.GetThumbnailImage(imageWidth, imageHeight, null, System.IntPtr.Zero);

                    // raise the event
                    this.ImageCaptured(this, capturedImageEventArg);
                }

                // restart the timer
                Application.DoEvents();
                if (!bStopped)
                    this.captureTimer.Start();
            }

            catch (Exception excep)
            {
                MessageBox.Show("An error ocurred while capturing the video image. The video capture will now be terminated.\r\n\n" + excep.Message);
                this.Stop(); // stop the process
            }
        }

        #endregion
    }
}
