using System;

namespace GP_Final_Catapult {
#if WINDOWS || LINUX
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
}
