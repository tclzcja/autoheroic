using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cub
{
    public class Character
    {
        public Cub.Type.Class Class { get; private set; }
        public int Range { get; private set; }
        public int HP { get; private set; }
        public int Mobility { get; private set; }
        public List<Cub.Tactic> Tactic { get; private set; }
		public string Name { get; set; }
		public string UName { get; set; }

        public Character()
        {
            this.Class = Type.Class.None;
            this.Range = 0;
            this.HP = 0;
            this.Mobility = 0;
            this.Tactic = new List<Tactic>();
        }

        public Character(Cub.Type.Class _Class)
        {
            this.Class = _Class;

            switch (_Class)
            {
                case Type.Class.None:
                    {
                        this.Range = Library.Class_None.Range;
                        this.HP = Library.Class_None.HP;
                        this.Mobility = Library.Class_None.Mobility;
                        this.Tactic = Library.Class_None.Tactic;
                        break;
                    }
                case Type.Class.Archer:
                    {
                        this.Range = Library.Class_Archer.Range;
                        this.HP = Library.Class_Archer.HP;
                        this.Mobility = Library.Class_Archer.Mobility;
                        this.Tactic = Library.Class_Archer.Tactic;
                        break;
                    }
                case Type.Class.Knight:
                    {
                        this.Range = Library.Class_Knight.Range;
                        this.HP = Library.Class_Knight.HP;
                        this.Mobility = Library.Class_Knight.Mobility;
                        this.Tactic = Library.Class_Knight.Tactic;
                        break;
                    }
                case Type.Class.Mage:
                    {
                        this.Range = Library.Class_Mage.Range;
                        this.HP = Library.Class_Mage.HP;
                        this.Mobility = Library.Class_Mage.Mobility;
                        this.Tactic = Library.Class_Mage.Tactic;
                        break;
                    }
            }

        }

        public bool Damage(int Amount)
        {
            this.HP -= Amount;
            if (this.HP <= 0)
            {
                Debug.Log("Die: " + this.Class);
                Main.Dispose(this);
				return true;
            }
			return false;
        }

		public void Go(EventController con)
        {
            foreach (Tactic T in this.Tactic)
            {
                bool TacticExecuted = false;

                List<Cub.Character> CL = new List<Character>();
                switch (T.T)
                {
                    case Type.Target.Enemy:
                        {
                            CL.AddRange(Cub.Main.Info_Character);
                            CL.Remove(this);

                            int Index = 0;

                            while (Index < CL.Count)
                            {
                                Cub.Character CT = CL[Index];

                                bool FitTheConditionsFlag = true;

                                foreach (Cub.Type.Condition C in T.C)
                                {
                                    switch (C)
                                    {
                                        case Type.Condition.In_Range:
                                            {
                                                Vector2 V1 = Cub.Main.Info_Position[this];
                                                Vector2 V2 = Cub.Main.Info_Position[CT];

                                                if (Mathf.Abs(V1.x - V2.x) + Mathf.Abs(V1.y - V2.y) > this.Range)
                                                {
                                                    FitTheConditionsFlag = false;
                                                }
                                                break;
                                            }
                                    }

                                    if (FitTheConditionsFlag == false)
                                    {
                                        CL.Remove(CT);
                                        break;
                                    }
                                }

                                Index++;
                            }

                            //If the tactic passes the test
                            if (CL.Count > 0)
                            {
                                switch (T.A)
                                {
                                    case Type.Action.Attack:
                                        {
                                            Action.Attack AO = new Action.Attack();
                                            AO.Target = new List<Character>() { this, CL[0] };
                                            AO.Body(con);
                                            Main.Cool(this, AO.Time_Cooldown);
                                            break;
                                        }
                                }
                                TacticExecuted = true;
                            }

                            break;
                        }
                    case Type.Target.Self:
                        {
                            bool FitTheConditionsFlag = true;

                            foreach (Type.Condition C in T.C)
                            {
                                switch (C)
                                {
                                    case Type.Condition.Any:
                                        {
                                            break;
                                        }
                                }
                            }

                            //If the tactic passes the test
                            if (FitTheConditionsFlag)
                            {
                                switch (T.A)
                                {
                                    case Type.Action.Explore:
                                        {
                                            Action.Explore EO = new Action.Explore();
                                            EO.Target = new List<Character>() { this };
                                            EO.Body(con);
                                            Main.Cool(this, EO.Time_Cooldown);
                                            break;
                                        }
                                }
                                TacticExecuted = true;
                            }

                            break;
                        }
                }

                if (TacticExecuted)
                {
                    break;
                }
            }
        }
    }

}