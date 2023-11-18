using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject MainGameObject;

    #endregion

    #region Action Variables



    #endregion

    #region Unity Methods

    private void Start()
    {

    }

    #endregion

    #region Custom Methods



    #endregion

}

#region Enums

public enum MainColors 
{ 
    Red = 0,
    Green = 1, 
    Blue = 2,
}

public enum AllColors 
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Magenta = 3, 
    Yellow = 4,
    Cyan = 5,
}

#endregion
