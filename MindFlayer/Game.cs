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
    public class Game
    {
        public Rectangle border;
        public Game(Rectangle initBorder)
        {
            border = initBorder;
        }

        public void Update()
        {
        }

        public Stack<Collision> GetCollisions()
        {
            return new Stack<Collision>();
        }

        public void HandleCollisions(Stack<Collision> collisions)
        {
        }

        public void purgeDead()
        {
        }
    }
}
