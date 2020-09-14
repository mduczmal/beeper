using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Beeper
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        private static Thread keyInterceptor;
        private static Thread beepPlayer;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                    beepPlayer.Abort();
                    keyInterceptor.Abort();
                    return true;
                case CtrlType.CTRL_LOGOFF_EVENT:
                    beepPlayer.Abort();
                    keyInterceptor.Abort();
                    return true;
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                    beepPlayer.Abort();
                    keyInterceptor.Abort();
                    return true;
                case CtrlType.CTRL_CLOSE_EVENT:
                    beepPlayer.Abort();
                    keyInterceptor.Abort();
                    return true;
                default:
                    return false;
            }
        }
        static void Main(string[] args)
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
            InterceptKeys interceptKeys = new InterceptKeys();
            Player player = new Player();
            beepPlayer = new Thread(new ThreadStart(Player.Init));
            keyInterceptor = new Thread(InterceptKeys.Init);
            beepPlayer.Start();
            keyInterceptor.Start(beepPlayer);
        }
    }
}
