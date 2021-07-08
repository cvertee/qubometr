using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CommandExecutor : Singleton<CommandExecutor>
{
    private AudioManager audioManager;
    private GameManager gameManager;

    [Inject]
    public void Init(
        AudioManager audioManager,
        GameManager gameManager)
    {
        this.audioManager = audioManager;
        this.gameManager = gameManager;
    }
    
    public void ExecuteRawCommand(string rawCommand)
    {
        Debug.Log($"CE: attempt to exec {rawCommand}");
        
        var array = rawCommand.Split(',');
        var command = array[0];
        var args = array.Skip(1).ToArray();
        
        ExecuteCommand(command, args);
    }
    
    public void ExecuteCommand(string command, string[] args)
    {
        Debug.Log($"CE: executing {command} with {args.Length} arguments");
        
        switch (command)
        {
            case "!giveKey":
                var keyName = args[0];
                var key = Resources.Load<Key>($"Items/Keys/{keyName}");
                GameData.AddKey(key);
                audioManager.PlaySound(AudioResource.KeyPickup);
                break;
            case "!giveItem":
                var itemName = args[0];
                var player = FindObjectOfType<Player>(); // TODO: no searching for player
                gameManager.AddItemById(itemName, player);
                audioManager.PlaySound(AudioResource.KeyPickup); // TODO: replace
                break;
            case "!activateTrigger":
                var objectName = args[0];
                var foundObject = FindObjectsOfType<AreaTrigger>().FirstOrDefault(x => x.name == objectName);
                foundObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
        }
    }
}
