using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera2D
{
    private Viewport viewport;

    public Camera2D(Viewport viewport)
    {
        this.viewport = viewport;
    }

    internal Matrix GetTransformation(GraphicsDevice GraphicsDevice, Matrix spriteScale, Vector2 vector2)
    {
        throw new NotImplementedException();
    }


    public void LookAt(Vector2 vector2)
    {
        throw new NotImplementedException();
    }

    public Rectangle Limits { get; set; }

    public Vector2 Position { get; set; }
}
