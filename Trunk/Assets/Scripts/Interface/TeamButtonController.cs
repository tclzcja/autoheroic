using UnityEngine;
using System.Collections;

public class TeamButtonController : Cub.Interface.MenuChoiceController
{

    public Cub.Model.TeamSave Team;
    public bool Adder;
    public UILabel TeamName;
    public UILabel OwnerName;


    public void Setup(Cub.Model.TeamSave team)
    {
        Team = team;
        if (Team != null)
        {
            TeamName.text = team.Name;
            OwnerName.text = team.Owner_Name;
            Adder = false;
        }
        else
        {
            TeamName.text = "";
            OwnerName.text = "";
            Adder = false;
        }
    }

    public void SetupAdder()
    {
        Team = null;
        TeamName.text = "ADD NEW TEAM";
        OwnerName.text = "";
        Adder = true;
    }
}
