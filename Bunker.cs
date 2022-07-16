using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Bunker: Entity
    {
        /// <summary>
        /// Create bunker
        /// </summary>
        /// <param name="x">X coordonate</param>
        /// <param name="y">Y coordonate</param>
        public Bunker(double x, double y)
        {
            Representation = new Bitmap(SpaceInvaders.Properties.Resources.bunker);
            Xdata = x;
            Ydata = y;
            Property = "bunker";
        }

        /// <summary>
        /// Draw its representation
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Representation, (float)Xdata, (float)Ydata);
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            
        }
    }
}
