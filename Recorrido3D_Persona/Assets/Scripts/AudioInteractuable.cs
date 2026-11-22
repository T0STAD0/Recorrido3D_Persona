using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioInteractuable : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioClip clipAudio;
    public bool alternarPausa = true; // Si vuelves a presionar E, lo detiene o reinicia

    private AudioSource fuenteAudio;

    void Awake()
    {
        fuenteAudio = GetComponent<AudioSource>();
        fuenteAudio.playOnAwake = false;
        if (clipAudio != null)
        {
            fuenteAudio.clip = clipAudio;
        }
    }

    // Este método se conecta al evento Al Interactuar () de ObjetoInteractuable
    public void AlternarAudio()
    {
        if (fuenteAudio == null) return;

        if (clipAudio != null && fuenteAudio.clip != clipAudio)
        {
            fuenteAudio.clip = clipAudio;
        }

        if (fuenteAudio.isPlaying && alternarPausa)
        {
            fuenteAudio.Stop();
        }
        else
        {
            fuenteAudio.Play();
        }
    }
}