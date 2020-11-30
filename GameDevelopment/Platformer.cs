using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using TestGame.Collision;
using TestGame.Input;
namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 spawnPoint;

        private Texture2D idleTexture;
        private Texture2D walkTexture;
        private Texture2D runTexture;
        private Texture2D jumpTexture;
        private Texture2D climbTexture;

        Hero hero;

        CollisionManager collisionManager;

        /**
         * Tiled map data
         */
        private TiledMap map;
        private TiledMapRenderer mapRenderer;

        TiledMapTileLayer groundLayer, lavaLayer;
        TiledMapTile? collisionTile;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

          
        }

        protected override void Initialize()
        {
            /**
             * Initialize map renderer.
             */
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            /**
             * Initialize collision manager.
             */
            collisionManager = new CollisionManager();

            spawnPoint = new Vector2(20, 372);
                
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            /**
             * Load hero textures.
             */
            idleTexture = Content.Load<Texture2D>("assets/player/Player_Idle");
            walkTexture = Content.Load<Texture2D>("assets/player/Player_walk");
            runTexture = Content.Load<Texture2D>("assets/player/Player_run");
            jumpTexture = Content.Load<Texture2D>("assets/player/Player_jump");
            climbTexture = Content.Load<Texture2D>("assets/player/Player_climb");

            /**
             * Load map.
             */
            map = Content.Load<TiledMap>("map/GameMap");
            mapRenderer.LoadMap(map);

            InitializeGameObjects();
            // TODO: use this.Content to load your game content here
        }

        private void InitializeGameObjects()
        {          
            hero = new Hero(this.spawnPoint, this.idleTexture, this.walkTexture, this.runTexture, new KeyBoardReader());

            groundLayer = this.map.GetLayer<TiledMapTileLayer>("GroundLayer");
            collisionTile = null;
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            hero.Update(gameTime);

            mapRenderer.Update(gameTime);

            if (groundLayer != null)
            {
                groundLayer.TryGetTile((ushort)(hero.Position.X / 32), (ushort)(hero.Position.Y / 32), out collisionTile);
            }

            base.Update(gameTime);
        }

       

        protected override void Draw(GameTime gameTime)
        {
            mapRenderer.Draw();

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            hero.Draw(_spriteBatch);

            if (!collisionTile.Value.IsBlank)
            {
                Debug.WriteLine("[" + gameTime.TotalGameTime + "] Collision detected.");
                Debug.WriteLine(collisionTile.ToString());
                hero.Undo();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
