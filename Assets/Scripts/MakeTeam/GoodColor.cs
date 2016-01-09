using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class GoodColor
{
    private static Color red = new Color(196 / 255.0F, 31 / 255.0F, 59 / 255.0F, 0.5f);
    private static Color orange = new Color(255 / 255.0F, 125 / 255.0F, 10 / 255.0F, 0.5f);
    private static Color yellow = new Color(255 / 255.0F, 245 / 255.0F, 105 / 255.0F, 0.5f);
    private static Color lightGreen = new Color(171 / 255.0F, 212 / 255.0F, 115 / 255.0F, 0.5f);
    private static Color fluoreGreen = new Color(0,255 / 255.0F, 150 / 255.0F, 0.5f);
    private static Color green = new Color(26 / 255.0F, 111 / 255.0F, 38 / 255.0F, 0.5f);
    private static Color skyBlue = new Color(105 / 255.0F, 204 / 255.0F, 240 / 255.0F, 0.5f);
    private static Color blue = new Color(0 / 255.0F, 112 / 255.0F, 222 / 255.0F, 0.5f);
    private static Color pink = new Color(245 / 255.0F, 140 / 255.0F, 186 / 255.0F, 0.5f);
    private static Color lightPurple = new Color(148 / 255.0F, 130 / 255.0F, 201 / 255.0F, 0.5f);
    private static Color purple = new Color(80 / 255.0F, 37 / 255.0F, 129 / 255.0F, 0.5f);
    private static Color lightBrown = new Color(199 / 255.0F, 156 / 255.0F, 110 / 255.0F, 0.5f);
    private static Color white = new Color(255 / 255.0F, 255 / 255.0F, 255 / 255.0F, 0.5f);
    public static List<Color> defaultColors = new List<Color>(); 
    public static Queue<Color> colors = new Queue<Color>();

    static GoodColor()
    {
        Init();
    }

    public static void Init()
    {
        InitDefaultColorList(
        red,
        orange,
        yellow,
        lightGreen,
        fluoreGreen,
        green,
        skyBlue,
        blue,
        pink,
        lightPurple,
        purple,
        lightBrown,
        white
        );
    }

    private static void InitDefaultColorList(params Color[] colors)
    {
        defaultColors.Clear();
        for (int i = 0; i < colors.Length; i++)
        {
            defaultColors.Add(colors[i]);
        }
    }

    public static void SetColorsList(int matchSize)
    {
        Shuffle(defaultColors);

        for (int i = 0; i < matchSize; i++)
            colors.Enqueue(defaultColors[i]);

        if (matchSize > 13)
        {
            for(int i = colors.Count; i < matchSize; i++)
                colors.Enqueue(new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1), 0.5f));
        }
    }

    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static Color DequeueColor()
    {
        if(colors.Count > 0) // 기본 13색 이상도 커버하는게 '기본스펙' 이므로 에러가 아님. 그래서 try문에서 if문으로 변경.
        {
            return colors.Dequeue();
        }
        else
        {
            return new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1), 0.5f);
        }
    }

    public static void EnQueueColor(Color color)
    {
        colors.Enqueue(color);
    }
}
