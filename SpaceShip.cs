using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SpaceShip : Entity
    {
        private double speedShip;
        private Font drawFont = new Font("Arial", 16);
        private Brush goodBrush = new SolidBrush(Color.Green);
        private Brush medBrush = new SolidBrush(Color.Orange);
        private Brush badBrush = new SolidBrush(Color.Red);

        /// <summary>
        /// value for incremente the x coordonate of the spaceship 
        /// </summary>
        public double SpeedShip
        {
            get
            {
                return speedShip;
            }
            set
            {
                speedShip = value;
            }
        }

        /// <summary>
        /// Create a spaceship for the game, set its property to "player" and set its life depends of the game difficulty
        /// </summary>
        /// <param name="representation">The bitmap for draw the spaceship</param>
        public SpaceShip(Bitmap representation) : base()
        {
            this.Xdata = 300;
            this.Ydata = 570;
            if(Game.difficulty == 1)
            {
                this.Life = 6;
            }
            else if (Game.difficulty == 4)
            {
                this.Life = 2;
            }
            else
            {
                this.Life = 4;
            }
            Representation = new Bitmap(representation);
            Property = "player";
        }


        /// <summary>
        /// draw representation and the level of life
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {

            graphics.DrawImage(Representation, (float)Xdata,(float) Ydata);
            //graphics.DrawRectangle(new Pen(Color.Black), (float)Xdata, (float)Ydata, Representation.Width, Representation.Height);
            if(Life >= 3)
            {
                graphics.DrawString("Life: " + Life, this.drawFont, this.goodBrush, 0, 580);
            }
            if(Life < 3)
            {
                graphics.DrawString("Life: " + Life, this.drawFont, this.medBrush, 0, 580);
            }
            if(Life == 1)
            {
                graphics.DrawString("Life: " + Life, this.drawFont, this.badBrush, 0, 580);
            }
            
        }

        /// <summary>
        /// deplace the spaceship depending of its speedship
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {

            if(SpeedShip < 0)
            {
                if(!(Xdata + SpeedShip < 0))
                {
                    Xdata += SpeedShip;
                } 
            }
            if(SpeedShip > 0)
            {
                if(!(Xdata + SpeedShip + Representation.Width >= gameInstance.gameSize.Width))
                {
                    Xdata += SpeedShip;
                }
            }
        }
    }
}
