using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoKey : MonoBehaviour
{
    // SERIALIZEFIELD VARIABLES
    [SerializeField]
    bool isBlackKey;
    [SerializeField]
    Text label;
    [SerializeField]
    Image image;

    // CLASS VARIABLES
    int m_number;
    public int number
    {
        get { return m_number; }
        set
        {
            m_number = value;
            label.text = MusicSheet.getNoteNameByNumber(m_number);
            gameObject.name = label.text;
        }
    }

    // CLASS FUNCTIONS
    public void highlight()
    {
        image.color = Color.green;
    }

    public void unhighlight()
    {
        if (isBlackKey)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
    }

    public void press()
    {
        image.color = Color.gray;
    }

    public void unpress()
    {
        if (isBlackKey)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
