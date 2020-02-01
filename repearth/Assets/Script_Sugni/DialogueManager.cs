using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Values
    public GameObject dialogueWindow;
    public GameObject messageContainer;
    public GameObject imgContainer;
    public GameObject nameContainer;
    public CharacterDialogue d;
    public delegate void DialogueEvent();
    public DialogueEvent OnClickWindow;
    private bool nextPiece;
    #endregion

    void Awake()
    {
        nextPiece = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            StartDialogue(d);
        }
    }

    public void OnWindowClick()
    {
        nextPiece = true;
    }

    public void StartDialogue(CharacterDialogue dialogue)
    {
        Debug.Log("Qui");
        if (dialogue) {
            Debug.Log("Qui 2");
            StartCoroutine(VisualizeDialogue(dialogue));
        }
    }

    private void SetImage(Sprite img)
    {
        imgContainer.GetComponent<Image>().sprite = img;
    }

    private void SetName(string chName)
    {
        nameContainer.GetComponent<Text>().text = chName;
    }

    private void SetDialogueText(string text)
    {
        messageContainer.GetComponent<Text>().text = text;
    }

    private void PunchWindow(float intensity, float time)
    {
        RectTransform rect = dialogueWindow.GetComponent<RectTransform>();
        Vector2 punchDir = new Vector2(1.0f, 1.0f) * intensity;
        rect.DOPunchAnchorPos(punchDir, time);
    }

    private IEnumerator VisualizeDialogue(CharacterDialogue dialogue)
    {
        Debug.Log("Evvai");
        dialogueWindow.SetActive(true);
        SetName(dialogue.character.name);
        SetImage(dialogue.character.img);
        int pieceIndex = 0;
        while (pieceIndex < dialogue.pieces.Count) {
            nextPiece = false;
            SetDialogueText(dialogue.pieces[pieceIndex]);
            yield return new WaitUntil(() => nextPiece);
            pieceIndex++;
        }
        yield return new WaitUntil(() => nextPiece);
        dialogueWindow.SetActive(false);
    }
}
