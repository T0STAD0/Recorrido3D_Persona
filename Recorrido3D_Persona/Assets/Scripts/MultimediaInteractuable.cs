using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using TMPro;

public class MultimediaInteractuable : MonoBehaviour
{
    public enum TipoContenido { Imagen, Video }

    [Header("Contenido de este modelo")]
    public TipoContenido tipo = TipoContenido.Imagen;
    public string tituloFicha = "Nombre del Modelo";
    public Texture imagenMostrar;      // Arrastra aquí tu PNG/JPG si elegiste Imagen
    public VideoClip videoMostrar;     // Arrastra aquí tu MP4 si elegiste Video

    [Header("Referencias UI y Video (Compartidas)")]
    public GameObject panelMultimedia;
    public RawImage visorMultimedia;
    public TextMeshProUGUI textoTitulo;
    public VideoPlayer reproductorVideo;
    public RenderTexture texturaVideo;

    [Header("Audio a silenciar al reproducir video")]
    public RocolaInteractiva rocola;   // Arrastra aquí el objeto que tiene el script de la rocola

    private bool abierto = false;
    private bool rocolaPausadaPorVideo = false; // true solo si este script fue quien la apagó

    void Update()
    {
        if (!abierto) return;

        bool cerrarConEsc = (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) ||
                            Input.GetKeyDown(KeyCode.Escape);

        if (cerrarConEsc)
        {
            CerrarMultimedia();
        }
    }

    // Este método se conecta al evento Al Interactuar () de ObjetoInteractuable
    public void AlternarMultimedia()
    {
        if (abierto)
            CerrarMultimedia();
        else
            AbrirMultimedia();
    }

    public void AbrirMultimedia()
    {
        if (panelMultimedia == null || visorMultimedia == null) return;

        if (textoTitulo != null)
            textoTitulo.text = tituloFicha;

        if (tipo == TipoContenido.Imagen)
        {
            if (reproductorVideo != null && reproductorVideo.isPlaying)
                reproductorVideo.Stop();

            visorMultimedia.texture = imagenMostrar;
        }
        else if (tipo == TipoContenido.Video && reproductorVideo != null)
        {
            // Detiene la música del vinyl para que no se encime con el audio del video
            if (rocola != null && rocola.EstaReproduciendo)
            {
                rocola.DetenerRocola();
                rocolaPausadaPorVideo = true;
            }

            reproductorVideo.clip = videoMostrar;
            visorMultimedia.texture = texturaVideo;
            reproductorVideo.Play();
        }

        panelMultimedia.SetActive(true);
        abierto = true;
    }

    public void CerrarMultimedia()
    {
        if (reproductorVideo != null && reproductorVideo.isPlaying)
            reproductorVideo.Stop();

        if (panelMultimedia != null)
            panelMultimedia.SetActive(false);

        // Reanuda la música solo si el video fue quien la detuvo
        if (rocolaPausadaPorVideo && rocola != null)
            rocola.ReanudarRocola();

        rocolaPausadaPorVideo = false;
        abierto = false;
    }
}
