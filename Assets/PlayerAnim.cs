using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private Animator _player;
    [SerializeField]
    private Animator _moose;

    public void PlayPlayerDeath()
    {
        _player.enabled = true;
        _player.SetTrigger("dead");
    }

    public void PlayMoose()
    {
        _moose.SetTrigger("kill");
    }
}
