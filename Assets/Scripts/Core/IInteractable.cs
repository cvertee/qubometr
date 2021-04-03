using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Core
{
    public interface IInteractable
    {
        void Interact();
        void StopInteract();
    }
}
