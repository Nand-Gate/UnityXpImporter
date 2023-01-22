using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityXpReader
{
    [System.Serializable]
    public class Layer
    {
        public int width;
        public int height;
        public int index;

        public Cell[] cells;
        public Layer(int w,int h)
        {
            width = w;
            height = h;
            index = w * h;
            cells = new Cell[index];
        }
        public static Layer blankLayer(int w, int h)
        {
            Layer l = new Layer(w, h);
            l.Fill();
            return l;
        }
        private bool IsOutOfLength(int i)
        {
            return i < 0 || index <= i;
        }
        private bool IsOutOfLength(int x,int y)
        {
            if (x < 0 || width <= x) return true;
            if (y < 0 || height <= y) return true;
            return false;
        }
        private int PosToIndex(int x,int y)
        {
            if (IsOutOfLength(x,y)) return -1;
            return y * width + x;
        }
        public Cell Get(int i)
        {
            if (IsOutOfLength(i)) return Cell.Blank;
            return cells[i];
        }
        public Cell Get(int x,int y)
        {
            if (IsOutOfLength(x, y)) return Cell.Blank;
            return cells[PosToIndex(x, y)];
        }
        public void Set(int i,Cell v)
        {
            if (IsOutOfLength(i)) return;
            cells[i] = v;
        }
        public void Set(int x,int y ,Cell v)
        {
            if (IsOutOfLength(x,y)) return;
            cells[PosToIndex(x, y)] = v;
        }
        public void Fill()
        {
            for(int i = 0;i < index; i++)
            {
                cells[i] = Cell.Blank;
            }
        }
        public void Stamp(Layer s, int posx = 0,int posy = 0)
        {
            for(int y = 0;y < s.height && posy + y >= height; y++)
            {
                for(int x = 0;x < s.width && posx + x >= width; x++)
                {
                    Set(posx + x, posy + y, s.Get(x, y));
                }
            }
        }
    }
}
