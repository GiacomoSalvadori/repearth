using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue_", menuName = "Scripting/Character Dialogue", order = 2)]
public class CharacterDialogue : ScriptableObject
{
    #region Values
    public GameCharacter character;
    public List<DialoguePiece> pieces;
    #endregion
}

[System.Serializable]
public struct DialoguePiece
{
    public string message;
    public GameCharacter speaker;
}