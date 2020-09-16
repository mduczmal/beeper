using System;
using System.Media;
using System.Threading;

namespace Beeper
{
    class Player
    {
        public static void Init()
        {
            SoundPlayer beep06 = new SoundPlayer(Properties.Resources.beep_06);
            SoundPlayer beep07 = new SoundPlayer(Properties.Resources.beep_07);
            SoundPlayer beep08 = new SoundPlayer(Properties.Resources.beep_08);
            SoundPlayer beep09 = new SoundPlayer(Properties.Resources.beep_09);
            while (true)
                try
                {
                    Console.WriteLine("You have 2 minutes");
                    Thread.Sleep(50000);
                    beep06.Play();
                    Console.WriteLine("1 minute 10 seconds left");
                    Thread.Sleep(10000);
                    beep07.Play();
                    Console.WriteLine("1 minute left");
                    Thread.Sleep(50000);
                    beep08.Play();
                    Console.WriteLine("10 seconds left");
                    Thread.Sleep(10000);
                    beep09.Play();
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("Timer restarted");
                }
        }
    }
}
