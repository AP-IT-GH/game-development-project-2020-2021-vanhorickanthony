using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameDevelopment.Core;
using GameDevelopment.Entity;
using GameDevelopment.Collision;

using GameDevelopment.GameState.Interfaces;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace GameDevelopment.GameState.Abstracts
{
    public class Level: RenderableState, ILevel
    {
        protected Camera2D _camera2D;

        protected CollisionManager _collisionManager;

        protected SpriteBatch _spriteBatch;

        
        /**
         * Character data
         */
        protected Hero _player;

        protected List<Tuple<Vector2, Hostile_NPC>> _NPCs;

        protected NPC_Controller.NPC_Controller _npcController;

        /**
         * Tiled map data
         */
        protected ContentManager _contentManager;

        protected String _mapName;
        
        protected TiledMap _map;
        protected TiledMapRenderer _mapRenderer;

        protected List<TiledMapTileLayer> _mapLayers;
        protected List<TiledMapTileLayer> _lethalLayers;
        protected TiledMapTileLayer _collisionLayer;

        public Level(
            Camera2D camera2D,
            CollisionManager collisionManager,
            SpriteBatch spriteBatch,
            ContentManager contentManager,
            String mapName,
            TiledMapRenderer mapRenderer
        )
        {
            _camera2D = camera2D;
            _collisionManager = collisionManager;

            _spriteBatch = spriteBatch;

            _NPCs = new List<Tuple<Vector2, Hostile_NPC>>();

            _contentManager = contentManager; 

            _mapRenderer = mapRenderer;
            _mapName = mapName;

            _mapLayers = new List<TiledMapTileLayer>();
            _lethalLayers = new List<TiledMapTileLayer>();
        }

        public void AddMapLayer(TiledMapTileLayer mapLayer)
        {
            _mapLayers.Add(mapLayer);
        }
        
        public void AddLethalMapLayer(TiledMapTileLayer mapLayer)
        {
            _lethalLayers.Add(mapLayer);
        }

        public void AddNpc(Hostile_NPC npc, Vector2 destination)
        {
            _NPCs.Add(new Tuple<Vector2, Hostile_NPC>(destination, npc));
        }
        
        
        public override void LoadContent()
        {
            
            /*
             * Load map.
             */
            _map = _contentManager.Load<TiledMap>(this._mapName);

            _mapRenderer.LoadMap(_map);
        }

        public override void InitializeGameObjects()
        {
            _npcController = new NPC_Controller.NPC_Controller(_NPCs);

            _camera2D.Track(_player);
            
            _camera2D.HorizontalBounds = new Vector2(0, _map.WidthInPixels);
            _camera2D.VerticalBounds = new Vector2(0, _map.HeightInPixels);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var lethalLayer in _lethalLayers)
            {
                if (_collisionManager.CheckCollision(_player, lethalLayer))
                {
                    _player.Health = 0;
                }
            }

            if (_npcController.CheckCollision(_collisionManager, _player))
            {
                _player.Health = 0;
            }
            
            _player.Update(gameTime);

            _npcController.Update(gameTime);
            
            _camera2D.Update();

            _mapRenderer.Update(gameTime);

            if (_player.Health <= 0)
            {
                Handle(
                    ContextHandler, 
                    new DeathMenu(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                );
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var mapLayer in _mapLayers)
            {
                _mapRenderer.Draw(mapLayer, _camera2D.TransformationMatrix);
            }

            _spriteBatch.Begin(transformMatrix: _camera2D.TransformationMatrix);

            _player.Draw(_spriteBatch);

            if (_collisionManager.CheckBottomCollision(_player, _collisionLayer))
            {
                _player.Gravity = new Vector2(0, 0);
            }
            else
            {
                _player.Gravity = new Vector2(
                    0f,
                    (_player.Gravity.Y != 0f) ? _player.Gravity.Y * GameSettings.Default.GravityVelocity : GameSettings.Default.GravityBase
                );
            }

            if (_collisionManager.CheckCollision(_player, _collisionLayer))
            {
                // Console.WriteLine("[" + gameTime.TotalGameTime + "] Collision detected.");

                _player.Undo();
            }


            _npcController.Draw(_spriteBatch);
            
            _npcController.ApplyGravity(_collisionManager, _collisionLayer);
            _npcController.CheckCollision(_collisionManager, _collisionLayer);
            
            _spriteBatch.End();
        }

        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            Console.WriteLine("Resetting level.");
            
            _contentManager.Unload();
            
            _contentManager.GetGraphicsDevice().Clear(Color.Black);
            
            ctx.State = nextState;
            
            ctx.State.ContextHandler = ctx;


            Console.WriteLine("Load content...");
            ctx.State.LoadContent();

            Console.WriteLine("Init game objects...");
            ctx.State.InitializeGameObjects();

            Console.WriteLine("Done!");
        }
    }
}