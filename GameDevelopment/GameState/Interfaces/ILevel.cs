using System;
using GameDevelopment.Core;
using GameDevelopment.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;

namespace GameDevelopment.GameState.Interfaces
{
    public interface ILevel
    {
        public void AddMapLayer(TiledMapTileLayer mapLayer);

        public void AddNpc(Hostile_NPC npc, Vector2 destination);
    }
}