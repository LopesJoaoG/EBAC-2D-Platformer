using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;


    public string compareTag = "Player";
    public string collectableTag;
    public ParticleSystem coinsparticleSystem;
    public GameObject graphicItem;

    


    public void Awake()
    {
        if (coinsparticleSystem != null) coinsparticleSystem.transform.SetParent(null);
        if (audioSource != null) audioSource.transform.SetParent(null);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            gameObject.SetActive(false);
            Collect();
        }
    }
    protected virtual void Collect()
    {
        OnCollect();
    }

    protected virtual void OnCollect()

    {
        if (coinsparticleSystem != null) coinsparticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}
