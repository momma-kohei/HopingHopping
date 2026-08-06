using UnityEngine;

public class JumpCtl : MonoBehaviour
{
    [SerializeField] private TriggerCtl groundTrigger;
    const float BASIC_VELOITY = 90f;

    private ChargeCtl chargeCtl;
    private LaunchCtl launchCtl;
    private Vector2 _jumpVector;
    public Vector2 JumpVector { get { return _jumpVector; } set { _jumpVector = value;  } }

    private void Start()
    {
        chargeCtl = GetComponent<ChargeCtl>();
        launchCtl = GetComponent<LaunchCtl>();
    }

    public void Charge()
    {
        chargeCtl.ChargeStart();
    }

    public void Jump()
    {
        if (groundTrigger.IsActive == false) return;
        chargeCtl.ChargeFinish();
        launchCtl.Launch(BASIC_VELOITY * chargeCtl.Charge * _jumpVector);
    }
}
