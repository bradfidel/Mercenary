using UnityEngine;
using UnityEngine.UI;

public sealed class DebugCanvas : MonoBehaviour
{
    public Text debugText;

    private static DebugCanvas m_instance;

    private void Awake()
    {
        m_instance = this;
    }

    public static void Display(string text)
    {
        m_instance.debugText.text = text;
    }
}
