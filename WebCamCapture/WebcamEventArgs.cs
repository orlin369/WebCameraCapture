using System;
using System.Drawing;

namespace WebCamControll
{
	/// <summary>
	/// EventArgs for the webcam control
	/// </summary>
	public class WebcamEventArgs : EventArgs
    {
        #region Variables

        /// <summary>
        /// Captured image.
        /// </summary>
		private Image capturedImage;

        /// <summary>
        /// Number of the frame.
        /// </summary>
		private ulong frameNumber = 0;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public WebcamEventArgs()
		{
		}

        #region Property

        /// <summary>
		///  WebCamImage
		///  This is the image returned by the web camera capture
		/// </summary>
		public Image Image
		{
			get
			{ 
                return capturedImage; 
            }

			set
			{ 
                capturedImage = value; 
            }
		}

		/// <summary>
		/// FrameNumber
		/// Holds the sequence number of the frame capture
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
    }
}
