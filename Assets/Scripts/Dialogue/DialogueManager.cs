using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] string sceneToLoadAfterComplete;
    [Header("Unity Setup Fields")]
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image speakerSprite;

    public SceneFader sceneFader;
    
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

    public void SkipDialogue()
    {
        Debug.Log("Skipping Dialogue");
        sceneFader.FadeTo(sceneToLoadAfterComplete);
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        speakerNameText.text = _dialogue.speakerName;
        speakerSprite.sprite = _dialogue.speakerSprite;
        speakerSprite.enabled = true;

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
            Invoke("EndDialogue", 2);
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
        sceneFader.FadeTo(sceneToLoadAfterComplete);
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
