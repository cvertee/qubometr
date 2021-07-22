using System;
using Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class RunScriptOnStart : MonoBehaviour
    {
        public LuaScriptSO scriptAsset;

        private ScriptService scriptService;

        [Inject]
        public void Construct(ScriptService scriptService)
        {
            this.scriptService = scriptService;
        }
        
        private void Start()
        {
            scriptService.RunScript(scriptAsset.GetScript());
        }
    }
}