using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public struct FadeInOutCommand
{
    public PanelContainerHud panel_A;
    public PanelContainerHud panel_B;
    public PanelContainerHud panel_C;
    public string ReceiverMesage;
    public GameObject Receiver;
    public float Speed;

}
public class RootHud : PanelContainerHud
{
    public GameObject CamActive;
    public GameObject Loader;
    public PanelGame PGame;
    public PanelGameOver PGameOver;
    public PanelTitle PTitle;
    public PanelGameOver PPause;
    public PanelContainerHud PFade;
    
    public override void Awake()
    {
        base.Awake();
    }

    public static HUDElement GetPanel(string id)
    {
        
        
            return HUDManager.Instance.GetFirst(id);
        
    }
    public static RootHud GetRoot 
    {
        get
        {
            return HUDManager.Instance.GetFirst("scn_menu") as RootHud;
        }
    }
    public PanelContainerHud PanelArrow
    {
        get
        {
            return HUDManager.Instance.GetFirst(HUDElementType.PanelArrow) as PanelContainerHud;
        }
    }

    public  void Click_FromTitle_toPlay()
    {
        
        Debug.LogError("El primer texto...de click Event");
        var t = new FadeInOutCommand() { panel_A= PFade, panel_B= PTitle, panel_C= PGameOver, Receiver=Loader, ReceiverMesage="loadtoplay"  };
        //SendMessage("fadeInPA_Remove_PB", t);


        SendMessage("fadeInPA", t);
        
    }
    public void DoPause()
    {
        var t = new FadeInOutCommand() { panel_A = PPause, Speed=.3f};
        SendMessage("fadeInPA", t);
        t = new FadeInOutCommand() { panel_A = PGame, Speed = .3f };
        SendMessage("fadeOutPA", t);
    }


    public void ReturnToGame()
    {
        var t = new FadeInOutCommand() { panel_A = PPause, Speed = .3f };
        SendMessage("fadeOutPA", t);
        
        t = new FadeInOutCommand() { panel_A = PGame, Speed = .3f };
        SendMessage("fadeInPA", t);
    }


    public void RestartToGame()
    {
        var t = new FadeInOutCommand() { panel_A = PPause, Speed = .1f };
        SendMessage("fadeOutPA", t);
        
        t = new FadeInOutCommand() { panel_A = PGame, Speed = .1f };
        SendMessage("fadeInPA", t);
    }

    public void Click_FromLoad_toPlay()
    {

        Debug.LogError("Load to play");
        PTitle.gameObject.SetActive(false);
         PGame.gameObject.SetActive(true);
        var t = new FadeInOutCommand() { panel_A = PFade, panel_B = PTitle, panel_C = PGameOver};
        


        SendMessage("fadeOutPA", t);

    }


    IEnumerator fadeInPA(FadeInOutCommand fdncmd)
    {
        var pfrom = fdncmd.panel_A;
        pfrom.gameObject.SetActive(true);
        var myPanelfrom = pfrom.GetComponent<CanvasGroup>();
        var Speed = 2.0f;
        if (fdncmd.Speed > 0)
        {
            Speed = fdncmd.Speed;
        }
        myPanelfrom.alpha = 0;
        while (myPanelfrom.alpha < 1)
        {

            myPanelfrom.alpha += Time.deltaTime / Speed;
            yield return null;
        }
        
        if (fdncmd.Receiver != null && !string.IsNullOrEmpty(fdncmd.ReceiverMesage))
        {
            fdncmd.Receiver.SendMessage(fdncmd.ReceiverMesage);
        }

    }

    IEnumerator fadeOutPA(FadeInOutCommand fdncmd)
    {
        var pfrom = fdncmd.panel_A;
        pfrom.gameObject.SetActive(true);
        var myPanelfrom = pfrom.GetComponent<CanvasGroup>();
        var Speed = 2.0f;
        if (fdncmd.Speed > 0)
        {
            Speed = fdncmd.Speed;
        }

        myPanelfrom.alpha = 1;
        while (myPanelfrom.alpha > 0)
        {

            myPanelfrom.alpha -= Time.deltaTime / Speed;
            yield return null;
        }
        pfrom.gameObject.SetActive(false);
        if (fdncmd.Receiver != null && !string.IsNullOrEmpty(fdncmd.ReceiverMesage))
        {
            fdncmd.Receiver.SendMessage(fdncmd.ReceiverMesage);
        }

    }
    IEnumerator fadeInPA_Remove_PB(FadeInOutCommand fdncmd)
    {
        var pfrom = fdncmd.panel_A;
        pfrom.gameObject.SetActive(true);
        var myPanelfrom = pfrom.GetComponent<CanvasGroup>();
        var Speed = 2.0f;
        if (fdncmd.Speed > 0)
        {
            Speed = fdncmd.Speed;
        }

        myPanelfrom.alpha = 0;
        while (myPanelfrom.alpha < 1)
        {

            myPanelfrom.alpha += Time.deltaTime / Speed;
            yield return null;
        }

        yield return new WaitForSeconds(.2f);
        fdncmd.panel_B.gameObject.SetActive(false);
        fdncmd.panel_C.gameObject.SetActive(true);


        while (myPanelfrom.alpha > 0)
        {

            myPanelfrom.alpha -= Time.deltaTime / Speed;
            yield return null;
        }

        myPanelfrom.gameObject.SetActive(false);
        yield return 0;
    }


    /*
     * 
     * 
    public PanelContainerHud PanelPlayer
    {
        get
        {
            return HUDManager<string>.Instance.GetFirst(HUDElementType.PanelPlayer) as PanelContainerHud;
        }
    }
     public PanelCruz PanelPlayerCruz
    {
        get
        {
            return HUDManager.Instance.GetFirst(HUDElementType.PanelSecondary) as PanelCruz;
        }
    }
    */


    public  IEnumerator HideAllmsg()
    {
        yield return new WaitForSeconds(1) ;
        DisableAllViews();
        CamActive.GetComponent<Camera>().enabled = true;
        yield return 0;
    }
    // Use this for initialization
    void Start () {
        CamActive.GetComponent<Camera>().enabled = false;
        SendMessage("HideAllmsg");
        //**DisableAllViews();
    }



    public void DisableAllViews()
    {
        turnOnOffGUIOverWorld(false);
        turnOnOffGuiMapOverworldLayer(false);
    }
    public void EnablePlayerOverWorldView()
    {

        turnOnOffGUIOverWorld(true);
     //**   turnOnOffGuiMapOverworldLayer(true);
    }
    

    public void turnOnOffGUIOverWorld(bool toogle)
    {


        
       //*** PanelPlayer.gameObject.SetActive(toogle);
        

        
     //**   PanelFadeArea.gameObject.SetActive(false);

    }
    public void turnOnOffGuiMapOverworldLayer(bool toogle)
    {
        
        //**PanelArrow.gameObject.SetActive(toogle);

     //**   PanelFadeArea.gameObject.SetActive(false);
    }
    public void turnOnOffFadeLayer(bool toogle)
    {

     //***   PanelFadeArea.gameObject.SetActive(toogle);
        
    }

    public void turnOnOfflayout(int idx)
    {
        /*
        PanelMapPause.gameObject.SetActive(false);
        PanelPauseSave.gameObject.SetActive(false);
        PanelQuitGame.gameObject.SetActive(false);
        PanelGameOver.gameObject.SetActive(false);
        PanelItemChecker.gameObject.SetActive(false);
        PanelStatusProgress.gameObject.SetActive(false);
        PanelLevelUp.gameObject.SetActive(false);
        
        switch (idx)
        {
            case 0:
                PanelStatusProgress.gameObject.SetActive(true);
                break;
            case 1:
                PanelMapPause.gameObject.SetActive(true);
                break;
            case 2:
                PanelLevelUp.gameObject.SetActive(true);
                break;
            case 3:
                PanelItemChecker.gameObject.SetActive(true);
                break;
            case 4:
                PanelPauseSave.gameObject.SetActive(true);
                break;
            case 5:
                PanelQuitGame.gameObject.SetActive(true);
                break;
            case 6:
                var uipanel=PanelGameOver.GetComponent<UIPanel>();
                uipanel.alpha = 0;
                PanelGameOver.gameObject.SetActive(true);
                break;

        }
        */
    }

    


    public void turnOnOffPause(bool topause)
    {
        if (topause) { 
            DisableAllViews();
            /*
            //**PanelArrow.gameObject.SetActive(tooglearrows);
            PanelMapPause.gameObject.SetActive(true);
            PanelPauseSave.gameObject.SetActive(false);
            PanelQuitGame.gameObject.SetActive(false);
            PanelGameOver.gameObject.SetActive(false);
            PanelItemChecker.gameObject.SetActive(false);
            PanelStatusProgress.gameObject.SetActive(false);
            PanelLevelUp.gameObject.SetActive(false);*/
        }
        else
        {
            /*
            PanelMapPause.gameObject.SetActive(false);
            PanelPauseSave.gameObject.SetActive(false);
            PanelItemChecker.gameObject.SetActive(false);
            PanelStatusProgress.gameObject.SetActive(false);
            PanelLevelUp.gameObject.SetActive(false);
            PanelQuitGame.gameObject.SetActive(false);
            PanelGameOver.gameObject.SetActive(false);
            
             * EnablePlayerOverWorldView();
        
             * 
             * */
        }


    }




}
