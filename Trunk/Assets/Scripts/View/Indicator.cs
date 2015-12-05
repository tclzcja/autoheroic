using System;
using UnityEngine;

namespace Cub.View
{
    public class Indicator : MonoBehaviour
    {
        private const float Height = -0.3F;

        public static void Cross(Position2 _Center)
        {
            Instantiate(Library.Get_Indicator(), new Vector3(_Center.X, Height, _Center.Y), Quaternion.identity);
            Instantiate(Library.Get_Indicator(), new Vector3(_Center.X + 1, Height, _Center.Y), Quaternion.identity);
            Instantiate(Library.Get_Indicator(), new Vector3(_Center.X - 1, Height, _Center.Y), Quaternion.identity);
            Instantiate(Library.Get_Indicator(), new Vector3(_Center.X, Height, _Center.Y + 1), Quaternion.identity);
            Instantiate(Library.Get_Indicator(), new Vector3(_Center.X, Height, _Center.Y - 1), Quaternion.identity);
        }

        public static void Generate(Position2 _From, Position2 _To)
        {
            Position2 _Now = _From;
            
            while (_Now != _To)
            {
                Instantiate(Library.Get_Indicator(), new Vector3(_Now.X, Height, _Now.Y), Quaternion.identity);

                if (_Now.X < _To.X)
                {
                    _Now.X++;
                }
                else if (_Now.X > _To.X)
                {
                    _Now.X--;
                }
                else if (_Now.Y < _To.Y)
                {
                    _Now.Y++;
                }
                else if (_Now.Y > _To.Y)
                {
                    _Now.Y--;
                }
            }

            Instantiate(Library.Get_Indicator(), new Vector3(_Now.X, Height, _Now.Y), Quaternion.identity);

            //Instantiate(Library.Get_Indicator(), new Vector3(_From.X, Height, _From.Y), Quaternion.identity);
            //Instantiate(Library.Get_Indicator(), new Vector3(_To.X, Height, _To.Y), Quaternion.identity);
        }

        public void Start()
        {
            //Debug.Log("FDSA");
            iTween.ScaleTo(this.gameObject, new Vector3(0.5F, 0.1F, 0.5F), 0.5F);
            Invoke("End", 1.0F);
        }

        public void End()
        {
            iTween.ScaleTo(this.gameObject, new Vector3(0F, 0.1F, 0F), 0.5F);
            Destroy(this.gameObject, 0.5F);
        }
    }
}
