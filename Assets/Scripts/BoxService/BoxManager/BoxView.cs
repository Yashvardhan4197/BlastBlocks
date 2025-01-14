using UnityEngine;

public class BoxView: MonoBehaviour
{
    private BoxController boxController;
    [SerializeField] Color color;
    [SerializeField] Renderer material;
    public Renderer Material { get { return material; } }   
    public void SetController(BoxController boxController)
    {
        this.boxController = boxController;
    }

}