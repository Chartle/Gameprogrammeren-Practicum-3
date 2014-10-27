using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    protected SpriteGameObject overlay;
    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        overlay = new SpriteGameObject("Overlays/spr_gameover");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        this.Add(overlay);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!inputHelper.KeyPressed(Keys.Enter))
            return;
        playingState.Reset();
        GameEnvironment.GameStateManager.SwitchTo("playingState");
    }

    public override void Update(GameTime gameTime, Camera2D camera)
    {
        overlay.Position = camera.Position + new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center; 
        playingState.Update(gameTime, camera);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}