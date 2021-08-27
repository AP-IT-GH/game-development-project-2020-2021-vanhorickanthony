﻿using System;
using System.Diagnostics;

using GameDevelopment.Animation;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Collision;
using GameDevelopment.Core;
using GameDevelopment.Input;
using GameDevelopment.Entity;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using SharpDX.DirectWrite;

namespace GameDevelopment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera2D _camera2D;

        private Vector2 _spawnPoint;

        private IAnimationSheet _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet;

        private Texture2D _collisionBox;

        Hero _hero;

        CollisionManager _collisionManager;

        /**
         * Tiled map data
         */
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;

        private TiledMapTileLayer _skyLayer, _treeLine1, _treeLine2, _treeLine3, _groundLayer1, _groundLayer2;

        private TiledMapGroupLayer _treeLayers, _skyLayers;

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

            _spawnPoint = new Vector2(0, 376);
                
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _heroIdleSheet = new AnimationSheet(
                Content.Load<Texture2D>("assets/player/Player_Idle"),
                32, 40);
            
            _heroWalkSheet = new AnimationSheet(
                Content.Load<Texture2D>("assets/player/Player_walk"),
                32, 40);
            
            _heroRunSheet = new AnimationSheet(
                Content.Load<Texture2D>("assets/player/Player_run"),
                32, 40);

            _heroJumpSheet = new AnimationSheet(
                Content.Load<Texture2D>("assets/player/Player_jump"),
                32, 40);

            /*
             * Load map.
             */
            _map = Content.Load<TiledMap>("map/GameMap");
            _mapRenderer.LoadMap(_map);

            this._collisionBox = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            this._collisionBox.SetData(new[] { Color.White });

            InitializeGameObjects();
            // TODO: use this.Content to load your game content here
        }

        private void InitializeGameObjects()
        {          
            _hero = new Hero(this._spawnPoint, _heroIdleSheet, _heroWalkSheet, _heroRunSheet, _heroJumpSheet, new KeyBoardReader());

            _camera2D.TrackEntity(_hero);

            _camera2D.HorizontalBounds = new Vector2(0, _map.WidthInPixels);
            _camera2D.VerticalBounds = new Vector2(0, _map.HeightInPixels);
            
            /*
             * Main ground layers - player walks on this.
             */
            _groundLayer1 = this._map.GetLayer<TiledMapTileLayer>("GroundLayer_1");
            _groundLayer2 = this._map.GetLayer<TiledMapTileLayer>("GroundLayer_2");
            
            /*
             * Background decoration layers - these layers do not provide interaction, but are mere decoration for the settings.
             */
            _treeLayers = this._map.GetLayer<TiledMapGroupLayer>("TreeLayers");
            
            /*
             * The main background - this is what we perform parallaxing on.
             */
            _skyLayers = this._map.GetLayer<TiledMapGroupLayer>("SkyLayers");
        }

        protected override void Update(GameTime gameTime)
        {

            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _hero.Update(gameTime);

            _camera2D.Update();

            Debug.WriteLine(_hero.Position);
            _mapRenderer.Update(gameTime);

            base.Update(gameTime);
        }

       

        protected override void Draw(GameTime gameTime)
        {
            /*
             * Render map layers with respect to their position relative to each other.
             * TiledMapRenderer does not provide a functionality to attach a different viewMatrix to a distinct layer,
             * so each layer needs to be drawn separately.
             */

            foreach (TiledMapLayer skyLayer in _skyLayers.Layers)
            {
                float parallaxFactor = 1;

                if (skyLayer.Properties.ContainsKey("ParallaxFactor"))
                {
                    parallaxFactor = float.Parse(skyLayer.Properties["ParallaxFactor"]);
                }

                Matrix viewMatrix = _camera2D.GetViewMatrix(new Vector2(1, 1));

                _mapRenderer.Draw(skyLayer, viewMatrix: viewMatrix);
            }
            
            foreach (TiledMapLayer treeLayer in _treeLayers.Layers)
            {
                _mapRenderer.Draw(treeLayer, viewMatrix: _camera2D.TransformationMatrix);
            }
            
            _mapRenderer.Draw(_groundLayer2, viewMatrix: _camera2D.TransformationMatrix);
            _mapRenderer.Draw(_groundLayer1, viewMatrix: _camera2D.TransformationMatrix);

            _spriteBatch.Begin(transformMatrix: _camera2D.TransformationMatrix);

            _hero.Draw(_spriteBatch);

            if (_collisionManager.CheckCollision(_hero, _groundLayer1))
            {
                Debug.WriteLine("[" + gameTime.TotalGameTime + "] Collision detected.");

                _hero.Undo();
            }

            Rectangle collisionRect = _hero.CollisionRectangle;

            DrawBorder(collisionRect, 1, Color.Red);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            _spriteBatch.Draw(_collisionBox, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            _spriteBatch.Draw(_collisionBox, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            _spriteBatch.Draw(_collisionBox, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            _spriteBatch.Draw(_collisionBox, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
