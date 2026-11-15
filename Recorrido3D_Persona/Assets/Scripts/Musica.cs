using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class MusicaRocola3D : MonoBehaviour
{
    [Header("Referencias")]
    public Transform jugador; // Arrastra aquí a tu Player

    [Header("Distancias (Ajustadas a tu mapa grande)")]
    public float distanciaMinima = 25f;   // Desde aquí hacia adentro ya alcanzó su volumen máximo
    public float distanciaMaxima = 350f;  // Cubre todo el mapa sin cortarse

    [Header("Control de Volumen")]
    [Range(0f, 1f)]
    public float volumenLejos = 0.25f;    // Volumen suave de fondo en todo el mapa
    [Range(0f, 1f)]
    public float volumenCerca = 0.75f;    // Volumen máximo al estar frente al disco (sin romper oídos)

    [Header("Ecualización (Filtro Pasa-Bajos)")]
    [Tooltip("Frecuencia cuando estás lejos (22000 = normal, 2500 = suave ambiental)")]
    public float frecuenciaLejos = 2500f;
    public float frecuenciaCerca = 22000f;

    private AudioSource audioSource;
    private AudioLowPassFilter filtroEcualizador;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        filtroEcualizador = GetComponent<AudioLowPassFilter>();

        // Configuración limpia sin distorsión Doppler
        audioSource.spatialBlend = 0.85f; // Mezcla 85% 3D / 15% 2D para que nunca se pierda la espacialidad ni el audio
        audioSource.dopplerLevel = 0f;    // CRÍTICO: Elimina por completo que la canción suene lenta o desafinada al caminar
        audioSource.rolloffMode = AudioRolloffMode.Custom;
        audioSource.minDistance = distanciaMinima;
        audioSource.maxDistance = distanciaMaxima;
        audioSource.loop = true;

        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (jugador == null) return;

        // Distancia entre el Player y la rocola
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // 0 = muy cerca, 1 = muy lejos
        float factorLejania = Mathf.InverseLerp(distanciaMinima, distanciaMaxima, distancia);

        // Interpola suavemente el volumen entre el mínimo ambiental y el máximo permitido
        audioSource.volume = Mathf.Lerp(volumenCerca, volumenLejos, factorLejania);

        // Interpola el ecualizador sin apagar demasiado la canción
        filtroEcualizador.cutoffFrequency = Mathf.Lerp(frecuenciaCerca, frecuenciaLejos, factorLejania);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
    }
}