/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class PRHelper : MonoBehaviour 
{

    static PRHelper()
    {
        s_Styles = new Styles();
    }

    public static bool GroupHeader(string text, bool isExpanded)
    {
        Rect rect = GUILayoutUtility.GetRect(20f, 18f, s_Styles.header);

        s_Styles.Backup();
        s_Styles.Apply();

        if (Event.current.type == EventType.Repaint)
            s_Styles.header.Draw(rect, text, isExpanded, isExpanded, isExpanded, isExpanded);

        Event e = Event.current;
        if (e.type == EventType.MouseDown)
        {
            if (rect.Contains(e.mousePosition))
            {
                isExpanded = !isExpanded;
                e.Use();
            }
        }

        s_Styles.Revert();
        return isExpanded;
    }

    private static Styles s_Styles;
    private class Styles
    {
        public GUIStyle header = "ShurikenModuleTitle";

        internal Styles()
        {
            header.font = (new GUIStyle("Label")).font;
        }

        RectOffset m_Border;
        float m_FixedHeight;
        Vector2 m_ContentOffset;
        TextAnchor m_TextAlign;
        FontStyle m_TextStyle;
        int m_FontSize;
        RectOffset m_Margin;

        public void Backup()
        {
            m_Border = s_Styles.header.border;
            m_FixedHeight = s_Styles.header.fixedHeight;
            m_ContentOffset = s_Styles.header.contentOffset;
            m_TextAlign = s_Styles.header.alignment;
            m_TextStyle = s_Styles.header.fontStyle;
            m_FontSize = s_Styles.header.fontSize;
            m_Margin = s_Styles.header.margin;
        }

        public void Apply()
        {
            s_Styles.header.border = new RectOffset(7, 7, 4, 4);
            s_Styles.header.fixedHeight = 20;
            s_Styles.header.contentOffset = new Vector2(8f, -2f);
            s_Styles.header.alignment = TextAnchor.MiddleLeft;
            s_Styles.header.fontStyle = FontStyle.Normal;
            s_Styles.header.fontSize = 12;
            s_Styles.header.margin = new RectOffset(0, 0, 0, 5);
        }

        public void Revert()
        {
            s_Styles.header.border = m_Border;
            s_Styles.header.fixedHeight = m_FixedHeight;
            s_Styles.header.contentOffset = m_ContentOffset;
            s_Styles.header.alignment = m_TextAlign;
            s_Styles.header.fontStyle = m_TextStyle;
            s_Styles.header.fontSize = m_FontSize;
            s_Styles.header.margin = m_Margin;
        }
    }
}
