using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public class LevelTest
    {
        public List<Ennemy> _enemies;
        readonly string _name;
        public List<Ennemy> EnemyList => _enemies;

        public LevelTest(string name, List<Ennemy> ennemies)
        {
            _enemies = ennemies;
            _name = name;
        }

        public void  Serialize(string path)
        {

           JObject j =  new JObject(
                new JProperty("Name", _name),
                new JProperty("Ennemies", _enemies.Select(i => i.Serialize()))
                );

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate,
      FileAccess.ReadWrite, FileShare.None))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonTextWriter jw = new JsonTextWriter(sw))
            {
                j.WriteTo(jw);
            }


        }
    }
}
