using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudColorChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public static BackgroudColorChange instance;
    public List<AudioClip> backgroundSounds = new List<AudioClip>();
    AudioSource audioSource;
    AudioClip audioClip;
    [SerializeField]List<Color> colors = new List<Color>();

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundSounds[Random.Range(0, backgroundSounds.Count)];
        audioClip = audioSource.clip;
        StartCoroutine(playMusic());
        instance = this;
    }

    IEnumerator playMusic()
    {
        yield return new WaitForSeconds(5);
        audioSource.Play();
    }

    void ChangeMusic()
    {
        int ClipIndex = Random.Range(0, backgroundSounds.Count);
        while (audioSource.clip == backgroundSounds[ClipIndex])
        {
            ClipIndex = Random.Range(0, backgroundSounds.Count);
        }
        audioSource.clip = backgroundSounds[ClipIndex];
        audioClip = audioSource.clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.time >= audioClip.length - 0.5f)
        {
            audioSource.Stop();
            ChangeMusic();
        }
        ChangeColor();
        
    }

    public void ChangeColor()
    {
            if(SpectrumAnalise.instance.spectrumValue > 0.03f)
                {
                spriteRenderer.color = colors[Random.Range(0, colors.Count)];
                }

    }
}

