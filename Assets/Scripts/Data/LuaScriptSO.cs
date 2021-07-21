using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LuaScriptName", menuName = "Lua/Script", order = 0)]
    public class LuaScriptSO : ScriptableObject
    {
        [SerializeField] private TextAsset textAsset;

        public string GetScript() => textAsset.text;
    }
}