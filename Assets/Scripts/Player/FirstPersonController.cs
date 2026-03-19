using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5.0f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform playerCamera;
    public float mouseSensitivity = 20f;
    [Tooltip("Límite vertical para que el jugador no se rompa el cuello")]
    public float maxLookAngle = 85f;

    private CharacterController controller;
    private PlayerControls inputActions; // La clase generada por el New Input System
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float cameraPitch = 0f; // Rotación en X de la cámara

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerControls();

        // Bloqueamos y ocultamos el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // El Input System REQUIERE que lo habilites y deshabilites
    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        // Leemos los valores del input cada frame
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        // Multiplicamos por deltaTime para que la sensibilidad no dependa de los FPS
        float lookX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float lookY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Calculamos el cabeceo (pitch) y lo limitamos (clamp)
        cameraPitch -= lookY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

        // Rotamos la cámara arriba/abajo
        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

        // Rotamos todo el cuerpo del jugador izquierda/derecha
        transform.Rotate(Vector3.up * lookX);
    }

    private void HandleMovement()
    {
        // Verificamos si tocamos el suelo para resetear la gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Un pequeño empuje hacia abajo para que el isGrounded sea estable
        }

        // Movimiento local relativo hacia donde mira el jugador
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * walkSpeed * Time.deltaTime);

        // Aplicamos gravedad (v = v0 + at)
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // Aplicamos el desplazamiento vertical
    }
}