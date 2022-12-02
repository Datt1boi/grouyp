using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponet;
    public string[] lines;
    public float textspeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponet.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponet.text == lines[index])
            {
                Nextline();
            }
            else
            {
                StopAllCoroutines();
                textComponet.text = lines[index];
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void Nextline()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponet.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
