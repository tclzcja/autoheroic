using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Model
{
    public static class Main
    {
        //public static Cub.View.SetupData SData { get; set; }
        public static List<Cub.Model.Team> List_Team { get; set; }

        public static int Turn { get; private set; }

        static int TurnLimit = 10;

        public static bool TimeOut = false;

        public static List<Cub.Model.Character> DeadCharacters { get; set; }

        public static void Initialization(Team teamOne, Team teamTwo)
        {
            Turn = 1;
            List_Team = new List<Team>{teamOne,teamTwo};
            DeadCharacters = new List<Character>();
            foreach (Team t in List_Team)
                foreach (Character c in t.Return_List_Character())
                    c.Info.Save.Matches++;
        }

        public static List<Cub.View.Eventon> Go()
        {
            Debug.Log("Turn " + Turn.ToString() + " Start");
            List<Cub.View.Eventon> GEL = new List<View.Eventon>();
            List<Cub.Model.Character> CL = GenerateTurnOrder();
            foreach (Cub.Model.Character C in CL)
                if (GameStillRunning() && !DeadCharacters.Contains(C))
                {
                    C.Stat.Cooldown -= 1;
                    if (C.Stat.Cooldown <= 0)
                    {
                        List<Cub.View.Eventon> EL = C.Go();
                        GEL.AddRange(EL);
                    }
                }
            Turn++;
            if (Turn >= TurnLimit)
            {
                TimeOut = true;
                GameEnds(GEL);
            }
            else if (!GameStillRunning())
                GameEnds(GEL);
            return GEL;
        }

        private static List<Character> GenerateTurnOrder()
        {
            List<Character> r = new List<Character>();
            int n = 0;
            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = false;
                foreach (Team t in List_Team)
                {
                    if (t.List_Character.Count <= n)
                        continue;
                    Character who = t.List_Character[n];
                    r.Add(who);
                    keepGoing = true;
                }
                n++;
            }
            
            //List<Character> temp = new List<Character>();
            //foreach (Team t in List_Team)
            //    foreach (Character c in t.List_Character)
            //        temp.Add(c);
            //while (temp.Count > 0)
            //{
            //    Character c = temp[UnityEngine.Random.Range(0, temp.Count)];
            //    temp.Remove(c);
            //    r.Add(c);
            //}
            return r;
        }

        public static void Dispose(Cub.Model.Character C, List<Cub.View.Eventon> events)
        {
            DeadCharacters.Add(C);
            foreach (Team team in List_Team)
            {
                team.Remove_Character(C);
                //if (team.Return_List_Character().Count == 0)
                //{
                //    GameEnds(events);
                //}
            }
        }

        public static bool GameStillRunning()
        {
            if (Turn >= TurnLimit)
                return false;
            foreach (Team team in List_Team)
                if (team.Return_List_Character().Count == 0)
                    return false;
            return true;
        }

        public static List<Character> AllCharacters()
        {
            List<Character> r = new List<Character>();
            foreach (Team team in List_Team)
                r.AddRange(team.Return_List_Character());
            return r;
        }

        public static void GameEnds(List<Cub.View.Eventon> events)
        {
            Debug.Log("Game Over: Turn " + Turn.ToString());
            if (TimeOut)
            {
                events.Add(new View.Eventon(Event.Time_Out, "Time Out!", false,new List<object>()));
                Debug.Log("Game Times Out");
            }
            int highScore = -999999999;
            List<Team> winningTeams = new List<Team>();
            foreach (Team team in List_Team)
            {
                //if (team.Return_List_Character().Count > 0)
                //    team.AddScore("Survival", 50);
                int score = team.Score;
                Debug.Log("### " + team.Name + " ### (" + team.TotalValue + " Total Value)");
                foreach (Score st in team.ScoreThings)
                    Debug.Log("-" + st.Name + ": " + st.Value + " points");
                Debug.Log("---Total: " + score + " points---");
                if (highScore < score)
                {
                    winningTeams.Clear();
                }
                if (highScore <= score){
                    winningTeams.Add(team);
                    highScore = score;
                }
            }
            List<Guid> winners = new List<Guid>();
            List<Guid> losers = new List<Guid>();
            Team winTeam = null;
            if (winningTeams.Count == 1)
            {
                winTeam = winningTeams[0];
                Debug.Log(winTeam.Name + " Wins!");
                winTeam.Save.Wins++;
                foreach (Team t in List_Team)
                    if (t != winTeam)
                        t.Save.Losses++;
            }
            else
                Debug.Log("TIE!");
            foreach (Team t in List_Team)
                foreach (Character c in t.List_Character)    
                {
                    if (t == winTeam)
                        winners.Add(c.ID);
                    else
                        losers.Add(c.ID);
                }
            events.Add(new View.Eventon(Event.Win, "Game Over!",false, new List<object>{winners,losers}));
            
        }

    }
}
