using Save;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MenuSaveButton : MonoBehaviour
    {

        // Use this for initialization
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => {
                SaveSystem.Save();
                UIManager.Instance.CloseLatestPopup(); // should be menu
            });
        }
    }
}