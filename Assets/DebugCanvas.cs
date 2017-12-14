using UnityEngine;
using UnityEngine.UI;

public sealed class DebugCanvas : MonoBehaviour
{
    public Text[] debugText;

    private static DebugCanvas m_instance;

    private void Awake()
    {
        m_instance = this;
    }

    public static void Display(string text, int i)
    {
        if(i >= m_instance.debugText.Length)
        {
            Debug.LogError("Too few debug texts");
        }

        m_instance.debugText[i].text = text;
    }
}
