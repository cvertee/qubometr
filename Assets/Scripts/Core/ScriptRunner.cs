using Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ScriptRunner : MonoBehaviour
    {
        [TextArea(1, 255)]
        public LuaScriptSO script;
        
        private ScriptService _scriptService;

        [Inject]
        public void Construct(ScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        public void Run(LuaScriptSO scriptData)
        {
            Run(scriptData.GetScript());
        }

        public void Run(TextAsset textAsset)
        {
            Run(textAsset.text);
        }

        private void Run(string scriptText)
        {
            _scriptService.RunScript(scriptText);
        }
    }
}