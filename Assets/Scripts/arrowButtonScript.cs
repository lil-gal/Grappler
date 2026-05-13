using UnityEngine;

public class arrowButtonScript : MonoBehaviour
{
    public enum Direction { Left = -1, Right = 1 }
    public Direction direction;
    public LevelSelectorManagerScript selectorManager;
    public levelsGridScript gridScript;
    
    public void changeGrid() {
        selectorManager.Change((int)direction);
        gridScript.Refresh();
    }
}
