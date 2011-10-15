using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameEngine.Bindings;

namespace GameEngine.Managers
{
	public class TextureManager
	{	
		#region Private Fields
		
		private Dictionary<string, TextureInfo> loadedTextures;
		
		#endregion
		
		#region Constructor
		
		internal TextureManager()
		{
			this.loadedTextures = new Dictionary<string, TextureInfo>();
		}
		
		#endregion
		
		#region Public Methods
		
		public void LoadFromFile(string path, string name)
		{
			string texturePath = path;
			if (!Path.IsPathRooted(texturePath))
			{
				string workingDirectory = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
				texturePath = Path.Combine(workingDirectory, texturePath);
			}
			
			this.loadedTextures.Add(name, TextureLoader.LoadFromFile(texturePath));
		}
		
		public void Unload(string name)
		{
			TextureLoader.Unload(this.loadedTextures[name]);
			this.loadedTextures.Remove(name);
		}
		
		public void Cleanup()
		{
			foreach(string name in this.loadedTextures.Keys)
			{
				TextureLoader.Unload(this.loadedTextures[name]);
			}
			
			this.loadedTextures.Clear();
		}
		
		public TextureInfo GetTexture(string name)
		{
			return this.loadedTextures[name];
		}
		
		#endregion
	}
}

