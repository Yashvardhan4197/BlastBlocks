using UnityEngine;

public class BoxView: MonoBehaviour
{
    private BoxController boxController;
    [SerializeField] Color color;
    [SerializeField] int colorID;


    public void SetController(BoxController boxController)
    {
        this.boxController = boxController;
    }
}