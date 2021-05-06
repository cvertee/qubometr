using Save;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ContinueButton : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (SaveSystem.GetSaveData() == null)
            {
                Destroy(gameObject);
                return;
            }

            GetComponent<Button>().onClick.AddListener(() => SaveSystem.Load());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}