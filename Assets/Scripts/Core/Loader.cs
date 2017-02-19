using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    public string scnTo_load="scn_gameplay";
    IEnumerator loadtoplay()
    {

        Debug.LogError("Inicia la carga");
        if (!SceneManager.GetSceneByName(scnTo_load).IsValid())
        {
            var a=SceneManager.LoadSceneAsync(scnTo_load);
            //a.allowSceneActivation=false;
            while (!a.isDone)
            {
                
                yield return new WaitForEndOfFrame();
            }
            SceneManager.SetActiveScene( SceneManager.GetSceneByName( scnTo_load) );



        }

        RootHud.GetRoot.Click_FromLoad_toPlay();
        
        yield return 0;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
