using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{

    //GetComponent<PlayerAudio>().Walk((currentOffset.magnitude > 0.01f)? true:false);
  
    AudioSource s;

    private void Start() {
        s = GetComponent<AudioSource>();
    }
    public void Walk(bool walking){
        if(){}
        if(walking){if(!s.isPlaying){
            s.Play(0);Debug.Log("aaaaaaaaa");}}
        else if (s.isPlaying){s.Stop();}
    }
}
