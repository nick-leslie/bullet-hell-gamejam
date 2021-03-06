using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class audioManiger : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] BackgroundJams;
    private int currentBackground;
    public int CurrentBackground
    {
        get
        {
            return currentBackground;
        }
        set
        {
            if (value == currentBackground - 1 || value == currentBackground + 1)
            {
                currentBackground = value;
            }
        }
    }
    [Range(0, 1)]
    private float pitch;
    [SerializeField]
    private AudioClip[] Interactions;
    [Range(0, 1)]
    public float backgroundVolume;
    [Range(0, 1)]
    public float sfxVolume;
    private AudioSource[] source;
    // Start is called before the first frame update
    void Start()
    {
        source = new AudioSource[Interactions.Length + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = gameObject.AddComponent<AudioSource>();
            if (i == 0)
            {
                source[i].clip = BackgroundJams[0];
                source[i].volume = backgroundVolume;
                source[i].loop = true;
            }
            else
            {
                source[i].clip = Interactions[i - 1];
                source[i].volume = sfxVolume;
            }
        }
        StartBackground();
        //StartCoroutine(testSoundMethods());
    }
    private void Update()
    {
    }
    public void StartBackground()
    {
        //Debug.Log(BackgroundJams[0].name);
        source[0].Play();
    }
    public void PlayNextBackground()
    {
        currentBackground++;
        source[0].clip = BackgroundJams[currentBackground];
        source[0].Play();
    }
    public void PlayBackgroundSong(string clipName)
    {
        AudioClip song = Array.Find(BackgroundJams, BackgroundJams => BackgroundJams.name == clipName);
        if (song == null)
        {
            return;
        }
        source[0].clip = song;
        source[0].Play();
    }
    public void PlayBackgroundSong(int index)
    {
        Debug.Log(BackgroundJams[index].name);
        if (index < 0 && index > BackgroundJams.Length)
        {
            return;
        }
        source[0].clip = BackgroundJams[index];
        source[0].Play();
    }
    public void PlaySfx(string clipName)
    {
        AudioSource s = Array.Find(source, source => source.clip.name == clipName);
        if (s == null)
        {
            return;
        }
        s.Play();
    }
    public void PlaySfx(int index)
    {
        if (index < 0 && index > BackgroundJams.Length)
        {
            return;
        }
        source[index + 1].Play();
    }
    public void SetVolume(GameObject self)
    {
        //backgroundVolume = self.GetComponent<Scrollbar>().value;
        source[0].volume = backgroundVolume;
    }
    public void SetSfx(GameObject self)
    {
        //sfxVolume = self.GetComponent<Scrollbar>().value;
        for (int i = 1; i < source.Length; i++)
        {
            source[i].volume = sfxVolume;
        }
    }
}
