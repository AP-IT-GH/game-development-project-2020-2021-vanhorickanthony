using System;

using GameDevelopment.Core;
using GameDevelopment.GameState.Interfaces;
using GameDevelopment.Collision;
using GameDevelopment.GameState.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled.Renderers;

namespace GameDevelopment.GameState
{
    public class DeathMenu: RenderableState
    {
        private Camera2D _camera2D;

        private CollisionManager _collisionManager;

        private SpriteBatch _spriteBatch;

        private NPC_Controller.NPC_Controller _npcController;
        
        protected ContentManager _contentManager;

        private TiledMapRenderer _mapRenderer;

        private int selectedAction;

        private double previousTime;
        private double debounce;

        private Texture2D _buttonPlay;
        private Texture2D _buttonPlaySelected;

        private Texture2D _buttonQuit;
        private Texture2D _buttonQuitSelected;
        

        private Vector2 _mouseLocation;


        public DeathMenu(
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

            selectedAction = 0;

            previousTime = 0;
            debounce = 400;
        }

        public override void LoadContent()
        {
            _buttonPlay = _contentManager.Load<Texture2D>("assets/menu/button-play");
            _buttonPlaySelected = _contentManager.Load<Texture2D>("assets/menu/button-play-selected");

            _buttonQuit = _contentManager.Load<Texture2D>("assets/menu/button-quit");
            _buttonQuitSelected = _contentManager.Load<Texture2D>("assets/menu/button-quit-selected");
            
            Console.WriteLine("Loading menu content...");
        }

        public override void InitializeGameObjects()
        {
            Console.WriteLine("Loading menu objects...");
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("1: " + gameTime.TotalGameTime.TotalMilliseconds);
            Console.WriteLine("2: " + previousTime);
            
            if ((gameTime.TotalGameTime.TotalMilliseconds - previousTime) > debounce)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = selectedAction - 1;
                    selectedAction = newSelection < 0 ? 1 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = selectedAction + 1;
                    selectedAction = newSelection > 1 ? 0 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;

                    if (selectedAction == 0)
                    {
                        Handle(
                            ContextHandler, 
                            new LevelOne(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                        );
                    }
                    else if (selectedAction == 1)
                    {
                        Handle(
                            ContextHandler, 
                            new MainMenu(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                        );
                    }
                }   
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(
                selectedAction == 0 ? _buttonPlaySelected : _buttonPlay, 
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2) - 350,
                    (_spriteBatch.GraphicsDevice.Viewport.Height / 2) - 50,
                    300, 
                    100
                    ),
                Color.White);
            
            _spriteBatch.Draw(
                selectedAction == 1 ? _buttonQuitSelected : _buttonQuit,
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2),
                    (_spriteBatch.GraphicsDevice.Viewport.Height / 2) - 50,
                    300, 
                    100
                    ),
                Color.White);
            
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