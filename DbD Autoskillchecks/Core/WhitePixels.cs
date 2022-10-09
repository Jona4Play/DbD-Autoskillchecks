using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
    internal class WhitePixels
    {
		private int LastFrameWhitePixel;

		public int LastFrameWhitePixels
		{
			get { return LastFrameWhitePixel; }
			set { LastFrameWhitePixel = value; }
		}

	}
}
