using System;
using System.Collections.Generic;

namespace GameEngine
{
	public delegate void TriggerTarget();
	
	public interface IGameTrigger
	{
		void Update(double deltaTime);
		void Execute();
	}
	
	public class IntervalTrigger : IGameTrigger
	{
		#region Public Fields
		
		public TimeSpan elapsedTime;
		public TimeSpan interval;
		public TriggerTarget target;
		
		#endregion
		
		#region Constructor
		
		public IntervalTrigger(TimeSpan interval, TriggerTarget target)
		{
			this.elapsedTime = TimeSpan.Zero;
			this.interval = interval;
			this.target = target;
		}
		
		#endregion
		
		#region Public Methods
		
		public void Update(double deltaTime)
		{
			this.elapsedTime += TimeSpan.FromMilliseconds(deltaTime * 1000);
			if (1 > this.interval.CompareTo(this.elapsedTime))
			{
				this.elapsedTime = TimeSpan.Zero;
				this.Execute ();
			}
		}
		
		public void Execute()
		{
			this.target();
		}
		
		#endregion
	}	
	
}

