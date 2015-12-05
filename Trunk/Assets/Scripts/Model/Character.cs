using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Model.Condition;

namespace Cub.Model
{

    public class Character_Save
    {
        public string Name;
        public Part_Head Head;
        public Part_Arms Arms;
        public Part_Body Body;
        public Part_Legs Legs;
        public Position2 Position;
        public System.Guid ID;
        public int Kills;
        public int Deaths;
        public int Matches;
        //public int X;
        //public int Y;

        public Cub.Model.BPHead Head_Part { get { return Cub.Model.Library.Get_Head(Head); } }
        public Cub.Model.BPBody Body_Part { get { return Cub.Model.Library.Get_Body(Body); } }
        public Cub.Model.BPArms Arms_Part { get { return Cub.Model.Library.Get_Arms(Arms); } }
        public Cub.Model.BPLegs Legs_Part { get { return Cub.Model.Library.Get_Legs(Legs); } }
        public Cub.Model.Weapon Weapon { get { return Cub.Model.Library.Get_Weapon(Arms); } }
        public int Value { get { return Head_Part.Cost + Arms_Part.Cost + Body_Part.Cost + Legs_Part.Cost; } }
        public int Health { get { return Body_Part.Health; } }
        public int Speed { get { return Legs_Part.Speed; } }

        public Character_Save()
        {

        }

        public Character_Save(string name, Part_Head head, Part_Arms arms, Part_Body body, Part_Legs legs, int _X, int _Y)
        {
            Name = name;
            Head = head;
            Arms = arms;
            Body = body;
            Legs = legs;
            Position = new Position2(_X, _Y);
            ID = System.Guid.NewGuid();
            Kills = 0;
            Deaths = 0;
            Matches = 0;
        }
    }

    public class Character_Info
    {
        public Part_Head Head { get; set; }
        public Part_Arms Arms { get; set; }
        public Part_Body Body { get; set; }
        public Part_Legs Legs { get; set; }
        public Cub.Model.BPHead Head_Part { get { return Cub.Model.Library.Get_Head(Head); } }
        public Cub.Model.BPBody Body_Part { get { return Cub.Model.Library.Get_Body(Body); } }
        public Cub.Model.BPArms Arms_Part { get { return Cub.Model.Library.Get_Arms(Arms); } }
        public Cub.Model.BPLegs Legs_Part { get { return Cub.Model.Library.Get_Legs(Legs); } }
        public Cub.Model.Weapon Weapon { get { return Cub.Model.Library.Get_Weapon(Arms); } }
        public List<Special_Effects> Effects { get; set; }
        //public Cub.Model.Weapon Weapon { get; set; }
        
        //public Cub.Class Class { get; set; }
        public Character_Save Save;
        public string Name { get; set; }
        public int Range { get; set; }
        public bool Blockable { get; set; }
        public int MHP { get; set; }
        public int Speed { get; set; }
        public int Value { get; set; }
        //public List<Cub.Tool.Tactic> List_Tactic { get; private set; }
        public List<Cub.Action> List_Special_Ability { get; set; }
    }

    public class Character_Stat
    {
        public Cub.Position2 Position { get; set; }
        public int HP { get; set; }
        public int Cooldown { get; set; }
        public Cub.Model.Team Team { get; set; }

        public void SetTeam(Team t)
        {
            this.Team = t;
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public System.Guid ID { get; set; }
        public System.Guid ID_Save { get; set; }
        public Character_Info Info { get; set; }
        public Character_Stat Stat { get; set; }
        //public List<Cub.Tool.Tactic> Bought_Tactic = new List<Tactic>();
        //public List<Cub.Tool.Tactic> Free_Tactic = new List<Tactic>();
        public List<Cub.Model.Tactic> Tactics = new List<Tactic>();
        public int Value { get { return FindValue(); } }
        public List<Cub.Action> ExhaustedActions = new List<Cub.Action>();

        public Character()
        {

        }

        public Character(Character_Save save)
        {
            Imprint(save);
        }

        public void Imprint(Character_Save save)
        {
            SetName(save.Name);
            this.ID = System.Guid.NewGuid();
            this.ID_Save = save.ID;
            this.Stat = new Character_Stat();
            this.Info = BuildInfo(save);

            SetLocation(save.Position);

            this.Stat.Cooldown = 0;
        }

        public bool Damage(int Amount, Character source, List<Cub.View.Eventon> events, Attack_Result hitLevel)
        {
            if (this.Stat.HP <= 0) return true;
            this.Stat.HP -= Amount;
            if (this.Stat.HP <= 0)
            {
                if (Info.Effects.Contains(Special_Effects.Explode_On_Death))
                {
                    events.Add(new View.Eventon(Event.Blow_Up, "Blow Up " + FindColorName(), false, new List<object> { ID }));
                    foreach (Character guy in Main.AllCharacters())
                        if (guy != this && Cub.Tool.Pathfinder.Distance(Stat.Position, guy.Stat.Position) <= 1.5f)
                            guy.Damage(2, source, events, Cub.Attack_Result.Hit);
                    //Cub.Model.Library.Get_Action(Cub.Action.Blow_Up).Body(this, new List<object>());
                    Debug.Log("Blow Up: " + this.Name);
                }
                else
                {
                    Debug.Log("Die: " + this.Name);
                    events.Add(new View.Eventon(Event.Die, "R.I.P. " + FindColorName(), false,new List<object> { ID }));
                }
                Info.Save.Deaths++;
                source.Info.Save.Kills++;
                if (source.Stat.Team != Stat.Team)
                    source.Stat.Team.AddScore("Kills", Value);
                Main.Dispose(this, events);
                return true;
            }
            else
            {
                //hitLevel tells you if this was from a normal attack or a crit. Maybe crits make an animation or something.
                events.Add(new View.Eventon(Event.Be_Attacked, Name + " <" + Amount.ToString() + " Damage>",false,
                    new List<object> { ID, Amount, this.Stat.HP, this.Info.MHP, hitLevel }));
            }
            return false;
        }

        public void Heal(int Amount, Character source, List<Cub.View.Eventon> events)
        {
            Amount = Mathf.Min(Amount, Info.MHP - Stat.HP);
            this.Stat.HP = Mathf.Min(this.Stat.HP + Amount,this.Info.MHP);
            events.Add(new View.Eventon(Event.Be_Healed, Name + " [" + Amount + " Heal]", false,
                new List<object> { ID, Amount, this.Stat.HP, this.Info.MHP }));
        }

        public List<Cub.View.Eventon> Go()
        {
            List<View.Eventon> r = new List<View.Eventon>();
            foreach (Tactic T in this.Tactics)
            {
                if (T.A == Cub.Action.None) continue;
                Cub.Model.Action.Base a = Library.Get_Action(T.A);
                if (a == null) Debug.Log("ERROR: " + T.A);
                List<object> Data = a.Confirm(this);
                if (Data == null) continue;
                if (T.C != Cub.Condition.None)
                {
                    Condition.Base c = Library.Get_Condition(T.C);
                    if (c == null) Debug.Log("ERROR: " + T.C);
                    Data = c.Confirm(this, Data);
                    if (Data == null) continue;
                }
                r = a.Body(this, Data);
                EndTurn(r);
                return r;
            }
            return r;
        }

        List<Cub.View.Eventon> EndTurn(List<Cub.View.Eventon> events)
        {
            if (Info.Effects.Contains(Special_Effects.Autoheal))
            {
                if (Stat.HP < Info.MHP && Stat.HP > 0)
                    Heal(1, this, events);
            }
            return events;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public Character_Info BuildInfo(Character_Save cs)
        {
            Character_Info info = new Character_Info();
            info.Save = cs;
            info.Head = cs.Head;
            info.Arms = cs.Arms;
            info.Body = cs.Body;
            info.Legs = cs.Legs;

            List<Cub.Model.Bodypart> parts = new List<Model.Bodypart>();
            parts.Add(cs.Head_Part);
            parts.Add(cs.Arms_Part);
            parts.Add(cs.Body_Part);
            parts.Add(cs.Legs_Part);

            int hp = Cub.Model.Library.Get_Body(info.Body).Health;
            int sp = Cub.Model.Library.Get_Legs(info.Legs).Speed;
            int value = 0;
            List<Special_Effects> effects = new List<Special_Effects>();
            List<Cub.Action> acts = new List<Cub.Action>();

            foreach (Cub.Model.Bodypart bp in parts)
            {
                value += bp.Cost;
                foreach (Special_Effects se in bp.Effects)
                    if (!effects.Contains(se))
                        effects.Add(se);
                foreach (Cub.Action a in bp.Special_Abilities)
                    if (!acts.Contains(a))
                        acts.Add(a);
            }

            info.Value = value;
            info.MHP = Mathf.Max(1,hp);
            info.Speed = Mathf.Max(1,sp);
            info.Blockable = Cub.Model.Library.Get_Legs(info.Legs).Blockable;
            info.List_Special_Ability = acts;
            info.Effects = effects;
            info.Range = info.Weapon.Range;

            //This is temporary.##
            //info.Class = Class.Soldier;

            this.Stat.HP = info.MHP;

            this.Tactics = BuildAIProfile(cs.Head,acts);
            
            return info;
        }

        public void SetLocation(Cub.Position2 where)
        {
            this.SetLocation(where.X, where.Y);
        }

        public void SetLocation(int x, int y)
        {
            this.Stat.Position = new Position2(x, y);
        }

        public List<Character> FindEnemies()
        {
			List<Character> r = new List<Character>();
            foreach (Team t in Cub.Model.Main.List_Team)
                if (t != Stat.Team)
                    r.AddRange(t.Return_List_Character());
            return r;
        }

        public Tactic BuyTactic(Cub.Condition condition, Cub.Action action)
        {
            Tactic tac = new Tactic(condition, action);
            Tactics.Add(tac);
            return tac;
        }

        int FindValue()
        {
            return Info.Head_Part.Cost + Info.Arms_Part.Cost + Info.Body_Part.Cost + Info.Legs_Part.Cost;
        }

        public List<Tactic> BuildAIProfile(Cub.Part_Head head, List<Cub.Action> acts)
        {
            List<Tactic> r = new List<Tactic>();
            List<Tactic> vhigh = new List<Tactic>();
            List<Tactic> high = new List<Tactic>();
            List<Tactic> med = new List<Tactic>();
            List<Tactic> low = new List<Tactic>();
            switch (head)
            {
                case Cub.Part_Head.Soldier:
                    high.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    low.Add(new Tactic(Cub.Condition.Any, Cub.Action.Follow_Enemy));
                    break;

                case Cub.Part_Head.Idiot:
                    high.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    low.Add(new Tactic(Cub.Condition.Any, Cub.Action.Explore));
                    break;

                case Cub.Part_Head.Protector:
                    high.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    low.Add(new Tactic(Cub.Condition.Closest, Cub.Action.Follow_Ally));
                    break;

                case Cub.Part_Head.Hunter:
                    AddTactic(high, Cub.Action.Attack, Cub.Condition.Almost_Dead);
                    //high.Add(new Tactic(Cub.Condition.Almost_Dead, Cub.Action.Attack));
                    //high.Add(new Tactic(Cub.Condition.Any, Cub.Action.Attack));
                    AddTactic(low, Cub.Action.Follow_Enemy, Cub.Condition.Almost_Dead);
                    //low.Add(new Tactic(Cub.Condition.Almost_Dead, Cub.Action.Follow_Enemy));
                    //low.Add(new Tactic(Cub.Condition.Any, Cub.Action.Follow_Enemy));
                    break;
            }
            foreach (Cub.Action act in acts)
            {
                switch (act)
                {
                    case Cub.Action.Charge:
                        AddTactic(med, Cub.Action.Charge, Cub.Condition.None);
                        break;
                    case Cub.Action.Heal:
                        med.Add(new Tactic(Cub.Condition.Is_Hurt, Cub.Action.Heal));
                        break;
                        //case Cub.Action.Missile:
                        //    vhigh.Add(new Tactic(Cub.Condition.Adjacent_2, Cub.Action.Missile));
                        //    break;
                        //case Cub.Action.Snipe:
                        //    AddTactic(vhigh, Cub.Action.Snipe, Cub.Condition.None);
                        //    break;
                }
            }
            r.AddRange(vhigh);
            r.AddRange(high);
            r.AddRange(med);
            r.AddRange(low);
            return r;
        }

        void AddTactic(List<Tactic> list, Cub.Action act, Cub.Condition favorite)
        {
            if (favorite != Cub.Condition.None)
                list.Add(new Tactic(favorite, act));
            list.Add(new Tactic(Cub.Condition.Any, act));
        }

        public void MakeUnique()
        {
            ID = System.Guid.NewGuid();
        }

        public string FindColorName()
        {
            string r = this.Name;
            //string color = "FF0000";
            //if (Cub.Model.Main.List_Team[0] != Stat.Team)
            //    color = "00FF00";
            //string color = c.r.ToString("X2") + c.g.ToString("X2") + c.b.ToString("X2");
            //string color = Color.cyan.ToString();
            Color32 color = Stat.Team.Colour_Primary;
            string c = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return "[" + c + "]" + r + "[-]";
        }
    }

}