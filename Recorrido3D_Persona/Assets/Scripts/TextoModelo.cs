using UnityEngine;
using TMPro;

public class InfoModeloClick : MonoBehaviour
{
    [Header("Contenido de esta ficha")]
    public string titulo = "Vi y Jinx";
    [TextArea(4, 10)]
    public string descripcion = "Escribe aquí la historia o descripción del modelo 3D...";

    [Header("Referencias del Panel UI")]
    public GameObject panelInfoUI;
    public TextMeshProUGUI textoTituloUI;
    public TextMeshProUGUI textoDescripcionUI;

    // Conecta este método al evento "Al Interactuar ()" de ObjetoInteractuable
    public void AlternarInformacion()
    {
        if (panelInfoUI == null) return;

        bool nuevoEstado = !panelInfoUI.activeSelf;
        panelInfoUI.SetActive(nuevoEstado);

        if (nuevoEstado)
        {
            if (textoTituloUI != null) textoTituloUI.text = titulo;
            if (textoDescripcionUI != null) textoDescripcionUI.text = descripcion;
        }
    }

    void Update()
    {
        // Permite cerrar el panel presionando E o ESC aunque ya no estés mirando el modelo
        if (panelInfoUI != null && panelInfoUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panelInfoUI.SetActive(false);
            }
        }
    }
}