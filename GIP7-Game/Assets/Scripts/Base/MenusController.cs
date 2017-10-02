using Assets.Scripts.Base;
using UnityEngine;

public class MenusController : MonoBehaviour
{
    void Update()
    {
        GoBack();
    }

    protected virtual bool ShouldGoBack
    {
        get { return Input.GetKeyUp(KeyCode.Escape); }
    }

    private void GoBack()
    {
        //Check if the user wants to go back to the previous screen / main menu
        if (ShouldGoBack)
        {
            LevelLoader.LoadMainMenu();
        }
    }
}
