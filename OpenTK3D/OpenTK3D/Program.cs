namespace OpenTK3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(1200, 500)) {
                game.Run();
            }
        }
    }
}
