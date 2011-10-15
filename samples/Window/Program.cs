using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Managers;
using GameEngine.Bindings;

namespace SampleGame
{
	class Sample
	{
		public static void Main(string[] args)
		{
			const int viewportWidth = 800;
			const int viewportHeight = 600;
			const bool isFullScreen = false;
			
			Game.Initialize(viewportWidth, viewportHeight, isFullScreen);	
			
			Game.Textures.LoadFromFile("Textures/clown.png", "clown");
			Game.Textures.LoadFromFile("Textures/star.png", "star");
			Game.Textures.LoadFromFile("Textures/background.png", "background");

			Game.Scenes.Add(new Scene(), "sample");
			
			Game.Start();
			Game.Cleanup();
		}
	}
	
	public class Scene : GameScene
	{
		private Queue<BouncingObject> elements;
		private Random randomizer;
		
		public Scene() : base()
		{
			this.elements = new Queue<BouncingObject>();
			this.randomizer = new Random();
			
			this.AddElement(new Sprite("background", 0, 0));
			
			this.AddTrigger(new IntervalTrigger(TimeSpan.FromSeconds(2), new TriggerTarget(Spawn)));
			this.AddTrigger(new IntervalTrigger(TimeSpan.FromSeconds(20), new TriggerTarget(Destroy)));
		}
			
		public void Spawn()
		{
			BouncingObject newElement = new BouncingObject(randomizer.Next(10) > 5 ? "star" : "clown");
			this.AddElement(newElement);
			this.elements.Enqueue(newElement);
		}
		
		public void Destroy()
		{
			this.RemoveElement(this.elements.Dequeue());
		}		
	}
		
	public class BouncingObject : Sprite
	{
		private Random randomizer;
		private float rotationSpeed;
		
		public BouncingObject(string textureName) : base(textureName)
		{
			this.randomizer = new Random();
			this.rotationSpeed = (0.5f + (2.0f * (float)randomizer.NextDouble()));
			this.Scale = (0.5f + (1.5f * (float)randomizer.NextDouble()));
			this.SetRandomPosition();
			this.SetRandomVelocity();
		}
		
		public override void Update(double deltaTime)
		{
			base.Update(deltaTime);
			this.BounceInEdges();
			
			this.Rotation += (float)(rotationSpeed * deltaTime / GameObject.ChangeFactor);
		}
		
		private void SetRandomPosition()
		{
			this.Position = new Vector2(
				randomizer.Next(Game.Graphics.ViewportWidth - (int)this.Width),
				randomizer.Next(Game.Graphics.ViewportHeight - (int)this.Height)
			);
		}
		
		private void SetRandomVelocity()
		{
			this.Velocity = new Vector2(
				0.5f + randomizer.Next(-4, 4) + (this.Width * 0.01f),
				0.5f + randomizer.Next(-4, 4) + (this.Height * 0.01f)
			);
		}
		
		private void BounceInEdges()
		{
			if ((this.Position.X < 0) || ((this.Position.X + this.Width) > Game.Graphics.ViewportWidth))
			{
				this.Velocity.X = -this.Velocity.X;
				this.Position.X += (this.Velocity.X * 2);
			}
			
			if ((this.Position.Y < 0) || ((this.Position.Y + this.Height) > Game.Graphics.ViewportHeight))
			{
				this.Velocity.Y = -this.Velocity.Y; 
				this.Position.Y += (this.Velocity.Y * 2);
			}
		}	
	}
}
