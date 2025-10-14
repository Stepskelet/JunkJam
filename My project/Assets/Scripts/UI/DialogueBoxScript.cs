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
    string[] dialogue;
    string[] test = { "1231231231231231", "asdasdasdasd", "asdas][][]sadfs", "af43t45wwefsadf" };

    private void Start()
    {
        textBox.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (!outOfDialogue && Input.GetMouseButtonDown(0))
        {
            if (isPrinting)
            {
                StopAllCoroutines();
                textBox.text = dialogue[index];
                isPrinting = false;
            }
            else NextLine(dialogue);
        }
        //if (Input.GetKeyDown(KeyCode.N)) StartDialogue(test);
    }

    public void StartDialogue(string[] lines)
    {
        outOfDialogue = false;
        dialogue = lines;
        dialogueBox.SetActive (true);
        index = 0;
        StartCoroutine(TypeLine(lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        isPrinting = true;
        foreach (char c in line.ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isPrinting = false;
    }

    void NextLine(string[] lines)
    {
        index++;
        if (index < lines.Length)
        {
            textBox.text = string.Empty;
            StartCoroutine(TypeLine(lines[index]));
        }
        else
        {
            dialogueBox.SetActive(false);
            outOfDialogue = true;
        }
    }
}
