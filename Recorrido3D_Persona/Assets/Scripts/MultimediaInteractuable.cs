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

    private bool abierto = false;

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

        abierto = false;
    }
}