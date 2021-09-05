using GameDevelopment.GameState.Abstracts;

namespace GameDevelopment.GameState.Interfaces
{
	public interface IState
	{
		public void Handle(ContextHandler ctx, RenderableState nextState);
	}
}