using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    #region Variables

    public static ColorManager Instance { get; private set; }

    [SerializeField] private ColorSpec[] ColorSpecs;

    #endregion

    #region Action Variables



    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    #region Custom Methods

    public Color ReturnColor(AllColors color)
    {
        Color targetColor = Color.black;

        for (int i = 0; i < ColorSpecs.Length; i++)
        {
            if (color == ColorSpecs[i].colorKey)
            {
                targetColor = ColorSpecs[i].color;
            }
        }

        print("<color=orange>" + $"Color Set: {color.ToString()}" + "</color>");

        return targetColor;
    }

    #endregion
}

[System.Serializable]
public struct ColorSpec
{
    public AllColors colorKey;
    public Color color;
}
