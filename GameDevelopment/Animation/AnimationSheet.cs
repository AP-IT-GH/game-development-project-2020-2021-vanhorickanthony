using GameDevelopment.Animation.Interfaces;

using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation
{
    public class AnimationSheet: IAnimationSheet
    {
        private readonly Texture2D _texture;

        private readonly int _spriteWidth;
        private readonly int _spriteHeight;

        public AnimationSheet(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            this._texture = texture;

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;
        }

        public Texture2D GetTexture()
        {
            return this._texture;
        }

        public int GetSpriteWidth()
        {
            return this._spriteWidth;
        }

        public int GetSpriteHeight()
        {
            return this._spriteHeight;
        }
    }
}