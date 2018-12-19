using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    // CLASS VARIABLES
    int m_noteNumber;
    string m_noteName;
    int m_noteIndex;
    float m_noteTime;

    public int noteNumber { get { return m_noteNumber; } }
    public string noteName { get { return m_noteName; } }
    public int noteIndex { get { return m_noteIndex; } }
    public float noteTime { get { return m_noteTime; } }

    // CLASS FUNCTIONS
    public void initialize(int num, string name, int index, float time)
    {
        m_noteNumber = num;
        m_noteName = name;
        m_noteIndex = index;
        m_noteTime = time;
    }

    public void complete()
    {
        GetComponentInChildren<Image>().color = Color.green;
    }

    public string ToString()
    {
        return noteIndex + ": " + noteNumber + " (" + noteName + "), " + noteTime;
    }
}
