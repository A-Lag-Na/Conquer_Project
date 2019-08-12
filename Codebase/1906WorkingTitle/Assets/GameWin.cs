using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    Text text;
    float time;
    int minutes, seconds;
    Color fadeIn, title, content, btnTxt1, btnTxt2, btnBack1, btnBack2;
    Color white, red, black;

    void Start()
    {
        white = new Color(1f, 1f, 1f, 1f);
        red = new Color(1f, 0f, 0f, 1f);
        black = new Color(0f, 0f, 0f, 1f);

        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        time = Time.realtimeSinceStartup;

        fadeIn = transform.GetChild(0).GetComponent<Image>().color;

        title = transform.GetChild(0).GetChild(0).GetComponent<Text>().color;

        content = transform.GetChild(0).GetChild(1).GetComponent<Text>().color;

        btnTxt1 = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().color;
        btnBack1 = transform.GetChild(0).GetChild(2).GetComponent<Image>().color;

        btnTxt2 = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().color;
        btnBack1 = transform.GetChild(0).GetChild(3).GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn != black)
        {
            fadeIn = Color.Lerp(fadeIn, black, .05f);
            content = Color.Lerp(content, black, .05f);
            btnBack1 = Color.Lerp(btnBack1, white, .05f);
            btnBack2 = Color.Lerp(btnBack2, white, .05f);
            btnTxt1 = Color.Lerp(btnTxt1, black, .05f);
            btnTxt2 = Color.Lerp(btnTxt2, black, .05f);
            title = Color.Lerp(title, red, .05f);
        }
        text.text = $"It took you {(int)time / 60} minutes and {(int)time % 60} seconds!";
    }
}
