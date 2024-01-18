using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 修复InputField输入时因空格引起的换行显示错误问题
/// </summary>
public class TextFixSpace : MonoBehaviour
{
    //代替空格的空格符
    private static readonly string no_breaking_space = "\u00A0";
    private Text m_Text;
    void Start()
    {
        m_Text = GetComponent<Text>();
        // 修复空格符
        m_Text.text = m_Text.text.Replace(" ", no_breaking_space);
    }
}
