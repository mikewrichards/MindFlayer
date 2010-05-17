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

        // will calulate all gravitational forces in game
        // then will apply the force to the given object
        public void ApplyGravityToObject(GameObject asteriod)
        {
            // woot
        }

        // will calulate all collision forces in game
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
