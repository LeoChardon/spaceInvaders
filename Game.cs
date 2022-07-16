using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {

        #region Entitys management

        public HashSet<Entity> entities = new HashSet<Entity>();
        public Army army;
        private SpaceShip spaceShip;
        private int playerImg = 0;
        private HashSet<Entity> pendingNewEntitys = new HashSet<Entity>();
        public static Bitmap[] representations = { SpaceInvaders.Properties.Resources.ship1,
        SpaceInvaders.Properties.Resources.ship2,
        SpaceInvaders.Properties.Resources.ship3,
        SpaceInvaders.Properties.Resources.ship4,
        SpaceInvaders.Properties.Resources.ship5,
        SpaceInvaders.Properties.Resources.ship6,
        SpaceInvaders.Properties.Resources.ship7,
        SpaceInvaders.Properties.Resources.ship8,
        SpaceInvaders.Properties.Resources.ship9,};

        public void AddNewEntity(Entity Entity)
        {
            pendingNewEntitys.Add(Entity);
        }
        #endregion

        #region game technical elements

        public Size gameSize;
        private bool beforeGame = true;
        public static int difficulty = 1;
        private bool isPause = false;
        private bool gameOverWin = false;
        private bool gameOverLoose = false;
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        public static Random randomNumber = new Random();
        public static Game game { get; private set; }
        private static Brush blackBrush = new SolidBrush(Color.Black);
        private static Brush redBrush = new SolidBrush(Color.Red);
        private static Brush orangeRedBrush = new SolidBrush(Color.OrangeRed);
        private static Brush orangeBrush = new SolidBrush(Color.Orange);
        private static Brush greenBrush = new SolidBrush(Color.Green);
        private static Brush blueBrush = new SolidBrush(Color.Blue);
        private static Font titleFont = new Font("Times New Roman", 50, FontStyle.Bold, GraphicsUnit.Pixel);
        private static Font normalFont = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel);

        #endregion


        #region constructors
        /// <summary>
        /// create and return the game object 
        /// </summary>
        /// <param name="gameSize">size of the screen</param>
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Create the size of the game
        /// </summary>
        /// <param name="gameSize"></param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
        }

        #endregion

        #region methods

        /// <summary>
        /// Clear all hashset
        /// </summary>
        private void Clear()
        {
            pendingNewEntitys.Clear();
            entities.Clear();
        }

        /// <summary>
        /// Create all entities for the game
        /// </summary>
        /// <param name="gameSize"></param>
        private void LoadGame(Size gameSize)
        {
            Clear();
            this.spaceShip = new SpaceShip(representations[playerImg]);
            createTheArmy();
            createTheBunkers();
            entities.Add(spaceShip);
            gameOverWin = false;
            gameOverLoose = false;
        }

        /// <summary>
        /// Create 3 bunker
        /// </summary>
        private void createTheBunkers()
        {
            for (int i = gameSize.Width / 8; i < this.gameSize.Width; i += gameSize.Width / 3)
                entities.Add(new Bunker((double)i, 0.85 * gameSize.Height));
        }

        /// <summary>
        /// Create the army of warship, depending of the difficulty
        /// </summary>
        private void createTheArmy()
        {
            if (difficulty == 1)
            {
                army = new Army(4, 5);
            }
            if (difficulty == 2)
            {
                army = new Army(4, 6);
            }
            if (difficulty == 3)
            {
                army = new Army(5, 7);
            }
            if (difficulty == 4)
            {
                army = new Army(6, 8);
            }

            foreach (WarShip ship in army.ArmyOfShip)
            {
                entities.Add(ship);
            }
        }

        /// <summary>
        /// Remove the key of the hashset of keys, if in game, stop the spaceship player
        /// </summary>
        /// <param name="key"></param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
            if (!beforeGame)
            {
                if (!keyPressed.Contains(Keys.Left) || !keyPressed.Contains(Keys.Right))
                {
                    spaceShip.SpeedShip = 0;
                }
            }
        }


        /// <summary>
        /// Draw all text and entities, depending of the game state
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (isPause)
            {
                g.DrawString("PAUSE", titleFont, blackBrush, 220, (float)0.5 * gameSize.Height);
                g.DrawString("Appuyez sur [P] pour reprendre", normalFont, blackBrush, 200, 380);
                g.DrawString("Appuyez sur [S] pour revenir au menu", normalFont, blackBrush, 200, 410);
            }
            if (gameOverWin)
            {
                g.DrawString("Gagné!", titleFont, blackBrush, 220, (float)0.5 * gameSize.Height);
                g.DrawString("Appuyez sur [espace] pour recommencer", normalFont, blackBrush, 200, 380);
                g.DrawString("Appuyez sur [S] pour revenir au menu", normalFont, blackBrush, 200, 410);
            }
            if (gameOverLoose)
            {
                g.DrawString("Perdu..", titleFont, blackBrush, 220, (float)0.5 * gameSize.Height);
                g.DrawString("Appuyez sur [espace] pour recommencer", normalFont, blackBrush, 200, 380);
                g.DrawString("Appuyez sur [S] pour revenir au menu", normalFont, blackBrush, 200, 410);
            }
            if (beforeGame)
            {
                g.DrawString("SPACE INVADERS", titleFont, blackBrush, 100, 100);
                g.DrawString("Créé par Léo Chardon en E3FI 2019-2020, projet de POO. ESIEE Paris", normalFont, blackBrush, 80, 160);
                g.DrawString("[S] lancer une partie", normalFont, blackBrush, 150, 260);
                g.DrawString("[D] changer la difficulté. Actuelle: ", normalFont, blackBrush, 150, 360);
                if (difficulty == 1)
                {
                    g.DrawString("facile :)", normalFont, greenBrush, 400, 360);
                }
                if (difficulty == 2)
                {
                    g.DrawString("normal", normalFont, orangeBrush, 400, 360);
                }
                if (difficulty == 3)
                {
                    g.DrawString("difficile !", normalFont, orangeRedBrush, 400, 360);
                }
                if (difficulty == 4)
                {
                    g.DrawString("impossible !!!!!!", normalFont, redBrush, 400, 360);
                }

                g.DrawString("[I] changer l'icon de votre vaisseau: ", normalFont, blackBrush, 150, 460);
                g.DrawImage(new Bitmap(representations[playerImg]), 420, 460);
            }

            foreach (Entity entity in entities)
                entity.Draw(this, g);
        }

        #region update methods

        /// <summary>
        /// This method manage the before game menu
        /// </summary>
        /// <param name="deltaT"></param>
        private void UpdateInBeforeGame (double deltaT)
         {
            if (keyPressed.Contains(Keys.S))
            {
                beforeGame = false;
                LoadGame(gameSize);
                ReleaseKey(Keys.Space);
            }
            if (keyPressed.Contains(Keys.D))
            {
                difficulty += 1;
                if (difficulty > 4)
                {
                    difficulty = 1;
                }
                ReleaseKey(Keys.D);
            }
            if (keyPressed.Contains(Keys.I))
            {
                playerImg += 1;
                if (playerImg > 8)
                {
                    playerImg = 0;
                }
                ReleaseKey(Keys.I);
            }
        }
        
        /// <summary>
        /// This method manage the pause menu.
        /// </summary>
        /// <param name="deltaT"></param>
        private void PauseMenu(double deltaT)
        {
            if (keyPressed.Contains(Keys.P))
            {
                if (isPause == true)
                {
                    isPause = false;
                }
                else if (gameOverLoose == false && gameOverWin == false)
                {
                    isPause = true;
                }
                ReleaseKey(Keys.P);

            }

            if (isPause == true)
            {
                if (keyPressed.Contains(Keys.S))
                {
                    beforeGame = true;
                    gameOverLoose = false;
                    gameOverWin = false;
                    isPause = false;
                    Clear();
                    ReleaseKey(Keys.S);
                    return;
                }
            }
        }

        /// <summary>
        /// This method manage a game.
        /// A missile appears when the player press space.
        /// The player can control the spaceship.
        /// Missile enemy can appears randomly.
        /// All entities are updated here.
        /// </summary>
        /// <param name="deltaT"></param>
        private void GameUpdateAction(double deltaT)
        {
            if (keyPressed.Contains(Keys.Space))
            {
                Entity missile = new Missile("player", spaceShip.Xdata + (spaceShip.Representation.Width) / 2, spaceShip.Ydata, "player");

                //Sound.PlayShoot();
                AddNewEntity(missile);

                ReleaseKey(Keys.Space);
            }

            if (keyPressed.Contains(Keys.Left))
            {
                spaceShip.SpeedShip = -1;

            }

            if (keyPressed.Contains(Keys.Right))
            {
                spaceShip.SpeedShip = 1;

            }


            foreach (Entity Entity in entities)
            {
                Entity.Update(this, deltaT);
            }

            WarShip rdmShip = army.getRandomShip();
            Entity missil = rdmShip.getMissileRdm();
            if (missil != null)
            {
                Sound.shoot.Play();
                AddNewEntity(missil);
            }

            if (army.TestWallCollision(this.gameSize) == 0)
            {
                gameOverLoose = true;
                Clear();
            }
            Entity.TestAnyCollisions(entities);

            entities.RemoveWhere(entity => !entity.IsAlive);
            army.ArmyOfShip.RemoveWhere(entity => !entity.IsAlive);

            if (army.ArmyOfShip.Count == 0)
            {
                gameOverWin = true;
                Clear();
                Sound.win.Play();
            }

            if (!spaceShip.IsAlive)
            {
                gameOverLoose = true;
                Clear();
                Sound.loose.Play();
            }
        }

        /// <summary>
        /// This method manage the GameOver menu
        /// </summary>
        /// <param name="deltaT"></param>
        private void GameOverMenu(double deltaT)
        {
            if ((gameOverLoose == true || gameOverWin == true))
            {
                if (keyPressed.Contains(Keys.Space))
                {
                    LoadGame(gameSize);
                    ReleaseKey(Keys.Space);
                }
                if (keyPressed.Contains(Keys.S))
                {
                    beforeGame = true;
                    gameOverLoose = false;
                    gameOverWin = false;
                    isPause = false;
                    Clear();
                    ReleaseKey(Keys.S);
                    return;
                }
            }
        }

        /// <summary>
        /// The main fonction Update for all the spaceInvaders.
        /// </summary>
        /// <param name="deltaT"></param>
        public void Update(double deltaT)
        {
            if (beforeGame)
            {
                UpdateInBeforeGame(deltaT);
            }
            else 
            { 
                entities.UnionWith(pendingNewEntitys);
                pendingNewEntitys.Clear();

                PauseMenu(deltaT);

                GameOverMenu(deltaT);

                if (isPause == false && gameOverLoose == false && gameOverWin == false && beforeGame == false)
                {
                    GameUpdateAction(deltaT);

                }
            }
        }
        #endregion
        #endregion
    }
}
