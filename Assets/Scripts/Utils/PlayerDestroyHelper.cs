using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    public void KillPlayer()
    {
        player.DestroyMe();
    }
}
