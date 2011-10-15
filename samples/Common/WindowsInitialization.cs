using System;
using System.Collections.Generic;
using GameEngine;

namespace GameEngine.Samples
{
	class Program
	{
		public static void Main(string[] args)
		{
			const int viewportWidth = 800;
			const int viewportHeight = 600;
			const bool isFullScreen = false;
			
			Game.Initialize(viewportWidth, viewportHeight, isFullScreen);
			Game.Scenes.Add(new GameScene(), "sample");
			Game.Start();
			Game.Cleanup();
		}
	}
}
