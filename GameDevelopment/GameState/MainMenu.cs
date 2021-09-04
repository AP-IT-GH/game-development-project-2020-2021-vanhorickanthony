using System;

using GameDevelopment.Core;
using GameDevelopment.GameState.Interfaces;
using GameDevelopment.Collision;
using GameDevelopment.GameState.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Tiled.Renderers;

namespace GameDevelopment.GameState
{
    public class MainMenu: RenderableState
    {
        private Camera2D _camera2D;

        private CollisionManager _collisionManager;

        private SpriteBatch _spriteBatch;

        private NPC_Controller.NPC_Controller _npcController;
        
        protected ContentManager _contentManager;

        private TiledMapRenderer _mapRenderer;

        private Texture2D buttonPlay;


        public MainMenu(
            Camera2D camera2D,
            CollisionManager collisionManager,
            SpriteBatch spriteBatch,
            ContentManager contentManager,
            TiledMapRenderer mapRenderer
        )
        {
            _camera2D = camera2D;

            _collisionManager = collisionManager;

            _spriteBatch = spriteBatch;

            _contentManager = contentManager;

            _mapRenderer = mapRenderer;
        }

        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            ctx.State = nextState;
        }

        public override void LoadContent()
        {
            buttonPlay = _contentManager.Load<Texture2D>("assets/menu/button-play");
            
            Console.WriteLine("Loading menu content...");
        }

        public override void InitializeGameObjects()
        {
            Console.WriteLine("Loading menu objects...");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                buttonPlay, 
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2) - 150,
                    (_spriteBatch.GraphicsDevice.Viewport.Height / 2) - 50,
                    300, 
                    100
                    ),
                Color.White); 
            _spriteBatch.End();
        }
    }
}