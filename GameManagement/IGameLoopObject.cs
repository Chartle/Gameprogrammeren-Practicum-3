using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IGameLoopObject
{
    void HandleInput(InputHelper inputHelper);

    void Update(GameTime gameTime, Camera2D camera);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();

}
