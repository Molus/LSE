using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameEngine
{	
	public struct TextureInfo
	{
		public object Reference;
		public uint Width;
		public uint Height;
		
		public TextureInfo(object reference, uint width, uint height)
		{
			this.Reference = reference;
			this.Width = width;
			this.Height = height;
		}
	}	
	
	internal class TextureLoader
	{	
		#region Public Methods
		
		public static TextureInfo LoadFromFile(string path)
		{
			Bindings.Texture2D texture = Bindings.LoadTexture2DFromFile(path);
			return new TextureInfo(texture.Id, texture.Width, texture.Height);
		}
		
		public static void Unload(TextureInfo texture)
		{
			Bindings.UnloadTexture((uint)texture.Reference);
		}
		
		#endregion
		
		#region P/Invoke
		
		private class Bindings
		{			
			[StructLayout(LayoutKind.Sequential)]
			public struct Texture2D
			{
				public uint Id;
				public uint Width;
				public uint Height;
			}
			
			[DllImport("libgamebinds.so")]
			public static extern Texture2D LoadTexture2DFromMemory(byte[] buffer, uint size);
			
			[DllImport("libgamebinds.so")]
			public static extern Texture2D LoadTexture2DFromFile(string path);
			
			[DllImport("libgamebinds.so")]
			public static extern void UnloadTexture(uint textureId);
		}
		
		#endregion
	}
}