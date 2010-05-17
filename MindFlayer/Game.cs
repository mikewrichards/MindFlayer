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
                float distance = diffVect.Length();
                Vector2 unitVector = diffVect / distance;

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
            return new Stack<Collision>();
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
