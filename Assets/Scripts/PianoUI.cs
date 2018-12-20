using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoUI : MonoBehaviour
{
    // INSTANCE
    static PianoUI m_instance;
    public static PianoUI instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PianoUI>();
            }
            return m_instance;
        }
    }

    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    GameObject whiteKeyPrefab;
    [SerializeField]
    GameObject blackKeyPrefab;

    // CLASS VARIABLES
    Dictionary<int, PianoKey> keyDict = new Dictionary<int, PianoKey>();

    // CLASS FUNCTIONS
    void generatePianoUI()
    {
        int whiteKeyIndex = 0;
        int blackKeyIndex = 0;
        Vector2 prevPos = Vector2.zero;
        GameObject tmp = Instantiate(whiteKeyPrefab);
        float whiteKeyWidth = tmp.GetComponent<RectTransform>().rect.width;
        Destroy(tmp);
        for (int i = 21; i <= 108; ++i)
        {
            if (!MusicSheet.getNoteNameByNumber(i).Contains("#"))
            {
                PianoKey key = Instantiate(whiteKeyPrefab, transform).GetComponent<PianoKey>();
                key.number = i;
                key.GetComponent<RectTransform>().localPosition = new Vector2(key.GetComponent<RectTransform>().rect.width * whiteKeyIndex - 960, 0);
                keyDict[i] = key;
                whiteKeyIndex++;
            }
            else
            {
                PianoKey key = Instantiate(blackKeyPrefab, transform).GetComponent<PianoKey>();
                key.number = i;
                if (i == 22)
                {
                    key.GetComponent<RectTransform>().localPosition = new Vector2(whiteKeyWidth - key.GetComponent<RectTransform>().rect.width * 0.5f - 960, 0);
                }
                else
                {
                    if (blackKeyIndex % 5 == 1 || blackKeyIndex % 5 == 3)
                        key.GetComponent<RectTransform>().localPosition = new Vector2(prevPos.x + 2 * whiteKeyWidth, 0);
                    else if (blackKeyIndex % 5 == 0 || blackKeyIndex % 5 == 2 || blackKeyIndex % 5 == 4)
                        key.GetComponent<RectTransform>().localPosition = new Vector2(prevPos.x + whiteKeyWidth, 0);
                }
                prevPos = key.GetComponent<RectTransform>().localPosition;
                keyDict[i] = key;
                blackKeyIndex++;
            }
        }
    }

    public void highlightKey(int number)
    {
        keyDict[number].highlight();
    }

    public void unhighlightKey(int number)
    {
        keyDict[number].unhighlight();
    }

    public void pressKey(int number)
    {
        keyDict[number].press();
    }

    public void unpressKey(int number)
    {
        foreach (Note n in MusicSheet.instance.currentNotes)
        {
            if (n.noteNumber == number)
            {
                highlightKey(number);
                return;
            }
        }
        keyDict[number].unpress();
    }

    // UNITY FUNCTIONS
    private void Start()
    {
        generatePianoUI();
    }

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
