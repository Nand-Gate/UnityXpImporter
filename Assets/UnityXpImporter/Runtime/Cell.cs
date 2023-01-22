using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityXpReader
{
    [System.Serializable]
    public struct Cell
    {
        public char value;
        public Color32 color;
        public Color32 backGroundColor;
        public Cell(char v,Color32 c,Color32 b)
        {
            value = v;
            color = c;
            backGroundColor = b;
        }
        public static Cell Blank
        {
            get { return new Cell(' ', new Color32(0, 0, 0, 0), new Color32(0, 0, 0, 0)); }
        }
    }
}
