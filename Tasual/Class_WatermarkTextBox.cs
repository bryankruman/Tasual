using System.Windows.Forms;
using System.Drawing;

namespace Tasual
{
	// A textbox that supports a watermark hint
	public class WatermarkTextBox : TextBox
	{
		// The text that will be presented as the watermark hint
		private string _WatermarkText = "Type here";

		// Gets or Sets the text that will be presented as the watermark hint
		public string WatermarkText
		{
			get { return _WatermarkText; }
			set { _WatermarkText = value; }
		}

		// Whether watermark effect is enabled or not
		private bool _WatermarkActive = true;

		// Gets or Sets whether watermark effect is enabled or not
		public bool WatermarkActive
		{
			get { return _WatermarkActive; }
			set { _WatermarkActive = value; }
		}

		// Create a new TextBox that supports watermark hint
		public WatermarkTextBox()
		{
			_WatermarkActive = true;
			Text = _WatermarkText;
			ForeColor = Color.Gray;

			GotFocus += (source, Args) =>
			{
				RemoveWatermark();
			};

			LostFocus += (source, Args) =>
			{
				ApplyWatermark();
			};

		}

		// Remove watermark from the textbox
		public void RemoveWatermark()
		{
			if (_WatermarkActivArgs)
			{
				_WatermarkActive = false;
				Text = "";
				ForeColor = Color.Black;
			}
		}

		// Applywatermark immediately
		public void ApplyWatermark()
		{
			if (!_WatermarkActive && string.IsNullOrEmpty(Text)
				|| ForeColor == Color.Gray)
			{
				_WatermarkActive = true;
				Text = _WatermarkText;
				ForeColor = Color.Gray;
			}
		}

		// Apply watermark to the textbox
		public void ApplyWatermark(string newText)
		{
			WatermarkText = newText;
			ApplyWatermark();
		}
	}
}
