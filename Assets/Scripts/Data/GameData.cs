using System.Collections.Generic;

[System.Serializable]
public class PuzzleData 
{
    public int id;
    public string tipo; // "KEYPAD", "COMPUTER", etc.
    public string pregunta; // El enigma generado por la IA
    public string respuesta; // La solución exacta
    public string pista; // Texto de ayuda
}

[System.Serializable]
public class GameData 
{
    public string tema; // Tema del escape room
    public List<PuzzleData> puzzles; // Lista de enigmas
}