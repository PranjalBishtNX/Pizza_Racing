using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_Button : MonoBehaviour
{

    bool active = true;
   // [SerializeField] Animator Cam_Control;
    [SerializeField] Animator Canva_Main;
    [SerializeField] Animator Main_Menu_BG;
    [SerializeField] Animator Canva_World;
    [SerializeField] Animator World_Select;


    public void PlayAnimation()
    {
        //Cam_Control.Play("Reverse");
        Canva_Main.Play("Reverse");
        Main_Menu_BG.Play("Reverse");
        Canva_World.Play("Reverse");
        World_Select.Play("Reverse");
    }


}
