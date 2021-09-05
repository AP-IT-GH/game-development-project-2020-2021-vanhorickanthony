using GameDevelopment.Collision.Interfaces;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace GameDevelopment.Collision
{
    public class CollisionManager
    {
        TiledMapTile? collisionTile;
        
        public Vector2 Gravity { get; set; }

        public bool CheckCollision(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.Intersects(rect2))
                return true;

            return false;
        }


        public bool CheckCollision(ICollision collider, TiledMapTileLayer collisionLayer)
        {
            for (int x = collider.CollisionRectangle.X; x < collider.CollisionRectangle.X + collider.CollisionRectangle.Width; x++)
            {
                for (int y = collider.CollisionRectangle.Y; y < collider.CollisionRectangle.Y + collider.CollisionRectangle.Height; y++)
                {
                    if (collisionLayer.TryGetTile(
                        (ushort)(x / collisionLayer.TileWidth),
                        (ushort)(y / collisionLayer.TileHeight),
                        out collisionTile
                        )
                    )
                    {
                        if (collisionTile.Value.IsBlank)
                        {
                            continue;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return false;

        }
        
        public bool CheckCollision(ICollision collider_1, ICollision collider_2)
        {
            return CheckCollision(collider_1.CollisionRectangle, collider_2.CollisionRectangle);

        }
        
        public bool CheckBottomCollision(ICollision collider, TiledMapTileLayer collisionLayer)
        {
            for (int x = collider.CollisionRectangle.X; x < collider.CollisionRectangle.X + collider.CollisionRectangle.Width; x++)
            {
                if (collisionLayer.TryGetTile(
                        (ushort)(x / collisionLayer.TileWidth),
                        (ushort)( (collider.CollisionRectangle.Y + collider.CollisionRectangle.Height) / collisionLayer.TileHeight),
                        out collisionTile
                    )
                )
                {
                    if (collisionTile.Value.IsBlank)
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    continue;
                }
            }

            return false;

        }
    }
}
