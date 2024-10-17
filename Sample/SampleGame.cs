using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Sample.Audio;
using Sample.Objects;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

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

        private TcpListener server;

        public int countBlocksX = 10;
        public int countBlocksY = 10;
        public int blockSize = 40;

        int playfieldPxSizeX;
        int playfieldPxSizeY;

        PlayField playfieldLinks;
        PlayField playfieldRechts;

        public GameMode gameMode = GameMode.None;

        List<Ship> ships;
        public Ship currentDraggedShip = null;

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

        private async void Init()
        {
            playfieldPxSizeX = countBlocksX * blockSize;
            playfieldPxSizeY = countBlocksY * blockSize;
            ships = new List<Ship>();
            ships.Add(new Ship(this, 500, 40, 3, Orientation.Vertical));

            playfieldLinks = new PlayField(this, 50, (GameBounds.Height - playfieldPxSizeY) / 2);
            playfieldLinks.Init(GameBounds);

            playfieldRechts = new PlayField(this, GameBounds.Width - playfieldPxSizeX - 50, (GameBounds.Height - playfieldPxSizeY) / 2);
            playfieldRechts.Init(GameBounds);

            //offsetX = (GameBounds.Width - playfieldSizeX * blockSize) / 2;
            //offsetY = (GameBounds.Height - playfieldSizeY * blockSize) / 2;

            //server = new TcpListener(4000);
            //server.Start();
            //var client = await server.AcceptTcpClientAsync();
            //var stream = client.GetStream();
            //stream.Write(Encoding.ASCII.GetBytes("Hallo"));
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            
            foreach (var ship in ships)
            {
                ship.Update(gameTime, keyboardState, mouseState);
            }

            playfieldLinks.Update(gameTime, keyboardState, mouseState);

            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    var x = (mouseState.X - offsetX) / blockSize;
            //    var y = (mouseState.Y - offsetY) / blockSize;
            //    if (x >= 0 && y >= 0 && x < playfieldSizeX && y < playfieldSizeY)
            //        playField[x, y] = 1;
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Warum eine Spritebatch?
            //Die Grafikkarte muss alle Zeichenfunktionen auf einma abarbeiten um efizient zu sein, daher braucht sie die ganzean anweisungen
            //in einer Liste (Batch), sonst würde es flackern
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            playfieldLinks.Draw(gameTime, _spriteBatch);
            playfieldRechts.Draw(gameTime, _spriteBatch);
            foreach (var ship in ships)
                ship.Draw(gameTime, _spriteBatch);
            //Das Bewirkt das die Batch abgearbeitet wird
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}