using System;
using MoonSharp.Interpreter;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ScriptService : MonoBehaviour
    {
        private Script _script;
        private IObjectDestroyer _objectDestroyer;

        [Inject]
        public void Construct(IObjectDestroyer objectDestroyer)
        {
            _objectDestroyer = objectDestroyer;
        }

        private void Awake()
        {
            _script = new Script();
            
            _script.Globals["log"] = (Action<string>) (Debug.Log);

            _script.Globals["showSprite"] = (Action<string>) (objName =>
            {
                var go = GameObject.Find(objName);

                go.GetComponent<SpriteRenderer>().enabled = true;
            });
            
            _script.Globals["hideSprite"] = (Action<string>) (objName =>
            {
                var go = GameObject.Find(objName);

                go.GetComponent<SpriteRenderer>().enabled = false;
            });

            _script.Globals["destroy"] = (Action<string>) (objName =>
            {
                _objectDestroyer.DestroyAndSave(GameObject.Find(objName));
            });
            
            _script.Globals["enableTrigger"] = (Action<string>) (objName =>
            {
                var go = GameObject.Find(objName);

                go.GetComponent<BoxCollider2D>().enabled = true;
            });
            _script.Globals["disableTrigger"] = (Action<string>) (objName =>
            {
                var go = GameObject.Find(objName);

                go.GetComponent<BoxCollider2D>().enabled = false;
            });
        }

        public void RunScript(string scriptText)
        {
            _script.DoString(scriptText);
        }
    }
}