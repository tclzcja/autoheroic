using UnityEngine;
using System.Collections;

namespace Cub.View
{
    public static class NarratorController
    {
        static DescriptionManager Desc;

        public static void Initialize(DescriptionManager desc)
        {
            Desc = desc;
        }

        public static void DisplayText(string text, float time){
            Desc.gameObject.SetActive(true);
            Desc.DisplayImage(text,time);
        }
    }
}