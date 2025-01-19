
using System;
using System.Collections.Generic;
using System.Drawing;
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
    private float shootTimer;


    private void Awake()
    {
        //positionsToMove = new Stack<Transform>();
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
                }

            }
        }
        foreach (var shooter in shootersToRemove)
        {
            shootersList.Remove(shooter);
            ResetStartingPositions();
            SetShootersClickableStatus();
        }
        foreach (var shooter in activeShootersAtTargetPosition)
        {
            HandleShooting(shooter);
        }
        shootersToRemove.Clear();
        foreach(var shooter in activeShootersAtTargetPosition)
        {
            if(shooter.CurrentBullets<=0)
            {
                shootersToRemove.Add(shooter);
                positionStatus[shooter.TargetShooterPosition] = false;
                shooter.SetIsAtShootingPosition(false);
                shooter.UpdateBullets(0);
                shooter.SetTargetShootingPosition(null);
                SetShootersClickableStatus();
                shooter.gameObject.SetActive(false);
            }
        }
        foreach(var shooter in shootersToRemove)
        {
            activeShootersAtTargetPosition.Remove(shooter);
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
            /*
            if (i == 0)
            {
                shootersList[i].SetIsClickable(true);
            }
            else
            {
                shootersList[i].SetIsClickable(false);
            }*/
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
        foreach(var box in list)
        {
             BulletView newBullet=Instantiate(BulletPrefab);
             newBullet.transform.position=shooter.MuzzleTransform.position;
             newBullet.SetDestination(box.GetTransform().position, colorID,box);
             shooter.UpdateBullets(shooter.CurrentBullets - 1);
        }
    }
}
