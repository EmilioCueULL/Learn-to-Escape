using UnityEngine;

// Fíjate en cómo añadimos ', IInteractable' después de MonoBehaviour 
public class BookInteractable : MonoBehaviour, IInteractable
{
    public string bookTitle = "El Libro de los Secretos";

    // Al tener la interfaz, estamos obligados a escribir esta función
    public void Interact()
    {
        Debug.Log($"[SISTEMA] Has interactuado con el libro: {bookTitle}. ¡El raycast funciona perfecto!");
        // En el futuro, aquí conectaremos con la UI para leer pistas de la IA
    }
}