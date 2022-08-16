using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Ghost ghost1;
    public Ghost ghost2;
    private float timeValue;
    private int index1;
    private int index2;


    // true for ghost 1
    // false for ghost 2
    public bool ghostToUse;

    // Update is called once per frame
    private void Awake()
    {
        timeValue = 0;

        if (ghost1.wasLastRecorded)
        {
            ghostToUse = false;
        }
        else
        {
            ghostToUse = true;
        }
    }

    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        if (ghostToUse)
        {
            GetIndex(true);
            SetTransform(true);
        }
        else
        {
            GetIndex(false);
            SetTransform(false);
        }
        
    }

    private void GetIndex(bool ghost)
    {
        if (ghost)
        {
            for (int i = 0; i < ghost1.timeStamp.Count - 2; i++)
            {
                if (ghost1.timeStamp[i] == timeValue)
                {
                    index1 = i;
                    index2 = i;
                    return;
                }
                else if (ghost1.timeStamp[i] < timeValue && timeValue < ghost1.timeStamp[i + 1])
                {
                    index1 = i;
                    index2 = i + 1;
                    return;
                }
            }

            index1 = ghost1.timeStamp.Count - 1;
            index2 = ghost1.timeStamp.Count - 1;
            return;
        }

        for (int i = 0; i < ghost2.timeStamp.Count - 2; i++)
        {
            if (ghost2.timeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghost2.timeStamp[i] < timeValue && timeValue < ghost2.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = ghost2.timeStamp.Count - 1;
        index2 = ghost2.timeStamp.Count - 1;
    }

    private void SetTransform(bool ghost)
    {
        if (ghost)
        {
            if (index1 == index2)
            {
                this.transform.position = ghost1.position[index1];
                this.transform.eulerAngles = ghost1.rotation[index1];
            }
            else
            {
                float interpolationFactor = (timeValue - ghost1.timeStamp[index1]) / (ghost1.timeStamp[index2] - ghost1.timeStamp[index1]);

                this.transform.position = Vector3.Lerp(ghost1.position[index1], ghost1.position[index2], interpolationFactor);
                this.transform.eulerAngles = Vector3.Lerp(ghost1.rotation[index1], ghost1.rotation[index2], interpolationFactor);

            }
            return;
        }

        if (index1 == index2)
        {
            this.transform.position = ghost2.position[index1];
            this.transform.eulerAngles = ghost2.rotation[index1];
        }
        else
        {
            float interpolationFactor = (timeValue - ghost2.timeStamp[index1]) / (ghost2.timeStamp[index2] - ghost2.timeStamp[index1]);

            this.transform.position = Vector3.Lerp(ghost2.position[index1], ghost2.position[index2], interpolationFactor);
            this.transform.eulerAngles = Vector3.Lerp(ghost2.rotation[index1], ghost2.rotation[index2], interpolationFactor);

        }
    }
}

