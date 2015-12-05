using UnityEngine;
using System;
using System.Collections.Generic;

namespace AM
{
    public static class Core
    {
        public static void Determine(AM.Character Who)
        {
            //Select Available Tactics

            List<AM.Tactic> _List_Tactic = new List<Tactic>();

            foreach (AM.Tactic T in Who.List_Tactic)
            {
                List<AM.Character> _List_Character = new List<Character>();

                switch (T.Target)
                {
                    case Type.TTarget.Self:
                        {
                            _List_Character.Add(Who);
                            break;
                        }
                    case Type.TTarget.Enemy:
                        {
                            foreach (GameObject[] GOL in Field.Units)
                                foreach (GameObject GO in GOL)
                                {
                                    if (GO != Who.gameObject && GO != null)
                                    {
                                        _List_Character.Add(GO.GetComponent<AM.Character>());
                                    }
                                }
                            break;
                        }
                }

                int _LCCount = _List_Character.Count;

                foreach (AM.Character C in _List_Character)
                {
                    foreach (AM.Type.TSituation S in T.Situation)
                    {
                        bool flag = true;
                        switch (S)
                        {
                            case Type.TSituation.No_Enemy_Insight:
                                {
                                    flag = flag && SE_No_Enemy_Insight(C, S);
                                    break;
                                }
                            case Type.TSituation.In_Melee_Range:
                                {
                                    flag = flag && SE_In_Melee_Range(C, S);
                                    break;
                                }
                        }
                        if (!flag)
                        {
                            _LCCount--;
                            break;
                        }
                    }

                }

                if (_LCCount > 0)
                {
                    _List_Tactic.Add(T);
                }
            }

            //Random one tactics

            int _Max = 0;

            foreach (AM.Tactic T in _List_Tactic)
            {
                _Max += T.Priority;
            }

            int _Index = UnityEngine.Random.Range(0, _Max);

            AM.Tactic _T = _List_Tactic[0];

            Debug.Log(Who.List_Tactic.Count.ToString() + "  fdsa");

            _Max = 0;

            foreach (AM.Tactic T in _List_Tactic)
            {
                if (_Index <= _Max + T.Priority)
                {
                    _T = T;
                    break;
                }
                else
                {
                    _Max += T.Priority;
                }
            }

            //Execute the tactic

            List<GameObject> _LAC = new List<GameObject>();

            switch (_T.Target)
            {
                case Type.TTarget.Self:
                    {
                        _LAC.Add(Who.gameObject);
                        break;
                    }
                case Type.TTarget.Enemy:
                    {
                        _LAC.Add(Who.gameObject);
                        foreach (GameObject[] GOL in Field.Units)
                            foreach (GameObject GO in GOL)
                            {
                                if (GO != Who.gameObject && GO != null)
                                {
                                    _LAC.Add(GO);
                                }
                            }
                        break;
                    }
            }

            switch (_T.Action)
            {
                case Type.TAction.Explore:
                    {
                        GameObject GO = UnityEngine.Object.Instantiate(Library.Prefab_Action_Explore) as GameObject;
                        GO.GetComponent<AM.Actions.Action>().Pump(_LAC);
                        Debug.Log("Explore");
                        break;
                    }
                case Type.TAction.Attack:
                    {
                        GameObject GO = UnityEngine.Object.Instantiate(Library.Prefab_Action_Attack) as GameObject;
                        GO.GetComponent<AM.Actions.Action>().Pump(_LAC);
                        Debug.Log("Attack" + _LAC[0].name + _LAC[1].name);
                        break;
                    }
            }
        }

        private static bool SE_No_Enemy_Insight(AM.Character C, AM.Type.TSituation S)
        {
            for (int i = (int)(C.Stats_Position.x - 1); i <= (int)(C.Stats_Position.x + 1); i++)
                for (int j = (int)(C.Stats_Position.y - 1); j <= (int)(C.Stats_Position.y + 1); j++)
                {
     
                    if (i >= 0 && j >= 0 && i < Field.Units.Length && j < Field.Units[i].Length)
                    {
                        if (Field.Units[i][j] != null && i != (int)C.Stats_Position.x && j != (int)C.Stats_Position.y)
                        {
                            return false;
                        }
                    }
                }
            return true;
        }

        private static bool SE_In_Melee_Range(AM.Character C, AM.Type.TSituation S)
        {
            for (int i = (int)(C.Stats_Position.x - 3); i <= (int)(C.Stats_Position.x + 3); i++)
                for (int j = (int)(C.Stats_Position.y - 3); j <= (int)(C.Stats_Position.y + 3); j++)
                {
                    
                    if (i >= 0 && j >= 0 && i < Field.Units.Length && j < Field.Units[i].Length)
                    {
                        if (Field.Units[i][j] != null && i != (int)C.Stats_Position.x && j != (int)C.Stats_Position.y)
                        {
                            return true;
                        }
                    }
                }
            return false;
        }

    }
}
