using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameEngine
{
	internal class GameInputs
	{
		#region Public Methods
				
		public static HashSet<Keys> GetPressedKeys()
		{
			HashSet<Keys> pressedKeys = new HashSet<Keys>();
			
			foreach(Keys key in Enum.GetValues(typeof(Keys)))
			{
				if(Bindings.GetIsKeyPressed((int)key))
				{
					pressedKeys.Add(key);
				}
			}
			
			return pressedKeys;
		}
		
		public static void GetMousePosition(out int x, out int y)
		{
			Bindings.GetMousePosition(out x, out y);
		}
		
		public static HashSet<MouseButtons> GetPressedMouseButtons()
		{
			HashSet<MouseButtons> pressedButtons = new HashSet<MouseButtons>();
			
			foreach(MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
			{
				if(Bindings.GetIsMouseButtonPressed((int)button))
				{
					pressedButtons.Add(button);
				}
			}
			
			return pressedButtons;
		}
		
		#endregion
		
		#region P/Invoke
		
		private class Bindings
		{
			[DllImport("libgamebinds.so")]
			public static extern bool GetIsKeyPressed(int key);

			[DllImport("libgamebinds.so")]
			public static extern bool GetIsMouseButtonPressed(int button);
			
			[DllImport("libgamebinds.so")]
			public static extern void GetMousePosition(out int x, out int y);			
		}		
		
		#endregion
	}
	
	/// <summary>
	/// Key mapping for glfw
	/// </summary>
	public enum Keys
    {
        Space = 32,

        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,
		
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,		
		
		Escape = 	(256 + 1),
		F1 =		(256 + 2),
        F2 =		(256 + 3),
        F3 =		(256 + 4),
        F4 =		(256 + 5),
        F5 =		(256 + 6),
        F6 =		(256 + 7),
        F7 =		(256 + 8),
        F8 =		(256 + 9),
        F9 =		(256 + 10),
        F10 =		(256 + 11),
        F11 =		(256 + 12),
        F12 =		(256 + 13),
        F13 =		(256 + 14),
        F14 =		(256 + 15),
        F15 =		(256 + 16),
        F16 =		(256 + 17),
        F17 =		(256 + 18),
        F18 =		(256 + 19),
        F19 =		(256 + 20),
        F20 =		(256 + 21),
        F21 =		(256 + 22),
        F22 =		(256 + 23),
        F23 =		(256 + 24),
        F24 =		(256 + 25),
		
        Up =		(256 + 27),
        Down =		(256 + 28),		
		Left =		(256 + 29),
		Right =		(256 + 30),		

        LeftShift =		(256 + 31),
        RightShift =	(256 + 32),
        LeftControl =	(256 + 33),
        RightControl =	(256 + 34),
        LeftAlt =		(256 + 35),
        RightAlt =		(256 + 36),		
		
		Tab = 		(256 + 37),
        Enter = 	(256 + 38),        
        Insert = 	(256 + 40),
        Delete =	(256 + 41),
		PageUp = 	(256 + 42),
        PageDown = 	(256 + 43),
        End = 		(256 + 45),
        Home = 		(256 + 44),

		NumPad0 =	(256 + 46),
        NumPad1 =	(256 + 47),
        NumPad2 =	(256 + 48),
        NumPad3 = 	(256 + 49),
        NumPad4 =	(256 + 50),
        NumPad5 =	(256 + 51),
        NumPad6 = 	(256 + 52),
        NumPad7 =	(256 + 53),
        NumPad8 =	(256 + 54),
        NumPad9 =	(256 + 55),
        
        Divide =	(256 + 56),
		Multiply =	(256 + 57),
        Subtract =	(256 + 58),
		Add =		(256 + 59),
        Decimal =	(256 + 60),              
    }	

	/// <summary>
	/// Mouse button mapping for glfw
	/// </summary>
	public enum MouseButtons
	{
		LeftButton		= 0,
		RightButton		= 1,
		MiddleButton	= 2,
	}
}

