using System;
using System.Runtime.InteropServices;

namespace GameEngine
{
	public class GraphicsDevice
	{
		#region Private Fields
		
		private bool isInitialized;
		
		#endregion
		
		#region Public Properties
		
		public bool IsWindowOpened
		{
			get 
			{ 
				if (!this.isInitialized)
				{
					return false;
				}
				
				return Bindings.GetIsWindowOpened(); 
			}
		}

		public int ViewportWidth { get; private set; }
		public int ViewportHeight { get; private set; }
		
		#endregion
		
		#region Public Methods
		
		public bool Initialize(int width, int height, bool fullscreen)
		{		
			this.ViewportWidth = width;
			this.ViewportHeight = height;
			this.isInitialized = Bindings.InitializeViewport(width, height, fullscreen);
			
			return this.isInitialized;
		}
				
		public void Cleanup()
		{
			Bindings.ReleaseViewport();
			
			this.isInitialized = false;
		}
		
		#endregion
		
		#region Internal Methods
		
		internal void DrawTexture(TextureInfo texture, float xPos, float yPos, float zPos, float rotation,
			float texLeft, float texTop, float texRight, float texBottom, float width, float height)
		{
			Bindings.DrawTexture2D(
				(uint)texture.Reference, 
				xPos, yPos, zPos, rotation,
				texLeft, texTop, texRight, texBottom, 
				width, height);
		}
		
		internal void CleanView()
		{
			Bindings.CleanBackBuffer();
		}
		
		internal void SwapView()
		{
			Bindings.SwapBackBuffer();
		}
		
		#endregion
		
		#region P/Invoke
		
		private class Bindings
		{
			[DllImport("libgamebinds.so")]
			public static extern bool InitializeViewport(int width, int height, bool fullscreen);
			
			[DllImport("libgamebinds.so")]
			public static extern void DrawTexture2D(uint textureId, float xPos, float yPos, float zPos, float rotation,
				float texLeft, float texTop, float texRight, float texBottom, float width, float height);
			
			[DllImport("libgamebinds.so")]
			public static extern void CleanBackBuffer();
			
			[DllImport("libgamebinds.so")]
			public static extern void SwapBackBuffer();
			
			[DllImport("libgamebinds.so")]
			public static extern bool GetIsWindowOpened();
			
			[DllImport("libgamebinds.so")]
			public static extern void ReleaseViewport();
		}
		
		#endregion
	}
}
