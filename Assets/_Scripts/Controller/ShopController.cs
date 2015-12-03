using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


public class ShopController : MonoBehaviour {
    
    public Transform Item;

    private Vector3 fingerStart;
    private Vector3 fingerEnd;
    private Vector3 currentPosition;
    private string _choose;
    private int countSwipe;
    private int maxSwipe;

    
    void Start()
    {
        countSwipe = 0;
        maxSwipe = Item.childCount - 1;
        SoundController.Inst.PlayGameBackGround();
    }

    void Update()
    {

        #region Bắt sự kiện vuốt màn hình sang trái hoặc sang phải để di chuyển các item trong shop
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerEnd = touch.position;
                    fingerStart = touch.position;
                    currentPosition = Item.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchDeltaPosition = touch.deltaPosition;
                    Item.position = new Vector3(Item.position.x + touchDeltaPosition.x * Time.deltaTime, Item.position.y, Item.position.z);
                    fingerEnd = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (Item.position.x > 0)
                    {
                        Item.DOMoveX(0, 0.3f);
                    }
                    else if (Item.position.x < -maxSwipe * 5)
                    {
                        Item.DOMoveX(-maxSwipe * 5, 0.3f);
                    }
                    else
                    {
                        if (Mathf.Abs(fingerEnd.x - fingerStart.x) > Mathf.Abs(fingerEnd.y - fingerStart.y))
                        {
                            //swipe left
                            if (fingerEnd.x < fingerStart.x)
                            {
                                //do some thing
                                Item.DOMoveX(currentPosition.x - 5, 0.3f);
                                countSwipe--;
                            }
                            //swipe right
                            else if (fingerEnd.x > fingerStart.x)
                            {
                                // do some thing
                                Item.DOMoveX(currentPosition.x + 5, 0.3f);
                                countSwipe++;
                            }
                            currentPosition = Vector3.zero;
                        }
                    }
                }
            }
        }
        #endregion


    }

    public void ChoosePlayer()
    {
        ////switch (countSwipe)
        ////{
        ////    case 0:
        ////        _choose = BallName.Ball_Cube;
        ////        break;
        ////    case -1:
        ////        _choose = BallName.Ball_Capsule;
        ////        break;
        ////    case -2:
        ////        _choose = BallName.Ball_Sphere;
        ////        break;
        ////    case -3:
        ////        _choose = BallName.Ball_Cylinder;
        ////        break;
        ////}
        ////PlayerPrefs.SetString(Config.Ball, _choose);
        //LoadPlayScene();

        
    }

    public void LoadPlayScene()
    {
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
