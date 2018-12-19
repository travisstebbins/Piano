using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffPair : MonoBehaviour
{
    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    Staff m_trebleStaff;
    [SerializeField]
    Staff m_bassStaff;

    // CLASS VARIABLES
    public static int staffLength = 9;
    public float startTime { get; set; }
    public Staff trebleStaff { get { return m_trebleStaff; } }
    public Staff bassStaff { get { return m_bassStaff; } }
    public int noteCount { get { return trebleStaff.noteCount + bassStaff.noteCount; } }

    // CLASS FUNCTIONS
    public void addNote(int number, string name, int index, float time)
    {
        if (number < 60)
        {
            bassStaff.addNote(number, name, index, time);
        }
        else
        {
            trebleStaff.addNote(number, name, index, time);
        }
    }
}
