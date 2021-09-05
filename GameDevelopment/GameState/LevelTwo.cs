using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GameDevelopment.Core;
using GameDevelopment.Entity;
using GameDevelopment.Collision;
using GameDevelopment.Input;

using GameDevelopment.Animation;
using GameDevelopment.Animation.Interfaces;

using GameDevelopment.GameState.Abstracts;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace GameDevelopment.GameState
{
    public class LevelTwo: Level
    {
        private Song _ambientBackground;

        private IAnimationSheet _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet;

        private Vector2 _spawnPoint;

        public LevelTwo(
            Camera2D camera2D, 
            CollisionManager collisionManager, 
            SpriteBatch spriteBatch,
            ContentManager contentManager,
            TiledMapRenderer mapRenderer) :
            base(camera2D, collisionManager, spriteBatch, contentManager, "map/LevelTwo", mapRenderer)
        {
            _spawnPoint = new Vector2(0, 170);
        }

        public override void Update(GameTime gameTime, Game mainGame)
        {
            if (_objectiveAchieved)
            {
                Handle(
                    ContextHandler, 
                    new EndMenu(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                );
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Handle(
                    ContextHandler, 
                    new LevelTwo(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                );
            }
            
            base.Update(gameTime, mainGame);
        }

        public override void LoadContent()
        {
            _ambientBackground = _contentManager.Load<Song>("assets/sound_effects/ambient-cave");

            /*
            * Load hero sprites.
            */ 
            _heroIdleSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/player/Player_Idle"),
                32, 40);
            
            _heroWalkSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/player/Player_walk"),
                32, 40);
            
            _heroRunSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/player/Player_run"),
                32, 40);

            _heroJumpSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/player/Player_jump"),
                32, 40);

            base.LoadContent();
            
            MediaPlayer.Play(_ambientBackground);

            MediaPlayer.IsRepeating = true;

            MediaPlayer.Volume = 0.6f;
        }
        
        public override void InitializeGameObjects()
        {          
            _player = new Hero(_spawnPoint, _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet, new KeyBoardReader());

            foreach (var tiledMapLayer in _map.TileLayers)
            {
                AddMapLayer(tiledMapLayer);
            }
            
            AddLethalMapLayer(_map.GetLayer<TiledMapTileLayer>("LavaLayer"));
            
            AddLethalMapLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Lethal"));
            
            SetCollisionLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Collision"));
            
            SetObjectiveLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Objective"));

            base.InitializeGameObjects();
        }
    }
}