
using UnityEngine;

public class BoxController
{
    private BoxView boxView;
    private int colorID;
    public BoxController(BoxView boxView,Transform boxParentTransform)
    {
        this.boxView = Object.Instantiate(boxView);
        this.boxView.SetController(this);
        this.boxView.transform.SetParent(boxParentTransform);
    }


    public void ActivateBox(Vector3 position)
    {
        boxView.gameObject.SetActive(true);
        SetPosition(position);
    }

    public void SetPosition(Vector3 position)
    {
        boxView.transform.position = position;
    }

    public void ReturnToPool()
    {
        //return to Pool
        boxView.gameObject.SetActive(false);
        GameService.Instance.BoxService.BoxPool.ReturnToPool(this);
    }

    public bool CheckColorID(int id)
    {
        if(colorID == id) return true;
        return false;
    }

    public Renderer GetBoxMaterial()=>boxView.Material;

    public void SetColorID(int id)
    {
        colorID = id;
    }
    public int GetColorID()
    {
        return colorID;
    }

    public Transform GetTransform()
    {
        return boxView?.transform;
    }


}

