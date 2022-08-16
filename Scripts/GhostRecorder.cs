using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost1;
    public Ghost ghost2;

    public bool ghostToUse; // true for ghost2 false for ghost1

    private float timer;
    private float timeValue;

    private void Awake()
    {
        if (ghost1.wasLastRecorded)
        {
            ghost2.ResetData();
            ghost2.wasLastRecorded = true;

            timeValue = 0;
            timer = 0;

            ghostToUse = true;
            ghost1.wasLastRecorded = false;
        }
        else
        {
            ghost1.ResetData();
            ghost1.wasLastRecorded = true;

            timeValue = 0;
            timer = 0;

            ghostToUse = false;
            ghost2.wasLastRecorded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if (ghostToUse)
        {
            if (timer >= 1 / ghost2.recordFrequency)
            {
                ghost2.timeStamp.Add(timeValue);
                ghost2.position.Add(this.transform.position);
                ghost2.rotation.Add(this.transform.eulerAngles);

                timer = 0;
            }
        }
        else
        {
            if (timer >= 1 / ghost1.recordFrequency)
            {
                ghost1.timeStamp.Add(timeValue);
                ghost1.position.Add(this.transform.position);
                ghost1.rotation.Add(this.transform.eulerAngles);

                timer = 0;
            }
        }
    }
}
