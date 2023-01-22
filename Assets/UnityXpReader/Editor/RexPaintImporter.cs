using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
namespace UnityXpReader
{
    [ScriptedImporter(1, "xp")]
    public class RexPaintImporter : ScriptedImporter
    {
        [SerializeField] private Font font;
        public override void OnImportAsset(AssetImportContext ctx)
        {
            RexImage rexImage = ScriptableObject.CreateInstance<RexImage>();
            rexImage = RexImage.CreateNewRexImage(File.Open(ctx.assetPath, FileMode.Open), font);
            ctx.AddObjectToAsset("Image", rexImage);
            ctx.SetMainObject(rexImage);
            try
            {
            }
            catch(System.Exception e)
            {
                Debug.LogError(e.Message, rexImage);
            }
        }
    }
}
