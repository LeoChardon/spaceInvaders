using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{

    class Missile : Entity
    {
        #region Fields

        private double speed = 400;

        private int way;

        private String instance;



        #endregion

        #region Constructor
        /// <summary>
        /// Create missile, the way depend of its side, "ia" or "player"
        /// </summary>
        /// <param name="property">property of the missile for collision, "ia" or "player"</param>
        /// <param name="x">X coordonate</param>
        /// <param name="y">Y coordonate</param>
        /// <param name="instance">Name of the missile, for the equals method for unique missile</param>
        public Missile(String property, double x, double y,String instance) : base()
        {
            if (property == "player")
            {
                Representation = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);
            }
            else
            {
                Representation = new Bitmap(SpaceInvaders.Properties.Resources.shoot1);
            }
            this.Property = property;
            if (Property.Equals("player"))
            {
                this.way = -1;
            }
            else
            {
                this.way = 1;
            }
            this.Xdata = x;
            this.Ydata = y;
            this.instance = instance;
        
        }
        #endregion

        #region Methods

        private String Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public int Way
        {
            get
            {
                return way;
            }
        }

        /// <summary>
        /// The missile goes up or goes down, it depend of its way
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Ydata += (speed*way) * deltaT;
            if (Ydata < 0)
                IsAlive = false;
            if (Ydata > gameInstance.gameSize.Height)
                IsAlive = false;
        }


        /// <summary>
        /// Draw the missile 
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Representation, (float)Xdata, (float)Ydata);
        }


        public override bool Equals(object obj)
        {
            var missile = obj as Missile;
            return missile != null &&
                   Instance == missile.Instance &&
                   Instance == missile.Instance;
        }

        public override int GetHashCode()
        {
            var hashCode = -115223568;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Instance);
            hashCode = hashCode * -1521134295 + Instance.GetHashCode();
            return hashCode;
        }




        #endregion


    }



}
