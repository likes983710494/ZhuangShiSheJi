using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//只适用有按钮组件 ，且有AudioSource组件的物体
public class ButtonAudioClick : MonoBehaviour
{
    private Button Button_;
    private AudioSource audio_;
    void Start()
    {
        Button_ = GetComponent<Button>();
        audio_ = GetComponent<AudioSource>();
        Button_.onClick.AddListener(ButtonAudioClick_);
    }
    public void ButtonAudioClick_()
    {
        audio_.Play();
    }



}
