using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Tool
{
    public static class Main
    {
        //public static Cub.View.SetupData SData { get; set; }
        public static List<Cub.Tool.Team> List_Team { get; set; }

        public static int Turn { get; private set; }

        static int TurnLimit = 10;

        public static bool TimeOut = false;

        public static List<Cub.Tool.Character> DeadCharacters { get; set; }

        public static void Initialization(Team teamOne, Team teamTwo)
        {
            Turn = 1;
            List_Team = new List<Team>{teamOne,teamTwo};
            DeadCharacters = new List<Character>();
        }

        public static void StartGameplay(){
            //foreach (Team t in List_Team)
            //        foreach (Character c in t.Return_List_Character())
            //            t.TotalValue += c.Value;
        }

        public static List<Cub.View.Eventon> Go()
        {
            Debug.Log("Turn " + Turn.ToString() + " Start");
            List<Cub.View.Eventon> GEL = new List<View.Eventon>();
            List<Cub.Tool.Character> CL = GenerateTurnOrder();
            foreach (Cub.Tool.Character C in CL)
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
            return GEL;
        }

        private static List<Character> GenerateTurnOrder()
        {
            List<Character> r = new List<Character>();
            List<Character> temp = new List<Character>();
            foreach (Team t in List_Team)
                foreach (Character c in t.List_Character)
                    temp.Add(c);
            while (temp.Count > 0)
            {
                Character c = temp[UnityEngine.Random.Range(0, temp.Count)];
                temp.Remove(c);
                r.Add(c);
            }
            return r;
        }

        public static void Dispose(Cub.Tool.Character C, List<Cub.View.Eventon> events)
        {
            DeadCharacters.Add(C);
            foreach (Team team in List_Team)
            {
                team.Remove_Character(C);
                if (team.Return_List_Character().Count == 0)
                {
                    GameEnds(events);
                }
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
                events.Add(new View.Eventon(Event.TimeOut, "Time Out!", new List<object>()));
                Debug.Log("Game Times Out");
            }
            int highScore = -999999999;
            List<Team> winningTeams = new List<Team>();
            foreach (Team team in List_Team)
            {
                if (team.Return_List_Character().Count > 0)
                    team.AddScore("Survival", 50);
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
            events.Add(new View.Eventon(Event.Win, "Game Over!", new List<object>{winners,losers}));
            
        }
    }
}
