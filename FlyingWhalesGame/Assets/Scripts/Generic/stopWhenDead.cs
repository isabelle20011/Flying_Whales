using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopWhenDead : MonoBehaviour
{
    private AudioSource audio;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.mute = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerHealth>().Health <= 0)
        {
            audio.mute = true;
        }
    }
}
