using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameDevelopment.Core;
using GameDevelopment.Collision;
using GameDevelopment.GameState.Abstracts;

using MonoGame.Extended.Content;
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

        private int _selectedAction;

        private double previousTime;
        private double debounce;

        private SpriteFont _font;

        private Texture2D _buttonPlay;
        private Texture2D _buttonPlaySelected;

        private Texture2D _buttonControls;
        private Texture2D _buttonControlsSelected;

        private Texture2D _buttonQuit;
        private Texture2D _buttonQuitSelected;
        
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

            _selectedAction = -1;

            previousTime = 0;
            debounce = 400;
        }

        public override void LoadContent()
        {
            Console.WriteLine("Load main menu content.");
            
            _font = _contentManager.Load<SpriteFont>("assets/fonts/default-lg");
            
            _buttonPlay = _contentManager.Load<Texture2D>("assets/menu/button-play");
            _buttonPlaySelected = _contentManager.Load<Texture2D>("assets/menu/button-play-selected");
            
            _buttonControls = _contentManager.Load<Texture2D>("assets/menu/button-controls");
            _buttonControlsSelected = _contentManager.Load<Texture2D>("assets/menu/button-controls-selected");

            _buttonQuit = _contentManager.Load<Texture2D>("assets/menu/button-quit");
            _buttonQuitSelected = _contentManager.Load<Texture2D>("assets/menu/button-quit-selected");
        }

        public override void InitializeGameObjects()
        {
            
        }

        public override void Update(GameTime gameTime, Game mainGame)
        {
            
            if ((gameTime.TotalGameTime.TotalMilliseconds - previousTime) > debounce)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = _selectedAction - 1;
                    _selectedAction = newSelection < 0 ? 2 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = _selectedAction + 1;
                    _selectedAction = newSelection > 2 ? 0 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;

                    if (_selectedAction == 0)
                    {
                        Handle(
                            ContextHandler, 
                            new LevelOne(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                        );
                    }
                    else if (_selectedAction == 1)
                    {
                        Handle(
                            ContextHandler, 
                            new ControlsMenu(_camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                        );
                    }
                    else if (_selectedAction == 2)
                    {
                        mainGame.Exit();
                    }
                }   
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(
                _selectedAction == 0 ? _buttonPlaySelected : _buttonPlay,
                new Rectangle(
                    _spriteBatch.GraphicsDevice.Viewport.Width - 350,
                    50,
                    300, 
                    100
                ),
                Color.White);

            _spriteBatch.Draw(
                _selectedAction == 1 ? _buttonControlsSelected : _buttonControls,
                new Rectangle(
                    _spriteBatch.GraphicsDevice.Viewport.Width - 350,
                    200,
                    300,
                    100
                ),
                Color.White);
            
            _spriteBatch.Draw(
                _selectedAction == 2 ? _buttonQuitSelected : _buttonQuit,
                new Rectangle(
                    _spriteBatch.GraphicsDevice.Viewport.Width - 350,
                    350,
                    300,
                    100
                ),
                Color.White);
            
            _spriteBatch.DrawString(
                _font, 
                "Platformer game", 
                new Vector2(50, 50), 
                Color.White
            );

            _spriteBatch.End();
        }
        
        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            Console.WriteLine("[MainMenu] Handle next state.");
            
            _contentManager.Unload();
            
            _contentManager.GetGraphicsDevice().Clear(Color.Black);
            
            ctx.State = nextState;
            
            ctx.State.ContextHandler = ctx;
            
            Console.WriteLine("[MainMenu] Start loading content.");
            ctx.State.LoadContent();

            Console.WriteLine("[MainMenu] Initialize game objects.");
            ctx.State.InitializeGameObjects();

            Console.WriteLine("[MainMenu] Finished. Goodbye.");
        }
    }
}