
using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//鼠标区域截图
public class SelectScreenshot : MonoBehaviour
{
    private bool isSelecting = false;
    private Vector3 mousePosition1;
    private Vector3 mousePosition2;
    public bool mIsOpenShot = false;
    public void SetIsOpenShot()
    {
        mIsOpenShot = true;

    }
    void Update()
    {
        if (IsMouseOnScreen())
        {
            if (mIsOpenShot)
            {
                // 当按下鼠标左键，开始选择区域
                if (Input.GetMouseButtonDown(0))
                {
                    isSelecting = true;
                    mousePosition1 = Input.mousePosition;
                    //Debug.Log("mousePosition1:---" + mousePosition1);
                }

            }
            // 当释放鼠标左键，结束选择区域并截图
            if (Input.GetMouseButtonUp(0) && isSelecting)
            {
                isSelecting = false;
                mousePosition2 = Input.mousePosition;
                StopAllCoroutines();
                int width = (int)Mathf.Abs(mousePosition1.x - mousePosition2.x);
                int height = (int)Mathf.Abs(mousePosition1.y - mousePosition2.y);
                if (width > 1 && height > 1)
                {
                    StartCoroutine(CaptureScreenshot(mousePosition1, mousePosition2));
                }
            }
        }
        else
        {
            // 当释放鼠标左键，结束选择区域并截图
            if (Input.GetMouseButtonUp(0) && isSelecting)
            {
                isSelecting = false;
                mousePosition2 = Input.mousePosition;
                // 判断鼠标是否在屏幕范围内
                if (mousePosition2.x < 0) mousePosition2.x = 0;
                if (mousePosition2.y < 0) mousePosition2.y = 0;
                if (mousePosition2.x > Screen.width) mousePosition2.x = Screen.width;
                if (mousePosition2.y > Screen.height) mousePosition2.y = Screen.height;
                StopAllCoroutines();
                int width = (int)Mathf.Abs(mousePosition1.x - mousePosition2.x);
                int height = (int)Mathf.Abs(mousePosition1.y - mousePosition2.y);
                if (width > 1 && height > 1)
                {
                    StartCoroutine(CaptureScreenshot(mousePosition1, mousePosition2));
                }
            }
        }
    }
    bool IsMouseOnScreen()
    {
        Vector3 mousePos = Input.mousePosition;
        return (mousePos.x >= 0 && mousePos.x <= Screen.width && mousePos.y >= 0 && mousePos.y <= Screen.height);
    }
    public (Texture2D, int, int) GetTexture2D()
    {
        if (File.Exists(Application.streamingAssetsPath + "/shot.png"))
        {
            return (LoadImageFromFile(Application.streamingAssetsPath + "/shot.png"), width, height);
        }
        else
        {
            return (null, 0, 0);
        }
    }
    public Texture2D GetOnlyTexture2D()
    {
        if (File.Exists(Application.streamingAssetsPath + "/shot.png"))
        {
            return LoadImageFromFile(Application.streamingAssetsPath + "/shot.png");
        }
        else
        {
            return null;
        }
    }
    public (Texture2D, int, int) GetTexture2D(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            if (File.Exists(path))
            {
                return (LoadImageFromFile(path), width, height);
            }
            else
            {
                return (null, 0, 0);
            }
        }
        else
        {
            return (null, 0, 0);
        }
    }
    int width;
    int height;
    private Texture2D LoadImageFromFile(string path)
    {
        // 从文件中读取图片数据
        byte[] imageData = System.IO.File.ReadAllBytes(path);

        // 创建一个新的Texture2D对象
        Texture2D texture = new Texture2D(width, height);

        // 加载图片数据到Texture2D对象
        texture.LoadImage(imageData);

        return texture;
    }

    private IEnumerator CaptureScreenshot(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // 等待渲染线程结束
        yield return new WaitForEndOfFrame();

        // 计算选区的屏幕坐标和大小
        int x = (int)Mathf.Min(screenPosition1.x, screenPosition2.x);
        int y = (int)Mathf.Min(screenPosition1.y, screenPosition2.y);
        width = (int)Mathf.Abs(screenPosition1.x - screenPosition2.x);
        height = (int)Mathf.Abs(screenPosition1.y - screenPosition2.y);
        // 创建一个Texture2D对象来保存选区的图像
        Texture2D screenImage = new Texture2D(width, height);

        // 读取屏幕像素信息并存储为纹理数据
        screenImage.ReadPixels(new Rect(x, y, width, height), 0, 0);
        screenImage.Apply();

        // 将纹理数据转换为字节数组，并保存为图片
        byte[] imageBytes = screenImage.EncodeToPNG();

        // 保存图片到文件
        // string filePath = Application.streamingAssetsPath + "/shot.png";
        // string path = Path.Combine("Assets/Resources/Shot", "screenshot.png");
        // System.IO.File.WriteAllBytes(filePath, imageBytes);
        // System.IO.File.WriteAllBytes(path, imageBytes);

        // 保存图片到桌面
        string Name = DateTime.Now.ToString("yyyyMMddHHmmss");
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath2 = Path.Combine(desktopPath, Name + ".png");
        System.IO.File.WriteAllBytes(filePath2, imageBytes);
        // 销毁Texture2D对象，释放资源
        Destroy(screenImage);
        mIsOpenShot = false;
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    void OnGUI()
    {
        // 如果正在选择区域，绘制一个矩形以表示选区
        if (isSelecting)
        {
            Vector3 mousePos = Input.mousePosition;
            // 判断鼠标是否在屏幕范围内
            if (mousePos.x < 0) mousePos.x = 0;
            if (mousePos.y < 0) mousePos.y = 0;
            if (mousePos.x > Screen.width) mousePos.x = Screen.width;
            if (mousePos.y > Screen.height) mousePos.y = Screen.height;

            // 转换屏幕坐标到GUI坐标
            var rect = Utils.GetScreenRect(mousePosition1, mousePos);
            Utils.DrawScreenRect(rect, new UnityEngine.Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, UnityEngine.Color.blue);
        }
    }
}

