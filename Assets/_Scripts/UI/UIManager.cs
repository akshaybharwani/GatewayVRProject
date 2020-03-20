using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Private Variables

    [Header("UI Elements")]
    
    [SerializeField]
    private CanvasGroup gameHudCanvasGroup;

    private KeyCode canvasToggleKeyCode = KeyCode.U;

    private bool canvasGroupAlphaBool = true;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the key U is pressed, toggle HUD Canvas
        if (Input.GetKeyDown(canvasToggleKeyCode))
        {
            toggleGameHUDCanvasGroup();
        }
    }

    /// <summary>
    /// Function to toggle (show and hide) HUD Canvas
    /// </summary>
    private void toggleGameHUDCanvasGroup()
    {
        if (canvasGroupAlphaBool)
        {
            gameHudCanvasGroup.alpha = 0;
            canvasGroupAlphaBool = false;
        }
        else
        {
            gameHudCanvasGroup.alpha = 1;
            canvasGroupAlphaBool = true;
        }
            
        
    }
}
