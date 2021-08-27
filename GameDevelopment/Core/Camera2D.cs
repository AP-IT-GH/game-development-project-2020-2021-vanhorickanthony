using System;

using GameDevelopment.Entity.Abstracts;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Core
{
    class Camera2D
    {
        public Matrix TransformationMatrix { get; private set; }

        private BaseEntity _entity;
        private Viewport _viewport;
        private Vector2 _origin;

        public Vector2 HorizontalBounds { get; set; }
        public Vector2 VerticalBounds { get; set; }

        public Camera2D(Viewport viewport)
        {
            this._viewport = viewport;
            
            this._origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        public void TrackEntity(BaseEntity entity)
        {
            this._entity = entity;
        }

        public void Update()
        {
            TransformationMatrix = Matrix.CreateTranslation(
                new Vector3(
                    -_applyBounds((int) _entity.Position.X, _viewport.Width / 2, (int) HorizontalBounds.Y - (_viewport.Width / 2)) + (_viewport.Width / 2),
                    -_applyBounds( (int) _entity.Position.Y, _viewport.Height / 2, (int) VerticalBounds.Y - (_viewport.Height / 2)) + (_viewport.Height / 2), 
                    0)
                );
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(
                new Vector3(
                    -_applyBounds((int) (_entity.Position.X), (int) ((_viewport.Width / 2) * parallax.X), (int) (HorizontalBounds.Y - (_viewport.Width / 2)) + (_viewport.Width / 2)),
                    -_applyBounds( (int) (_entity.Position.Y * parallax.Y), _viewport.Height / 2, (int) VerticalBounds.Y - (_viewport.Height / 2)) + (_viewport.Height / 2), 
                    0)
            );
        }

        private int _applyBounds(int value, int boundOne, int boundTwo)
        {
            int leftBound = boundOne < boundTwo ? boundOne : boundTwo;
            int rightBound = boundTwo > boundOne ? boundTwo : boundOne;

            return Math.Clamp(value, leftBound, rightBound);
        }
    }
}
