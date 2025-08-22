using Lecture_SmallGame.SmallEngine;
using Lecture_SmallGame.SmallEngine.Components;

namespace Lecture_SmallGame;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();

        bool colorTest = false;

        if (colorTest)
        {
            ColorTest();
        }
        else
        {
            Player player = Engine.Instantiate<Player>(new(Writer.Width / 2, Writer.Height / 2));
            player.Transform.Name = "Player";

            CoinSpawner spawner = Engine.Instantiate<CoinSpawner>();
        }

        // CHECK Is there some sort of better way to not let the app end?
        Task.Run(() => { while (true) Task.Delay(1_000_000).Wait(); }).Wait();
    }

    private static void ColorTest()
    {
        Color color1 = new Color(120, 10, 200);
        Color color2 = new Color(220, 110, 50);

        GameObject someText = Engine.Instantiate<GameObject>();
        TextRenderer renderer1 = someText.AddComponent<TextRenderer>();
        Task.Delay(1000).Wait();
        renderer1.Text = "This is a test line.";
        Task.Delay(1000).Wait();
        renderer1.ColorTopLeft = color1;
        Task.Delay(1000).Wait();
        renderer1.ColorTopRight = color2;
    }
}
