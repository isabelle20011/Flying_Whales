using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currTime = 0f;
    float startTime = 10f;

    [SerializeField] Text countdown;

    private void Start()
    {
        currTime = startTime;
    }

    private void Update()
    {
        currTime -= 1 * Time.deltaTime;
        countdown.text = currTime.ToString("0");

        if (currTime <= 0)
        {
            currTime = 0;
        }
    }
}
