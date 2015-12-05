using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AM
{
    public class Test : MonoBehaviour
    {
        public void Awake()
        {
            _Field();
            _Character();
        }

        private void _Field()
        {
            Field.Terrains = new Type.TTerrain[3][];
            Field.Terrains[0] = new Type.TTerrain[5] { Type.TTerrain.Grass, Type.TTerrain.Grass, Type.TTerrain.Grass, Type.TTerrain.Grass, Type.TTerrain.Grass };
            Field.Terrains[1] = new Type.TTerrain[5] { Type.TTerrain.Grass, Type.TTerrain.Grass, Type.TTerrain.Grass, Type.TTerrain.Desert, Type.TTerrain.Desert };
            Field.Terrains[2] = new Type.TTerrain[5] { Type.TTerrain.Desert, Type.TTerrain.Desert, Type.TTerrain.Desert, Type.TTerrain.Desert, Type.TTerrain.Desert };

            Field.Heights = new int[3][];
            Field.Heights[0] = new int[5] { 0, 0, 0, 0, 0 };
            Field.Heights[1] = new int[5] { 0, 0, 0, 0, 0 };
            Field.Heights[2] = new int[5] { 0, 0, 0, 0, 0 };

            Field.Units = new GameObject[3][];
            Field.Units[0] = new GameObject[5] { null, null, null, null, null };
            Field.Units[1] = new GameObject[5] { null, null, null, null, null };
            Field.Units[2] = new GameObject[5] { null, null, null, null, null };
        }

        private void _Character()
        {
            List<AM.Actions.Action> LAO = new List<Actions.Action>();

            List<AM.Equipments.Equipment> LEO = new List<Equipments.Equipment>();

            AM.Tactic T1 = new Tactic(AM.Type.TTarget.Self, new List<Type.TSituation>() { AM.Type.TSituation.No_Enemy_Insight }, Type.TAction.Explore, 100);
            //AM.Tactic T2 = new Tactic(AM.Type.TTarget.Self, new List<Type.TSituation>() { AM.Type.TSituation.No_Enemy_Insight }, Type.TAction.Observe, 30);
            //AM.Tactic T3 = new Tactic(AM.Type.TTarget.Enemy, new List<Type.TSituation>() { AM.Type.TSituation.Not_In_Melee_Range }, Type.TAction.Approach, 50);
            AM.Tactic T4 = new Tactic(AM.Type.TTarget.Enemy, new List<Type.TSituation>() { AM.Type.TSituation.In_Melee_Range }, Type.TAction.Attack, 30);

            List<AM.Tactic> LTO = new List<Tactic>() { T1, T4 };

            GameObject _Fat = Instantiate(Library.Prefab_Character_Fat, new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
            GameObject _Normal = Instantiate(Library.Prefab_Character_Normal, new Vector3(2, 1, 2), Quaternion.identity) as GameObject;

            AM.Field.Units[0][0] = _Fat;
            AM.Field.Units[2][2] = _Normal;

            AM.Character Fat = _Fat.GetComponent<AM.Character>();
            Fat.Pump("Fatty", true, LAO, LEO, LTO);

            AM.Character Normal = _Normal.GetComponent<AM.Character>();
            Normal.Pump("Normmy", true, LAO, LEO, LTO);

            Fat.Stats_Position = new Vector2(0, 0);
            Normal.Stats_Position = new Vector2(2, 2);
        }
    }
}
