using UnityEngine;
using System.Collections;
using Cub.View;
using Cub;
using System.Collections.Generic;

public class CharacterEditorController : MonoBehaviour
{
    /*
    GameObject Mod;
    GameObject GO_Head;
    GameObject GO_Body;
    GameObject GO_Hand_Left;
    GameObject GO_Hand_Right;
    GameObject GO_Foot_Left;
    GameObject GO_Foot_Right;
    GameObject GO_Equipment_Left;
    GameObject GO_Equipment_Right;
    List<GameObject> All_GO;

    //List<Cube> All_Cube;

    Character_Model CM;
    Character_Model CMT;

    bool TeamOne = true;

    float Timer = 0;
    float MaxTimer = 1;

    UIPopupList UIPL;

    // Use this for initialization
    void Start()
    {
        Cub.View.Library.Initialization();

        UIPL = (UIPopupList)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("UIPopupList"));
        SpawnModel("Knight");
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = MaxTimer;
            RedrawButton();
        }
    }

    public void LoadButton()
    {
        SpawnModel(UIPL.value);
    }

    public void ToggleButton()
    {
        if (TeamOne) TeamOne = false;
        else TeamOne = true;
    }

    public void RedrawButton()
    {
        foreach (GameObject go in All_GO)
            foreach (Transform child in go.transform.FindChild("Model").transform)
                if (child.name != "Equipment_Left" && child.name != "Equipment_Right")
                {
                    Cube cube = ((Cube)child.GetComponent("Cube"));
                    //cube.SetMaterial(cube.CubeType, TeamOne);
                }
    }

    public void RoundingButton()
    {
        foreach (GameObject go in All_GO)
            foreach (Transform child in go.transform.FindChild("Model").transform)
                if (child.name != "Equipment_Left" && child.name != "Equipment_Right")
                {
                    Vector3 pos = child.localPosition;
                    Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
                    Debug.Log(newPos.x + " " + newPos.y + " " + newPos.z);
                    child.localPosition = newPos;
                    // .Set(newPos.x,newPos.y,newPos.z);
                }
    }

    public void SaveButton()
    {
        RoundingButton();
        CMT = new Character_Model();
        //CMT.Class = CM.Class;
        CMT.Position_Body = CM.Position_Body;
        CMT.Position_Body_Head = CM.Position_Body_Head;
        CMT.Position_Body_Arms_Left = CM.Position_Body_Arms_Left;
        CMT.Position_Body_Arms_Right = CM.Position_Body_Arms_Right;
        CMT.Position_Body_Legs_Left = CM.Position_Body_Legs_Left;
        CMT.Position_Body_Legs_Right = CM.Position_Body_Legs_Right;
        //CMT.Position_Hand_Left_Equipment_Left = CM.Position_Hand_Left_Equipment_Left;
        //CMT.Position_Hand_Right_Equipment_Right = CM.Position_Hand_Right_Equipment_Right;
        CMT.Rotation_Body = CM.Rotation_Body;
        CMT.Rotation_Body_Head = CM.Rotation_Body_Head;
        CMT.Rotation_Body_Arms_Left = CM.Rotation_Body_Arms_Left;
        CMT.Rotation_Body_Arms_Right = CM.Rotation_Body_Arms_Right;
        CMT.Rotation_Body_Legs_Left = CM.Rotation_Body_Legs_Left;
        CMT.Rotation_Body_Legs_Right = CM.Rotation_Body_Legs_Right;
        //CMT.Rotation_Hand_Left_Equipment_Left = CM.Rotation_Hand_Left_Equipment_Left;
        //CMT.Rotation_Hand_Right_Equipment_Right = CM.Rotation_Hand_Right_Equipment_Right;

        List<Cubon> r = new List<Cubon>();
        foreach (Transform child in GO_Head.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Head = r;
        r = new List<Cubon>();
        foreach (Transform child in GO_Body.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Body = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Hand_Left.transform.FindChild("Model").transform)
        {
            if (child.name == "Equipment_Left")
                continue;
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Hand_Left = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Hand_Right.transform.FindChild("Model").transform)
        {
            if (child.name == "Equipment_Right")
                continue;
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Hand_Right = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Foot_Left.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Foot_Left = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Foot_Right.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Foot_Right = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Equipment_Left.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Equipment_Left = r;

        r = new List<Cubon>();
        foreach (Transform child in GO_Equipment_Right.transform.FindChild("Model").transform)
        {
            Cube cube = ((Cube)child.GetComponent("Cube"));
            r.Add(new Cubon(cube.CubeType, new
                Position3((int)child.transform.localPosition.x, (int)child.transform.localPosition.y, (int)child.transform.localPosition.z)));
        }
        CMT.Cubon_Equipment_Right = r;


        Cub.Tool.Xml.Serialize(CMT, "Data/Character_Model_" + UIPL.value + ".xml");

        Debug.Log("SAVED!");
    }

    void SpawnModel(string str)
    {
        All_GO = new List<GameObject>();
        //All_Cube = new List<Cube>();

        if (Mod != null)
            Destroy(Mod);

        Mod = (GameObject)Instantiate(Cub.View.Library.Get_Character(), Vector3.zero, Quaternion.identity);

        ((Animator)Mod.GetComponent("Animator")).enabled = false;

        GO_Head = Mod.transform.FindChild("Head").gameObject;
        GO_Body = Mod.transform.FindChild("Body").gameObject;
        GO_Hand_Left = Mod.transform.FindChild("Hand_Left").gameObject;
        GO_Hand_Right = Mod.transform.FindChild("Hand_Right").gameObject;
        GO_Foot_Left = Mod.transform.FindChild("Foot_Left").gameObject;
        GO_Foot_Right = Mod.transform.FindChild("Foot_Right").gameObject;
        GO_Equipment_Left = GO_Hand_Left.transform.FindChild("Model/Equipment_Left").gameObject;
        GO_Equipment_Right = GO_Hand_Right.transform.FindChild("Model/Equipment_Right").gameObject;

        All_GO.Add(GO_Head);
        All_GO.Add(GO_Body);
        All_GO.Add(GO_Hand_Left);
        All_GO.Add(GO_Hand_Right);
        All_GO.Add(GO_Foot_Left);
        All_GO.Add(GO_Foot_Right);
        All_GO.Add(GO_Equipment_Left);
        All_GO.Add(GO_Equipment_Right);

        Character_Model Model = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model),
            "Data/Character_Model_" + str + ".xml") as Cub.View.Character_Model;

        CM = Model;

        GO_Body.transform.localPosition = Model.Position_Body;
        GO_Head.transform.localPosition = Model.Position_Body + Model.Position_Body_Head;
        GO_Hand_Left.transform.localPosition = Model.Position_Body + Model.Position_Body_Arms_Left;
        GO_Hand_Right.transform.localPosition = Model.Position_Body + Model.Position_Body_Arms_Right;
        GO_Foot_Left.transform.localPosition = Model.Position_Body + Model.Position_Body_Legs_Left;
        GO_Foot_Right.transform.localPosition = Model.Position_Body + Model.Position_Body_Legs_Right;
        GO_Equipment_Left.transform.localPosition = Model.Position_Body + Model.Position_Hand_Left_Equipment_Left;
        GO_Equipment_Right.transform.localPosition = Model.Position_Body + Model.Position_Hand_Right_Equipment_Right;

        GO_Body.transform.rotation = Quaternion.Euler(Model.Rotation_Body);
        GO_Head.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Head);
        GO_Hand_Left.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Arms_Left);
        GO_Hand_Right.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Arms_Right);
        GO_Foot_Left.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Legs_Left);
        GO_Foot_Right.transform.rotation = Quaternion.Euler(Model.Rotation_Body_Foot_Right);
        GO_Equipment_Left.transform.rotation = Quaternion.Euler(Model.Rotation_Hand_Left_Equipment_Left);
        GO_Equipment_Right.transform.rotation = Quaternion.Euler(Model.Rotation_Hand_Right_Equipment_Right);

        foreach (Cubon C in Model.Cubon_Head)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Head.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);  
        }

        foreach (Cubon C in Model.Cubon_Body)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Body.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Hand_Left)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Hand_Left.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Hand_Right)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Hand_Right.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            // All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Foot_Left)
        {
            // Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Foot_Left.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Foot_Right)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Foot_Right.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Equipment_Left)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Equipment_Left.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            // All_Cube.Add(cube);
        }

        foreach (Cubon C in Model.Cubon_Equipment_Right)
        {
            //Material M = Cub.View.Library.Get_Cube(C.CubeType, TeamOne);
            GameObject G = Instantiate(Library.Get_Cube()) as GameObject;
            Cube cube = (Cube)G.GetComponent("Cube");
            cube.SetMaterial(C.Colour, TeamOne);
            G.transform.parent = GO_Equipment_Right.transform.FindChild("Model").transform;
            G.transform.localPosition = C.Position.ToVector3();
            G.transform.localScale = G.transform.lossyScale;
            G.transform.localRotation = Quaternion.identity;
            //All_Cube.Add(cube);
        }

        foreach (GameObject g in All_GO)
            g.transform.rotation = Quaternion.identity;
    }

    Colour ReverseMaterial(Material ct)
    {
        Debug.Log(ct.name);
        Colour r = Colour.Black;
        //foreach (CubeType c in CTDict.Keys)
        //    if (ct == CTDict[c])
        //    {
        //        r = c;
        //        break;
        //    }
        //if (r == CubeType.TeamColorOneA || r == CubeType.TeamColorOneB)
        //    r = CubeType.TeamColorOne;
        //if (r == CubeType.TeamColorTwoA || r == CubeType.TeamColorTwoB)
        //    r = CubeType.TeamColorTwo;
        //if (r != CubeType.Black)
        //    Debug.Log(r.ToString());
        return r;
    }
     * */
}