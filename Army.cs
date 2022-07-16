using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Army
    {
        private HashSet<WarShip> armyOfShip;
        private float armySpeed = 0.090f;
        private int armyWay = 1;
        private int shootProba = 500;

        /// <summary>
        /// This represent the speed of the army
        /// </summary>
        public float speed
        {
            get
            {
                return armySpeed;
            }
            set
            {
                armySpeed = value;
            }
        }

        /// <summary>
        /// This represent the probability of shoot for a warship, 500 by default
        /// </summary>
        public int ShootProba
        {
            get
            {
                return shootProba;
            }
            set
            {
                shootProba = value;
            }
        }

        /// <summary>
        /// This represent the way of the army, right or left
        /// </summary>
        public int way
        {
            get
            {
                return armyWay;
            }
            set
            {
                armyWay = value;
            }
        }

        public HashSet<WarShip> ArmyOfShip
        {
            get
            {
                return armyOfShip;
            }
        } 

        /// <summary>
        /// Create an hashset of army and put warship inside for line and column in parameters
        /// </summary>
        /// <param name="line"> number of ennemies per line</param>
        /// <param name="column">number of ennemies per column</param>
        public Army(int line,int column)
        {
            armyOfShip = new HashSet<WarShip>();
            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    WarShip warship = new WarShip(60 * j, (60 - i) * i, new Bitmap(Game.representations[i % 8]),this,(i+j).ToString("D4"));
                    armyOfShip.Add(warship);
                }
            }
        }

        /// <summary>
        /// This method return randomly a warship of the army
        /// </summary>
        /// <returns>warship of the army</returns>
        public WarShip getRandomShip()
        {
            WarShip rdm = armyOfShip.ElementAt(Game.randomNumber.Next(armyOfShip.Count));
            return rdm;
        }

        /// <summary>
        /// This method tests the edge's collision for the whole army
        /// </summary>
        /// <param name="gameSize"></param>
        /// <returns> 0 if a warship are too down, else 1 </returns>
        public int TestWallCollision(Size gameSize)
        {
            foreach (WarShip ship in armyOfShip)
            {
                if (ship.Xdata < -20)
                {
                    changeWay(1);
                    return 1;
                }
                if (ship.Xdata + ship.Representation.Width -20 >= gameSize.Width)
                {
                    changeWay(-1);
                    return 1;
                }
                if(ship.Ydata > 0.75 * gameSize.Height)
                {
                    return 0;
                }
            }
            return 1;
        }

        /// <summary>
        /// This method are called when a warship touch an edge, speed increases and shootprobality decreases
        /// All warship go down a little
        /// </summary>
        /// <param name="i"> 1 or -1, depend of the edge right or lef </param>
        public void changeWay(int i)
        {
            way = i;
            speed = (speed + 0.045f);
            if(Game.difficulty == 1) 
            {
                ShootProba -= 32;
            }
            if (Game.difficulty == 2)
            {
                ShootProba -= 62;
            }
            if (Game.difficulty == 3)
            {
                ShootProba -= 82;
            }
            if (Game.difficulty == 4)
            {
                ShootProba -= 102;
            }
            if (ShootProba <= 0)
            {
                ShootProba = 1;
            }
            foreach (WarShip ship in armyOfShip)
            {
                ship.Ydata = (ship.Ydata+10);
            }
        }


    }
}
