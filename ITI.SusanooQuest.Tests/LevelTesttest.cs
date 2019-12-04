using ITI.SusanooQuest.Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace ITI.SusanooQuest.Tests

{
    [TestFixture]
    class LevelTesttest
    {
        [Test]

        public void Serialize()
        {
            Game game = new Game(500, 3, 0) ;
            Ennemy sut = new Ennemy(0,new Vector(0,0), 15,game , 20, 20);
            Ennemy ennemy = new Ennemy(0, new Vector(0, 0), 15, game, 20, 20);
            List<Ennemy> ennemies = new List<Ennemy>();
            ennemies.Add(sut);
            ennemies.Add(ennemy);

            LevelTest lvt = new LevelTest("level1", ennemies);

            lvt.Serialize(@"..\level.json");
        }
    }
}
