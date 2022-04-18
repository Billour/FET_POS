using System;
using System.Collections;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
/// 處理中文字型資訊
/// </summary>
public static class ChineseFontFactory
{
    private static Dictionary<Font, WeakReference> flyweights =
        new Dictionary<Font, WeakReference>();

    /// <summary>
    /// 新建中文字型物件
    /// </summary>
    /// <param name="name">字型名稱</param>
    /// <param name="size">字型大小</param>
    /// <param name="style">字型樣式</param>
    /// <returns></returns>
    public static Font Create(string name, float size, int style)
    {
        string fontPath = GetFontPath(name);
        if (fontPath == null)
        {
            throw new Exception("The specified font is not found.");
        }

        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        Font font = new Font(baseFont, size, style);
        if (!flyweights.ContainsKey(font))
        {
            flyweights.Add(font, new WeakReference(font));
        }
        return flyweights[font].Target as Font;
    }

    /// <summary>
    /// 新建中文字型物件
    /// </summary>
    /// <param name="name">字型名稱</param>
    /// <param name="size">字型大小</param>
    /// <param name="style">字型樣式</param>
    /// <param name="bc">字型顏色</param>
    /// <returns></returns>
    public static Font Create(string name, float size, int style, BaseColor bc)
    {
        string fontPath = GetFontPath(name);
        if (fontPath == null)
        {
            throw new Exception("The specified font is not found.");
        }

        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        Font font = new Font(baseFont, size, style, bc);
        if (!flyweights.ContainsKey(font))
        {
            flyweights.Add(font, new WeakReference(font));
        }
        return flyweights[font].Target as Font;
    }


    /// <summary>
    /// 取得字型檔路徑
    /// </summary>
    /// <param name="fontName">字型名稱</param>
    /// <returns></returns>
    private static string GetFontPath(string fontName)
    {
        string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\";
        switch (fontName)
        {
            case "細明體":
            case "新細明體":
                return fontPath + "simhei.ttf";
            case "標楷體":
                return fontPath + "kaiu.ttf";
        }
        return null;
    }
}