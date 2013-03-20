using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WirelessProject
{
    class LEDPictureBox : PictureBox 
	{
		// this maps to PictureBox created visually on the design form
		private PictureBox thePbox;
		private LEDBitmap theLED;
		private int m_nLedMode;

		// minimum constructor, create default
		public LEDPictureBox(System.Windows.Forms.Control parent,
			                 PictureBox pbox, Bitmap image)
		{
			thePbox = pbox;
			base.Parent = parent;
			theLED = new LEDBitmap(image);
			theLED.m_nLedColor = LEDBitmap.LED_COLOR.RED;
			theLED.m_nLedMode  = LEDBitmap.LED_STATE.OFF;
			theLED.m_nLedShape = LEDBitmap.LED_TYPE.ROUND;
			theLED.BlinkRate   = 0.0f;
			// register the handler with the LED so it can be called
			// when the LED is re-drawn. Must register before .LedDraw() since
			// that fires the event.
			theLED.LedChanged += new ChangedLEDEventHandler(BlinkDemoHandler);
			if(theLED.ledimage == null)
				pbox.Dispose();
			else
				theLED.LedDraw();
		}

		public LEDPictureBox(System.Windows.Forms.Control parent, PictureBox pbox, 
			Bitmap image,
			LEDBitmap.LED_COLOR color,
			LEDBitmap.LED_STATE state,
			LEDBitmap.LED_TYPE type)
		{
			thePbox = pbox;

			base.Parent = parent;

			theLED = new LEDBitmap(image);
			theLED.m_nLedColor = color;
			theLED.m_nLedMode  = state;
			theLED.m_nLedShape = type;
			theLED.BlinkRate   = 0.0f;
			// register the handler with the LED so it can be called
			// when the LED is re-drawn
			// Must register before .LedDraw() since
			// that fires the event.
		//	theLED.LedChanged += new ChangedLEDEventHandler(BlinkDemoHandler);
			if(theLED.ledimage == null)
				pbox.Dispose();
			else
				theLED.LedDraw();

		}
        public void ChangeColor(Bitmap newImage)
        {
            theLED.ledimage = newImage;
        }

		public void BlinkDemoHandler(object obj, LEDEventArgs e)
		{
            LEDBitmap bm = (LEDBitmap)obj;
				// copy the current LED image to the picturebox
				thePbox.Image = (Image)bm.displaybitmap;
			//	Rectangle rect = new Rectangle(10,10,bm.ledimage.Width,bm.ledimage.Height);
			//	Invalidate(rect);
		}

		public void LedOn()
		{
			theLED.LedOn();
		}
		public void LedDisable()
		{
			theLED.LedDisable();
		}
		public void LedOff()
		{
			theLED.LedOff();
		}
		public int  LedState
		{
			get {return (int)theLED.m_nLedMode;}
			set {m_nLedMode = (int)value;}
		}
		public float  BlinkRate
		{
			get {return theLED.m_nLedBlinkRate;}
			set {theLED.m_nLedBlinkRate = value;}
		}
		public void BlinkEnable()
		{
			theLED.BlinkEnable();
		}

		public void BlinkDisable()
		{
			theLED.BlinkDisable();
		}
		
	}
}

