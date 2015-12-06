using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


public class ShopController : MonoBehaviour {
    
    public Transform Item;

    public GameObject btn_Key;
    public Text txt_Key;
    public Text txt_Coin;

    public Sprite keyNotActive;
    public Sprite keyActive;


    public int _coinChespin;
    public int _coinChicken;
    public int _coinCow;
    public int _coinPatrick;
    public int _coinPenguin;
    public int _coinStrongeBob;

    private Vector3 _fingerStart;
    private Vector3 _fingerEnd;
    private Vector3 _currentPosition;
    
    private string _chooseItems;
    private int _currentItem;
    private int _countItems;

    void Start()
    {
        _currentItem = 0;
        _countItems = Item.childCount - 1;
        SoundController.Inst.PlayGameBackGround();
    }


    void Update()
    {
        txt_Coin.text = PlayerPrefs.GetInt(Config.Coin).ToString();


        #region Bắt sự kiện vuốt màn hình sang trái hoặc sang phải để di chuyển các item trong shop
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerEnd = touch.position;
                    _fingerStart = touch.position;
                    _currentPosition = Item.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchDeltaPosition = touch.deltaPosition;
                    Item.position = new Vector3(Item.position.x + touchDeltaPosition.x * Time.deltaTime, Item.position.y, Item.position.z);
                    _fingerEnd = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (Item.position.x > 0)
                    {
                        Item.DOMoveX(0, 0.3f);
                    }
                    else if (Item.position.x < -_countItems * 5)
                    {
                        Item.DOMoveX(-_countItems * 5, 0.3f);
                    }
                    else
                    {
                        if (Mathf.Abs(_fingerEnd.x - _fingerStart.x) > Mathf.Abs(_fingerEnd.y - _fingerStart.y))
                        {
                            //swipe left
                            if (_fingerEnd.x < _fingerStart.x)
                            {
                                //do some thing
                                Item.DOMoveX(_currentPosition.x - 5, 0.3f);
                                _currentItem--;
                            }
                            //swipe right
                            else if (_fingerEnd.x > _fingerStart.x)
                            {
                                // do some thing
                                Item.DOMoveX(_currentPosition.x + 5, 0.3f);
                                _currentItem++;
                            }
                            _currentPosition = Vector3.zero;
                        }
                    }
                }
            }
        }
        
        #endregion

        #region kiem tra cac dieu kien cua cac item
        switch (_currentItem)
        {
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chespin) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinChespin.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinChespin);
                    }
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chicken) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinChicken.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinChicken);
                    }
                }
                break;
            case -3:
                {
                    if (PlayerPrefs.GetString(ItemActive.Cow) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinCow.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinCow);
                    }
                }
                break;
            case -4:
                {
                    if (PlayerPrefs.GetString(ItemActive.Patrick) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinPatrick.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinPatrick);
                    }
                }
                break;
            case -5:
                {
                    if (PlayerPrefs.GetString(ItemActive.Penguin) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinPenguin.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinPenguin);
                    }
                }
                break;
            case -6:
                {
                    if (PlayerPrefs.GetString(ItemActive.SpongeBob) == State.Active)
                    {
                        txt_Key.text = "";
                        btn_Key.SetActive(false);
                    }
                    else
                    {
                        txt_Key.text = _coinStrongeBob.ToString();
                        btn_Key.SetActive(true);
                        CompareCoin(_coinStrongeBob);
                    }
                }
                break;
            default:
                {
                    txt_Key.text = null;
                    btn_Key.SetActive(false);
                }
                break;
        }
        #endregion

    }


    private void CompareCoin(int t)
    {
        if (PlayerPrefs.GetInt(Config.Coin) >= t)
        {
            btn_Key.GetComponent<Button>().image.overrideSprite = keyActive;
            btn_Key.GetComponent<Button>().enabled = true;
        }
        else
        {
            btn_Key.GetComponent<Button>().image.overrideSprite = keyNotActive;
            btn_Key.GetComponent<Button>().enabled = false;
        }
    }

    public void ChoosePlayer()
    {
        switch (_currentItem)
        {
            case 0:
                {
                    _chooseItems = PlayerNames.BabyDuck;
                }
                break;
            case -1:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chespin) == State.Active)
                        _chooseItems = PlayerNames.Chespin;
                }
                break;
            case -2:
                {
                    if (PlayerPrefs.GetString(ItemActive.Chicken) == State.Active)
                        _chooseItems = PlayerNames.Chicken;
                }
                break;
            case -3:
                {
                    if (PlayerPrefs.GetString(ItemActive.Cow) == State.Active)
                        _chooseItems = PlayerNames.Cow;
                }
                break;
            case -4:
                {
                    if (PlayerPrefs.GetString(ItemActive.Patrick) == State.Active)
                        _chooseItems = PlayerNames.Patrick;
                }
                break;
            case -5:
                {
                    if (PlayerPrefs.GetString(ItemActive.Penguin) == State.Active)
                        _chooseItems = PlayerNames.Penguin;
                }
                break;
            case -6:
                {
                    if (PlayerPrefs.GetString(ItemActive.SpongeBob) == State.Active)
                        _chooseItems = PlayerNames.SpongeBob;
                }
                break;
            default:
                _chooseItems = PlayerNames.BabyDuck;
                break;
        }
        PlayerPrefs.SetString(Config.Player, _chooseItems);
        //Debug.Log(_chooseItems);
    }

    public void ActiveItem()
    {
        switch (_currentItem)
        {
            case -1:
                {
                    PlayerPrefs.SetString(ItemActive.Chespin, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinChespin;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -2:
                {
                    PlayerPrefs.SetString(ItemActive.Chicken, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinChicken;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -3:
                {
                    PlayerPrefs.SetString(ItemActive.Cow, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinCow;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -4:
                {
                    PlayerPrefs.SetString(ItemActive.Patrick, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinPatrick;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -5:
                {
                    PlayerPrefs.SetString(ItemActive.Penguin, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinPenguin;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
            case -6:
                {
                    PlayerPrefs.SetString(ItemActive.SpongeBob, State.Active);
                    int t = PlayerPrefs.GetInt(Config.Coin);
                    t = t - _coinStrongeBob;
                    PlayerPrefs.SetInt(Config.Coin, t);
                }
                break;
        }
    }


    public void LoadPlayScene()
    {
        ChoosePlayer();
        Application.LoadLevel(SceneName.Level1);
    }

    public void LoadIntroScene()
    {
        Application.LoadLevel(SceneName.Intro);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
