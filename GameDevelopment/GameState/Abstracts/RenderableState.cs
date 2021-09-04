using GameDevelopment.GameState.Interfaces;
using Microsoft.Xna.Framework;

namespace GameDevelopment.GameState.Abstracts
{
	public abstract class RenderableState: IRenderable, IState
	{
		public ContextHandler ContextHandler { get; set; }
		
		public abstract void LoadContent();
		public abstract void InitializeGameObjects();
		public abstract void Update(GameTime gameTime);
		public abstract void Draw(GameTime gameTime);
		public abstract void Handle(ContextHandler ctx, RenderableState nextState);
	}
}