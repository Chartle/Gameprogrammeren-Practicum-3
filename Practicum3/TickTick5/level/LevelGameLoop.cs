using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    SpriteGameObject testobj = new SpriteGameObject("Sprites/Player/spr_explode@5x5", 190, "test", 8);
    Vector2 cameraPos, cameraOrig;

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (quitButton.Pressed)
        {
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }      
    }

    public override void Update(GameTime gameTime, Camera2D camera)
    {
        base.Update(gameTime, camera);
        

        // set the player as camera origin
        Player player = this.Find("player") as Player;
        camera.Limits = new Rectangle(0, 0, tiles.CellWidth * width, tiles.CellHeight * height);
        camera.LookAt(player.Position);
        
        cameraPos = camera.Position;
        cameraOrig = camera.Origin;

        // display the overlay things relative to the camera position
        timer.Position = cameraPos + new Vector2(25,50);
        timerBackground.Position = cameraPos + new Vector2(10, 25);
        hintfield.Position = cameraPos + new Vector2((GameEnvironment.Screen.X - hint_frame.Width) / 2, 35);
        quitButton.DrawPosition = cameraPos + new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 35);
       
        // parallax
        foreach(GameObject obj in backgrounds.Objects)
        {
            if (obj.ID == "clouds")
            {

            }
            else
            {
                SpriteGameObject sprObj = obj as SpriteGameObject;
                sprObj.Position = (cameraOrig - sprObj.Center) * sprObj.Layer/2f;
            }
        }


        // check if we died
        if (!player.IsAlive)
            timer.Running = false;

        // check if we ran out of time
        if (timer.GameOver)
            player.Explode();
                       
        // check if we won
        if (this.Completed && timer.Running)
        {
            player.LevelFinished();
            timer.Running = false;
        }
    }

    public override void Reset()
    {
        base.Reset();
        VisibilityTimer hintTimer = this.Find("hintTimer") as VisibilityTimer;
        
        hintTimer.StartVisible();
    }
}
