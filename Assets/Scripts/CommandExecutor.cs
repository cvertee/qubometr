using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandExecutor : Singleton<CommandExecutor>
{
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
                AudioManager.Instance.PlaySound("keyPickup");
                break;
            case "!giveItem":
                var itemName = args[0];
                var player = FindObjectOfType<Player>(); // TODO: no searching for player
                GameManager.Instance.AddItemById(itemName, player);
                AudioManager.Instance.PlaySound("keyPickup"); // TODO: replace
                break;
        }
    }
}
