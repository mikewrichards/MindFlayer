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
        private const float GRAVITATIONAL_CONSTANT = 75f;

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
                Vector2 unitVector = diffVect / diffVect.Length();

                Vector2 currentForce = (50f / diffVect.LengthSquared()) * GRAVITATIONAL_CONSTANT * unitVector;
                netForce += currentForce;
            }
            //netForce *= gameObject.mass;

            // gameObject.applyForce(netForce);
        }

        // calulate all collision forces in game
        // then will apply the force to the given object
        public void ApplyCollisionForceToObject(GameObject asteriod)
        {
            // woot
        }



        public Stack<Collision> GetCollisions()
        {
            Stack<Collision> collisions = new Stack<Collision>();
            foreach (GameObject asteroid in asteroids)
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


            return collisions;
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
            float xmin = MathHelper.Max(MathHelper.Min(line11.X, line12.X), MathHelper.Min(line21.X, line22.X));
            float ymin = MathHelper.Max(MathHelper.Min(line11.Y, line12.Y), MathHelper.Min(line21.Y, line22.Y));
            float xmax = MathHelper.Min(MathHelper.Max(line11.X, line12.X), MathHelper.Max(line21.X, line22.X));
            float ymax = MathHelper.Min(MathHelper.Max(line11.Y, line12.Y), MathHelper.Max(line21.Y, line22.Y));
            float m1 = (line12.Y - line11.Y) / (line12.X - line11.X);
            float m2 = (line22.Y - line21.Y) / (line22.X - line21.X);
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
        }


    }
}
