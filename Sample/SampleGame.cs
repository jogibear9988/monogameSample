using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sample.Audio;

namespace Sample
{
    public class SampleGame : Game
    {
        #region Core Game Stuff
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Point GameBounds = new Point(1280, 720); //window resolution
        public Texture2D Texture;
        private AudioSource SoundFX;

        public SampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GameBounds.X;
            _graphics.PreferredBackBufferHeight = GameBounds.Y;
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Reset();
        }

        private void Reset()
        {
            if (Texture == null)
            {
                Texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
                Texture.SetData<Color>(new Color[] { Color.White });
            }
            if (SoundFX == null)
            {
                SoundFX = new AudioSource();
            }

            Init();
        }

        #endregion

        #region Base Drawing functions
        
        private void DrawRectangle(SpriteBatch sb, Rectangle Rec, Color color)
        {
            Vector2 pos = new Vector2(Rec.X, Rec.Y);
            sb.Draw(Texture, pos, Rec, color * 1.0f, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.00001f);
        }
        #endregion

        int playerX;
        int playerY;
        int playerWidth = 60;
        int playerHeight = 20;

        private void Init()
        {
            playerX = GameBounds.X / 2 - playerWidth/2;
            playerY = GameBounds.Y / 2 - playerHeight/2;
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                playerY = playerY - 1;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            DrawRectangle(_spriteBatch, new Rectangle(playerX, playerY, playerWidth, playerHeight), Color.Red);


            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}