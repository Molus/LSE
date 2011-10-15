using System;
using System.Runtime.InteropServices;

namespace GameEngine
{
	internal class GameTimer
	{
		#region Public Methods
		
		public static double GetElapsedTime()
		{
			return Bindings.GetElapsedTime();
		}
		
		#endregion
		
		#region P/Invoke
		
		private class Bindings
		{
			[DllImport("libgamebinds.so")]
			public static extern double GetElapsedTime();
		}
		
		#endregion
	}
}

