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

    public Mode CurrentMode {  get; private set; } = Mode.Normal;//最初のモードはnormal

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
}
