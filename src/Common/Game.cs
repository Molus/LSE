using System;

namespace GameEngine
{
	public class Game
	{
		#region Public Properties
		
		public static GraphicsDevice Graphics { get; private set; }	
		public static SceneManager Scenes { get; private set; }
		public static TextureManager Textures { get; private set; }
		public static InputManager Inputs { get; private set; }

		#endregion
		
		#region Private Fields
		
		private static bool IsInitialized;
		private static bool IsRunning;
				
		#endregion
		
		#region Public Methods
		
		public static void Initialize(int viewportWidth, int viewportHeight, bool fullscreen)
		{
			Game.Graphics = new GraphicsDevice();
			Game.Scenes = new SceneManager();
			Game.Textures = new TextureManager();
			Game.Inputs = new InputManager();
			
			Game.IsInitialized = Graphics.Initialize(viewportWidth, viewportHeight, fullscreen);		
			
			if(!Game.IsInitialized)
			{
				throw new InvalidOperationException("Error initializing viewport");	
			}
		}
			
		public static void Start()
		{
			if(!Game.IsInitialized)
			{
				throw new InvalidOperationException("Could not start, game is not initialized");	
			}
			
			Game.IsRunning = true;
			
			double previousTime = 0;
			double currentTime = GameTimer.GetElapsedTime();
						
			while(Graphics.IsWindowOpened && Game.IsRunning)
			{				
				previousTime = currentTime;
				currentTime = GameTimer.GetElapsedTime();
				
				Graphics.CleanView();
				Inputs.Update();
				Scenes.Update(currentTime - previousTime);
				Scenes.Draw();
				Graphics.SwapView();
			}
		}
		
		public static void Quit()
		{
			Game.IsRunning = false;
		}
		
		public static void Cleanup()
		{
			if(Game.IsInitialized)
			{
				Textures.Cleanup();
				Graphics.Cleanup();
			}
			
			Game.IsInitialized = false;
		}
		
		#endregion
	}
}

