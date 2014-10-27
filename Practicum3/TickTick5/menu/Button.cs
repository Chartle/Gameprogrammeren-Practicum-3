using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
class Button : SpriteGameObject
{
    protected bool pressed;
    protected Vector2 drawPosition;

    public Button(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id)
    {
        pressed = false;
        drawPosition = position;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
            return;
        sprite.Draw(spriteBatch, this.drawPosition, origin);
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }

    public override Vector2 Position
    {
        get
        {
            return base.Position;
        }
        set
        {
            base.Position = value;
            drawPosition = value;
        }
    }

    public Vector2 DrawPosition
    {
        set { drawPosition = value; }
    }
}
