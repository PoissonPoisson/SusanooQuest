using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    class LevelTwo : ILevels
    {
        readonly List<Ennemy> _ennemiesAlive;
        readonly List<Ennemy> _ennemiesDeath;

        Game _context;
        int _i = 0;

        public List<Ennemy> Alive => _ennemiesAlive;         

        public List<Ennemy> Dead => _ennemiesDeath;
       

        
        public LevelTwo(List<Ennemy> ennemiesAlive, List<Ennemy> ennemiesDeath, Game ctx)
        {
            _ennemiesAlive = ennemiesAlive;
            _ennemiesDeath = ennemiesDeath;
            _context = ctx;
        }


        DateTime _comparaison = DateTime.Now;
        DateTime _LaunchNewVague = DateTime.Now.AddSeconds(10);
        public void Chronometer()
        {            
            if (_comparaison < _LaunchNewVague) ReStartChronometer();
            else Update();         
        }

        public void ReStartChronometer()
        {
            System.Threading.Thread.Sleep(1000);
            _comparaison = DateTime.Now;
            Chronometer();
        }


        public void Update()
        {            
            NextVague();
        }
         

        public void NextVague()
        {
          switch (_i)
            {
                case 0:                    
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard");
                        _context.Update();                    
                    _i++;
                    Chronometer();
                    break;                   
                case 1:
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(5, 100), 20, _context, 25, 5, "standard");
                        _context.Update();                                       
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 2:                    
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(50, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(40, 60), 20, _context, 25, 5, "standard");
                        _context.Update();                    
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 3:
                    
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(50, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(40, 60), 20, _context, 25, 5, "standard");
                        _context.Update();
                    _i++;                    
                    if (_context.Ennemy.Count() == 0)
                        Update();                   
                    break;
                case 4:
                    if (_context.Ennemy.Count() == 0) FirstBoss();
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 5:                   
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 40), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(50, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(40, 60), 20, _context, 25, 5, "standard");
                        _context.Update();
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 6:                    
                        _context.CreateEnnemy(new Vector(100, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(100, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(100, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(100, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(100, 40), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(50, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(40, 100), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 80), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 70), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 60), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 50), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(90, 40), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(80, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(70, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(60, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(50, 90), 20, _context, 25, 5, "standard");
                        _context.CreateEnnemy(new Vector(40, 90), 20, _context, 25, 5, "standard");
                        _context.Update();                    
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 7:
                    if (_context.Ennemy.Count() == 0) Boss();
                    _i++;
                    if (_context.Ennemy.Count() == 0)
                        Update();
                    break;
                case 8:
                    if (_context.Ennemy.Count() == 0) NextLevel();
                    break;
            }
        }



            public void Boss()
        {
            _context.CreateEnnemy(new Vector(100, 100), 75, _context, 500, 8, "boss");
            _context.Update();
        }

        public void FirstBoss()
        {
            _context.CreateEnnemy(new Vector(100, 100), 50, _context, 100, 7, "boss");
            _context.Update();
        }

        public void NextLevel()
        {
            
        }
                        
    }
}