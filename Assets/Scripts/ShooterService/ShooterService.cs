
using System.Collections.Generic;
using UnityEngine;

public class ShooterService: MonoBehaviour
{
    [SerializeField] List<ShooterManager> shootersList;
    [SerializeField] List<Transform> shooterPositions;
    [SerializeField] Transform firstShooterPos;
    [SerializeField] BulletView BulletPrefab;
    [SerializeField] float shootInterval;
    private Dictionary<Transform, bool> positionStatus;
    private List<ShooterManager> activeShootersAtTargetPosition;
    private ShooterManager activeShooter;
    private float shootTimer;
    private int currentActiveShooterIndex;

    private void Awake()
    {
        positionStatus = new Dictionary<Transform, bool>();
        activeShootersAtTargetPosition = new List<ShooterManager>();
        foreach (var shooter in shooterPositions)
        {
            positionStatus[shooter] = false;
        }
    }

    private void Start()
    {
        OnGameStart();
        activeShootersAtTargetPosition.Clear();
    }

    public void OnGameStart()
    {
        SetShootersClickableStatus();
        shootTimer = 0f;
        ResetStartingPositions();
    }

    private void Update()
    {
        List<ShooterManager> shootersToRemove = new List<ShooterManager>();
        foreach (var shooter in shootersList)
        {
            if (shooter.IsMoving)
            {
                Debug.Log("will move");
                MoveToShootingPosition(shooter);
                if(shooter.IsMoving==false&&shooter.IsAtShootingPosition==true)
                {
                    shootersToRemove.Add(shooter);
                    activeShootersAtTargetPosition.Add(shooter);
                    if(activeShootersAtTargetPosition.Count==1)
                    {
                        activeShooter = activeShootersAtTargetPosition[0];
                        currentActiveShooterIndex = 0;
                    }
                }

            }
        }
        foreach (var shooter in shootersToRemove)
        {
            shootersList.Remove(shooter);
            ResetStartingPositions();
            SetShootersClickableStatus();
        }
        if(activeShooter!=null)
        {
            if (GameService.Instance.BoxService.CheckEachFirstBox((int)activeShooter.ColorID))
            {
                HandleShooting(activeShooter);
            }
            else
            {
                currentActiveShooterIndex = (currentActiveShooterIndex + 1)%activeShootersAtTargetPosition.Count;
                activeShooter = activeShootersAtTargetPosition[currentActiveShooterIndex];
            }
        }
        shootersToRemove.Clear();
        if(activeShooter!=null)
        {
            if(activeShooter.CurrentBullets<=0)
            {
                shootersToRemove.Add(activeShooter);
                positionStatus[activeShooter.TargetShooterPosition] = false;
                activeShooter.SetIsAtShootingPosition(false);
                activeShooter.UpdateBullets(0);
                activeShooter.SetTargetShootingPosition(null);
                SetShootersClickableStatus();
                activeShooter.gameObject.SetActive(false);
            }
        }
        foreach(var shooter in shootersToRemove)
        {
            activeShootersAtTargetPosition.Remove(shooter);
            if (activeShootersAtTargetPosition.Count > 0)
            {
                activeShooter = activeShootersAtTargetPosition[0];
                currentActiveShooterIndex = 0;
            }
        }

        CheckForGameLossStatus();
    }

    private void CheckForGameLossStatus()
    {
        if(activeShootersAtTargetPosition.Count==shooterPositions.Count)
        {
            bool checkColor = true;
            foreach(var item in  activeShootersAtTargetPosition)
            {
                if(GameService.Instance.BoxService.CheckEachFirstBox((int)item.ColorID)==false)
                {
                    checkColor = false;
                }
                else
                {
                    checkColor = true;
                    break;
                }
            }
            if(!checkColor)
            {
                Debug.Log("GAME LOST");
            }
        }
    }

    private void ResetStartingPositions()
    {
        for(int i=0;i<shootersList.Count;i++)
        {
            if(i==0)
            {
                shootersList[i].transform.position=firstShooterPos.position;
            }
            else
            {
                shootersList[i].transform.position=new Vector3(firstShooterPos.position.x, firstShooterPos.position.y, /*shootersList[i].transform.position.z*/firstShooterPos.position.z-(i*2));
            }
        }
    }

    public void SetShootersClickableStatus()
    {
        for(int i=0;i<shootersList.Count;i++)
        {
            shootersList[i].SetIsClickable(true);
            shootersList[i].SetShooterService(this);
        }
    }

    public void AssignToShootingPosition(ShooterManager shooter)
    {
        foreach (var position in shooterPositions)
        {
            if (!positionStatus[position])
            {
                shooter.SetTargetShootingPosition(position);
                positionStatus[position] = true;
                shooter.SetisMoving(true);
                break;
            }
        }
    }

    private void MoveToShootingPosition(ShooterManager shooter)
    {
        shooter.transform.position=Vector3.MoveTowards(shooter.transform.position, shooter.TargetShooterPosition.position, 10f*Time.deltaTime);
        if(Vector3.Distance(shooter.transform.position,shooter.TargetShooterPosition.position)<=0f)
        {
            shooter.SetisMoving(false);
            shooter.SetIsAtShootingPosition(true);

        }
    }

    private void HandleShooting(ShooterManager shooter)
    {
        shootTimer += Time.deltaTime;
        if(shootTimer>=shootInterval)
        {
            SpawnProjectile(shooter);
            shootTimer = 0f;
        }
    }

    private void SpawnProjectile(ShooterManager shooter)
    {
        BoxColorID colorID =shooter.ColorID;
        List<BoxController> list = GameService.Instance.BoxService.GetFirstBoxesList((int)colorID) ;
        if (list.Count > 0)
        {
            BulletView newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = shooter.MuzzleTransform.position;
            newBullet.SetDestination(list[0].GetTransform().position, colorID, list[0]);
            shooter.UpdateBullets(shooter.CurrentBullets - 1);
        }
    }
}
