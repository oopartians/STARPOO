using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class TeamColor
{
    private Color red = new Color(196 / 255.0F, 31 / 255.0F, 59 / 255.0F, 0.5f);
    private Color orange = new Color(255 / 255.0F, 125 / 255.0F, 10 / 255.0F, 0.5f);
    private Color yellow = new Color(255 / 255.0F, 245 / 255.0F, 105 / 255.0F, 0.5f);
    private Color lightGreen = new Color(171 / 255.0F, 212 / 255.0F, 115 / 255.0F, 0.5f);
    private Color fluoreGreen = new Color(0,255 / 255.0F, 150 / 255.0F, 0.5f);
    private Color green = new Color(26 / 255.0F, 111 / 255.0F, 38 / 255.0F, 0.5f);
    private Color skyBlue = new Color(105 / 255.0F, 204 / 255.0F, 240 / 255.0F, 0.5f);
    private Color blue = new Color(0 / 255.0F, 112 / 255.0F, 222 / 255.0F, 0.5f);
    private Color pink = new Color(245 / 255.0F, 140 / 255.0F, 186 / 255.0F, 0.5f);
    private Color lightPurple = new Color(148 / 255.0F, 130 / 255.0F, 201 / 255.0F, 0.5f);
    private Color purple = new Color(80 / 255.0F, 37 / 255.0F, 129 / 255.0F, 0.5f);
    private Color lightBrown = new Color(199 / 255.0F, 156 / 255.0F, 110 / 255.0F, 0.5f);
    private Color white = new Color(255 / 255.0F, 255 / 255.0F, 255 / 255.0F, 0.5f);
    public List<Color> defaultColors = new List<Color>(); 
    public Queue<Color> colors = new Queue<Color>();

    public TeamColor()
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

    private void InitDefaultColorList(params Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
            defaultColors.Add(colors[i]);
    }

    public void SetColorsList(int matchSize)
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

    public void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public Color DequeueTeamColor()
    {
        try
        {
            return colors.Dequeue();
        }
        catch (Exception ex)
        {
            Debug.Log("[TeamColor] Error catched : " + ex.Message);
            return new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1), 0.5f);
        }
    }

    public void EnQueueTeamColor(Color color)
    {
        colors.Enqueue(color);
    }
}
