using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NewGameButton : MonoBehaviour
{
    private GameManager gameManager;

    [Inject]
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SaveSystem.EmptyAllData();
            gameManager.LoadScene("intro");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
