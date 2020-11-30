using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Animation
{
    public interface IEntityAnimation
    {
        AnimationHandler Animatie { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, IEntityDirection spriteDirection);
    }
}
