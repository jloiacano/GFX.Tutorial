using System;

namespace GFX.Tutorial
{
    internal class EntryPoint
    {
        [STAThread] //Single thread attribute
        private static void Main() => new Client.Program().Run();

        // above same as: 

        //private static void Main()
        //{
        //    new Client.Program().Run();
        //}
    }
}
