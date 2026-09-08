using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions input;

    public Vector2 WalkInput { get; private set; }//歩く
    public bool JumpPressed{ get; private set; }//ジャンプ
    public bool SwordPressed { get; private set; }//攻撃
    public bool TetunagiPressed { get; private set; }//手つなぎモードに変更
    public bool DakkoPressed { get; private set; }//抱っこモードに変更
    public bool NormalPressed { get; private set; }//ノーマルモード
    public bool ShootPressed {  get; private set; }//弓発射

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

        SwordPressed = input.Player.Sword.WasPressedThisFrame();

        TetunagiPressed = input.Player.Tetunagi.WasPressedThisFrame();

        DakkoPressed = input.Player.Dakko.WasPressedThisFrame();

        NormalPressed = input.Player.Normal.WasPressedThisFrame();

        ShootPressed = input.Player.Shoot.IsPressed();
    }
}
