using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class Camera2D 
{
    protected float zoom, rotation;
    protected Vector2 position, origin;
    protected Rectangle bounds, limits;
    protected Viewport viewport;
    public Camera2D(Viewport viewport) 
    {
        Bounds = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
        zoom = 1.0f;
        rotation = 0f;
        position = new Vector2(0,0);
        Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        limits = Rectangle.Empty;
        this.viewport = viewport;
    }
    
    public void LookAt(Vector2 lookPos)
    {
        Origin = lookPos;
        position = origin - new Vector2(viewport.Width, viewport.Height) *0.9f;
    }

    public float Zoom 
    {
        get { return zoom; }
        set { zoom = value; }
    }
    public Vector2 Position 
    {
        get { return position; }
        set { position = value; }
    }
    
    public Vector2 Origin
    {
        get { return origin; }
        set
        {
            float posX, posY;
            posX = MathHelper.Clamp(value.X, limits.X + viewport.Width * 0.9f, limits.X + limits.Width - viewport.Width * 0.9f);
            posY = MathHelper.Clamp(value.Y, limits.Y + viewport.Height* 0.86f, limits.Y + limits.Height - viewport.Height * 0.86f);
            origin = new Vector2(posX, posY);
        }
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Rectangle Bounds
    {
        get { return bounds; }
        set { bounds = value; }
    }

    public Rectangle Limits
    {
        get { return limits; }
        set { limits = value; }
    }

    public Matrix TransformMatrix
    { 
        get {
            return
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
        }
    }

}
