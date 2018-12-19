using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using NAudio.Midi;

public class MidiReader : MonoBehaviour
{
    // INSTANCE
    static MidiReader m_instance;
    public static MidiReader instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MidiReader>();
            }
            return m_instance;
        }
    }

    // CLASS FUNCTIONS
    public void readMidiFile(string fileName)
    {
        if (GetComponent<MidiLoad>().Load(fileName))
        {
            int index = 0;
            for (int n = 0; n < GetComponent<MidiLoad>().midifile.Tracks; ++n)
            {
                foreach (MidiEvent midiEvent in GetComponent<MidiLoad>().midifile.Events[n])
                {
                    if (MidiEvent.IsNoteOn(midiEvent))
                    {
                        NoteEvent ne = (NoteEvent)midiEvent;
                        MusicSheet.instance.addNote(ne.NoteNumber, ne.NoteName,
                            index++, (float)midiEvent.AbsoluteTime / GetComponent<MidiLoad>().midifile.DeltaTicksPerQuarterNote);
                    }
                }
            }
            MusicSheet.instance.initialize();
        }
        else
        {
            Debug.Log("error loading midi file");
        }
    }

    // UNITY FUNCTIONS
    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}