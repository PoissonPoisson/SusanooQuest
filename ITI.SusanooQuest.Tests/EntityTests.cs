using System;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using ITI.SusanooQuest.UI;
using ITI.SusanooQuest.Lib;

namespace ITI.SusanooQuest.Tests
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Add_Valid_Ennemy()
        {
            Game game = new Game();

            game.Create_Ennemy(new Vector(), 5, game, 1000);
            Entity a = game.Create_Ennemy(new Vector(), 5, game, 3000);

            List<Ennemy> ennemies = game.Ennemy;

            Assert.That(ennemies.Count, Is.EqualTo(2));
            Assert.That(ennemies[1], Is.EqualTo(a));
        }


        
    }
}
