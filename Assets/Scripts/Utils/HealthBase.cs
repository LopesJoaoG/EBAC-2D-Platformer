using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife;
    private int currentLife;

    public float delayToKill;

    public bool destroyOnKill = false;
    private bool isDead = false;

    private void Awake()
    {
        Init();
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
        
    }
}
