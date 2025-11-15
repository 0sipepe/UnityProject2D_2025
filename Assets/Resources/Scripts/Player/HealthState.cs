
using System;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    private bool isAlive = true;

    [SerializeField]
    private PlayerAnim playerAnim;

    [SerializeField]
    private bool isFish = false;

    public event EventHandler OnDeath;
    public event EventHandler OnWin;
    public event EventHandler OnLoose;
    public event EventHandler OnBanControl;
    public event EventHandler OnAvailControl;


    public static HealthState Instance;

    public HealthState()
    {
        if (Instance != null)
        {
            Console.WriteLine("error in health state");
        }
        Instance = this;
    }
  
 
    public bool IsAlive
    {
        get { return isAlive; }
        
        set
        {
            if (value == false)
            {
                if (!isFish)
                {
                    isAlive = false;
                }
                else
                {
                    Respawn();
                }
            }
        }

    }
    private void Respawn()
    {
        Debug.Log("Respawn");
        OnDeath?.Invoke(this, EventArgs.Empty);

    }
    private void Win()
    {
        OnWin?.Invoke(this, EventArgs.Empty);
        Debug.Log("You win");
    }
    public void CompletelyDead()
    {
        isFish = false;
        OnLoose?.Invoke(this, EventArgs.Empty);
        //TODO: добавить состояние когда кончилась музыка и все закончилось

    }

    public void CheckState()
    {
        if (IsAlive)
        {
            Win();
        }
        else
        {
            if (!isFish)
            {
                playerAnim.PlayPlayerDeath();
            }
            else
            {
                CompletelyDead();
            }
                
        }
    }
    public void BecomeFish()
    {
        isFish = true;
        isAlive = true;
        Respawn();
    }

    public void BanControl()
    {

        OnBanControl?.Invoke(this, EventArgs.Empty);
    }
    public void LetControl()
    {
        OnAvailControl?.Invoke(this, EventArgs.Empty);
    }
}
