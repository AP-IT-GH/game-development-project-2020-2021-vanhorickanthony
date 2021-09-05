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
    public class EndMenu: RenderableState
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

        private SpriteFont _fontSmall;
        private SpriteFont _fontMedium;
        private SpriteFont _fontLarge;

        private Texture2D _buttonContinue;
        private Texture2D _buttonContinueSelected;

        public EndMenu(
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
            _fontSmall = _contentManager.Load<SpriteFont>("assets/fonts/default-sm");
            _fontMedium = _contentManager.Load<SpriteFont>("assets/fonts/default-md");
            _fontLarge = _contentManager.Load<SpriteFont>("assets/fonts/default-lg");

            _buttonContinue = _contentManager.Load<Texture2D>("assets/menu/button-continue");
            _buttonContinueSelected = _contentManager.Load<Texture2D>("assets/menu/button-continue-selected");
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
                _selectedAction == 0 ? _buttonContinueSelected : _buttonContinue,
                new Rectangle(
                    (_spriteBatch.GraphicsDevice.Viewport.Width / 2) - 150,
                    _spriteBatch.GraphicsDevice.Viewport.Height - 150,
                    300, 
                    100
                ),
                Color.White);
            
            _spriteBatch.DrawString(
                _fontSmall, 
                "Created and Directed by", 
                new Vector2(150, 50), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontMedium, 
                "Anthony Van Horick", 
                new Vector2(150, 70), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontSmall, 
                "Executive Producer", 
                new Vector2(150, 120), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontMedium, 
                "Anthony Van Horick", 
                new Vector2(150, 140), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontSmall, 
                "Technical Lead", 
                new Vector2(500, 120), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontMedium, 
                "Anthony Van Horick", 
                new Vector2(500, 140), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontSmall, 
                "Producer", 
                new Vector2(150, 190), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontMedium, 
                "Anthony Van Horick", 
                new Vector2(150, 210), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontSmall, 
                "Gameplay & level design", 
                new Vector2(500, 190), 
                Color.White
            );
            
            _spriteBatch.DrawString(
                _fontMedium, 
                "Anthony Van Horick", 
                new Vector2(500, 210), 
                Color.White
            );

            _spriteBatch.End();
        }
        
        public override void Handle(ContextHandler ctx, RenderableState nextState)
        {
            Console.WriteLine("[EndMenu] Handle next state.");
            
            _contentManager.Unload();
            
            _contentManager.GetGraphicsDevice().Clear(Color.Black);
            
            ctx.State = nextState;
            
            ctx.State.ContextHandler = ctx;
            
            Console.WriteLine("[EndMenu] Start loading content.");
            ctx.State.LoadContent();

            Console.WriteLine("[EndMenu] Initialize game objects.");
            ctx.State.InitializeGameObjects();

            Console.WriteLine("[EndMenu] Finished. Goodbye.");
        }
    }
}