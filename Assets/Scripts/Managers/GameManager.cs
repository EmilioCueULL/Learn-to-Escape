using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameData currentGameData;

    public KeypadPuzzle testSafe;

    private void Start()
    {
        LoadMockData(); 
    }

    private void LoadMockData()
    {
        // streamingAssetsPath es automaticamente la carpeta StreamingAssets dentro de la carpeta Assets
        string filePath = Path.Combine(Application.streamingAssetsPath, "mock_puzzles.json");

        if (File.Exists(filePath))
        {
            // 1. Leemos el texto del archivo
            string jsonText = File.ReadAllText(filePath);
            Debug.Log("[GameManager] JSON leído del disco duro:\n" + jsonText);

            // 2. Convertimos el texto en nuestras clases de C#
            currentGameData = JsonUtility.FromJson<GameData>(jsonText);

            // 3. Comprobamos que ha funcionado
            if (currentGameData != null && currentGameData.puzzles != null)
            {
                Debug.Log($"[GameManager] ¡Éxito! Tema cargado: {currentGameData.tema}");
                Debug.Log($"[GameManager] Puzzles cargados: {currentGameData.puzzles.Count}");
                
                // Imprimimos el primer puzzle para verificar
                Debug.Log($"Puzzle 1 -> Pregunta: {currentGameData.puzzles[0].pregunta} | Respuesta: {currentGameData.puzzles[0].respuesta}");

                testSafe.SetupPuzzle(currentGameData.puzzles[0]);
            }
            else
            {
                Debug.LogError("[GameManager] Error crítico: El JSON se leyó pero no se pudo convertir a GameData.");
            }
        }
        else
        {
            Debug.LogError($"[GameManager] No se encontró el archivo mock en la ruta: {filePath}");
        }
    }
}