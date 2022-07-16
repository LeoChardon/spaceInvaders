using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SpaceInvaders
{
    /// <summary>
    /// This class contains all sounds for the game
    /// </summary>
    class Sound
    {

        public static SoundPlayer shoot = new SoundPlayer(Properties.Resources.shoot);
        public static SoundPlayer win = new SoundPlayer(Properties.Resources.win);
        public static SoundPlayer loose = new SoundPlayer(Properties.Resources.loose);
        public static SoundPlayer desinc1 = new SoundPlayer(Properties.Resources.desinc1);
        public static SoundPlayer desinc2 = new SoundPlayer(Properties.Resources.desint2);
        public static SoundPlayer explode = new SoundPlayer(Properties.Resources.explode);

        public static MediaPlayer player = new System.Windows.Media.MediaPlayer();
        public static void playr()
        {
            //Sound.player.Open(Properties.Resources.explode);
            Sound.player.Play();
        }
    }


}
