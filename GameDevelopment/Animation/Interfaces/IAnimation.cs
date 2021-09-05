using GameDevelopment.Animation.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation.Interfaces
{
    public interface IAnimation
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, IDirection spriteDirection);
    }
}
