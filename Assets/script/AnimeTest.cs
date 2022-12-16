using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimeTest : MonoBehaviour
{
    private Animator anim;
    [SerializeField] int frame;
    [SerializeField] InputField inputfield;
    [SerializeField] Text nowframe; //今の遷移フレーム
    [SerializeField] Text nowanime;
    [SerializeField] RuntimeAnimatorController RtEXS; //走り→スライディング(予備なし)
    [SerializeField] RuntimeAnimatorController RtMS;
    [SerializeField] RuntimeAnimatorController WtEXS;
    [SerializeField] RuntimeAnimatorController WtMS;
    [SerializeField] RuntimeAnimatorController RtEXJ; //歩き→ジャンプ(予備あり)
    [SerializeField] RuntimeAnimatorController RtMJ;
    [SerializeField] RuntimeAnimatorController WtEXJ;
    [SerializeField] RuntimeAnimatorController WtMJ;

    void Start()
    {
        Application.targetFrameRate = 60; 
        anim = this.GetComponent<Animator>();
        anim.runtimeAnimatorController = RtEXS; //コントローラーを走りスライディングに
        nowanime.text = "走り→スライディング(予備なし)";

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        SetFrameText(30);
        inputfield.text = "30";
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        transition.duration = 0.5f;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeAnime();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            int frame_num = int.Parse(inputfield.text);
            if(frame_num <= 5){
                frame_num = 1;
            }
            else{
                frame_num -= 5;
            }
            float time = (float)frame_num/60f;
            inputfield.text = frame_num.ToString();
            SetFrameText(frame_num);
            foreach(var layer in ac.layers){
                foreach(var curState in layer.stateMachine.states){
                    foreach(var transition in curState.state.transitions){
                        if(transition.name.Equals("ChangeState")){
                            transition.duration = time;
                        }
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            int frame_num = int.Parse(inputfield.text);
            if(frame_num >= 55){
                frame_num = 60;
            }
            else if(frame_num == 1){
                frame_num = 5;
            }
            else{
                frame_num+=5;
            }
            float time = (float)frame_num/60f;
            inputfield.text = frame_num.ToString();
            SetFrameText(frame_num);
            foreach(var layer in ac.layers){
                foreach(var curState in layer.stateMachine.states){
                    foreach(var transition in curState.state.transitions){
                        if(transition.name.Equals("ChangeState")){
                            transition.duration = time;
                        }
                    }
                }
            }
        }
    }

    //後で名前変える
    //遷移させる関数
    void ChangeAnime()
    {
        anim.SetTrigger("isChange");
    }

    //入力値を遷移フレームにする関数
    public void SetTransitionDurationForFrame()
    {
        
        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

        int frame_num = int.Parse(inputfield.text);
        if(frame_num > 60){
            frame_num = 60;
            inputfield.text = frame_num.ToString();
        }else if(frame_num < 1){
            frame_num = 1;
            inputfield.text = frame_num.ToString();
        }
        float time = (float)frame_num/60f;
        SetFrameText(frame_num);
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        transition.duration = time;
                    }
                }
            }
        }
    }

    void SetFrameText(int frame)
    {
        nowframe.text = "現在は" + frame + "F";
    }

    public void Cliptest()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        for (int i=0; i < clipInfo.Length; i++)
        {
            string clipName = clipInfo[i].clip.name;
            Debug.Log(clipName + ":" + i);
        }
    }

    //ここからアニメーション変更関数
    public void RuntoEXSlide()
    {
        nowanime.text = "走り→スライディング(予備なし)";
        anim.runtimeAnimatorController = RtEXS; //走りスライディングに変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void RuntoMildSlide()
    {
        nowanime.text = "走り→スライディング(予備あり)";
         anim.runtimeAnimatorController = RtMS; //走りスライディング(予備動作)に変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void WalktoEXSlide()
    {
        nowanime.text = "歩き→スライディング(予備なし)";
         anim.runtimeAnimatorController = WtEXS; //歩きスライディングに変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        SetFrameText((int)(transition.duration * 60));
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void WalktoMildSlide()
    {
        nowanime.text = "歩き→スライディング(予備あり)";
         anim.runtimeAnimatorController = WtMS; //歩きスライディング(予備動作)に変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void RuntoEXJump()
    {
        nowanime.text = "走り→ジャンプ(予備なし)";
         anim.runtimeAnimatorController = RtEXJ; //走りジャンプに変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void RuntoMildJump()
    {
        nowanime.text = "走り→ジャンプ(予備あり)";
         anim.runtimeAnimatorController = RtMJ; //走りジャンプ(予備動作)に変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void WalktoEXJump()
    {
        nowanime.text = "歩き→ジャンプ(予備なし)";
         anim.runtimeAnimatorController = WtEXJ; //歩きジャンプに変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }

    public void WalktoMildJump()
    {
        nowanime.text = "歩き→ジャンプ(予備あり)";
        anim.runtimeAnimatorController = WtMJ; //歩きジャンプ(予備動作)に変更

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach(var layer in ac.layers){
            foreach(var curState in layer.stateMachine.states){
                foreach(var transition in curState.state.transitions){
                    if(transition.name.Equals("ChangeState")){
                        int num = (int)(transition.duration * 60);
                        SetFrameText(num);
                        inputfield.text = num.ToString();
                    }
                }
            }
        }
    }
}
