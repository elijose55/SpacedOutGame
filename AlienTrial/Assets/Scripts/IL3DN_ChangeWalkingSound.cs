namespace IL3DN
{
    using UnityEngine;
    /// <summary>
    /// Override player sound when walking in different environments
    /// Attach this to a trigger
    /// </summary>
    public class IL3DN_ChangeWalkingSound : MonoBehaviour
    {
        public AudioClip[] footStepsOverride;
        public AudioClip jumpSound;
        public AudioClip landSound;
    }
}

/*
public  AudioSource audioSourceMain;
public AudioClip[] footstepsSounds;
public bool step;

    IEnumerator stepWaiter()
    {
        yield return new WaitForSeconds(0.3f);
        step = false;
    }
            if(Mathf.Abs(moveDirection.x/walkSpeed) > 0.1 && isGrounded) {
                if(!audioSourceMain.isPlaying && !step) {
                    step = true;
                    audioSourceMain.PlayOneShot(footstepsSounds[Random.Range(0,footstepsSounds.Length)]); //fazer um som de footstep diferente a cada vez
                    StartCoroutine(stepWaiter());
                }
            }
*/
