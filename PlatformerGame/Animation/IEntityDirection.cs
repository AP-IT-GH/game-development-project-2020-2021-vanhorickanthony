using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestGame.Interfaces
{
    public interface IEntityDirection
    {
        SpriteEffects Direction { get; set; }

        void Update(Vector2 newDirection);

    }
}
