using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    Image accidental;
    [SerializeField]
    Image ledgerLine;

    // CLASS VARIABLES
    int m_noteNumber;
    string m_noteName;
    int m_noteIndex;
    float m_noteTime;

    public int noteNumber { get { return m_noteNumber; } }
    public string noteName { get { return m_noteName; } }
    public int noteIndex { get { return m_noteIndex; } }
    public float noteTime { get { return m_noteTime; } }
    public Staff staff { get { return transform.parent.parent.GetComponent<Staff>(); } }

    // CLASS FUNCTIONS
    public void initialize(int num, string name, int index, float time)
    {
        m_noteNumber = num;
        m_noteName = name;
        m_noteIndex = index;
        m_noteTime = time;
        if (name.Contains("#"))
        {
            accidental.gameObject.SetActive(true);
        }
        else
        {
            accidental.gameObject.SetActive(false);
        }
        if (staff == staff.staffPair.trebleStaff && (num >= 81 || num <= 60))
        {
            ledgerLine.gameObject.SetActive(true);
        }
        else if (staff == staff.staffPair.bassStaff && (num <= 40 || num >= 48))
        {
            ledgerLine.gameObject.SetActive(true);
        }
        else
        {
            ledgerLine.gameObject.SetActive(false);
        }
    }

    public void complete()
    {
        GetComponentInChildren<Image>().color = Color.green;
    }

    public override string ToString()
    {
        return noteIndex + ": " + noteNumber + " (" + noteName + "), " + noteTime;
    }
}
