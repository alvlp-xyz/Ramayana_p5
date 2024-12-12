using UnityEngine;

public class FlipController : MonoBehaviour
{
    public Animator animator;

    public void PlayFlipAnimation()
    {
        animator.SetTrigger("Flip");
    }
}
