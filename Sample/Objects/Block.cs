using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Sample.Objects
{
    public class Block
    {
        public enum BlockMode
        {
            None,
            Ship,
            Shoot,
            Hit
        }

        int pxX;
        int pxY;
        SampleGame game;
        public BlockMode blockMode;

        public Block(SampleGame game, int pxX, int pxY, BlockMode blockMode)
        {
            this.game = game;
            this.pxX = pxX;
            this.pxY = pxY;
            this.blockMode = blockMode;
        }

        public void Init(Size gameBounds)
        {

        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {

        }

        public bool MouseInside(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {
            if (mouseState.X >= pxX && mouseState.X < pxX + game.blockSize &&
                mouseState.Y >= pxY && mouseState.Y < pxY + game.blockSize)
            {
                return true;
            }
            return false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new RectangleF(pxX, pxY, game.blockSize, game.blockSize), Color.Black, 3);
            if (blockMode == BlockMode.Ship)
            {
                spriteBatch.FillRectangle(new RectangleF(pxX, pxY, game.blockSize, game.blockSize - 4), Color.DarkOliveGreen);
            }
            else if (blockMode == BlockMode.Shoot)
            {
                spriteBatch.DrawCircle(pxX + game.blockSize / 2, pxY + game.blockSize / 2, 12, 50, Color.Yellow, 12);
            }
        }
    }
}
