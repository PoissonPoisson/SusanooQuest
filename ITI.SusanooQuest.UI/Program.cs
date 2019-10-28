using System;
using SFML.Window;
using SFML.Graphics;

namespace ITI.SusanooQuest.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RenderWindow window = new RenderWindow(VideoMode.FullscreenModes[0], "SusanooQuest.UI", Styles.Close | Styles.Titlebar ))
            {
                IMenu currentMenu = new MainMenu(window);

                window.Closed += (s, e) => window.Close();

                window.KeyPressed += (s, e) =>
                {
                    currentMenu.KeyPressed(e);
                };

                window.MouseButtonPressed += (s, e) =>
                {
                    currentMenu.MouseButtonPressed(e);
                    Console.WriteLine(e);
                };

                while(window.IsOpen)
                {
                    window.DispatchEvents();
                    currentMenu = currentMenu.GetNextMenu;
                    if (currentMenu.IsUpdate)
                    {
                        currentMenu.Render();
                        currentMenu.IsUpdate = false;
                        window.Display();
                    }
                }
            }
        }
    }
}
