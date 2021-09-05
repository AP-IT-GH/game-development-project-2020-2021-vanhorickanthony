using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameDevelopment.Core;
using GameDevelopment.Entity;
using GameDevelopment.Collision;
using GameDevelopment.Input;

using GameDevelopment.Animation;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.GameState.Abstracts;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace GameDevelopment.GameState
{
    public class LevelOne: Level
    {
        private Song _ambientBackground;
        
        private IAnimationSheet _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet;

        private IAnimationSheet _wolfIdleSheet, _wolfWalkSheet, _wolfRunSheet;

        private Vector2 _spawnPoint;

        public LevelOne(
            Camera2D camera2D, 
            CollisionManager collisionManager, 
            SpriteBatch spriteBatch,
            ContentManager contentManager,
            TiledMapRenderer mapRenderer) :
            base(camera2D, collisionManager, spriteBatch, contentManager, "map/LevelOne", mapRenderer)
        {
            _spawnPoint = new Vector2(200, 640);
        }

        public override void Update(GameTime gameTime, Game mainGame)
        {
            if (_objectiveAchieved)
            {
                Handle(
                    ContextHandler, 
                    new LevelTwo(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                );
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Handle(
                    ContextHandler, 
                    new LevelOne(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                );
            }
            
            base.Update(gameTime, mainGame);
        }

        public override void LoadContent()
        {
            _ambientBackground = _contentManager.Load<Song>("assets/sound_effects/ambient-forest");

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


            /*
            * Load wolf sprites.
            */
            _wolfIdleSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/npc/Wolf_01_Idle"),
                64, 32);

            _wolfWalkSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/npc/Wolf_02_Move"),
                64, 32);

            _wolfRunSheet = new AnimationSheet(
                _contentManager.Load<Texture2D>("assets/npc/Wolf_04_Run-Loop"),
                64, 32);
            
            base.LoadContent();
        }
        
        public override void InitializeGameObjects()
        {          
            _player = new Hero(_spawnPoint, _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet, new KeyBoardReader());

            Hostile_NPC _enemy_wolf_1 = new Hostile_NPC(new Vector2(-30, 645), _wolfIdleSheet, _wolfWalkSheet, _wolfRunSheet, _wolfIdleSheet, new AIInputReader());

            AddNpc(_enemy_wolf_1, new Vector2(620, 645));
            
            foreach (var tiledMapLayer in _map.TileLayers)
            {
                AddMapLayer(tiledMapLayer);
            }
            
            AddLethalMapLayer(_map.GetLayer<TiledMapTileLayer>("LavaLayer"));
            
            AddLethalMapLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Lethal"));
            
            SetCollisionLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Collision"));
            
            SetObjectiveLayer(_map.GetLayer<TiledMapTileLayer>("GroundLayer_Objective"));
            
            base.InitializeGameObjects();
            
            MediaPlayer.Play(_ambientBackground);

            MediaPlayer.IsRepeating = true;

            MediaPlayer.Volume = 0.6f;
        }
    }
}