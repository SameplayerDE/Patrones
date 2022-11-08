using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Patrones.Shared
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            IsFixedTimeStep = true;
            MaxElapsedTime = TimeSpan.FromSeconds(1);
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("p_w");
        }

        protected override void Update(GameTime gameTime)
        {
            GameHandler.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            int i = 0;
            while (i < GameHandler.Instance.Positions.Count)
            {
                var position = GameHandler.Instance.Positions[i];
                _spriteBatch.Draw(_texture, new Rectangle(position.ToPoint() - new Point(4, 4), new Point(8, 8)), Color.White);
                i++;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
