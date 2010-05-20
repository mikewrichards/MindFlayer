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
using System.Collections;

namespace MindFlayer
{
    public class Game
    {
        public Rectangle border;
        public ArrayList asteroids;
        private const float GRAVITATIONAL_CONSTANT = 50f;

        public Game(Rectangle initBorder)
        {
            border = initBorder;
            asteroids = new ArrayList();
        }

        // method called during game loop
        public void Update()
        {
            ApplyForces();
            PurgeDead();
        }

        public void ApplyForces()
        {
            foreach (GameObject asteroid in asteroids)
            {
                ApplyGravityToObject(asteroid);
                ApplyCollisionForceToObject(asteroid);
                asteroid.UpdateObject();
            }
        }

        // calulate all gravitational forces in game
        // then will apply the force to the given object
        public void ApplyGravityToObject(GameObject gameObject)
        {
            Vector2 netForce = new Vector2(0f, 0f);
            Vector2 position = gameObject.position;

            foreach (GameObject opposer in asteroids)
            {
                // find unit vector
                Vector2 diffVect = opposer.position - position;
                Vector2 unitVector;
                if (diffVect.Length() == 0.0f) continue;
                unitVector.X = diffVect.X / diffVect.Length();
                unitVector.Y = diffVect.Y / diffVect.Length();

                Vector2 currentForce;
                currentForce.X = (opposer.mass / diffVect.LengthSquared()) * GRAVITATIONAL_CONSTANT * unitVector.X;
                currentForce.Y = (opposer.mass / diffVect.LengthSquared()) * GRAVITATIONAL_CONSTANT * unitVector.Y;
                netForce += currentForce;
            }
            netForce.X = netForce.X * gameObject.mass;
            netForce.Y = netForce.Y * gameObject.mass;

            gameObject.UpdateVelocityWithForce(netForce);
        }

        // calulate all collision forces in game
        // then will apply the force to the given object
        public void ApplyCollisionForceToObject(GameObject asteriod)
        {
            // woot
        }


        //returns a stack of collisions, where each collision is represented
        //by two integers, each a single object.  If the integer is -1, this
        //represents the edge of the game screen.
        public Stack<Collision> GetCollisions()
        {
            Stack<Collision> collisions = new Stack<Collision>();
            foreach (GameObject asteroid in asteroids)
            {
                HandleObjectCollisions(collisions, asteroid);
                HandleEdgeCollisions(collisions, asteroid);
            }


            return collisions;
        }

        private void HandleEdgeCollisions(Stack<Collision> collisions, GameObject asteroid)
        {
            if (CollidesWithEdge(asteroid))
            {
                collisions.Push(new Collision(asteroids.IndexOf(asteroid), -1));
            }
        }

        private void HandleObjectCollisions(Stack<Collision> collisions, GameObject asteroid)
        {
            foreach (GameObject asteroid2 in asteroids)
            {
                if (asteroid.Equals(asteroid2))
                {
                    continue;
                }

                if (IsWithinRadius(asteroid, asteroid2))
                {
                    //possible collision
                    if (IsCollision(asteroid, asteroid2))
                    {
                        collisions.Push(new Collision(asteroids.IndexOf(asteroid), asteroids.IndexOf(asteroid2)));
                    }
                }

            }
        }

        private bool CollidesWithEdge(GameObject asteroid)
        {
            //called when checking if an object collides with the border of the screen.
            asteroid.vertices.Add(asteroid.vertices.First());

            for (int i = 0; i < asteroid.vertices.Count - 1; i++)
            {
                Vector2 v1 = asteroid.vertices.ElementAt(i);
                Vector2 v2 = asteroid.vertices.ElementAt(i + 1);
                Vector2 topLeft = new Vector2(border.Left, border.Top);
                Vector2 topRight = new Vector2(border.Right, border.Top);
                Vector2 bottomRight = new Vector2(border.Right, border.Bottom);
                Vector2 bottomLeft = new Vector2(border.Left, border.Bottom);

                if (Intersects(v1, v2, topLeft, topRight))
                {
                    asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
                    return true;
                }
                else if (Intersects(v1, v2, topRight, bottomRight))
                {
                    asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
                    return true;
                }
                else if (Intersects(v1, v2, bottomRight, bottomLeft))
                {
                    asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
                    return true;
                }
                else if (Intersects(v1, v2, bottomLeft, topLeft))
                {
                    asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
                    return true;
                }
            }

            asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
            return false;
        }

        private bool IsCollision(GameObject asteroid, GameObject asteroid2)
        {
            asteroid.vertices.Add(asteroid.vertices.First());
            asteroid2.vertices.Add(asteroid2.vertices.First());
            for (int i = 0; i < asteroid.vertices.Count - 1; i++)
            {
                for (int j = 0; j < asteroid2.vertices.Count - 1; j++)
                {
                    if (Intersects(asteroid.vertices.ElementAt(i), asteroid.vertices.ElementAt(i + 1),
                        asteroid2.vertices.ElementAt(j), asteroid2.vertices.ElementAt(j + 1)))
                    {
                        asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
                        asteroid2.vertices.RemoveAt(asteroid2.vertices.Count - 1);
                        return true;
                    }
                }
            }
            asteroid.vertices.RemoveAt(asteroid.vertices.Count - 1);
            asteroid2.vertices.RemoveAt(asteroid2.vertices.Count - 1);
            return false;
        }

        private bool Intersects(Vector2 line11, Vector2 line12, Vector2 line21, Vector2 line22)
        {
            float xmin, xmax, ymin, ymax;

            if (Overlaps(line11.X, line12.X, line21.X, line22.X))
            {
                xmin = MathHelper.Max(MathHelper.Min(line11.X, line12.X), MathHelper.Min(line21.X, line22.X));
                xmax = MathHelper.Min(MathHelper.Max(line11.X, line12.X), MathHelper.Max(line21.X, line22.X));
            }
            else return false;
            if (Overlaps(line11.Y, line12.Y, line21.Y, line22.Y))
            {
                ymin = MathHelper.Min(MathHelper.Min(line11.Y, line12.Y), MathHelper.Min(line21.Y, line22.Y));
                ymax = MathHelper.Max(MathHelper.Max(line11.Y, line12.Y), MathHelper.Max(line21.Y, line22.Y));
            }
            else return false;


            float m1, m2;
            if (line12.X - line11.X == 0)
            {
                m1 = float.MaxValue;
            }
            else
            {
                m1 = (line12.Y - line11.Y) / (line12.X - line11.X);
            }
            if (line22.X - line21.X == 0)
            {
                m2 = float.MaxValue;
            }
            else
            {
                m2 = (line22.Y - line21.Y) / (line22.X - line21.X);
            }
            float b1 = line11.Y - m1 * line11.X;
            float b2 = line21.Y - m2 * line21.X;

            if (m1 == m2)
            {
                if (b1 == b2)
                {
                    return true;
                }
                return false;
            }
            float x = (b2 - b1) / (m1 - m2);

            float y = m1 * x + b1;

            if (xmin <= x && x <= xmax && ymin <= y && y <= ymax)
            {
                return true;
            }

            return false;
        }

        private bool Overlaps(float x11, float x12, float x21, float x22)
        {
            float w11, w12, w21, w22;
            if (x11 > x12)
            {
                w11 = x12;
                w12 = x11;
            }
            else
            {
                w11 = x11;
                w12 = x12;
            }
            if (x21 > x22)
            {
                w21 = x22;
                w22 = x21;
            }
            else
            {
                w21 = x21;
                w22 = x22;
            }

            if (w11 > w22 || w21 > w12)
            {
                return false;
            }
            return true;
        }

        private static bool IsWithinRadius(GameObject asteroid, GameObject asteroid2)
        {
            return Vector2.Distance(asteroid.position, asteroid2.position) <= asteroid.radius + asteroid2.radius;
        }

        public void HandleCollisions(Stack<Collision> collisions)
        {
        }

        // will destroy any dead GameObjects from Game
        public void PurgeDead()
        {
            foreach (GameObject obj in asteroids)
            {
                if (!obj.alive)
                {
                    asteroids.Remove(obj);
                }
            }
        }


    }
}
