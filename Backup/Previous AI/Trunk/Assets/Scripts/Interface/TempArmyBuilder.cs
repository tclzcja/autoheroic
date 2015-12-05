#region BoringStuff
using Cub.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interface
{
    class TempArmyBuilder
    {
        //float Points = 500;

        public Cub.Tool.Team RedTeam = new Team();
        public Cub.Tool.Team BlueTeam = new Team();
        List<Character> RedArmy = new List<Character>();
        List<Character> BlueArmy = new List<Character>();
        int[,] RedStagingArea;
        int[,] BlueStagingArea;
        Character RedOne { get { return RedArmy[0]; } }
        Character RedTwo { get { return RedArmy[1]; } }
        Character RedThree { get { return RedArmy[2]; } }
        Character RedFour { get { return RedArmy[3]; } }
        Character RedFive { get { return RedArmy[4]; } }
        Character BlueOne { get { return BlueArmy[0]; } }
        Character BlueTwo { get { return BlueArmy[1]; } }
        Character BlueThree { get { return BlueArmy[2]; } }
        Character BlueFour { get { return BlueArmy[3]; } }
        Character BlueFive { get { return BlueArmy[4]; } }
        public List<Character> MasterList = new List<Character>();
        Cub.Position2 StageSize;

        public TempArmyBuilder()
        {
            //StageSize = new Cub.Position2(Library.Stage_Terrain.Count(), Library.Stage_Terrain[0].Count());
            StageSize = new Cub.Position2(10,10);
            RedArmy.Add(new Character(Cub.Class.None, 0, 0));
            RedArmy.Add(new Character(Cub.Class.None, 0, 0));
            RedArmy.Add(new Character(Cub.Class.None, 0, 0));
            RedArmy.Add(new Character(Cub.Class.None, 0, 0));
            RedArmy.Add(new Character(Cub.Class.None, 0, 0));
            BlueArmy.Add(new Character(Cub.Class.None, 0, 0));
            BlueArmy.Add(new Character(Cub.Class.None, 0, 0));
            BlueArmy.Add(new Character(Cub.Class.None, 0, 0));
            BlueArmy.Add(new Character(Cub.Class.None, 0, 0));
            BlueArmy.Add(new Character(Cub.Class.None, 0, 0));
            BuildRedArmy();
            BuildBlueArmy();
            ParseArmies(true);
            ParseArmies(false);

        }

#endregion
        #region Red Army Builder
        void BuildRedArmy()
        {
            //Alright, name your team here.
            RedTeam.SetName("Fluffy Cloud", "Elyse");
            //Place your characters in your starting zone.
            //A '1' will place RedOne, 2 -> RedTwo, and so on.
            //Any characters not placed will not be hired.
            RedStagingArea = new int[2, 6]{
                {0,1,0,2,0,0},
                {0,0,4,0,3,5}
            };

            //Now build your characters.
            //Name is a character's name.
            //The Classes:
            //Soldier: Ranged unit. (100 pts)
            //Knight: Melee unit. (100 pts)
            //Rocket: Weak soldier with a one-use rocket launcher. (100 pts)
            //Jerk: Weak but cheap unit. (50 pts)
            //Sniper: Weak soldier with a one-use instant kill. (100 pts)
            //Medic: Can heal nearby units. (100 pts)

            RedOne.SetName("Sir Fluffington III, Esq.");
            RedOne.SetClass("Soldier");

            RedTwo.SetName("Court Mage Elizabeth Stuffyfluff");
            RedTwo.SetClass("Soldier");

            RedThree.SetName("Viscountess Cloud McStuffing");
            RedThree.SetClass("Knight");

            RedFour.SetName("Lady Legolass Greenleaf");
            RedFour.SetClass("Medic");

            RedFive.SetName("Red Five");
            RedFive.SetClass("Sniper");
        }
        #endregion
        #region Blue Army Builder
        void BuildBlueArmy()
        {
            //Alright, name your team here.
            BlueTeam.SetName("Alec's Team", "Alec");
            //Place your characters in your starting zone.
            //A '1' will place BlueOne, 2 -> BlueTwo, and so on.
            //Any characters not placed will not be hired.
            BlueStagingArea = new int[2, 6]{
                {0,0,0,0,0,0},
                {1,2,3,4,5,0}
            };

            //Now build your characters.
            //Name is a character's name.
            //The Classes:
            //Soldier: Ranged unit. (100 pts)
            //Knight: Melee unit. (100 pts)
            //Rocket: Weak soldier with a one-use rocket launcher. (100 pts)
            //Jerk: Weak but cheap unit. (50 pts)
            //Sniper: Weak soldier with a one-use instant kill. (100 pts)
            //Medic: Can heal nearby units. (100 pts)

            BlueOne.SetName("Bennett");
            BlueOne.SetClass("Soldier");

            BlueTwo.SetName("Eric");
            BlueTwo.SetClass("Soldier");

            BlueThree.SetName("Frank");
            BlueThree.SetClass("Rocket");

            BlueFour.SetName("Andy");
            BlueFour.SetClass("Rocket");

            BlueFive.SetName("Clara");
            BlueFive.SetClass("Sniper");
        }
        #endregion
        #region MoreBoringStuff

        void ParseArmies(bool red)
        {
            List<Character> chars = BlueArmy;
            int[,] map = BlueStagingArea;
            Cub.Tool.Team team = BlueTeam;
            List<int> UsedNums = new List<int> { 0 };
            if (red)
            {
                chars = RedArmy;
                map = RedStagingArea;
                team = RedTeam;
            }
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int num = map[x, y];
                    if (!UsedNums.Contains(num))
                    {
                        Character c = chars[num - 1];
                        team.Add_Character(c);
                        c.SetLocation(TranslateStartPosition(
                            x, y, new Cub.Position2(map.GetLength(0), map.GetLength(1)), red));
                        //if (red)
                        //    c.SetLocation(1 - x, ((int)StageSize.y - 1) - y);
                        //else
                        //    c.SetLocation(((int)StageSize.x - 1) - (1 - x), y);
                        MasterList.Add(c);
                        UsedNums.Add(num);
                    }
                }
        }

        public Cub.Position2 TranslateStartPosition(int x, int y, Cub.Position2 spawnSize, bool teamOne)
        {
            Cub.Position2 r = new Cub.Position2(0, 0);
            int boost = (StageSize.Y - spawnSize.Y) / 2;
            boost = 2;
            if (teamOne)
                r = new Cub.Position2(1 - x, (StageSize.Y - 1) - y - boost);
            else
                r = new Cub.Position2((StageSize.X - 1) - (1 - x), y + boost);
            return r;
        }
    }
}
        #endregion