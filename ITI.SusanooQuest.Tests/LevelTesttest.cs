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
            Ennemy sut = new Ennemy(new Vector(10,10), 15,game , 20, 20);
            Ennemy ennemy = new Ennemy( new Vector(20,20), 15, game, 20, 20);
            List<Ennemy> ennemies = new List<Ennemy>();
            ennemies.Add(sut);
            ennemies.Add(ennemy);

            Player player = new Player(new Vector(0, 0), 50, game, 100, 10);
            Projectile projectile = new Projectile(15, 100, ennemy.Position, player, "cosY");
            Projectile projectile2 = new Projectile(20, 100, player.Position, player, "cosY");
            List<Projectile> projectiles = new List<Projectile>();
            projectiles.Add(projectile2);
            projectiles.Add(projectile);

            SerializeLevel lvt = new SerializeLevel("level1", ennemies, projectiles,player);

            lvt.Serialize(@"..\level.json");
        }

    }
}
