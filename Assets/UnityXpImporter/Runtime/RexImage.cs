using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace UnityXpReader
{
    /// <summary>
    /// 左上が(0,0)で右下が(width - 1,height - 1)の座標系
    /// </summary>
    public class RexImage : ScriptableObject
    {
        public List<Layer> layers;
        public int width;
        public int height;
        public Font font;

        public void SetData(int w,int h,Font f)
        {
            layers = new List<Layer>();
            width = w;
            height = h;
            font = f;
        }
        public void AddLayer(Layer l)
        {
            layers.Add(l);
        }
        public void InsertLayer(Layer l, int i)
        {
            layers.Insert(i, l);
        }
        public void AddImage(RexImage image)
        {
            foreach(Layer l in image.layers)
            {
                layers.Add(l);
            }
        }
        public void InsertImage(RexImage image,int i)
        {
            foreach (Layer l in image.layers)
            {
                layers.Insert(i,l);
                i++;
            }
        }
        public void AddBlank(int count = 1)
        {
            if (count <= 0) return;
            for(int i = 0;i < count; i++)
            {
                layers.Add(Layer.blankLayer(width, height));
            }
        }
        public void InsertBlank(int index, int count = 1)
        {
            if (count <= 0) return;
            for (int i = 0; i < count; i++)
            {
                layers.Insert(index, Layer.blankLayer(width, height));
            }
        }
        public void StampLayer(Layer l,int i,int x = 0,int y = 0)
        {
            if(i < 0 || layers.Count <= i)
            {
                Debug.Log("範囲外指定すんなボケ");
                return;
            }
            layers[i].Stamp(l,x,y);
        }
        public void StampImage(RexImage image, int index, int x = 0, int y = 0)
        {
            int gap = image.layers.Count + index - layers.Count;
            AddBlank(gap);
            for(int i = 0;i < image.layers.Count; i++)
            {
                StampLayer(image.layers[i], i + index);
            }
        }
        public void Remove(int i)
        {
            layers.RemoveAt(i);
        }
        public void Remove(Layer l)
        {
            layers.Remove(l);
        }
        public static Color32 GetColor(byte r,byte g,byte b)
        {
            if(r == 255 && g == 0 && b == 255)
            {
                return new Color32(0, 0, 0, 0);
            }
            return new Color32(r, g, b, 255);
        }
        public static RexImage CreateNewRexImage(Stream stream,Font f)
        {
            GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress);
            using(BinaryReader reader = new BinaryReader(gzip))
            {
                int version = reader.ReadInt32();
                int layerCount = reader.ReadInt32();
                RexImage rexImage = null;
                for(int i = 0;i < layerCount; i++)
                {
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();
                    if (i == 0)
                    {
                        rexImage = CreateInstance<RexImage>();
                        rexImage.SetData(width, height,f);

                    }
                    rexImage.AddBlank();
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            rexImage.layers[i].Set(x, y, new Cell((char)reader.ReadInt32(), new Color32(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(),255), GetColor(reader.ReadByte(), reader.ReadByte(), reader.ReadByte())));
                        }
                    }
                }
                return rexImage;
            }
        }
    }
}