using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera2D
{
    protected float zoom, rotation;
    public Matrix transform;
    public Vector2 position, origin;
    private readonly Viewport viewPort;
    private Rectangle? limits;

    public Camera2D(Viewport viewPort)
    {
        this.viewPort = viewPort;
        zoom = 1.0f;
        rotation = 0.0f;
        position = new Vector2(GameEnvironment.Screen.X / 2.0f, GameEnvironment.Screen.Y / 2.0f);
        origin = new Vector2(GameEnvironment.Screen.X / 2.0f, GameEnvironment.Screen.Y / 2.0f);
    }

    public float Zoom
    {
        get { return zoom; }
        set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public void Move(Vector2 amount)
    {
        position += amount;
    }

    public Vector2 Position
    {
        get { return position; }
        set
        {
            position = value;

            // If there's a limit set and the camera is not transformed clamp position to limits
            if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
            {
                position.X = MathHelper.Clamp(position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - viewPort.Width);
                position.Y = MathHelper.Clamp(position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - viewPort.Height);
            }
        }
    }

    public Rectangle? Limits
    {
        get { return limits; }
        set
        {
            if (value != null)
            {
                // Assign limit but make sure it's always bigger than the viewport
                limits = new Rectangle
                {
                    X = value.Value.X,
                    Y = value.Value.Y,
                    Width = System.Math.Max(viewPort.Width, value.Value.Width),
                    Height = System.Math.Max(viewPort.Height, value.Value.Height)
                };

                // Validate camera position with new limit
                Position = Position;
            }
            else
            {
                limits = null;
            }
        }
    }

    


    public Matrix GetTransformation(GraphicsDevice graphicsDevice, Matrix spriteScale, Vector2 parallax)
    {
        return Matrix.CreateTranslation(new Vector3(-position * parallax, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateScale(zoom, zoom, 1) *
            Matrix.CreateTranslation(new Vector3(origin, 0.0f)) * spriteScale;
    }

    public void LookAt(Vector2 position)
    {
        Position = position - new Vector2(GameEnvironment.Screen.X / 2.0f, GameEnvironment.Screen.Y / 2.0f);
    }
}
