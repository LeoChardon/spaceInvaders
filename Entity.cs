using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    abstract class Entity
    {
        #region Field
        private Bitmap representation;
        private double x, y;
        private String property;
        private int life = 1;
        private bool alive = true;
        #endregion

        #region constructor

        /// <summary>
        /// This class is a base for all entity in game which can interact with others
        /// It has getter and setter for all of its attributes, x, y, bitmap represensation
        /// property, life, and boolean is alive or not. It hasn't attributes in its
        /// constructor for more flexibility of its childrens
        /// </summary>
        public Entity()
        {
           
        }
        #endregion

        /// <summary>
        /// This method is given for all drawing of an entity
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="graphics"></param>

        #region method
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        /// This method is given for updated an entity in the time
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public abstract void Update(Game gameInstance, double deltaT);
        #endregion

        #region getterSetter
        public bool IsAlive
        {
            get
            {
                return this.alive;
            }
            set
            {
                this.alive = value;
            }

        }

        public Bitmap Representation
        {
            get
            {
                return this.representation;
            }
            set
            {
                this.representation = value;
            }
        }

        public double Xdata
        {
            get
            {
                return x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Ydata
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Thanks to this attribute, collision can be managed correctly
        /// </summary>
        public String Property
        {
            get
            {
                return this.property;
            }
            set
            {
                this.property = value;
            }
        }

        public int Life
        {
            get
            {
                return this.life;
            }
            set
            {
                this.life = value;
            }
        }

        #endregion

        #region collisions

        /// <summary>
        /// Destroy random range pixel from an impact gived
        /// </summary>
        /// <param name="way"> -1 or 1</param>
        /// <param name="rnd"> random number </param>
        /// <param name="entity">bunker</param>
        /// <param name="xIntersec">coordonate x of the collision</param>
        /// <param name="yIntersec">coordonate y of the collision</param>
        private static void DestroyPixelRandom(int way,int rnd, Entity entity, double xIntersec, double yIntersec)
        {
            for (int k = 0; k < rnd; k++)
            {
                for (int l = 0; l < rnd; l++)
                {
                    if (yIntersec + k*way >= 0 && yIntersec + k*way < entity.Representation.Height)
                    {
                        entity.Representation.SetPixel((int)xIntersec, (int)yIntersec + k*way, Color.FromArgb(0, 255, 255, 255));
                    }
                    if (yIntersec + k*way >= 0 && yIntersec + k*way < entity.Representation.Height)
                    {
                        if (xIntersec - l >= 0 && xIntersec - l < entity.Representation.Width)
                        {
                            entity.Representation.SetPixel((int)xIntersec - l, (int)yIntersec + k * way, Color.FromArgb(0, 255, 255, 255));
                        }
                    }
                    if (yIntersec + k*way >= 0 && yIntersec + k*way < entity.Representation.Height)
                    {
                        if (xIntersec + l >= 0 && xIntersec + l < entity.Representation.Width)
                        {
                            entity.Representation.SetPixel((int)xIntersec + l, (int)yIntersec + k * way, Color.FromArgb(0, 255, 255, 255));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Collisions effect between two entities
        /// </summary>
        /// <param name="entity">Entity one</param>
        /// <param name="test">Entity two</param>
        /// 
        private static void CollisionsEffect(Entity entity, Entity test)
        {
            double xIntersec;
            double yIntersec;

            for (double i = 0; i < test.Representation.Height; i++)
            {
                for (double j = 0; j < test.Representation.Width; j++)
                {
                    xIntersec = (test.Xdata) - (entity.Xdata) + j;
                    yIntersec = (test.Ydata) - (entity.Ydata) + i;
                    if (entity.Representation.GetPixel((int)xIntersec, (int)yIntersec) == Color.FromArgb(255, 0, 0, 0))
                    {
                        entity.Representation.SetPixel((int)xIntersec, (int)yIntersec, Color.FromArgb(0, 255, 255, 255));
                        if ((test.Property.Equals("ia") || test.Property.Equals("player")) && entity.Property.Equals("bunker"))
                        {
                            int rnd = Game.randomNumber.Next(2, 5);
                            Missile m = (Missile)test;
                            if (m.Way == 1)
                            {
                                DestroyPixelRandom(1,rnd, entity, xIntersec, yIntersec);
                            }
                            if (m.Way == -1)
                            {
                                DestroyPixelRandom(-1, rnd, entity, xIntersec, yIntersec);
                            }
                        }
                        
                        
                        if (!test.Property.Equals("bunker"))
                        {
                            EntityTouched(test);
                        }
                        if (!entity.Property.Equals("bunker"))
                        {
                            EntityTouched(entity);
                        }
                    }
                    return;
                }
            }
        }
        /// <summary>
        /// Removes a life from an entity and kill it if it has not anymore
        /// </summary>
        /// <param name="entity">the entity</param>
        private static void EntityTouched(Entity entity)
        {
            entity.Life -= 1;
            if (entity.Life == 0)
            {
                Sound.explode.Play();
                entity.IsAlive = false;
            }
        }

        /// <summary>
        /// Test collisions with all entity in an hashset
        /// </summary>
        /// <param name="entitiesH">hashset name</param>
        public static void TestAnyCollisions(HashSet<Entity> entities)
        {
            foreach(Entity entity in entities)
            {
                foreach(Entity test in entities)
                {

                    if (entity == test)
                    {
                        continue;
                    }
                    if (entity.Property.Equals(test.Property))
                    {
                        continue;
                    }
                    TestCollisionsBetween(entity, test);

                }
            }
        }

        /// <summary>
        /// Test collisions between two entities
        /// </summary>
        /// <param name="entity">Entity one</param>
        /// <param name="test">Entity two</param>
        private static void TestCollisionsBetween(Entity entity, Entity test)
        {
            if (test.Xdata > entity.Xdata && test.Xdata < entity.Xdata + entity.Representation.Width)
            {
                if (test.Ydata > entity.Ydata && test.Ydata < entity.Ydata + entity.Representation.Height)
                {
                    CollisionsEffect(entity, test);
                }
            }
        }
        #endregion
    }

}
