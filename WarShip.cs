using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class WarShip : Entity
    {
 
        private Army army;
        private String numberInArmy;

        /// <summary>
        /// This Class represents an enemy of the army
        /// </summary>
        /// <param name="x">X coordonate</param>
        /// <param name="y">Y coordonate</param>
        /// <param name="representation">bitmap representation</param>
        /// <param name="army">reference to its army</param>
        /// <param name="number">string given for unique warship in the army</param>
        public WarShip(double x, double y, Bitmap representation, Army army, String number): base()
        {
            this.army = army;
            Representation = representation;
            this.Xdata = x + 35 - Representation.Width/2;
            this.Ydata = y;
            Property = "ia";
            numberInArmy = number;

        }

        /// <summary>
        /// Draw the warship
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Representation, (float)Xdata , (float)Ydata);
            

        }

        /// <summary>
        /// This method has 1 chance on 500 (at the begin) to return missile. It depends of army ShootProba
        /// </summary>
        /// <returns>return an ennemy missile to add in the entities hashset</returns>
        public Missile getMissileRdm()
        {
            Missile missile = null;
            int proba = Game.randomNumber.Next(1, army.ShootProba);
            //int proba = 1;
            Console.WriteLine("" + proba);
            if (proba == 1)
            { 
                missile = new Missile("ia", this.Xdata + this.Representation.Width, this.Ydata, numberInArmy);
            }
            
           return missile; 
        }

        /// <summary>
        /// deplace the warship, depends of the army way (left or right) and army speed
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Xdata += (army.speed * army.way);
        }

    }
}
