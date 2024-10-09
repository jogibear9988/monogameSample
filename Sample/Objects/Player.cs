using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Sample.Objects
{
    public class Player
    {
        int playerX;
        int playerY;

        int playerWidth = 60;
        int playerHeight = 20;

        public Player() {
        }

        public void Init(Size gameBounds)
        {
            playerX = gameBounds.Width / 2 - playerWidth / 2;
            playerY = gameBounds.Height / 2 - playerHeight / 2;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                playerY = playerY - 1;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new RectangleF(playerX, playerY, playerWidth, playerHeight), Color.Red);
        }
    }
}
