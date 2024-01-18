using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldToContent : MonoBehaviour
{
    private static readonly string no_breaking_space = "\u00A0";

    InputField m_InputField;
    Text m_Text;
    void Start()
    {
        m_InputField = GetComponent<InputField>();
        m_Text = transform.parent.GetComponent<Text>();
        m_InputField.onValueChanged.AddListener((value) =>
       {
           //替换空格编码格式
           m_InputField.text = m_InputField.text.Replace(" ", no_breaking_space);

           m_Text.text = m_InputField.text;
       });
    }
}
