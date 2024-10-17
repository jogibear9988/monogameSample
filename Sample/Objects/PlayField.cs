using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Sample.Objects
{
    public class PlayField
    {
        int pxX;
        int pxY;
        SampleGame game;

        Block[,] playField;

        public PlayField(SampleGame game, int pxX, int pxY)
        {
            this.game = game;
            this.pxX = pxX;
            this.pxY = pxY;
        }

        public void Init(Size gameBounds)
        {
            playField = new Block[game.countBlocksX, game.countBlocksY];
            for (int i = 0; i < game.countBlocksX; i++)
            {
                for (int j = 0; j < game.countBlocksY; j++)
                {
                    playField[i, j] = new Block(game, i * game.blockSize + pxX, j * game.blockSize + pxY, Block.BlockMode.None);
                }
            }
        }

        public Point PixelCoordsToPlayField(int pX, int pY)
        {
            var x = (pX - pxX) / game.blockSize;
            var y = (pY - pxY) / game.blockSize;
            return new Point(x, y);
        }

        public bool MouseInside(MouseState mouseState)
        {
            if (mouseState.X >= pxX && mouseState.X < pxX + game.blockSize * game.countBlocksX &&
                mouseState.Y >= pxY && mouseState.Y < pxY + game.blockSize * game.countBlocksY)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {
            if (game.gameMode == GameMode.DragShip && MouseInside(mouseState) && mouseState.LeftButton == ButtonState.Pressed)
            {
                var ccords = PixelCoordsToPlayField(mouseState.X, mouseState.Y);
                var block = playField[ccords.X, ccords.Y];
                block.blockMode = Block.BlockMode.Ship;

                game.gameMode = GameMode.None;
                game.currentDraggedShip.dragShip = false;
                game.currentDraggedShip.destoryed = true;
                game.currentDraggedShip = null;
            }

            if (mouseState.RightButton == ButtonState.Pressed && game.currentDraggedShip != null)
            {
                if (game.currentDraggedShip.orientation == Orientation.Horizontal)
                    game.currentDraggedShip.orientation = Orientation.Vertical;
                else
                    game.currentDraggedShip.orientation = Orientation.Horizontal;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < game.countBlocksX; i++)
            {
                for (int j = 0; j < game.countBlocksY; j++)
                {
                    var block = playField[i, j];
                    block.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
