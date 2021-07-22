using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LuaScriptName", menuName = "Lua/Script", order = 0)]
    public class LuaScriptSO : ScriptableObject
    {
        public string script;

        public string GetScript() => script;
    }
}