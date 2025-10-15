using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class DialogueBoxScript : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text textBox;
    private float textSpeed = 0.04f;
    public static bool outOfDialogue = true;
    public static bool isPrinting = false;
    private int index = 0;
    List<string> dialogue;
    List<float> timingX;
    string[] test = { "1231231231231231", "asdasdasdasd", "asdas][][]sadfs", "af43t45wwefsadf" };

    [SerializeField] DialogueCollection dialogueCollection;

    private void Start()
    {
        textBox.text = string.Empty;
        dialogueBox.SetActive(false);

        if(!MainMenu.mainMenuActive)
        {
            StartCoroutine(WaitForDelayedStart());
        }
        else { 

        }
    }

    IEnumerator WaitForGameStart()
    {
        yield return new WaitForSeconds(1);
        if(MainMenu.mainMenuActive)
        {
            StartCoroutine(WaitForGameStart());
        }
        else
        {
            DelayedStart();
        }
    }

    IEnumerator WaitForDelayedStart()
    {
        yield return new WaitForSeconds(1f);
        DelayedStart();
    }

    private void DelayedStart()
    {
        StartDialogue(dialogueCollection.lines, dialogueCollection.timing);
    }

    public void StartDialogue(List<string> lines, List<float> timing)
    {
        if(lines.Count != 0)
        {
            timingX = timing;
            outOfDialogue = false;
            dialogue = lines;
            dialogueBox.SetActive(true);
            index = 0;
            StartCoroutine(TypeLine(lines[index], timingX[index]));
        }
    }

    IEnumerator TypeLine(string line, float timing)
    {
        isPrinting = true;
        foreach (char c in line.ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isPrinting = false;
        yield return new WaitForSeconds(timing);
        NextLine(dialogue, timingX);
    }

    void NextLine(List<string> lines, List<float> timing)
    {
        index++;
        if (index < lines.Count)
        {
            textBox.text = string.Empty;
            StartCoroutine(TypeLine(lines[index], timingX[index]));
        }
        else
        {
            dialogueBox.SetActive(false);
            outOfDialogue = true;
        }
    }

}
