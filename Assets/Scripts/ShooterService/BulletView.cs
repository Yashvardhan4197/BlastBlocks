
using UnityEngine;

public class BulletView: MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private Vector3 destination;
    private bool isMoving;
    private BoxColorID colorID;
    private BoxController targetBoxController;
    private void OnEnable()
    {
        isMoving = false;
        targetBoxController = null;
    }

    private void Update()
    {
        if (isMoving == true)
        {
            MoveToDestination();
        }
    }


    public void MoveToDestination()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,destination,bulletSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //return toPool;
        isMoving = false;
        GameService.Instance.BoxService.DestroyFirstBox(targetBoxController);
        //Handheld.Vibrate();
        Destroy(this.gameObject);
    }

    public void SetDestination(Vector3 destination,BoxColorID boxColorID,BoxController targetBoxController)
    { 
        this.destination=destination;
        colorID = boxColorID;
        isMoving = true;
        this.targetBoxController = targetBoxController;
    }


}

