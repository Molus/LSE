using System;
using System.Collections.Generic;
using GameEngine.Bindings;
using GameEngine.Managers;

namespace GameEngine
{	
	public class GameScene
	{		
		#region Private Fields
		
		private List<GameObject> elements = new List<GameObject>();
		private List<IGameTrigger>triggers = new List<IGameTrigger>();
		
		#endregion
					
		#region Public Methods
		
		public void AddElement(GameObject element)
		{
			this.elements.Add(element);
		}
		
		public void RemoveElement(GameObject element)
		{
			this.elements.Remove(element);
		}

		public void AddTrigger(IGameTrigger trigger)
		{
			this.triggers.Add(trigger);
		}
		
		public void RemoveTrigger(IGameTrigger trigger)
		{
			this.triggers.Remove(trigger);
		}		
				
		public virtual void Update(double deltaTime)
		{			
			foreach(GameObject element in elements)
			{
				element.Update(deltaTime);
			}
			
			foreach (IGameTrigger trigger in this.triggers)
			{
				trigger.Update(deltaTime);
			}
		}
		
		public virtual void Draw()
		{
			foreach(GameObject element in elements)
			{
				element.Draw();
			}
		}
		
		#endregion
	}
}

