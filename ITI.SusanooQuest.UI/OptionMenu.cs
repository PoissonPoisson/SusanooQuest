using System;
using System.Collections.Generic;
using ITI.SusanooQuest.Lib;
using SFML.Window;

namespace ITI.SusanooQuest.UI
{
    public class OptionMenu : IMenu
    {
        public IMenu GetNextMenu => throw new NotImplementedException();

        public bool IsUpdate
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void MouseButtonPressed(MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void KeyPressed(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
