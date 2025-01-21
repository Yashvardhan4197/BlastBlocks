
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShooterManager:MonoBehaviour
{
    [SerializeField] int TotalBullets;
    private bool isClickable;
    private Transform targetShooterPosition;
    private ShooterService shooterService;
    [SerializeField] BoxColorID colorID;
    [SerializeField] Renderer materialRenderer;
    [SerializeField] TextMeshPro bulletTextNumber;
    [SerializeField] Transform muzzleTransform;
    private int currentBullets;
    private bool isMoving;
    private bool isAtShootingPosition;
    public int CurrentBullets {  get { return currentBullets; } }
    public Transform MuzzleTransform { get { return muzzleTransform; } }
    public BoxColorID ColorID { get { return colorID; } }
    public bool IsMoving {  get { return isMoving; } }
    public bool IsAtShootingPosition {  get { return isAtShootingPosition; } }
    public Transform TargetShooterPosition { get { return targetShooterPosition; } }

    private void Start()
    {
        OnGameStart();
    }




    public void OnGameStart()
    {
        if (colorID == BoxColorID.RED)
        {
            materialRenderer.material.SetColor("_Color", Color.red);
        }else if(colorID == BoxColorID.GREEN)
        {
            materialRenderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            materialRenderer.material.SetColor("_Color", Color.blue);
        }
        UpdateBullets(TotalBullets);
        isMoving = false;
        isAtShootingPosition = false;
    }

    private void Update()
    {
        


        if(Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput(Input.mousePosition);
        }

        if(Input.touchCount>0)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput(touch.position);
            }
        }

    }

    private void HandleInput(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        {
            ShooterManager shooter = hit.collider.GetComponent<ShooterManager>();

            if (shooter != null && shooter.isClickable)
            {
                shooter.isClickable = false;
                shooterService.AssignToShootingPosition(shooter);
                GameService.Instance.SoundService.PlaySFX(Sound.PLAYER_CLICK);
            }
        }
    }

    public void SetIsClickable(bool isClickable)
    {
        this.isClickable = isClickable;
    }

    public void UpdateBullets(int number)
    {
        currentBullets = number;
        bulletTextNumber.text=currentBullets.ToString();
    }

    public void SetShooterService(ShooterService shooterService)
    {
        this.shooterService = shooterService;
    }
        
    public void SetisMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    public void SetIsAtShootingPosition(bool isShooting)
    {
        isAtShootingPosition = isShooting;
    }

    public void SetTargetShootingPosition(Transform targetShootingPosition)
    {
        this.targetShooterPosition = targetShootingPosition;
    }

}
