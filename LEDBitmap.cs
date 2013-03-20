using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WirelessProject
{
    public delegate void ChangedLEDEventHandler(object sender, LEDEventArgs e);
    public class LEDEventArgs : EventArgs
    {
        private LEDBitmap.LED_STATE lstate;

        internal LEDEventArgs(LEDBitmap.LED_STATE ledstate)
        {
            lstate = ledstate;
        }
        internal LEDEventArgs()
        {
            lstate = LEDBitmap.LED_STATE.INVALID;
        }

        internal LEDBitmap.LED_STATE RetID()
        {
            return lstate;
        }

    }
    public class LEDBitmap
    {
        internal const int LED_SIZE = 44;

        internal enum LED_STATE
        {
            ON = 0,            // col 0
            OFF = 1,            // col 1
            DISABLED = 2,            // col 2
            INVALID = 3
        };
        internal enum LED_TYPE
        {
            ROUND = 0,
            SQUARE = 4 * LED_SIZE
        };
        internal enum LED_COLOR
        {
            RED = 0 * LED_SIZE,   // row 0
            GREEN = 1 * LED_SIZE,   // row 1
            YELLOW = 2 * LED_SIZE,   // row 2
            BLUE = 3 * LED_SIZE    // row 3
        };

        internal LED_COLOR m_nLedColor;
        internal LED_TYPE m_nLedShape;
        internal LED_STATE m_nLedMode;

        private Timer timer = new Timer();
        internal float m_nLedBlinkRate;

        private Bitmap DisplayBitmap = new Bitmap(44, 44);
        public Bitmap displaybitmap
        {
            get { return DisplayBitmap; }
            set { DisplayBitmap = value; }
        }

        private Bitmap LedImage;
        public Bitmap ledimage
        {
            get { return LedImage; }
            set { LedImage = value; }
        }

        private bool blinkenabled;

        public event ChangedLEDEventHandler LedChanged;

        public LEDBitmap(Bitmap bitmaps)
        {
            // Make Transparent
            try
            {
                ledimage = bitmaps;
                Bitmap TempLED = ledimage;
                Color backColor = ledimage.GetPixel(1, 1);
                TempLED.MakeTransparent(backColor);
                ledimage = TempLED;
            }
            catch
            {
                MessageBox.Show(
                    @"Error: Bitmaps invalid ",
                    "",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);

                ledimage = null;
            }

            //			foreach(LED_COLOR ledcolor in Enum.GetValues(typeof (LED_COLOR)))
            //				Debug.WriteLine(ledcolor);
        }

        public void LedDraw()
        {
            // draw on the bitmap to be displayed in OnPaint
            Graphics gImage = Graphics.FromImage(displaybitmap);
            Rectangle rect = new Rectangle((int)m_nLedMode * LED_SIZE,
                (int)m_nLedColor + (int)m_nLedShape,
                LED_SIZE, LED_SIZE);
            // draw from the larger image to the smaller
            gImage.DrawImage(ledimage, 0, 0, rect, GraphicsUnit.Pixel);


            gImage.Dispose();
            // cause an event, notify anyone interested
            // wrapper for LedChanged delegate event
            OnLedChanged(new LEDEventArgs(m_nLedMode)); // custom event arg
        }

        // Responsible method for notifying registered objects of the event
        // Can be overridden by derived class
        protected virtual void OnLedChanged(LEDEventArgs e)
        {
            LED_STATE CurrentState = e.RetID(); // custom event arg
            // These are the LED state changes we choose to update on
            if ((CurrentState == LED_STATE.ON) |
                (CurrentState == LED_STATE.OFF) |
                (CurrentState == LED_STATE.DISABLED))
            {
                if (LedChanged != null)		// if anyone has signed up to be
                    LedChanged(this, e);    //notified of an LED change
            }
        }

        /// <summary>
        /// IBitmapLED interface implenentation
        /// </summary>
        public void LedOn()
        {
            m_nLedMode = LED_STATE.ON;
            LedDraw();
        }
        public void LedDisable()
        {
            m_nLedMode = LED_STATE.DISABLED;
            LedDraw();
        }
        public void LedOff()
        {
            m_nLedMode = LED_STATE.OFF;
            LedDraw();
        }
        public int LedState
        {
            get { return (int)m_nLedMode; }
            set { m_nLedMode = (LED_STATE)value; }
        }
        public float BlinkRate
        {
            get { return m_nLedBlinkRate; }
            set { m_nLedBlinkRate = value; }
        }

        public void BlinkEnable()
        {
            if (blinkenabled != true)
            {
                blinkenabled = true;
                BlinkRate = 1.0f;
                timer.Interval = (int)BlinkRate * 1000;
                timer.Tick += new EventHandler(BlinkOnTick);
                timer.Start();
            }
        }

        public void BlinkEnable(float rate)
        {
            if (blinkenabled != true)
            {
                blinkenabled = true;
                BlinkRate = rate;
                timer.Interval = (int)rate * 1000;
                timer.Tick += new EventHandler(BlinkOnTick);
                timer.Start();
            }
        }

        public void BlinkDisable()
        {
            if (blinkenabled == true)
            {
                blinkenabled = false;
                timer.Stop();
                timer.Tick -= new EventHandler(BlinkOnTick);
            }
        }

        public void BlinkOnTick(object obj, EventArgs e)
        {
            if (LedState == (int)LED_STATE.ON)
                LedState = (int)LED_STATE.OFF;
            else
                LedState = (int)LED_STATE.ON;

            LedDraw();
        }
    }
}
