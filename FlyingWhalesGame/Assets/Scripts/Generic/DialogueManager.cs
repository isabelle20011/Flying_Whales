using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Daniel Otaigbe
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences; //works like a list, but more restricted. It's FIFO (First in, First Out) so new sentences are loaded from the end of the queu

    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
    }
}
