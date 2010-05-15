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

        public void Update()
        {
            foreach(GameObject asteroid in asteroids)
            {
                asteroid.UpdateObject();
            }
        }

        public Stack<Collision> GetCollisions()
        {
            return new Stack<Collision>();
        }

        public void HandleCollisions(Stack<Collision> collisions)
        {
        }

        public void PurgeDead()
        {
        }
    }
}
