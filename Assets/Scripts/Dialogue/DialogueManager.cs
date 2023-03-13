using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    
    private Queue<string> sentences;
    public static DialogueManager instance;

    void Awake() {
        if(instance != null)
        {
            Debug.LogError("More than 1 dialogue manager in scene");
            return;
        }
        
        instance = this;
    }

    private void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        speakerNameText.text = _dialogue.speakerName;

        sentences.Clear();

        foreach (string _sentence in _dialogue.sentences)
        {
            sentences.Enqueue(_sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceEffect(sentence));
        // dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        // TODO: Once this has been reached, make a transition to the actual level.
    }

    IEnumerator TypeSentenceEffect(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

}
