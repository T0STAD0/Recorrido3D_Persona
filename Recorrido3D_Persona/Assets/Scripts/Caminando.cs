using UnityEngine;
using UnityEngine.InputSystem;

public class Caminando : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if (anim == null) return;

        bool estaCaminando = false;

        // Soporte para el nuevo Input System de Unity 6
        if (Keyboard.current != null)
        {
            estaCaminando = Keyboard.current.wKey.isPressed ||
                            Keyboard.current.aKey.isPressed ||
                            Keyboard.current.sKey.isPressed ||
                            Keyboard.current.dKey.isPressed ||
                            Keyboard.current.upArrowKey.isPressed ||
                            Keyboard.current.downArrowKey.isPressed ||
                            Keyboard.current.leftArrowKey.isPressed ||
                            Keyboard.current.rightArrowKey.isPressed;
        }
        else
        {
            // Respaldo para Input Manager clásico
            estaCaminando = Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;
        }

        anim.SetBool("Caminando", estaCaminando);
    }
}