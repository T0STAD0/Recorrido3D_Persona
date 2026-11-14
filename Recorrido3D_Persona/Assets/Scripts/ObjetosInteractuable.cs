using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteractuable : MonoBehaviour
{
    public string nombreObjeto = "Objeto";
    [TextArea]
    public string descripcion = "Presiona E para interactuar";

    [Header("Acción al presionar E")]
    public UnityEvent alInteractuar;

    public void Interactuar()
    {
        Debug.Log("Interactuaste con: " + nombreObjeto);
        alInteractuar.Invoke();
    }
}
