using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    protected SpriteSheet sprite;
    protected Vector2 origin;
    protected float scale, rotation;

    public SpriteGameObject(string assetname, int layer = 0, string id = "", int sheetIndex = 0, float scale = 1.0f, float rotation = 0.0f)
        : base(layer, id)
    {
        this.rotation = rotation;
        this.scale = scale;
        if (assetname != "")
            sprite = new SpriteSheet(assetname, sheetIndex);
        else
            sprite = null;
    }    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
            return;
        sprite.Draw(spriteBatch, this.GlobalPosition, origin, scale, rotation);
    }

    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return this.origin; }
        set { this.origin = value; }
    }

    public float Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!this.Visible || !obj.Visible || !BoundingBox.Intersects(obj.BoundingBox))
            return false;
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.GetPixelColor(thisx, thisy).A != 0
                    && obj.sprite.GetPixelColor(objx, objy).A != 0)
                    return true;
            }
        return false;
    }
}

