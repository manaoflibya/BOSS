using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePanel : MonoBehaviour
{
    public void AddPlayerDieSound()
    {
        PlayerActions player = FindObjectOfType<PlayerActions>();

        SoundManager.I.PlayEffectSound(player.Die_S);


    }
}
