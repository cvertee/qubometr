using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, "lua")]
public class LuaImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var scriptSo = ScriptableObject.CreateInstance<LuaScriptSO>();
        var script = File.ReadAllText(ctx.assetPath);

        scriptSo.script = script;

        ctx.AddObjectToAsset("Script", scriptSo);
        ctx.SetMainObject(scriptSo);
    }
}
