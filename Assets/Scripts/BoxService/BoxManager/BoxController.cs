
using UnityEngine;

public class BoxController
{
    private BoxView boxView;

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

    private void SetPosition(Vector3 position)
    {
        boxView.transform.position = position;
    }

    public void ReturnToPool()
    {
        //return to Pool
        boxView.gameObject.SetActive(false);
        GameService.Instance.BoxService.BoxPool.ReturnToPool(this);
    }


}

