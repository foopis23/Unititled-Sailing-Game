using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    
    public Vector4 waveA;
    public Vector4 waveB;
    public Vector4 waveC;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Instance of Wave Manager Already Exists");
            Destroy(gameObject);
        }
    }

    private float GerstnerWave (Vector4 wave, Vector3 p) {
        var steepness = wave.z;
        var wavelength = wave.w;
        var k = 2 * Mathf.PI / wavelength;
        var c = Mathf.Sqrt(9.8f / k);
        var d = new Vector2(wave.x, wave.y).normalized;
        var f = k * (Vector2.Dot(d, new Vector2(p.x, p.z)) - c * Time.time);
        var a = steepness / k;

        return a * Mathf.Sin(f);
    }

    public float GetWaveHeight(Vector3 pos)
    {
        return GerstnerWave(waveA, pos) + GerstnerWave(waveB, pos) + GerstnerWave(waveC, pos);
    }
}
