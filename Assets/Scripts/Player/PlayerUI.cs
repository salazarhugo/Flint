using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    #region Private Fields
    static public PlayerUI Instance;
    private PlayerStats target;

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText;

    [Tooltip("UI Text to display Player's Ammunition")]
    [SerializeField]
    private Text ammunitionText;

    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Image playerHealthBar;


    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Text killcount;

    public int kills = 0;
    #endregion


    #region MonoBehaviour Callbacks

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Player UI(Clone)").GetComponent<Transform>(), false);
    }
    private void Start()
    {
        Instance = this;
    }
    public void PlusOneKill()
    {
        kills += 1;
        killcount.text = kills.ToString();
    }
    void Update()
    {
        // Reflect the Player Health
        if (playerHealthBar != null)
        {
            DisplayHealthStats(target.healthScript.health);
        }

        // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
   
    #endregion


    #region Public Methods

    public void SetTarget(PlayerStats _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100;
        playerHealthBar.fillAmount = healthValue;
    }

    #endregion

}
