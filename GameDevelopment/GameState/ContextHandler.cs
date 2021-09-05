using GameDevelopment.GameState.Abstracts;
using GameDevelopment.GameState.Interfaces;

namespace GameDevelopment.GameState
{
	public class ContextHandler
	{
		private RenderableState _currentState;

		public ContextHandler()
		{
			this.State = null;
		}
		
		public ContextHandler(RenderableState initialState)
		{
			this.State = initialState;
		}
		
		public RenderableState State
		{
			get => _currentState;
			set
			{
				_currentState = value;
			}
		}
	}
}