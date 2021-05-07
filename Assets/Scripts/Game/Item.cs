using System;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum ItemType
    {
        Weapon,
        Shield,
        Armor,
        Unknown
    }
    
    [RequireComponent(typeof(Animator))]
    public class Item : MonoBehaviour
    {
        public string id;
        public ItemType type;
        public Sprite icon;
        public RuntimeAnimatorController animatorController;
        public Player owner; // TODO: possibly character or IItemHolder etc
        public int price;
        public AudioClip pickupSound;

        public bool isBeingUsed = false;
        public float protectionMultiplier = 0.0f;
        
        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
        }

        public virtual void Use()
        {
            throw new NotImplementedException();
        }

        public virtual void StopUse()
        {
            throw new NotImplementedException();
        }
    }
}