using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Sample.Objects
{
    public class Ship
    {
        int pxX;
        int pxY;

        int offX;
        int offY;

        int len;
        SampleGame game;
        public Orientation orientation;
        int margin = 2;

        public Ship(SampleGame game, int pxX, int pxY, int len, Orientation orientation)
        {
            this.game = game;
            this.pxX = pxX;
            this.pxY = pxY;
            this.len = len;
            this.orientation = orientation;
        }

        public void Init(Size gameBounds)
        {
        }

        bool wasDown = false;
        public bool dragShip = false;
        public bool destoryed = false;

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {
            if (!destoryed)
            {
                if (wasDown && MouseInside(mouseState) && mouseState.LeftButton == ButtonState.Released)
                {
                    if (game.gameMode == GameMode.None)
                    {
                        dragShip = true;
                        game.gameMode = GameMode.DragShip;
                        offX = mouseState.X - pxX;
                        offY = mouseState.Y - pxY;
                        game.currentDraggedShip = this;
                    }
                }
                if (mouseState.LeftButton == ButtonState.Pressed && MouseInside(mouseState))
                {
                    wasDown = true;
                }
                else
                {
                    wasDown = false;
                }

                if (dragShip)
                {
                    pxX = mouseState.X - offX;
                    pxY = mouseState.Y - offY;
                }
            }
        }

        public bool MouseInside(MouseState mouseState)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (mouseState.X >= pxX && mouseState.X < pxX + game.blockSize * len &&
                    mouseState.Y >= pxY && mouseState.Y < pxY + game.blockSize)
                {
                    return true;
                }
            }
            else
            {
                if (mouseState.X >= pxX && mouseState.X < pxX + game.blockSize &&
                   mouseState.Y >= pxY && mouseState.Y < pxY + game.blockSize * len)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!destoryed)
            {
                for (int i = 0; i < len; i++)
                {
                    if (orientation == Orientation.Horizontal)
                        spriteBatch.FillRectangle(new RectangleF(pxX + margin + i * game.blockSize, pxY + margin, game.blockSize - margin * 2, game.blockSize - 4), Color.DarkOliveGreen);
                    else
                        spriteBatch.FillRectangle(new RectangleF(pxX + margin, pxY + margin + i * game.blockSize, game.blockSize - margin * 2, game.blockSize - 4), Color.DarkOliveGreen);
                }
            }
        }
    }
}

