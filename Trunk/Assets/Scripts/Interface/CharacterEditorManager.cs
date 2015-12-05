using UnityEngine;
using System.Collections;
using Cub.Interface;
using System.Collections.Generic;

public class CharacterEditorManager : OptionsListController
{

    public GameObject PersonalCamera;

    Dictionary<MenuChoiceController, List<Cub.Model.Bodypart>> OptionOptions;
    //MenuChoiceController SelectedHori;
    //List<Cub.Model.BPHead> Heads;
    //List<Cub.Model.BPBody> Bodies;
    //List<Cub.Model.BPArms> Arms;
    //List<Cub.Model.BPLegs> Legs;
    Color32 GrayedOut = new Color(0.1f, 0.1f, 0.1f, 1);

    Cub.Model.BPHead Head;
    Cub.Model.BPBody Body;
    Cub.Model.BPArms Arms;
    Cub.Model.BPLegs Legs;

    Cub.Model.BPHead IHead;
    Cub.Model.BPBody IBody;
    Cub.Model.BPArms IArms;
    Cub.Model.BPLegs ILegs;

    //int MaxCost = 140;

    float HoriTimer;

    Cub.View.Character Who;
    Cub.Model.Character_Save WhoSave;
    public Cub.Model.TeamSave Team { get; set; } 

    public UILabel Name;
    //public UITexture CostBG;
    //public UITexture CostHead;
    //public UITexture CostArms;
    //public UITexture CostBody;
    //public UITexture CostLegs;
    public UILabel CostText;
    public UILabel RemainingPointsText;
    //public UILabel HeadPts;
    //public UILabel ArmsPts;
    //public UILabel BodyPts;
    //public UILabel LegsPts;
    //public UITexture HealthFG;
    //public UITexture SpeedFG;
    //public UITexture DamageFG;
    //public UITexture RangeFG;
    //public UILabel SpecialDesc;
    public UILabel AIDesc;
    public UILabel WeaponDesc;
    public UILabel ArmorDesc;
    public UILabel LegsDesc;

    public UILabel DamageDesc;
    public List<UI2DSprite> DamageIcons;
    public UILabel RangeDesc;
    public List<UI2DSprite> RangeIcons;
    public UILabel CritDesc;
    public List<UI2DSprite> CritIcons;
    public UILabel HealthDesc;
    public List<UI2DSprite> HealthIcons;
    public UILabel SpeedDesc;
    public List<UI2DSprite> SpeedIcons;
    public UILabel HeadSpecial;
    public UILabel WeaponSpecial;
    public UILabel BodySpecial;
    public UILabel LegsSpecial;

    public UILabel PtsLeft;

    public UILabel CurrentName;
    public UILabel CurrentDesc;
    public UILabel CurrentCost;
    //public UILabel Cost;
    //public UILabel AI;
    //public UILabel Weapon;
    //public UILabel Special;

    //public UILabel Health;
    //public UILabel Speed;
    //public UILabel Range;
    //public UILabel Damage;

    //Cub.Interface.TeamPickerManager MyPicker;
    TeamEditorManager MyTEditor;
    //TextInputController MyTextEditor;

    void Start()
    {
        if (PlayerOne)
        {
            //MyPicker = GM.LeftPicker;
            MyTEditor = GM.LeftEditor;
            //MyTextEditor = GM.LeftNameEditor;
        }
        else
        {
            //MyPicker = GM.RightPicker;
            MyTEditor = GM.RightEditor;
            //MyTextEditor = GM.RightNameEditor;
        }
    }

    void Update()
    {
        if (!CurrentlyActive)
            return;
        if (Timer > 0)
            Timer -= Time.deltaTime;

        if (GetInput("Vertical") > 0.1f && Timer <= 0)
        {
            ChangeSelection(-1);
            Timer = SelectTimer;
        }
        else if (GetInput("Vertical") < -0.1f && Timer <= 0)
        {
            ChangeSelection(1);
            Timer = SelectTimer;
        }
        else if (Timer > 0 && Mathf.Abs(GetInput("Vertical")) < 0.1f)
            Timer = 0;
        if (HoriTimer > 0)
            HoriTimer -= Time.deltaTime;

        if (GetInput("Horizontal") > 0.1f && HoriTimer <= 0)
        {
            ChangeHoriSelection(-1);
            HoriTimer = SelectTimer;
        }
        else if (GetInput("Horizontal") < -0.1f && HoriTimer <= 0)
        {
            ChangeHoriSelection(1);
            HoriTimer = SelectTimer;
        }
        else if (HoriTimer > 0 && Mathf.Abs(GetInput("Horizontal")) < 0.1f)
            HoriTimer = 0;
        if (GetInput("Click") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                Click();
            }
        }
        else if (GetInput("Escape") > 0.8f)
        {
            if (!Clicking)
            {
                Clicking = true;
                MyTEditor.gameObject.SetActive(true);
                MyTEditor.TurnOn();
                Clear();
                Who.Initialize_Part();
            }
        }
        else if (GetInput("Namer") > 0.8f)
        {
            if (!Clicking)
            {
                Clicking = true;
                NameUpdate(Cub.Model.Library.CharacterName());
            }
        }
        else
            Clicking = false;
        //else if (Mathf.Abs(GetInput("Namer")) > 0.5f)
        //{
        //    if (!Clicking)
        //    {
        //        Clicking = true;
        //        MyTextEditor.gameObject.SetActive(true);
        //        MyTextEditor.SetupCharacter(this, WhoSave);
        //        gameObject.SetActive(false);
        //    }
        //}
    }

    public void Clear()
    {
        WhoSave.Head = IHead.E;
        WhoSave.Body = IBody.E;
        WhoSave.Arms = IArms.E;
        WhoSave.Legs = ILegs.E;
        Who.Stat.Head = IHead.E;
        Who.Stat.Body = IBody.E;
        Who.Stat.Arms = IArms.E;
        Who.Stat.Legs = ILegs.E;
        Who.Delete_Part();
        gameObject.SetActive(false);
        PersonalCamera.SetActive(false);
    }


    protected override void Click()
    {
        MyTEditor.gameObject.SetActive(true);
        MyTEditor.TurnOn();
        gameObject.SetActive(false);
        PersonalCamera.SetActive(false);
    }

    public void Setup(Cub.View.Character VChar, Cub.Model.Character_Save SChar, Cub.Model.TeamSave team)
    {   
        Who = VChar;
        WhoSave = SChar;
        Team = team;
        PersonalCamera.SetActive(true);
        float offset = 2;
        if (!PlayerOne)
            offset = -2;
        PersonalCamera.transform.position =
            new Vector3(VChar.transform.position.x + offset, VChar.transform.position.y - 0.8f, VChar.transform.position.z + 0.7f);
        CurrentlyActive = true;
        OnSelectChange();
        //Heads = Cub.Model.Library.List_Heads();
        //Arms = Cub.Model.Library.List_Arms();
        //Bodies = Cub.Model.Library.List_Bodies();
        //Legs = Cub.Model.Library.List_Legs();
        OptionOptions = new Dictionary<MenuChoiceController, List<Cub.Model.Bodypart>>();
        OptionOptions.Add(Options[0], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Heads())
            OptionOptions[Options[0]].Add(bp);

        OptionOptions.Add(Options[1], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Arms())
            OptionOptions[Options[1]].Add(bp);

        OptionOptions.Add(Options[2], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Bodies())
            OptionOptions[Options[2]].Add(bp);

        OptionOptions.Add(Options[3], new List<Cub.Model.Bodypart>());
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Legs())
            OptionOptions[Options[3]].Add(bp);

        IHead = Head = SChar.Head_Part;
        IArms = Arms = SChar.Arms_Part;
        IBody = Body = SChar.Body_Part;
        ILegs = Legs = SChar.Legs_Part;

        WriteDescriptions();

        //PersonalCamera.transform.LookAt(VChar.transform);
    }

    void WriteDescriptions()
    {
        Name.text = WhoSave.Name;
        CostText.text = "Cost: " + WhoSave.Value.ToString();
        int freePts = Cub.Model.Library.PointCap - Team.TotalValue;
        RemainingPointsText.text = "Remaining: " + freePts.ToString();
        if (freePts >= 0)
            RemainingPointsText.color = Color.white;
        else
            RemainingPointsText.color = Color.red;
        //int costWidth = 100 * WhoSave.Head_Part.Cost / MaxCost;
        //CostHead.SetDimensions(costWidth, 10);
        //costWidth += 100 * WhoSave.Arms_Part.Cost / MaxCost;
        //CostArms.SetDimensions(costWidth, 10);
        //costWidth += 100 * WhoSave.Body_Part.Cost / MaxCost;
        //CostBody.SetDimensions(costWidth, 10);
        //costWidth += 100 * WhoSave.Legs_Part.Cost / MaxCost;
        //CostLegs.SetDimensions(costWidth, 10);
        //CostText.text = WhoSave.Value.ToString();
        //HeadPts.text = WhoSave.Head_Part.Cost.ToString();
        //ArmsPts.text = WhoSave.Arms_Part.Cost.ToString();
        //BodyPts.text = WhoSave.Body_Part.Cost.ToString();
        //LegsPts.text = WhoSave.Legs_Part.Cost.ToString();
        //int costWidth = 100 * WhoSave.Body_Part.Health / 4;
        //HealthFG.SetDimensions(costWidth, 10);
        //costWidth = 100 * WhoSave.Legs_Part.Speed / 6;
        //SpeedFG.SetDimensions(costWidth, 10);
        //costWidth = 100 * WhoSave.Weapon.HitDam / 4;
        //DamageFG.SetDimensions(costWidth, 10);
        //costWidth = 100 * WhoSave.Weapon.Range / 6;
        //RangeFG.SetDimensions(costWidth, 10);
        AIDesc.text = WhoSave.Head_Part.Name + " AI";
        WeaponDesc.text = WhoSave.Arms_Part.Name;
        ArmorDesc.text = WhoSave.Body_Part.Name;
        LegsDesc.text = WhoSave.Legs_Part.Name;

        //string dmg = "DAM: ";
        //for (int n = 0; n < WhoSave.Weapon.HitDam; n++)
        //    dmg += "*";
        //DamageDesc.text = dmg;
        int num = 0;
        foreach (UI2DSprite sp in DamageIcons)
        {
            if (num < WhoSave.Weapon.HitDam)
                sp.color = Color.white;
            else
                sp.color = GrayedOut;
            num++;
        }

        num = 0;
        foreach (UI2DSprite sp in RangeIcons)
        {
            if (num < WhoSave.Weapon.Range)
                sp.color = Color.white;
            else
                sp.color = GrayedOut;
            num += 2;
        }
        num = 0;
        foreach (UI2DSprite sp in CritIcons)
        {
            if (num < WhoSave.Weapon.CritDam - WhoSave.Weapon.HitDam)
                sp.color = Color.white;
            else
                sp.color = GrayedOut;
            num += 1;
        }

        num = 1;
        foreach (UI2DSprite sp in HealthIcons)
        {
            if (num < WhoSave.Body_Part.Health)
                sp.color = Color.white;
            else
                sp.color = GrayedOut;
            num++;
        }

        num = 1;
        foreach (UI2DSprite sp in SpeedIcons)
        {
            if (num < WhoSave.Legs_Part.Speed)
                sp.color = Color.white;
            else
                sp.color = GrayedOut;
            num++;
        }

        HeadSpecial.text = WhoSave.Head_Part.SpDescription;
        WeaponSpecial.text = WhoSave.Arms_Part.SpDescription;
        BodySpecial.text = WhoSave.Body_Part.SpDescription;
        LegsSpecial.text = WhoSave.Legs_Part.SpDescription;

        //string rng = "RNG: ";
        //for (int n = 0; n < WhoSave.Weapon.Range; n++)
        //    rng += "*";
        //RangeDesc.text = rng;

        //string hp = "HP: ";
        //for (int n = 0; n < WhoSave.Body_Part.Health; n++)
        //    hp += "*";
        //HealthDesc.text = hp;

        //string spd = "SPD: ";
        //for (int n = 0; n < WhoSave.Legs_Part.Speed; n++)
        //    spd += "*";
        //SpeedDesc.text = spd;
        
        string spDesc = "Special: ";
        bool comma = false;
        if (WhoSave.Head_Part.SpDescription != "")
        {
            spDesc += WhoSave.Head_Part.SpDescription;
            comma = true;
        }
        if (WhoSave.Arms_Part.SpDescription != "")
        {
            if (comma)
                spDesc += ", ";
            spDesc += WhoSave.Arms_Part.SpDescription;
            comma = true;
        }
        if (WhoSave.Body_Part.SpDescription != "")
        {
            if (comma)
                spDesc += ", ";
            spDesc += WhoSave.Body_Part.SpDescription;
            comma = true;
        }
        if (WhoSave.Legs_Part.SpDescription != "")
        {
            if (comma)
                spDesc += ", ";
            spDesc += WhoSave.Legs_Part.SpDescription;
        }
        //SpecialDesc.text = spDesc;

        if (Selected.Option == MenuOptions.Head)
        {
            CurrentName.text = WhoSave.Head_Part.Name + " AI";
            CurrentDesc.text = WhoSave.Head_Part.Description;
            CurrentCost.text = "Cost: " + WhoSave.Head_Part.Cost;
        }
        else if (Selected.Option == MenuOptions.Arms)
        {
            CurrentName.text = WhoSave.Arms_Part.Name;
            CurrentDesc.text = WhoSave.Arms_Part.Description;
            CurrentCost.text = "Cost: " + WhoSave.Arms_Part.Cost;
        }
        else if (Selected.Option == MenuOptions.Body)
        {
            CurrentName.text = WhoSave.Body_Part.Name;
            CurrentDesc.text = WhoSave.Body_Part.Description;
            CurrentCost.text = "Cost: " + WhoSave.Body_Part.Cost;
        }
        else if (Selected.Option == MenuOptions.Legs)
        {
            CurrentName.text = WhoSave.Legs_Part.Name;
            CurrentDesc.text = WhoSave.Legs_Part.Description;
            CurrentCost.text = "Cost: " + WhoSave.Legs_Part.Cost;
        }
        //AIDesc.text = WhoSave.Head_Part.AIDesc;
        
        
        int TotalPts = Cub.Model.Library.PointCap;
        int Spent = Team.TotalValue;
        PtsLeft.text = "Pts Left: " + (TotalPts - Spent).ToString() + "pts";
        if (Spent > TotalPts)
            PtsLeft.color = Color.red;
        else if (Spent < TotalPts)
            PtsLeft.color = Color.green;
        else
            PtsLeft.color = Color.white;
        //Cost.text = "Cost: " + WhoSave.Value.ToString() + "pts";
        //AI.text = "AI: " + WhoSave.Head_Part.Description + " (" + WhoSave.Head_Part.Cost.ToString() + "pts)";
        //Weapon.text = "Wpn: " + WhoSave.Arms_Part.Name + " (" + WhoSave.Arms_Part.Cost.ToString() + "pts)";
        //Special.text = "Spcl: " + "--";

        //Health.text = "HP: " + WhoSave.Body_Part.Health.ToString();
        //Speed.text = "Speed: " + WhoSave.Legs_Part.Speed.ToString();
        //Range.text = "Range: " + WhoSave.Weapon.Range.ToString();
        //Damage.text = "Damage: " + WhoSave.Weapon.HitDam.ToString();
    }


    protected virtual void ChangeHoriSelection(int n)
    {
        Cub.Model.Bodypart bp = null;
        if (Selected.Option == MenuOptions.Head) bp = Head;
        else if (Selected.Option == MenuOptions.Arms) bp = Arms;
        else if (Selected.Option == MenuOptions.Body) bp = Body;
        else if (Selected.Option == MenuOptions.Legs) bp = Legs;
        int current = OptionOptions[Selected].IndexOf(bp);
        current += n;
        if (current >= OptionOptions[Selected].Count)
            current = 0;
        else if (current < 0)
            current = OptionOptions[Selected].Count - 1;
        if (Selected.Option == MenuOptions.Head)
        {
            Head = (Cub.Model.BPHead)OptionOptions[Selected][current];
            Who.Stat.Head = ((Cub.Model.BPHead)Head).E;
            WhoSave.Head = ((Cub.Model.BPHead)Head).E;
        }
        else if (Selected.Option == MenuOptions.Arms)
        {
            Arms = (Cub.Model.BPArms)OptionOptions[Selected][current];
            Who.Stat.Arms = ((Cub.Model.BPArms)Arms).E;
            WhoSave.Arms = ((Cub.Model.BPArms)Arms).E;
        }
        else if (Selected.Option == MenuOptions.Body)
        {
            Body = (Cub.Model.BPBody)OptionOptions[Selected][current];
            Who.Stat.Body = ((Cub.Model.BPBody)Body).E;
            WhoSave.Body = ((Cub.Model.BPBody)Body).E;
        }
        else if (Selected.Option == MenuOptions.Legs)
        {
            Legs = (Cub.Model.BPLegs)OptionOptions[Selected][current];
            Who.Stat.Legs = ((Cub.Model.BPLegs)Legs).E;
            WhoSave.Legs = ((Cub.Model.BPLegs)Legs).E;
        }
        Who.Delete_Part();
        Who.Initialize_Part();
        WriteDescriptions();
    }

    protected override void OnSelectChange()
    {
        foreach (MenuChoiceController mcc in Options)
        {
            foreach (Transform t in mcc.transform)
            {
                ((UISprite)t.gameObject.GetComponent("UISprite")).color = Color.white;
            }
        }
        foreach (Transform t in Selected.transform)
        {
            ((UISprite)t.gameObject.GetComponent("UISprite")).color = Color.red;
        }
        WriteDescriptions();
    }

    public void NameUpdate(string name)
    {
        WhoSave.Name = name;
        Who.name = name;
        Name.text = name;
        Clicking = true;
    }
}
