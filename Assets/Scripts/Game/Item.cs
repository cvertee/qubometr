using System;
using UnityEngine;
using Zenject;


[RequireComponent(typeof(Animator))]
public class Item : MonoBehaviour
{
    public ItemSO data;

    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected GameSettingsSO gameSettings;

    [Inject]
    public void Init(GameSettingsSO gameSettings)
    {
        this.gameSettings = gameSettings;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    public void Initialize(ItemSO data)
    {
        this.data = data;
        
        spriteRenderer.sprite = data.icon;
        animator.runtimeAnimatorController = data.animatorController;
    }

    public virtual void Use()
    {
        
    }

    public virtual void StopUse()
    {
        
    }

    public class Factory : PlaceholderFactory<GameSettingsSO, Item>
    {
        
    }
}