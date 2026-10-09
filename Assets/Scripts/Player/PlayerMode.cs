using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController normalController;
    [SerializeField] private RuntimeAnimatorController tetunagiController;
    [SerializeField] private RuntimeAnimatorController dakkoController;
    [SerializeField] private RuntimeAnimatorController swordController;

    public enum Mode//騎士のモードの一覧
    {
        Normal,
        Tetunagi,
        Dakko,
    }

    public enum FightMode//騎士の戦闘モードの一覧
    {
        Normal,
        Arrow,
        Sword
    }

    public Mode CurrentMode {  get; private set; } = Mode.Normal;//最初のモードはnormal
    public FightMode CurrentFightMode { get; private set; } = FightMode.Normal;//最初の戦闘モードはnormal

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeMode(Mode newMode)//Playerスクリプトでモードを変えるための関数
    {
        CurrentMode = newMode;

        switch(CurrentMode)//今のモードに応じて騎士のanimatorを変える
        {
            case Mode.Normal:
                animator.runtimeAnimatorController = normalController;
                break;

            case Mode.Tetunagi:
                animator.runtimeAnimatorController = tetunagiController;
                break;

            case Mode.Dakko:
                animator.runtimeAnimatorController = dakkoController;
                break;

        }
    }

    public void ChangeFightMode(FightMode newMode)//Playerスクリプトで戦闘モードを変えるための関数
    {
        CurrentFightMode = newMode;

        switch (CurrentFightMode)//今の戦闘モードに応じて変える
        {
            case FightMode.Normal:
                animator.runtimeAnimatorController = normalController;
                break;

            case FightMode.Arrow:
                animator.runtimeAnimatorController = tetunagiController;
                break;

            case FightMode.Sword:
                animator.runtimeAnimatorController = dakkoController;
                break;

        }
    }
}
