using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public string[] textWriter;
    public int speed = 2;
    private int pos = 0;
    private int charPos = 0;

    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        currSpeed = speed;
    }

    private int currSpeed = 0;
    private int currWait = 0;
    void Update()
    {
        if(++currWait < currSpeed) {
            return;
        }

        currWait = 0;
        currSpeed = speed;
        text.text += textWriter[pos][charPos++];

        if(charPos >= textWriter[pos].Length) {
            pos++;

            if(pos >= textWriter.Length) {
                this.enabled = false;
            }
        } else {
            currSpeed = (int)(5.0 / Time.deltaTime);
        }
    }
}
