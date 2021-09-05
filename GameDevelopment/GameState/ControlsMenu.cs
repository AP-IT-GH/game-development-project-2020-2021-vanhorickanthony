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
    public class ControlsMenu: RenderableState
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

        private Texture2D _buttonBack;
        private Texture2D _buttonBackSelected;

        public ControlsMenu(
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
            Console.WriteLine("Load controls menu content.");
            
            _font = _contentManager.Load<SpriteFont>("assets/fonts/default-md");
            
            _buttonBack = _contentManager.Load<Texture2D>("assets/menu/button-back");
            _buttonBackSelected = _contentManager.Load<Texture2D>("assets/menu/button-back-selected");
        }

        public override void InitializeGameObjects()
        {
            
        }

        public override void Update(GameTime gameTime, Game mainGame)
        {
            
            if ((gameTime.TotalGameTime.TotalMilliseconds - previousTime) > debounce)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = _selectedAction - 1;
                    _selectedAction = newSelection < 0 ? 0 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;
                
                    var newSelection = _selectedAction + 1;
                    _selectedAction = newSelection > 0 ? 0 : newSelection;
                }
            
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    previousTime = gameTime.TotalGameTime.TotalMilliseconds;

                    if (_selectedAction == 0)
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
                _selectedAction == 0 ? _buttonBackSelected : _buttonBack,
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2) - 150,
                    _spriteBatch.GraphicsDevice.Viewport.Height - 150,
                    300, 
                    100
                ),
                Color.White);
            
            _spriteBatch.DrawString(
                _font, 
                "Arrows <= => to move left/right.", 
                new Vector2(150, 50), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _font, 
                "Use shift to sprint while moving.", 
                new Vector2(150, 150), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _font, 
                "Use space to jump.", 
                new Vector2(150, 250), 
                Color.White
            );

            _spriteBatch.End();
        }
        
        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            Console.WriteLine("[ControlsMenu] Handle next state.");
            
            _contentManager.Unload();
            
            _contentManager.GetGraphicsDevice().Clear(Color.Black);
            
            ctx.State = nextState;
            
            ctx.State.ContextHandler = ctx;
            
            Console.WriteLine("[ControlsMenu] Start loading content.");
            ctx.State.LoadContent();

            Console.WriteLine("[ControlsMenu] Initialize game objects.");
            ctx.State.InitializeGameObjects();

            Console.WriteLine("[ControlsMenu] Finished. Goodbye.");
        }
    }
}