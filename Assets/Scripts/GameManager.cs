﻿using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;
using MidiPlayerTK;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // INSTANCE
    static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    MusicSheet musicSheet;

    // CLASS FUNCTIONS
    void onNoteDown(MidiChannel channel, int note, float velocity)
    {
        PianoUI.instance.pressKey(note);
        if (Mathf.Abs(velocity) > float.Epsilon)
        {
            Debug.Log(note);
            bool allNotesPressed = true;
            foreach(Note n in MusicSheet.instance.currentNotes)
            {
                if (Mathf.Abs(MidiMaster.GetKey(n.noteNumber)) < float.Epsilon)
                {
                    allNotesPressed = false;
                }
            }
            if (allNotesPressed)
            {
                MusicSheet.instance.advance();
            }
        }
    }

    void onNoteUp(MidiChannel channel, int note)
    {
        PianoUI.instance.unpressKey(note);
    }

    // UNITY FUNCTIONS
    void Start()
    {
        MidiReader.instance.readMidiFile("mond_1");
    }

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += onNoteDown;
        MidiMaster.noteOffDelegate += onNoteUp;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= onNoteDown;
        MidiMaster.noteOffDelegate -= onNoteUp;
    }
}
