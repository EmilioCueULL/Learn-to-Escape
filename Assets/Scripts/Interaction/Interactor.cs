using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // ¡NUEVO! Vital para acceder al componente Image

public class Interactor : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float interactRange = 2.5f; 
    public LayerMask interactableLayer; 

    [Header("UI Feedback")]
    [Tooltip("Arrastra aquí tu Crosshair desde el Canvas")]
    public Image crosshair; 
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f); // Blanco semitransparente
    public Color interactColor = Color.green; // Verde al mirar un puzzle/libro

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        // 1. Primero gestionamos el feedback visual (cambio de color)
        HandleRaycastFeedback();

        // 2. Luego escuchamos si el jugador pulsa la 'E'
        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            TryInteract();
        }
    }

    // NUEVO MÉTODO: Actualiza el color de la retícula cada frame
    private void HandleRaycastFeedback()
    {
        // Si no hemos asignado la retícula en el inspector, no hacemos nada para evitar errores
        if (crosshair == null) return;

        // Disparamos el mismo rayo, pero solo para comprobar visualmente
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange, interactableLayer))
        {
            // Si golpeamos algo que tiene la interfaz...
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                crosshair.color = interactColor; // Se pone verde
                return; // Salimos de la función
            }
        }
        
        // Si no golpeamos nada interactuable, vuelve a su color normal
        crosshair.color = normalColor;
    }

    private void TryInteract()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange, interactableLayer))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            
            if (interactableObject != null)
            {
                interactableObject.Interact(); 
            }
        }
    }
}