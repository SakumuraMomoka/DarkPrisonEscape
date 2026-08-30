using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions input;

    public Vector2 WalkInput { get; private set; }//歩く
    public bool JumpPressed{ get; private set; }//ジャンプ
    public bool AttackPressed { get; private set; }//攻撃
    public bool TetunagiPressed { get; private set; }//手つなぎモードに変更
    public bool DakkoPressed { get; private set; }//抱っこモードに変更
    public bool SwordPressed { get; private set; }//ソードモードに変更
    public bool KaijyoPressed { get; private set; }//手つなぎ、抱っこ、またはソードモードを解除してノーマルモードになる

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        WalkInput = input.Player.Walk.ReadValue<Vector2>();

        JumpPressed = input.Player.Jump.WasPressedThisFrame();

        AttackPressed = input.Player.Attack.WasPressedThisFrame();

        TetunagiPressed = input.Player.Tetunagi.WasPressedThisFrame();

        DakkoPressed = input.Player.Dakko.WasPressedThisFrame();

        SwordPressed = input.Player.Sword.WasPressedThisFrame();

        KaijyoPressed = input.Player.Kaijyo.WasPressedThisFrame();

    }
}
