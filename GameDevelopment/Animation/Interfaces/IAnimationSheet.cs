using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation.Interfaces
{
    public interface IAnimationSheet
    {
        public Texture2D GetTexture();
        
        public int GetSpriteWidth();

        public int GetSpriteHeight();
    }
}