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

        public Vector2 position;
        public Vector2 velocity;
        public float rotation = 0;
        public float rSpeed = 0;
        public List<Vector2> vertices;
        public Color colour;
        public int size;
        public float radius;
        public float mass;
        



        public GameObject(Vector2 initialPosition, Vector2 initialVelocity, int initialSize, Color initialColour, float initialRotation, float initialRSpeed)
        {
            //initializes an object, including size, position, velocity, and color
            position = initialPosition;
            velocity = initialVelocity;
            rotation = initialRotation;
            rSpeed = initialRSpeed;
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
            //updates vertices based on position and rotation of object
            vertices.Clear();

            float thetaOne;
            float theta;
            Vector2 temp = new Vector2();
            radius = size * 5;
            thetaOne = MathHelper.TwoPi / size;
            for (int i = 0; i < size; i++)
            {
                theta = thetaOne * i + rotation;
                temp.X = radius * (float) Math.Cos(theta) + position.X;
                temp.Y = radius * (float) Math.Sin(theta) + position.Y;
                vertices.Add(temp);
            }
        }

        internal void UpdateObject()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateVertices();
        }

        public void UpdateVelocityWithForce(Vector2 force)
        {
            mass = size * size;
            velocity.X += force.X / mass;
            velocity.Y += force.Y / mass;
        }
    }
}
