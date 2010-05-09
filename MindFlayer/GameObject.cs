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
            position = initialPosition;
            velocity = initialVelocity;
            size = initialSize;
            colour = initialColour;

            // create some awesome vertices
        }



        internal void updatePosition()
        {
            //method for updating object's position based on speed, etc.
        }
    }
}
