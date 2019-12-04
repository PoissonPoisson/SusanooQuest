using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class Level
    {
        public List<Ennemy> _enemies;
        readonly string _name;
        public List<Projectile> _projectile;
        Player _player; 
        
        public List<Ennemy> EnemyList => _enemies;
        public List<Projectile> ProjectileList => _projectile;

        public Level(string name, List<Ennemy> ennemies, List<Projectile> projectile, Player player)
        {
            _enemies = ennemies;
            _name = name;
            _projectile = projectile;
            _player = player;
        }

        public void  Serialize(string path)
        {

            JObject j = new JObject(
                 new JProperty("Name", _name),
                 new JProperty("Ennemies", _enemies.Select(i => i.Serialize()))
               );

            JObject p = new JObject(
                new JProperty("Name", _name),
                new JProperty("Player", _player.SerializePlayer()));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate,
      FileAccess.ReadWrite, FileShare.None))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonTextWriter jw = new JsonTextWriter(sw))


            {
                j.WriteTo(jw);
           
                p.WriteTo(jw);
            }

        }


    }
}
