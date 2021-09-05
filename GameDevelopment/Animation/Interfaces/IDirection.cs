using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDevelopment.Animation.Interfaces
{
    public interface IDirection
    {
        SpriteEffects CurrentDirection { get; set; }

        void Update(Vector2 newDirection);

    }
}
