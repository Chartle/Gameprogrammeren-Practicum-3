using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IGameLoopObject
{
    Dictionary<string, IGameLoopObject> gameStates;
    IGameLoopObject currentGameState;

    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameLoopObject>();
        currentGameState = null;
    }

    public void AddGameState(string name, IGameLoopObject state)
    {
        gameStates[name] = state;
    }

    public IGameLoopObject GetGameState(string name)
    {
        return gameStates[name];
    }

    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
            currentGameState = gameStates[name];
        else
            throw new KeyNotFoundException("Could not find game state: " + name);
    }

    public IGameLoopObject CurrentGameState
    {
        get
        {
            return currentGameState;
        }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (currentGameState != null)
            currentGameState.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime, Camera2D camera)
    {
        if (currentGameState != null)
            currentGameState.Update(gameTime, camera);
        if(currentGameState != GetGameState("playingState"))
        {
            camera.Origin = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
            currentGameState.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
        if (currentGameState != null)
            currentGameState.Reset();
    }
}
