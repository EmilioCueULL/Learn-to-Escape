using UnityEngine;
using TMPro; // Obligatorio para usar TextMeshPro

public class KeypadPuzzle : MonoBehaviour, IInteractable
{
    private PuzzleData myPuzzleData; // Aquí guardaremos la información del GDD

    [Header("UI Elements")]
    public GameObject keypadPanel; // Arrastra aquí tu Keypad_UI
    public TextMeshProUGUI questionText; // Arrastra tu Text_Question
    public TextMeshProUGUI displayInputText; // Arrastra tu Text_Display

    private string currentInput = ""; // Lo que el jugador ha tecleado
    private bool isSolved = false;

    // Esta función la llamará el GameManager para "inyectar" los datos
    public void SetupPuzzle(PuzzleData data)
    {
        myPuzzleData = data;
        questionText.text = data.pregunta; 
        displayInputText.text = "----";
    }

    // Viene de la interfaz IInteractable
    public void Interact()
    {
        if (isSolved) return; // Si ya está resuelto, no hacemos nada

        // Abrimos la UI
        keypadPanel.SetActive(true);
        
        // ¡Magia de Senior! Liberamos el ratón para poder pulsar los botones de la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Esta función se la asignaremos a los botones del 0 al 9 en el editor
    public void AddDigit(string digit)
    {
        if (currentInput.Length < 4) // Limitamos a 4 cifras según tu petición
        {
            currentInput += digit;
            displayInputText.text = currentInput;
        }
    }

    // Esta función se la asignamos al botón "Enter"
    public void ValidateCode()
    {
        if (currentInput == myPuzzleData.respuesta) // Comparamos con el mock JSON
        {
            Debug.Log("[Keypad] ¡Código correcto! Caja fuerte abierta.");
            displayInputText.color = Color.green;
            isSolved = true;
            CloseKeypad();
            // En el futuro aquí llamaremos a un sonido de éxito [cite: 247]
        }
        else
        {
            Debug.LogWarning("[Keypad] Código incorrecto.");
            displayInputText.text = "ERR";
            displayInputText.color = Color.red;
            currentInput = ""; // Reseteamos
            // En el futuro aquí llamaremos a un sonido de error [cite: 248]
        }
    }

    // Asignamos esto al botón "Cerrar"
    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);
        // Volvemos a bloquear el ratón para el FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (!isSolved) 
        {
            displayInputText.color = Color.white;
            displayInputText.text = "----";
            currentInput = "";
        }
    }
}