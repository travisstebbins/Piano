using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    // INSTANCE
    static MusicSheet m_instance;
    public static MusicSheet instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<MusicSheet>();
            }
            return m_instance;
        }
    }

    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    GameObject staffPairPrefab;

    // CLASS VARIABLES
    public StaffPair[] staffs { get { return GetComponentsInChildren<StaffPair>(); } }
    public List<Note> notes { get; set; } = new List<Note>();
    public List<Note> currentNotes { get; set; } = new List<Note>();
    int currentNoteIndex = 0;

    // CLASS FUNCTIONS
    public void addNote(int number, string name, int index, float time)
    {
        while (time >= staffs[staffs.Length - 1].startTime + StaffPair.staffLength)
        {
            StaffPair sp = Instantiate(staffPairPrefab, transform).GetComponent<StaffPair>();
            sp.startTime = staffs[staffs.Length - 2].startTime + StaffPair.staffLength;
        }
        int toAddIndex = 0;
        while (time >= staffs[toAddIndex].startTime + StaffPair.staffLength)
        {
            toAddIndex++;
        }
        staffs[toAddIndex].addNote(number, name, index, time);
    }

    public void initialize()
    {
        notes.Sort(delegate (Note n1, Note n2) { return n1.noteTime.CompareTo(n2.noteTime); });
        currentNotes.Add(notes[0]);
        currentNoteIndex = 1;
        while (currentNoteIndex < notes.Count && System.Math.Abs(currentNotes[0].noteTime - notes[currentNoteIndex].noteTime) < float.Epsilon)
        {
            currentNotes.Add(notes[currentNoteIndex]);
            currentNoteIndex++;
        }
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, staffs[0].GetComponent<RectTransform>().rect.height * (staffs.Length + 0.5f));
    }

    public void advance()
    {
        foreach(Note n in currentNotes)
        {
            n.complete();
        }
        currentNotes.Clear();
        currentNotes.Add(notes[currentNoteIndex++]);
        while (currentNoteIndex < notes.Count && System.Math.Abs(currentNotes[0].noteTime - notes[currentNoteIndex].noteTime) < float.Epsilon)
        {
            currentNotes.Add(notes[currentNoteIndex]);
            currentNoteIndex++;
        }
        string s = "";
        foreach (Note n in currentNotes)
        {
            s += n.noteNumber + ", ";
        }
        if (currentNotes[0].staff.staffPair.transform.localPosition.y < 0)
        {
            Debug.Log("scrolling music");
            transform.localPosition = new Vector2(transform.transform.localPosition.x,
            /*-currentNotes[0].staff.staffPair.transform.position.y*/ -currentNotes[0].staff.staffPair.transform.localPosition.y);
        }
    }

    StaffPair getStaffAtTime(float time)
    {
        int index = 0;
        while (staffs[index].startTime + StaffPair.staffLength < time)
        {
            index++;
        }
        return staffs[index];
    }

    // UNITY FUNCTIONS
    private void Start()
    {
        StaffPair sp = Instantiate(staffPairPrefab, transform).GetComponent<StaffPair>();
        sp.startTime = 0;
    }
    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
