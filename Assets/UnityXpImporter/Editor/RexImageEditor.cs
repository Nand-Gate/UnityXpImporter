using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace UnityXpReader
{
    [CustomEditor(typeof(RexImage))]
    public class RexImageEditor : Editor
    {
        private RexImage rexImage;
        private void OnEnable()
        {
            rexImage = (RexImage)target;
        }
        public override void OnInspectorGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            EditorGUILayout.LabelField("Width:" + rexImage.width,style);
            EditorGUILayout.LabelField("Height:" + rexImage.height, style);
            EditorGUILayout.LabelField("Layers:" + rexImage.layers.Count, style);
            EditorGUILayout.LabelField("Å°Preview", style);
            Rect inspectorSize = EditorGUILayout.GetControlRect();
            GUIStyle previewstyle = new GUIStyle();
            int cellsize = (int)(inspectorSize.width / (float)rexImage.width);
            previewstyle.font = rexImage.font;
            previewstyle.fontSize = (int)(inspectorSize.width / (float)rexImage.width);
            previewstyle.normal.textColor = Color.white;
            float previewY = EditorGUIUtility.singleLineHeight * 5;
            Rect textrect = new Rect(0, previewY, 0, 0);
            EditorGUI.DrawRect(new Rect(0, previewY, cellsize * rexImage.width, cellsize * rexImage.height), Color.black);
            foreach (Layer layer in rexImage.layers)
            {
                StringBuilder builder = new StringBuilder();
                for (int y = 0; y < layer.height; y++)
                {
                    for (int x = 0; x < layer.width; x++)
                    {

                        Rect backRect = new Rect(x * cellsize, previewY +  y * cellsize, cellsize, cellsize);
                        Cell cell = layer.Get(x, y);
                        EditorGUI.DrawRect(backRect, cell.backGroundColor);
                        builder.Append("<color=#").Append(ColorUtility.ToHtmlStringRGB(cell.color)).Append(">");
                        builder.Append(cell.value).Append("</color>");
                    }
                    if (y < layer.height - 1) builder.Append("\n");
                }
                EditorGUI.LabelField(textrect, builder.ToString(), previewstyle);
            }
        }
    }
}

