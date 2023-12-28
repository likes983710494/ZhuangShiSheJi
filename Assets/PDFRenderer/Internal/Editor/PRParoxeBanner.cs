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
using UnityEditor;

public class PRParoxeBanner
{
    Texture2D m_ParoxeIcon;
    Texture2D m_TwitterIcon;
    Texture2D m_RatingIcon;
    Texture2D m_FacebookIcon;

    Texture2D m_OpenedIcon;
    Texture2D m_ClosedIcon;

    string m_Path;

    string ParoxePath
    {
        get { return Path("Paroxe32.png"); }
    }

    string TwitterPath
    {
        get { return Path("Twitter32.png"); }
    }

    string FacebookPath
    {
        get { return Path("Facebook32.png"); }
    }

    string RatingPath
    {
        get { return Path("Rating32.png"); }
    }

    string OpenedPath
    {
        get { return Path("Open32.png"); }
    }

    string ClosedPath
    {
        get { return Path("Close32.png"); }
    }

    public PRParoxeBanner(string path)
    {
        m_Path = path;

        Intilialize();
    }

    string Path(string rel)
    {
        return m_Path + "/" + rel;
    }

    Texture2D GetTexture(string path)
    {
        Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        tex.hideFlags = HideFlags.HideAndDontSave;
        return tex;
    }

    void Intilialize()
    {
        m_ParoxeIcon = GetTexture(ParoxePath);
        m_TwitterIcon = GetTexture(TwitterPath);
        m_RatingIcon = GetTexture(RatingPath);
        m_FacebookIcon = GetTexture(FacebookPath);

        m_OpenedIcon = GetTexture(OpenedPath);
        m_ClosedIcon = GetTexture(ClosedPath);
    }

    void Space(float width, float height)
    {
        GUILayoutUtility.GetRect(width, height);
    }

    void Space()
    {
        float w = 4.0f;
        float h = 32 * 0.75f;
        Space(w,h);
    }

    bool OnCloseOpenGUI( bool isOpened)
    {
        Texture2D icon = isOpened ? m_ClosedIcon : m_OpenedIcon;

        Rect r = GUILayoutUtility.GetRect(icon.width * 0.3f, icon.height * 0.3f);
        GUI.DrawTexture(r, icon, ScaleMode.ScaleToFit);

        if (GUI.Button(r, "", new GUIStyle()))
        {
            return !isOpened;
        }

        return isOpened;
    }

    void OnInconGUI(Texture icon, string weblink)
    {
        Rect r = GUILayoutUtility.GetRect(icon.width*0.75f, icon.height*0.75f);
        GUI.DrawTexture(r, icon, ScaleMode.ScaleToFit);

        if (GUI.Button(r, "", new GUIStyle()))
        {
            Application.OpenURL(weblink);
        }
    }

    public bool DoOnGUI(bool isOpened)
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        isOpened = OnCloseOpenGUI(isOpened);
        if (isOpened)
        {
            Space();
            OnInconGUI(m_ParoxeIcon, "http://paroxe.com/");
            Space();
            OnInconGUI(m_RatingIcon, "https://www.assetstore.unity3d.com/en/#!/content/32815");
            Space();
            OnInconGUI(m_TwitterIcon, "https://twitter.com/Paroxe_dev");
            Space();
            OnInconGUI(m_FacebookIcon, "https://www.facebook.com/paroxe.multimedia/");
        }
        EditorGUILayout.EndHorizontal();

        return isOpened;
    }
}
