using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife;
    private int currentLife;

    public float delayToKill;

    public bool destroyOnKill = false;
    private bool isDead = false;

    [SerializeField] public FlashColor _flashColor;

    public Action OnKill;
    private void Awake()
    {
        Init();
        
        if(_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        isDead = false;
        currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (isDead) return;

        currentLife -= damage;

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }

        if(currentLife <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        isDead = true;
        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }

        OnKill?.Invoke();
    }
}
