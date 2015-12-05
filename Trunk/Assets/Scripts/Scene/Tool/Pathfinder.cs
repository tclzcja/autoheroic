using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cub.Tool
{
    static class Pathfinder
    {
        public static List<Cub.Position2> findPath(Cub.Position2 start, Cub.Position2 finish, bool blockable)
        {
            return findPath(start, finish, 1000, blockable);
        }

        public static List<Cub.Position2> findPath(Cub.Position2 start, Cub.Position2 finish, int distance, bool blockable)
        {
            //if (!blockable)
            List<Cub.Position2> sq;
            List<Cub.Position2> path = new List<Cub.Position2>();
            if (finish.X == -999) return path;
            Dictionary<Cub.Position2, PathPoint> openList = new Dictionary<Cub.Position2, PathPoint>();
            Dictionary<Cub.Position2, PathPoint> closedList = new Dictionary<Cub.Position2, PathPoint>();
            openList.Add(start, new PathPoint(start, new Cub.Position2(-999, -999), 0, ManWalk(start, finish)));
            PathPoint seed = null;
            for (int n = distance; n > 0; n--)
            {
                seed = null;
                foreach (PathPoint pp in openList.Values)
                    if (seed == null || pp.F <= seed.F)
                        seed = pp;
                if (seed == null)
                    break;
                if (seed.Square == finish)
                {
                    closedList.Add(seed.Square, seed);
                    break;
                }
                sq = GetAdjacents(seed.Square);
                foreach (Cub.Position2 s in sq)
                    if (s.X != -999 && (s == finish || (CheckAccessable(s, blockable) && !closedList.ContainsKey(s))))
                    {
                        int dist = GetTerrainDifficulty(seed.Square);
                        if (!openList.Keys.Contains(s))
                            openList.Add(s, new PathPoint(s, seed.Square, seed.G + dist, ManWalk(s, finish)));
                        else if (openList[s].F > seed.G + dist + ManWalk(s, finish))
                        {
                            openList.Remove(s);
                            openList.Add(s, new PathPoint(s, seed.Square, seed.G + dist, ManWalk(s, finish)));
                        }
                    }
                closedList.Add(seed.Square, seed);
                openList.Remove(seed.Square);
            }
            if (!closedList.Keys.Contains(finish))
                return path;
            seed = closedList[finish];
            while (true)
            {
                path.Insert(0, seed.Square);
                if (seed.pSquare == start)
                    break;
                else if (seed.pSquare.X < 0)
                {
                    Debug.Log("Not reading start ----------------------------------------------------------------------------------");
                    break;
                }
                else
                {
                    seed = closedList[seed.pSquare];
                }
            }
            if (!CheckAccessable(finish,true) && finish != start)
                path.Remove(finish);
            return path;
        }

        public static int ManWalk(Cub.Position2 start, Cub.Position2 finish)
        {
            int w = Distance(start, finish);
            //w += Math.Abs(finish.Coordinate.X - Coordinate.X);
            //w += Math.Abs(finish.Coordinate.Y - Coordinate.Y);
            w *= 10;
            return w;
        }

        public static int Distance(Cub.Position2 start, Cub.Position2 target)
        {
            int x = Math.Abs(target.X - start.X);
            int y = Math.Abs(target.Y - start.Y);
            //int l = Math.Max(x, y);
            //int s = Math.Min(x, y);
            //return l + s / 2;
            return x + y;

        }

        public static bool CheckAccessable(Cub.Position2 where, bool blockable)
        {
            if (where.X < 0 || where.X >= Cub.Model.Library.MapSizeX || where.Y < 0
                || where.Y >= Cub.Model.Library.MapSizeY)
                return false;
            if (!blockable)
                return true;
            foreach (Cub.Model.Team t in Cub.Model.Main.List_Team)
            {
                foreach (Cub.Model.Character c in t.Return_List_Character())
                {
                    if (c.Stat.Position.X == where.X && c.Stat.Position.Y == where.Y)
                        return false;
                }
            }
            return true;
        }

        public static List<Cub.Position2> GetAdjacents(Cub.Position2 where)
        {
            List<Cub.Position2> r = new List<Cub.Position2>();
            List<Cub.Position2> dirs = new List<Cub.Position2> { new Cub.Position2(1, 0), new Cub.Position2(-1, 0), new Cub.Position2(0, 1),
                new Cub.Position2(0,-1),new Cub.Position2(1,1),new Cub.Position2(-1,1),new Cub.Position2(-1,-1),new Cub.Position2(1,-1)};
            foreach (Cub.Position2 d in dirs)
                r.Add(new Cub.Position2(where.X + d.X, where.Y + d.Y));
            return r;
        }

        public static int GetTerrainDifficulty(Cub.Position2 where)
        {
            return 1;
        }
    }

    class PathPoint
    {
        public Cub.Position2 Square;
        public Cub.Position2 pSquare;
        public int G;
        public int H;
        public int F { get { return G + H; } }

        public PathPoint(Cub.Position2 square, Cub.Position2 previous, int g, int h)
        {
            Square = square;
            pSquare = previous;
            G = g;
            H = h;
        }
    }
}
