using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RocolaInteractiva : MonoBehaviour
{
    [Header("Piezas del Tocadiscos")]
    [Tooltip("Arrastra aquí a Empty_002 (el conjunto de brazos y disco)")]
    public Transform conjuntoBrazosDisco;
    
    [Tooltip("Arrastra aquí a Cylinder_002 (el disco que debe girar)")]
    public Transform discoGiratorio;

    [Header("Movimiento hacia la Pared")]
    [Tooltip("Cuánto se mueve hacia la pared al activarse (prueba en X, Y o Z según tu orientación)")]
    public Vector3 desplazamientoHaciaPared = new Vector3(0f, 0f, -0.5f);
    public float velocidadMovimiento = 3f;

    [Header("Giro del Disco")]
    public float velocidadGiro = 120f;
    public Vector3 ejeGiro = new Vector3(0f, 1f, 0f); // Ajusta si gira en otro eje

    private AudioSource audioSource;
    private Vector3 posicionInicialLocal;
    private Vector3 posicionActivaLocal;
    private bool estaReproduciendo = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Inicia apagada hasta que el jugador presione E
        audioSource.Stop();
        audioSource.loop = true;

        if (conjuntoBrazosDisco != null)
        {
            posicionInicialLocal = conjuntoBrazosDisco.localPosition;
            posicionActivaLocal = posicionInicialLocal + desplazamientoHaciaPared;
        }
    }

    void Update()
    {
        if (conjuntoBrazosDisco == null) return;

        // 1. Mueve suavemente los brazos y el disco hacia la pared (o de regreso)
        Vector3 destino = estaReproduciendo ? posicionActivaLocal : posicionInicialLocal;
        conjuntoBrazosDisco.localPosition = Vector3.Lerp(
            conjuntoBrazosDisco.localPosition, 
            destino, 
            Time.deltaTime * velocidadMovimiento
        );

        // 2. Si está encendido, hace girar el disco continuamente
        if (estaReproduciendo && discoGiratorio != null)
        {
            discoGiratorio.Rotate(ejeGiro * velocidadGiro * Time.deltaTime, Space.Self);
        }
    }

    // Esta función la llamará tu script ObjetoInteractuable al presionar E
    public void AlternarRocola()
    {
        estaReproduciendo = !estaReproduciendo;

        if (estaReproduciendo)
        {
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else
        {
            audioSource.Pause(); // Pausa la canción (usa .Stop() si quieres que reinicie desde 0)
        }
    }
}