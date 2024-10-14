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

        int sizeX = 10;
        int sizeY = 10;

        int offsetX = 0;
        int offsetY = 0;

        int blockSize = 30;

        int[,] playField;

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
            playField = new int[sizeX, sizeY];
            playField[0, 0] = 1;
            playField[4, 6] = 1;
            playField[4, 7] = 1;
            offsetX = (GameBounds.Width - sizeX * blockSize) / 2;
            offsetY = (GameBounds.Height - sizeY * blockSize) / 2;
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var x = (mouseState.X - offsetX) / blockSize;
                var y = (mouseState.Y - offsetY) / blockSize;
                if (x >= 0 && y >= 0 && x < sizeX && y < sizeY)
                    playField[x, y] = 1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Warum eine Spritebatch?
            //Die Grafikkarte muss alle Zeichenfunktionen auf einma abarbeiten um efizient zu sein, daher braucht sie die ganzean anweisungen
            //in einer Liste (Batch), sonst würde es flackern
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    _spriteBatch.DrawRectangle(new Rectangle(i * blockSize + offsetX, j * blockSize + offsetY, blockSize, blockSize), Color.Black, 2);
                    if (playField[i, j] == 1)
                    {
                        _spriteBatch.DrawCircle(new CircleF(new Vector2(i * blockSize + offsetX + blockSize/2, j * blockSize + offsetY+blockSize/2), blockSize / 2 - 2), 30, Color.Wheat, 20);
                    }
                }
            }


            //Das Bewirkt das die Batch abgearbeitet wird
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}