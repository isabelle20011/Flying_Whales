using UnityEngine;

//made by Daniel Otaigbe
//using this class as an object we can pass it into the dialogue manager when we want to start a new dialogue.
//it will hold all of the information we need to start a new dialogue
[System.Serializable] //so we can edit it in the inspector
public class Dialogue
{

	public string name;
	[TextArea(3, 10)] //3 is the minimum amount of lines the text will use, and 10 is the maximum.
	public string[] sentences;
}
