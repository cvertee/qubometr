using System.Collections;
using System.Text;
using Assets.Scripts.Game;
using Save;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MenuStatsButton : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                StatsUI.Instance.Show();
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}