using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatsButton : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { StatsUI.Instance.Show(); });
    }

    // Update is called once per frame
    private void Update()
    {
    }
}