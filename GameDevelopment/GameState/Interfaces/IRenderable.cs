using Microsoft.Xna.Framework;

namespace GameDevelopment.GameState.Interfaces
{
	public interface IRenderable
	{
		public void LoadContent();

		public void InitializeGameObjects();

		public void Update(GameTime gameTime);

		public void Draw(GameTime gameTime);
	}
}