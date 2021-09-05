using Microsoft.Xna.Framework;

using GameDevelopment.Entity;

using MonoGame.Extended.Tiled;

namespace GameDevelopment.GameState.Interfaces
{
    public interface ILevel
    {
        public void SetCollisionLayer(TiledMapTileLayer mapLayer);

        public void SetObjectiveLayer(TiledMapTileLayer mapLayer);
        
        public void AddMapLayer(TiledMapTileLayer mapLayer);

        public void AddLethalMapLayer(TiledMapTileLayer mapLayer);

        public void AddNpc(Hostile_NPC npc, Vector2 destination);
    }
}