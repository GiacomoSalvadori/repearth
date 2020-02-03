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
    public DialogueEvent OnOpenWindow;
    public DialogueEvent OnCloseWindow;
    private bool nextPiece;
    #endregion

    void Awake()
    {
        nextPiece = false;
    }

    private void Start()
    {
        StartDialogue(d);
    }

    void Update()
    {

    }

    public void OnWindowClick()
    {
        nextPiece = true;
    }

    public void StartDialogue(CharacterDialogue dialogue)
    {
        dialogueWindow.SetActive(true);
        MoveDialogueWindow(true);
        if (OnOpenWindow != null) {
            OnOpenWindow();
        }
        if (dialogue) {
            StartCoroutine(VisualizeDialogue(dialogue));
        }
    }

    public void StartDialogueEnd(CharacterDialogue dialogue)
    {
        dialogueWindow.SetActive(true);
        MoveDialogueWindow(true);

        if (dialogue) {
            StartCoroutine(VisualizeDialogue(dialogue));
        }
    }

    private void MoveDialogueWindow(bool goUp)
    {
        float destination = goUp ? 0.0f : -320.0f;
        if (!goUp) {
            dialogueWindow.GetComponent<RectTransform>().DOAnchorPosY(destination, 1.0f, false).OnComplete(() => DisableWindow());
        }
        dialogueWindow.GetComponent<RectTransform>().DOAnchorPosY(destination, 1.0f, false);
    }

    private void DisableWindow()
    {
        if(OnCloseWindow != null) {
            OnCloseWindow();
        }

        dialogueWindow.SetActive(false);
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
        SetName(dialogue.character.chName);
        SetImage(dialogue.character.img);
        int pieceIndex = 0;
        while (pieceIndex < dialogue.pieces.Count) {
            nextPiece = false;
            SetDialogueText(dialogue.pieces[pieceIndex]);
            yield return new WaitUntil(() => nextPiece);
            pieceIndex++;
        }
        yield return new WaitUntil(() => nextPiece);
        MoveDialogueWindow(false);
        //dialogueWindow.SetActive(false);
    }
}
