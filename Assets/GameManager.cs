using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    enum State { Ready, Play, Die, KeyBlock }

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI stepText;

    State state = State.Ready;

    public int CurStage
    {
        get => _curStage;
        set
        {
            stageText.SetText("stage: " + value.ToString());
            _curStage = value;
        }
    }
    private int _curStage = 0;

    public Transform Bar;
    public DotSpawn dotspawner;
    public GameObject Plate;
    public GameObject lockSteel;

    public float AngleMargin = 10f;

    private Bar Bar_script;


    private int StepLeft
    {
        get => _stepLeft;
        set
        {
            stepText.SetText(value.ToString());
            _stepLeft = value;
        }
    }
    private int _stepLeft = 0;

    int[] step_num = { 3, 3, 4, 4, 5, 5, 6, 6, 7, 8, 9, 9, 10 }; // stageº° ½ºÅÜ¼ö


    private static GameManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }

    }

    private void Start()
    {

        StepLeft = step_num[CurStage];
        Bar_script = Bar.GetComponent<Bar>();

        dotspawner.MakeDot();

        state = State.Ready;

    }
    private void Update()
    {
        var dotAngle = dotspawner.GetCurDotAngle();
        var diff = Mathf.DeltaAngle(Bar.transform.rotation.eulerAngles.z, dotAngle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (state)
            {
                case State.KeyBlock:
                    break;
                case State.Ready:

                    Bar_script.isPlaying = true;
                    state = State.Play;

                    break;
                case State.Die:

                    StepLeft = step_num[CurStage];
                    Bar_script.ResetBar();

                    dotspawner.MakeDot();
                    state = State.Ready;

                    break;
                case State.Play:

                    var distance = Mathf.Abs(diff);
                    if (distance < AngleMargin)
                    {

                        SoundManager.Instance.PlayPointSound();
                        Bar_script.KeyPressed();
                        StepLeft--;
                        if (StepLeft <= 0)
                        {
                            state = State.KeyBlock;
                            StartCoroutine(LockOpenSuccess());
                        }
                        else
                            dotspawner.MakeDot();
                    }
                    else
                    {
                        state = State.KeyBlock;
                        StartCoroutine(Die());
                    }
                    break;
            }

        }

        if (state == State.Play)
        {
            if (
                (Bar_script.clockwise && diff > 20) ||
                (!Bar_script.clockwise && diff < -20)
                )
            {
                Debug.Log("³Ñ¾î¼¶. Die");

                state = State.KeyBlock;
                StartCoroutine(Die());
            }
        }
    }


    IEnumerator LockOpenSuccess()
    {
        Bar_script.isPlaying = false;

        yield return lockSteel.GetComponent<lockSteel>().Unlock();

        CurStage++;
        StepLeft = step_num[CurStage];

        Bar_script.ResetBar();
        dotspawner.MakeDot();

        state = State.Ready;
    }

    IEnumerator Die()
    {
        Bar_script.isPlaying = false;
        yield return Plate.GetComponent<ShakeObject>().ShakeCorout();

        state = State.Die;
    }

}
