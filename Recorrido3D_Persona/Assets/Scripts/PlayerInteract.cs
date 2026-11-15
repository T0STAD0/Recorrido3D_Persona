using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class PlayerInteract : MonoBehaviour
{
    [Header("Configuración de Raycast")]
    public Transform puntoOrigenCamara; // Arrastra aquí la Main Camera
    public float distanciaInteraccion = 20f; // Aumentado para la escala de tu mapa
    public KeyCode teclaInteraccion = KeyCode.E;

    [Header("Interfaz de Usuario (UI)")]
    [Tooltip("Arrastra aquí el objeto de texto UI (TextMeshPro) que dice 'Presiona E...'")]
    public GameObject cartelInteractuarUI;
    public TextMeshProUGUI textoCartelUI;

    private ObjetoInteractuable objetoActual;

    void Start()
    {
        if (cartelInteractuarUI != null)
        {
            cartelInteractuarUI.SetActive(false);
        }
    }

    void Update()
    {
        DetectarObjeto();

        if (Input.GetKeyDown(teclaInteraccion) && objetoActual != null)
        {
            objetoActual.Interactuar();
        }
    }

    void DetectarObjeto()
    {
        Ray rayo = new Ray(puntoOrigenCamara.position, puntoOrigenCamara.forward);
        
        // Dibuja una línea roja en la vista Scene para que veas exactamente hasta dónde llega tu mirada
        Debug.DrawRay(puntoOrigenCamara.position, puntoOrigenCamara.forward * distanciaInteraccion, Color.red);

        if (Physics.Raycast(rayo, out RaycastHit impacto, distanciaInteraccion))
        {
            // Busca el script tanto en el objeto tocado como en su objeto padre (ej. Vinil)
            ObjetoInteractuable objeto = impacto.collider.GetComponentInParent<ObjetoInteractuable>();

            if (objeto != null)
            {
                objetoActual = objeto;
                MostrarCartel(true, objeto.descripcion);
                return;
            }
        }

        // Si no estás mirando nada interactuable, oculta el aviso
        objetoActual = null;
        MostrarCartel(false, "");
    }

    void MostrarCartel(bool estado, string mensaje)
    {
        if (cartelInteractuarUI != null)
        {
            cartelInteractuarUI.SetActive(estado);
            if (estado && textoCartelUI != null && !string.IsNullOrEmpty(mensaje))
            {
                textoCartelUI.text = mensaje;
            }
        }
    }
}