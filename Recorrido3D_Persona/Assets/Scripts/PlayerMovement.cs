using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 12f;
    public float velocidadCorrer = 20f;

    [Header("Salto y Gravedad")]
    public float alturaSalto = 3f;
    public float gravedad = -32f;
    public float multiplicadorCaida = 1.8f; // Hace que caer sea más rápido y firme

    [Header("Cámara")]
    public Transform camaraJugador;
    public float sensibilidadRaton = 150f;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private float rotacionX = 0f;

    // Margen de tolerancia para que el salto nunca falle
    private float tiempoCoyote = 0.15f;
    private float contadorCoyote;
    private float tiempoBufferSalto = 0.15f;
    private float contadorBufferSalto;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoverJugador();
        RotarCamara();
    }

    void MoverJugador()
    {
        // Control de Coyote Time (detecta si está en el suelo o recién salió de un borde)
        if (controller.isGrounded)
        {
            contadorCoyote = tiempoCoyote;
            if (velocidadVertical.y < 0)
            {
                velocidadVertical.y = -4f; // Mantiene al jugador firme en rampas y desniveles
            }
        }
        else
        {
            contadorCoyote -= Time.deltaTime;
        }

        // Control de Buffer de Salto (guarda la pulsación si presionas justo antes de tocar piso)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            contadorBufferSalto = tiempoBufferSalto;
        }
        else
        {
            contadorBufferSalto -= Time.deltaTime;
        }

        // Movimiento horizontal (W, A, S, D)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidadCaminar;
        Vector3 movimiento = transform.right * x + transform.forward * z;
        controller.Move(movimiento * velocidadActual * Time.deltaTime);

        // Ejecutar salto si hay margen válido
        if (contadorBufferSalto > 0f && contadorCoyote > 0f)
        {
            velocidadVertical.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
            contadorBufferSalto = 0f;
            contadorCoyote = 0f;
        }

        // Aplicar gravedad normal al subir y acelerada al caer
        if (velocidadVertical.y < 0)
        {
            velocidadVertical.y += gravedad * multiplicadorCaida * Time.deltaTime;
        }
        else
        {
            velocidadVertical.y += gravedad * Time.deltaTime;
        }

        controller.Move(velocidadVertical * Time.deltaTime);
    }

    void RotarCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadRaton * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadRaton * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -85f, 85f);

        camaraJugador.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}