using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveSystem ss = new SaveSystem();
        TestData td1 = new TestData(1, 1, "test", 0.5f);
        TestData td2 = new TestData(1, 2, "test2", 3.5f);
        TestData2 td3 = new TestData2(1, "test3", 0.2f);
        /*
        TestData2 td4 = new TestData2(2, "Totaal Niet Aangekwatst", 0.1f);
        TestData2 td5 = new TestData2(3, "Totaal Niet Aangekwatst", 0.1f);
        TestData2 td6 = new TestData2(4, "Totaal Niet Aangekwatst", 0.1f);
        TestData2 td7 = new TestData2(5, "Totaal Niet Aangekwatst", 0.1f);*/
        ss.Add(td1);
        ss.Add(td2);
        ss.Add(td3);
        /*
        ss.Add(td4);
        ss.Add(td5);
        ss.Add(td6);
        ss.Add(td7);*/
        ss.Save(); //Saves all objects in the savesystem to a binary object.
        ss.Clear(); //Clears the objects in the savesystem.
        ss.Load(); //
        TestData2 l1 = ss.GetObject<TestData2>();
        l1.Test1 = 1337;
        l1.Test3 = "Replaced";
        //l1 = new TestData2(1, "test", 4);
        ss.Replace(l1);
        TestData2 r1 = ss.GetObject<TestData2>();
        List<TestData> l2 = ss.GetObjects<TestData>();
        List<object> objects = ss.GetObjects();
        ss.RemoveAll<TestData>();
        List<object> objects2 = ss.GetObjects();

        Debug.Log("Did it works?");

        /*
        SaveSystem saveSystem = new SaveSystem(); //Verkrijgen instantie van het save systeem.
        saveSystem.Clear(); //Legen van de buffer in saveSystem. (Leegt dus alle objecten, ze bestaan nog wel in de file vanwege het opslaan.)
        TestData td = new TestData(1, 1, "test", 0.5f);
        saveSystem.Add(td); //Een object toevoegen.
        saveSystem.Save("Jou file naam"); //Jou object wordt opgeslagen naar een file met de door jou gegeven naam.
        saveSystem.Clear(); //SaveSystem wordt geleegd dus er zit niks meer in.
        saveSystem.Load("Jou file naam"); //Nu wordt het bestand wat je hebt opgeslagen ingeladen mits je de juiste filename geeft.
        TestData Loaded = saveSystem.GetObject<TestData>(); //Verkrijg het bestand wat je had opgeslagen.
        */
    }

    // Update is called once per frame
    void Update () {
		
	}
}
