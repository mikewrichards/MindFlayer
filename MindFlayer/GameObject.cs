using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MindFlayer
{
    
    public class GameObject
    {

        Vector2 position;
        Vector2 velocity;
        float rotation = 0;
        float rSpeed = 0;

        List<Vector2> vertices;
        Color colour;
        int size;
        



        public GameObject(Vector2 initialPosition, Vector2 initialVelocity, int initialSize, Color initialColour )
        {
            //initializes an object, including size, position, velocity, and color
            position = initialPosition;
            velocity = initialVelocity;
            rotation = 0.0f;
            rSpeed = 0.0f;
            size = initialSize;
            colour = initialColour;
            vertices = new List<Vector2>();

            //initializes vertices
            UpdateVertices();
        }



        internal void UpdatePosition()
        {
            // updates object's position and rotation, then recalculates vertices
            position += velocity;
        }

        private void UpdateRotation()
        {
            rotation += rSpeed;
            while (rotation >= MathHelper.TwoPi)
            {
                rotation -= MathHelper.TwoPi;
            }
            while (rotation <= 0)
            {
                rotation += MathHelper.TwoPi;
            }
        }

        private void UpdateVertices()
        {
            vertices.Clear();
            //updates vertices based on position and rotation of object
        }

        internal void UpdateObject()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateVertices();
        }
    }
}
