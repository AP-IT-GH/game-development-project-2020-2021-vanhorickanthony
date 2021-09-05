using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameDevelopment.Core;
using GameDevelopment.Collision;
using GameDevelopment.GameState.Abstracts;

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

        private Song _soundEffect;

        private Texture2D _imageYouDied;
        private double _imageFadeInTime;
        private double _imageTimerStart;
        private float _timerCompletion;

        private Texture2D _buttonPlay;
        private Texture2D _buttonPlaySelected;

        private Texture2D _buttonQuit;
        private Texture2D _buttonQuitSelected;

        private Level _currentLevel;
        
        public DeathMenu(
            Camera2D camera2D,
            CollisionManager collisionManager,
            SpriteBatch spriteBatch,
            ContentManager contentManager,
            TiledMapRenderer mapRenderer,
            Level currentLevel
        )
        {
            _camera2D = camera2D;

            _collisionManager = collisionManager;

            _spriteBatch = spriteBatch;

            _contentManager = contentManager;

            _mapRenderer = mapRenderer;

            selectedAction = -1;

            _imageFadeInTime = 5000;
            _imageTimerStart = 0;
            _timerCompletion = 0;

            previousTime = 0;
            debounce = 400;

            _currentLevel = currentLevel;
        }

        public override void LoadContent()
        {
            _soundEffect = _contentManager.Load<Song>("assets/sound_effects/you_died");
            
            _imageYouDied = _contentManager.Load<Texture2D>("assets/menu/image-you_died");
            
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

        public override void Update(GameTime gameTime, Game mainGame)
        {
            _timerCompletion =
                (Single) ((float) Math.Min(gameTime.TotalGameTime.TotalMilliseconds - _imageTimerStart, _imageFadeInTime) /
                          _imageFadeInTime);
            
            if (_imageTimerStart == 0)
            {
                _imageTimerStart = gameTime.TotalGameTime.TotalMilliseconds;
                
                MediaPlayer.Play(_soundEffect);
            }
            
            if ((gameTime.TotalGameTime.TotalMilliseconds - previousTime) > debounce && _timerCompletion >= 0.9)
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
                            (RenderableState)Activator.CreateInstance(_currentLevel.GetType(), _camera2D, _collisionManager, _spriteBatch, _contentManager, _mapRenderer)
                        );
                    }
                    else if (selectedAction == 1)
                    {
                        Console.WriteLine("Main menu");
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
                _imageYouDied,
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2) - (640/2),
                    0,
                    640, 
                    360
                ),
                Color.White * _timerCompletion);

            if (_timerCompletion >= 0.9)
            {
                _spriteBatch.Draw(
                    selectedAction == 0 ? _buttonPlaySelected : _buttonPlay,
                    new Rectangle(
                        50,
                        _spriteBatch.GraphicsDevice.Viewport.Height - 150,
                        300, 
                        100
                    ),
                    Color.White);
            
                _spriteBatch.Draw(
                    selectedAction == 1 ? _buttonQuitSelected : _buttonQuit,
                    new Rectangle(
                        _spriteBatch.GraphicsDevice.Viewport.Width - 350,
                        _spriteBatch.GraphicsDevice.Viewport.Height - 150,
                        300, 
                        100
                    ),
                    Color.White);
            }

            _spriteBatch.End();
        }
        
        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            Console.WriteLine("[DeathMenu] Handle next state.");
            
            _contentManager.Unload();
            
            _contentManager.GetGraphicsDevice().Clear(Color.Black);
            
            ctx.State = nextState;
            
            ctx.State.ContextHandler = ctx;
            
            Console.WriteLine("[DeathMenu] Start loading content.");
            ctx.State.LoadContent();

            Console.WriteLine("[DeathMenu] Initialize game objects.");
            ctx.State.InitializeGameObjects();

            Console.WriteLine("[DeathMenu] Finished. Goodbye.");
        }
    }
}