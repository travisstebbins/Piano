using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiPlayerTK;
using NAudio.Midi;

public class Staff : MonoBehaviour
{
    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    GameObject notePrefab;
    [SerializeField]
    GameObject m_noteHolder;
    [SerializeField]
    bool isBassStaff = false;

    // CLASS VARIABLES
    public StaffPair staffPair { get { return GetComponentInParent<StaffPair>(); } }
    public GameObject noteHolder { get { return m_noteHolder; } }
    public Note[] notes { get { return noteHolder.GetComponentsInChildren<Note>(); } }
    public int noteCount { get { return noteHolder.transform.childCount; } }
    public Note this[int i] {
        get
        {
            return notes[i];
        }
        set
        {
            noteHolder.GetComponentsInChildren<Note>()[i] = value;
        }
    }

    // CLASS FUNCTIONS
    public void addNote(int number, string name, int index, float time)
    {
        Note note = Instantiate(notePrefab, noteHolder.transform).GetComponent<Note>();
        note.initialize(number, name, index, time);
        note.transform.localPosition = new Vector2(-GetComponent<RectTransform>().rect.width * 0.5f +
        (time - staffPair.startTime) * MidiReader.instance.GetComponent<MidiLoad>().midifile.DeltaTicksPerQuarterNote * 0.5f - 400.0f, getNoteYPosition(note));
        note.GetComponentInChildren<Text>().text = note.noteName;
        MusicSheet.instance.notes.Add(note);
    }

    float getNoteYPosition(Note n)
    {
        float pos = 0;
        if (!isBassStaff)
        {
            switch (int.Parse("" + n.noteName[n.noteName.Length - 1]))
            {
                case 4:
                    pos = -60;
                    break;
                case 5:
                    pos = 10;
                    break;
                case 6:
                    pos = 80;
                    break;
            }
            switch (n.noteName[0])
            {
                case 'D':
                    pos += 10;
                    break;
                case 'E':
                    pos += 20;
                    break;
                case 'F':
                    pos += 30;
                    break;
                case 'G':
                    pos += 40;
                    break;
                case 'A':
                    pos += 50;
                    break;
                case 'B':
                    pos += 60;
                    break;
            }
            return pos;
        }
        switch (int.Parse("" + n.noteName[n.noteName.Length - 1]))
        {
            case 1:
                pos = -150;
                break;
            case 2:
                pos = -80;
                break;
            case 3:
                pos = -10;
                break;
            case 4:
                pos = 60;
                break;
        }
        switch (n.noteName[0])
        {
            case 'D':
                pos += 10;
                break;
            case 'E':
                pos += 20;
                break;
            case 'F':
                pos += 30;
                break;
            case 'G':
                pos += 40;
                break;
            case 'A':
                pos += 50;
                break;
            case 'B':
                pos += 60;
                break;
        }
        return pos;
    }
}
