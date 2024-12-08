using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> audioClipList;

    public List<AudioSource> audioSourceList;

    private int _index = 0;

    public PlayerController player;

    public void PlayRandom()
    {
        if (_index >= audioSourceList.Count) _index = 0;

        var audioSource = audioSourceList[_index];

        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];

        if (player.isGrounded())
        {
            audioSource.Play();
        }

        _index++;
    }
}
