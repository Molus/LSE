using System;
using System.Runtime.InteropServices;

namespace GameEngine.Graphics
{
	public class GraphicsDevice
	{
		#region Public Properties
		
		public bool IsWindowOpened
		{
			get 
			{ 
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
			
			return Bindings.InitializeViewport(width, height, fullscreen);
		}
		
		public void DrawTexture(TextureInfo texture, float xPos, float yPos, float zPos, float rotation,
			float texLeft, float texTop, float texRight, float texBottom, float width, float height)
		{
			Bindings.DrawTexture2D(
				(uint)texture.Reference, 
				xPos, yPos, zPos, rotation,
				texLeft, texTop, texRight, texBottom, 
				width, height);
		}
		
		public void CleanView()
		{
			Bindings.CleanBackBuffer();
		}
		
		public void SwapView()
		{
			Bindings.SwapBackBuffer();
		}
		
		public void Cleanup()
		{
			Bindings.ReleaseViewport();
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

