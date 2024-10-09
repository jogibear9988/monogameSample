using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Sample.Audio;
using Sample.Objects;

namespace Sample
{
    public class SampleGame : Game
    {
        private Size GameBounds = new Size(1280, 720); //window resolution

        #region Core Game Stuff

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D Texture;
        private AudioSource SoundFX;

        public SampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GameBounds.Width;
            _graphics.PreferredBackBufferHeight = GameBounds.Height;
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

        Player player;

        private void Init()
        {
            player = new Player();
            player.Init(GameBounds);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, keyboardState, mouseState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Warum eine Spritebatch?
            //Die Grafikkarte muss alle Zeichenfunktionen auf einma abarbeiten um efizient zu sein, daher braucht sie die ganzean anweisungen
            //in einer Liste (Batch), sonst würde es flackern
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            player.Draw(gameTime, _spriteBatch);

            //Das Bewirkt das die Batch abgearbeitet wird
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}