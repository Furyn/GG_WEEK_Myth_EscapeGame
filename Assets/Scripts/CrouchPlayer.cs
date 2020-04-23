using UnityEngine;

[RequireComponent(typeof(RotationPlayer))]
[RequireComponent(typeof(MovePlayer))]
public class CrouchPlayer : MonoBehaviour
{

    private RotationPlayer rp;
    private MovePlayer mp;

    private float widthInitOfPlayer;
    [SerializeField]
    private float widthOnCrouch = 0.5f;
    [SerializeField]
    private GameObject playerBody;

    [HideInInspector]
    public bool onCrouch = false;

    void Start()
    {
        widthInitOfPlayer = playerBody.transform.localScale.y;
        rp = GetComponent<RotationPlayer>();
        mp = GetComponent<MovePlayer>();
    }

    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            if (onCrouch)
            {
                playerBody.transform.localScale = new Vector3(transform.localScale.x, widthOnCrouch, transform.localScale.z);
                rp.screenCheckHoldJump = true;
                rp.isCrouch = true;
                mp.isCrouch = true;
            }
            else
            {
                playerBody.transform.localScale = new Vector3(transform.localScale.x, widthInitOfPlayer, transform.localScale.z);
                rp.screenCheckHoldJump = false;
                rp.isCrouch = false;
                mp.isCrouch = false;
            }
        }
    }
}
