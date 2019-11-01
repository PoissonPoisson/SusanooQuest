using System;
using SFML.Window;
using SFML.Graphics;

namespace ITI.SusanooQuest.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RenderWindow window = new RenderWindow(VideoMode.FullscreenModes[0], "SusanooQuest", Styles.Default ))
            {
                IController currentMenu = new MainMenu(window);

                window.Closed += (s, e) => window.Close();

                window.KeyPressed += (s, e) =>
                {
                    currentMenu.KeyPressed(e);
                    Console.WriteLine(e);
                };

                window.MouseButtonPressed += (s, e) =>
                {
                    currentMenu.MouseButtonPressed(e);
                    Console.WriteLine(e);
                };
                
                while(window.IsOpen)
                {
                    window.DispatchEvents();

                    if (currentMenu != currentMenu.GetNextMenu) currentMenu.Dispose();
                    currentMenu = currentMenu.GetNextMenu;

                    window.Clear();

                    //currentMenu.Update();
                    currentMenu.Render();

                    window.Display();
                }
            }
        }
    }
}
