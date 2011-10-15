using System;
using GameEngine.Bindings;

namespace GameEngine
{
	public abstract class GameObject
	{
		#region Constants
		
		public const double ChangeFactor = (1.0 / 60);
		
		#endregion
		
		#region Private Fields
		
		private static Vector2 IncrementFactor = new Vector2((float)ChangeFactor);
		protected float originalWidth;
		protected float originalHeight;
		
		#endregion
		
		#region Public Fields
		
		public Vector2 Position;
		public Vector2 Velocity;
		public float Rotation;
		public bool IsVisible;
		public float Scale;
		
		#endregion
		
		#region Public Properties
				
		public float Width 
		{ 
			get { return this.Scale * originalWidth; }
		}
		
		public float Height
		{
			get { return this.Scale * originalHeight; }
		}
		
		#endregion
		
		#region Public Methods
				
		public virtual void Update(double deltaTime)
		{
			if (this.Velocity != Vector2.Zero)
			{
				Vector2 deltaIncrement = new Vector2((float)deltaTime);
				this.Position += (this.Velocity * deltaIncrement / IncrementFactor);
			}
		}
		
		public abstract void Draw();
		
		#endregion
	}
	
	public class Sprite : GameObject
	{
		#region Constants
		
		protected const float DefaultTexLeft = 0.0f;
		protected const float DefaultTexTop = 0.0f;
		protected const float DefaultTexRight = 1.0f;
		protected const float DefaultTexBottom = 1.0f;		
		
		#endregion
		
		#region Public Properties
		
		public TextureInfo Texture { get; private set; }
		public float TexLeft { get; private set; }
		public float TexTop { get; private set; }
		public float TexRight { get; private set; }
		public float TexBottom { get; private set; }		
		public float Depth { get; set; }
		
		#endregion
		
		#region Constructors
		
		public Sprite(string textureName)
		: this(textureName, 0, 0) { }		

		public Sprite(string textureName, float x, float y)
		: this(textureName, new Vector2(x, y), 0.0f) { }		
		
		public Sprite(string textureName, float x, float y, float depth)
		: this(textureName, new Vector2(x, y), depth) { }

		public Sprite(string textureName, Vector2 position, float depth)
		: this(textureName, position, depth, DefaultTexLeft, DefaultTexTop, DefaultTexRight, DefaultTexBottom) { }
		
		public Sprite(string textureName, float x, float y, float depth, float texLeft, float texTop, float texRight, float texBottom)
		: this(textureName, new Vector2(x, y), depth, texLeft, texTop, texRight, texBottom) { }
			
		public Sprite(string textureName, Vector2 position, float depth, float texLeft, float texTop, float texRight, float texBottom)
		{
			this.Texture = Game.Textures.GetTexture (textureName);
			this.Position = position;
			this.Depth = depth; 
			this.Velocity = Vector2.Zero;
			this.Rotation = 0;
			this.Scale = 1;
			this.IsVisible = true;
			
			this.SetTexturePosition(texLeft, texTop, texRight, texBottom);
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Draw()
		{
			if (this.IsVisible)
			{
				Game.Graphics.DrawTexture(
					this.Texture,
					this.Position.X, this.Position.Y, this.Depth, this.Rotation,
					this.TexLeft, this.TexTop, this.TexRight, this.TexBottom,
					this.Width, this.Height);
			}
		}
		
		public void SetTexturePosition(float texLeft, float texTop, float texRight, float texBottom)
		{
			this.TexLeft = texLeft;
			this.TexTop = texTop;
			this.TexRight = texRight;
			this.TexBottom = texBottom;
			
			this.originalWidth = (this.Texture.Width * (texRight - texLeft));
			this.originalHeight = (this.Texture.Height * (texBottom - texTop));
		}
		
		#endregion
	}
	
	public class Animation : Sprite
	{
		#region Constants
		
		public const double FrameRate = (1.0 / 10);
		public const bool AutoPlay = true;
		public const bool Loop = true;
		
		#endregion
		
		#region Private Fields
		
		private uint totalFrames;
		private uint currentFrame;
		private float originalTexLeft;
		private float frameWidth;
		private double elapsedTime;
		
		#endregion
	
		#region Public Properties
		
		public bool IsLoop { get; set; }
		public bool IsPlaying { get; private set; }
		
		#endregion
		
		#region Constructors

		public Animation(string textureName, float x, float y, float texLeft, float texTop, float texRight, float texBottom, uint frames, bool loop, bool autoPlay)
		: this(textureName, new Vector2(x, y), texLeft, texTop, texRight, texBottom, frames, loop, autoPlay) { }		
		
		public Animation(string textureName, float x, float y, float depth, float texLeft, float texTop, float texRight, float texBottom, uint frames, bool loop, bool autoPlay)
		: this(textureName, new Vector2(x, y), depth, texLeft, texTop, texRight, texBottom, frames, loop, autoPlay) { }
		
		public Animation(string textureName, Vector2 position, float texLeft, float texTop, float texRight, float texBottom, uint frames, bool loop, bool autoPlay)
		: this(textureName, position, 0.0f, texLeft, texTop, texRight, texBottom, frames, loop, autoPlay) { }
		
		public Animation(string textureName, Vector2 position, float depth, float texLeft, float texTop, float texRight, float texBottom, uint frames, bool loop, bool autoPlay)
		: base(textureName, position, depth, texLeft, texTop, texRight, texBottom)
		{
			this.totalFrames = frames;
			this.originalTexLeft = texLeft;
			this.frameWidth = (texRight - texLeft);
			this.IsLoop = loop;
			this.elapsedTime = 0;
			
			if(autoPlay)
			{
				this.Play ();
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public void Play()
		{
			this.currentFrame = 0;
			this.IsPlaying = true;
		}

		public void Pause()
		{
			this.IsPlaying = false;
		}
		
		public override void Update(double deltaTime)
		{
			base.Update (deltaTime);
			
			elapsedTime += deltaTime;
			
			if (elapsedTime >= FrameRate)
			{
				elapsedTime = 0;
				Tick();
			}
		}
		
		public void Tick()
		{
			if (this.IsPlaying)
			{
				float newTexLeft = (this.originalTexLeft +(this.frameWidth * this.currentFrame));
				this.SetTexturePosition(newTexLeft, this.TexTop,(newTexLeft + this.frameWidth), this.TexBottom);
				
				if (this.currentFrame < (this.totalFrames - 1))
				{
					this.currentFrame++;
				}
				else if (this.IsLoop)
				{
					this.currentFrame = 0;
				}
				else
				{
					this.IsPlaying = false;
				}
			}
		}
		
		#endregion
	}
}

