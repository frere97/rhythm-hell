using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAnalise : MonoBehaviour
{
   public float spectrumValue = 0;
    public float filteredSpectrumValue = 0;
   public AudioSource audioSource;
   public static SpectrumAnalise instance;

   void Awake(){
         instance = this;
         audioSource = GetComponent<AudioSource>();
   }
    void Start()
    {
        Debug.Log(audioSource.clip.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
       spectrumValue = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {   
            if(spectrum[i] > spectrumValue)
            {
                spectrumValue = spectrum[i];
            }
            if (spectrum[i] > 0.03f)
            {
                filteredSpectrumValue = spectrum[i];
            }
        }
        
    }
}
