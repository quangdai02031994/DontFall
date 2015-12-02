﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class IntroControl : MonoBehaviour {

    public Sprite _notMute;
    public Sprite _mute;

    public Button sound;
    public Text Title;
    
    public GameObject thandongho;
    public GameObject kimdai;
    public GameObject kimngan;

    public Color startColor = Color.black;
    public Color endColor = Color.black;

    public float timeToRotate;
    public int soundState;
    public string _volume;

    private float t = 0;
    private float i = 0;

	void Start () {

        Time.timeScale = 1;
        endColor = new Color(Random.value, Random.value, Random.value);
        if (PlayerPrefs.GetString(Config.Volume) == Volume.Mute)
        {
            soundState = 0;
            sound.GetComponent<Button>().image.overrideSprite = _mute;
        }
        else
        {
            soundState = 1;
            sound.GetComponent<Button>().image.overrideSprite = _notMute;
        }
        SoundController.Inst.PlayGameBackGround();
	}

	
	void Update () {


        _volume = PlayerPrefs.GetString(Config.Volume);

        t -= Time.deltaTime;
        i += Time.deltaTime;

        if (i >= 3)
        {
            i = 0;
            startColor = endColor;
            endColor = new Color(Random.value, Random.value, Random.value);
        }
        else
        {
            Title.color = Color.Lerp(startColor, endColor, i);
        }

        if (SoundController.Inst._isMute)
        {
            sound.GetComponent<Button>().image.overrideSprite = _mute;
        }
        else
        {
            sound.GetComponent<Button>().image.overrideSprite = _notMute;
        }
	}


    void LateUpdate()
    {
        kimdai.transform.localEulerAngles = new Vector3(0, 0, t * timeToRotate * 10);
        kimngan.transform.localEulerAngles = new Vector3(0, 0, t * timeToRotate);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        Title.color = Color.black;
        Application.LoadLevel(SceneName.Level1);
        //StartCoroutine(LoadScene(SceneName.Level1));
    }


    public void GoShop()
    {
        Title.color = Color.black;
        Application.LoadLevel(SceneName.Shop);
        //StartCoroutine(LoadScene(SceneName.Shop));
    }


}
