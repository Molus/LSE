using System;
using System.Collections.Generic;
using GameEngine.Bindings;

namespace GameEngine.Managers
{
	public class InputManager
	{	
		#region Private Fields
		
		private KeyboardState currentKeyState;
		private KeyboardState previousKeyState;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		
		#endregion		
		
		#region Constructor
		
		internal InputManager()
		{
			this.currentKeyState = KeyboardState.Empty;
			this.previousKeyState = KeyboardState.Empty;
			this.currentMouseState = MouseState.Empty;
			this.previousMouseState = MouseState.Empty;
		}
		
		#endregion
		
		#region Public Properties
		
		public int MouseX
		{
			get { return this.currentMouseState.X; }
		}
		
		public int MouseY
		{
			get { return this.currentMouseState.Y; }
		}
		
		#endregion
		
		#region Public Methods
		
		internal void Update()
		{
			this.previousKeyState = this.currentKeyState;
			this.currentKeyState = new KeyboardState();
			
			this.previousMouseState = this.currentMouseState;
			this.currentMouseState = new MouseState();
		}
		
		public bool IsKeyPressed(Keys key)
		{
			return this.currentKeyState.IsKeyPressed(key);
		}
		
		public bool WasKeyPressed(Keys key)
		{
			return (this.currentKeyState.IsKeyPressed(key) && !this.previousKeyState.IsKeyPressed(key));
		}
		
		public bool WasKeyReleased(Keys key)
		{
			return (!this.currentKeyState.IsKeyPressed(key) && this.previousKeyState.IsKeyPressed(key));
		}		
				
		public bool IsMouseButtonPressed(MouseButtons button)
		{
			return this.currentMouseState.IsMouseButtonPressed(button);
		}
		
		public bool WasMouseButtonPressed(MouseButtons button)
		{
			return (this.currentMouseState.IsMouseButtonPressed(button) && !this.previousMouseState.IsMouseButtonPressed(button));
		}
		
		public bool WasMouseButtonReleased(MouseButtons button)
		{
			return (!this.currentMouseState.IsMouseButtonPressed(button) && this.previousMouseState.IsMouseButtonPressed(button));
		}		
		
		#endregion
	}
	
	internal class KeyboardState
	{
		#region Fields
		
		public static KeyboardState Empty = new KeyboardState(new HashSet<Keys>()); 
		private HashSet<Keys> pressedKeys;
		
		#endregion
		
		#region Constructors
		
		public KeyboardState()
		{
			this.pressedKeys = GameInputs.GetPressedKeys();
		}
		
		private KeyboardState(HashSet<Keys> pressedKeys)
		{
			this.pressedKeys = pressedKeys;
		}		
		
		#endregion
		
		#region Methods
		
		public bool IsKeyPressed(Keys key)
		{
			return pressedKeys.Contains(key);
		}
		
        public override bool Equals(object obj)
        {
            return ((obj is KeyboardState) ? false : (KeyboardState)obj == this);
        }

        public override int GetHashCode()
        {
            return pressedKeys.GetHashCode();
        }
		
        public static bool operator != (KeyboardState a, KeyboardState b)
        {
            return !(a == b);
        }

        public static bool operator == (KeyboardState a, KeyboardState b)
        {
            return (a.pressedKeys == b.pressedKeys);
        }
		
		#endregion
	}
	
	internal class MouseState
	{
		#region Fields
		
		public static MouseState Empty = new MouseState(new HashSet<MouseButtons>(), 0, 0);
		private HashSet<MouseButtons> pressedButtons;
		
		#endregion
		
		#region Properties
		
		public int X { get; private set; }
		public int Y { get; private set; }
		
		#endregion
		
		#region Constructors
		
		private MouseState(HashSet<MouseButtons> pressedButtons, int x, int y)
		{
			this.pressedButtons = pressedButtons;
			this.X = x;
			this.Y = y;
		}
		
		public MouseState()
		{
			int x, y;
			GameInputs.GetMousePosition(out x, out y);
			this.pressedButtons = GameInputs.GetPressedMouseButtons();			
			this.X = x;
			this.Y = y;
		}
		
		#endregion
		
		#region Methods
		
		public bool IsMouseButtonPressed(MouseButtons button)
		{
			return this.pressedButtons.Contains(button);
		}
		
        public override bool Equals(object obj)
        {
            return ((obj is MouseState) ? false : (MouseState)obj == this);
        }

        public override int GetHashCode()
        {
            return pressedButtons.GetHashCode();
        }
		
        public static bool operator != (MouseState a, MouseState b)
        {
            return !(a == b);
        }

        public static bool operator == (MouseState a, MouseState b)
        {
            return (a.pressedButtons == b.pressedButtons);
        }
		
		#endregion
	}
}

