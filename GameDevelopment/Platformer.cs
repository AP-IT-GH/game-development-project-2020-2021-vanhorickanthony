using GameDevelopment.Collision;
using GameDevelopment.Core;

using GameDevelopment.GameState;
using GameDevelopment.GameState.Abstracts;

using Microsoft.Xna.Framework;

using MonoGame.Extended.Tiled.Renderers;

using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GameDevelopment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera2D _camera2D;
        
        NPC_Controller.NPC_Controller _npcController;

        CollisionManager _collisionManager;

        /**
         * Tiled map data
         */
        private TiledMapRenderer _mapRenderer;
        
        private RenderableState _initialState;

        private ContextHandler _contextHandler;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            /*
             * Initialize map renderer.
             */
            _mapRenderer = new TiledMapRenderer(GraphicsDevice);

            /*
             * Initialize camera.
             */
            _camera2D = new Camera2D(_graphics.GraphicsDevice.Viewport);

            /*
             * Initialize collision manager.
             */
            _collisionManager = new CollisionManager();

            _contextHandler = new ContextHandler();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _initialState = new MainMenu(_camera2D, _collisionManager, _spriteBatch, Content, _mapRenderer);
            
            _initialState.LoadContent();

            _contextHandler = new ContextHandler(_initialState);

            _initialState.ContextHandler = _contextHandler;

            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            _contextHandler.State.InitializeGameObjects();
        }

        protected override void Update(GameTime gameTime)
        {
            _contextHandler.State.Update(gameTime, this);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _contextHandler.State.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
