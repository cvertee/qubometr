using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class RunScriptOnStart : MonoBehaviour
    {
        public TextAsset scriptAsset;

        private ScriptService scriptService;

        [Inject]
        public void Construct(ScriptService scriptService)
        {
            this.scriptService = scriptService;
        }
        
        private void Start()
        {
            scriptService.RunScript(scriptAsset.text);
        }
    }
}