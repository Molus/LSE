using System;
using System.Collections.Generic;

namespace GameEngine.Managers
{
	public class SceneManager
	{
		#region Private Fields
		
		private Dictionary<string, GameScene> scenes;
		private Stack<string> renderStack;

		#endregion
		
		#region Constructor
		
		internal SceneManager()
		{
			this.scenes = new Dictionary<string, GameScene>();
			this.renderStack = new Stack<string>();
		}
		
		#endregion
		
		#region Public Properties
		
		public bool HasActiveScene 
		{
			get
			{
				return (this.renderStack.Count > 0);
			}
		}
		
		public GameScene ActiveScene
		{
			get
			{
				return this.scenes[this.renderStack.Peek()];
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public void Add(GameScene scene, string name)
		{
			this.scenes.Add(name, scene);
			
			if(!this.HasActiveScene)
			{
				this.Set(name);	
			}
		}
		
		public void Remove(string name)
		{
			this.scenes.Remove(name);
		}
		
		public void Push(string name)
		{
			this.renderStack.Push(name);
		}
		
		public void Pop()
		{
			this.renderStack.Pop();
		}
		
		public void Set(string name)
		{
			if(this.HasActiveScene)
			{
				this.renderStack.Pop();
			}
			this.renderStack.Push(name);
		}
		
		public void Update(double deltaTime)
		{
			if(HasActiveScene)
			{
				this.ActiveScene.Update(deltaTime);
			}
		}
		
		public void Draw()
		{
			if(HasActiveScene)
			{
				this.ActiveScene.Draw();
			}
		}
		
		#endregion
	}
}