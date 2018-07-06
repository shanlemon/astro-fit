﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// It's a singleton
/// </summary>
public class BeatDriver : MonoBehaviour {

    public static BeatDriver Instance;
    
    List<Beat> beatz;

    [SerializeField]
    private AudioSource song;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private float timeItTakesForTheBeatToActuallyGetToThePlayer;

    [SerializeField]
    private float startDelay;
    
	void Start() {

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        beatz = new List<Beat>();

        //for (int i = 0; i < 172; i++)
        //{
        //    //int randomLoc = Random.Range(1, 9);
        //    beatz.Add(new Beat(i, Random.Range(1, 9)));
        //    if (i % 4 == 1)
        //    {
        //        beatz.Add(new Beat(i + .5f, Random.Range(1, 9)));
        //    }
        //}
        
        StartCoroutine(StartSong());
        Begin();

    }
    
    private void Begin() {

        foreach (Beat beat in beatz) {
            StartCoroutine(SpawnOnTime(beat));
        }
    }

    IEnumerator SpawnOnTime(Beat beat) {

        float time = beat.timestamp - timeItTakesForTheBeatToActuallyGetToThePlayer + startDelay;

        yield return new WaitForSeconds(time);

        spawner.Spawn(beat);
    }

    IEnumerator StartSong() {

        yield return new WaitForSeconds(startDelay);
        
        song.Play();
    }
    
}