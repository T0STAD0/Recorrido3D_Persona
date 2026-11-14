using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform puntoOrigenCamara; // Arrastra aquí la Main Camera
    public float distanciaInteraccion = 3.5f;
    public KeyCode teclaInteraccion = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(teclaInteraccion))
        {
            IntentarInteractuar();
        }
    }

    void IntentarInteractuar()
    {
        Ray rayo = new Ray(puntoOrigenCamara.position, puntoOrigenCamara.forward);
        if (Physics.Raycast(rayo, out RaycastHit impacto, distanciaInteraccion))
        {
            // Busca si el objeto golpeado tiene el script ObjetoInteractuable
            ObjetoInteractuable objeto = impacto.collider.GetComponent<ObjetoInteractuable>();
            
            if (objeto != null)
            {
                objeto.Interactuar();
            }
        }
    }
}