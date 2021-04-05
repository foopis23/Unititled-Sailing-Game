using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public bool hasPowerCoil;

    public bool activatedTower1;
    public bool activatedTower2;
    public bool activatedTower3;

    public Animator endingAnimator;

    private bool _playedEndingAnimation = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!activatedTower1 || !activatedTower2 || !activatedTower3 || _playedEndingAnimation) return;
        
        _playedEndingAnimation = true;
        endingAnimator.Play("Ending");
    }
}
