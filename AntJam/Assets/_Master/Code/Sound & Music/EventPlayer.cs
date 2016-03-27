using UnityEngine;
using System.Collections;
//Gör så att vi kommer åt FMODUnity-relaterad kod.
using FMODUnity;

//Detta script är tänkt att ersätta StudioEventEmitter. Anledningen är för att ni ska slippa rota in i deras inbyggda
//scripts för mycket. Se info ovanför varje funktion. Det som inte har kommentarer är inget ni behöver använda.

	public class EventPlayer : MonoBehaviour
	{
//I denna string som exponeras i Inspector skriver ni in sökvägen för det event eller snapshot ni vill använda t.ex.
//event:/Ambiance/MittEvent
		public string eventName;
		
//Denna bool som är exponerad i Inspector avgör om eventet eller snapshotet ska starta automatiskt när scriptet startas.
		public bool startOnAwake = true;
		private Rigidbody cachedRigidBody;

		
		[FMODUnity.EventRef]
		FMOD.Studio.EventInstance eventToPlay;
		FMOD.Studio.ParameterInstance paramInstance;
		//FMOD.Studio.CueInstance cueInstance;

		void Start()
		{
			cachedRigidBody = GetComponent<Rigidbody>();
			eventToPlay = FMODUnity.RuntimeManager.CreateInstance (eventName);
			if(startOnAwake)
			{
				eventToPlay.start ();
			}
		}

		void Update()
		{
//Denna rad kod gör så att 3D-ljud fungerar som de ska. Kan vara bra att veta om man vill skriva något liknande själv.
//Kräver using FMODUnity; högst upp.
			if (eventToPlay != null)
			{
				eventToPlay.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
			}
		}
		
//Genom att skapa en referens till det här scriptet i ett annat script på följande sätt:
//public EventPlayer exampleNamne;
//så kan ni köra nedanstående funktioner från andra scripts.
//Fördelen med detta är att ni kan återanvända det här scriptet till så många events ni vill på olika GameObjects
//utan att behöva ändra något i detta scriptet.


//Denna funktion startar det event eller snapshot som ni angivit i string eventName.
//För att köra funktionen från ett annat script skriv:
//exampleName.PlayEvent();
		public void PlayEvent()
		{
			eventToPlay.start ();
		}
		
//Stoppar eventet eller snapshoten, antingen direkt(IMMIDIATE) eller med modulation(ALLOWFADEOUT).
//För att köra funktionen från ett annat script skriv:
//exampleName.StopEvent(true); eller exampleName.StopEvent(false);
		public void StopEvent(bool instant)
		{
			if (instant == true)
			{
				eventToPlay.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			}
			else 
			{
				eventToPlay.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			}
		}

    public bool UpdateEventToPlay(string eventName)
    {
        StopEvent(true);
        eventToPlay = FMODUnity.RuntimeManager.CreateInstance(eventName);
        return (eventToPlay!= null);
    }

//Denna funktion sätter valfri parameter i eventet till ett valfritt värde.
//För att köra funktionen från ett annat script skriv:
//exampleName.ChangeParameter("NamnetPåDinParameter", 1.0f);
//1.0f är såklart bara ett exempel-värde.
		public void ChangeParameter(string name, float value)
		{
			eventToPlay.getParameter (name, out paramInstance);
			paramInstance.setValue (value);
		}

    public float GetParamValue(string name)
    {
        eventToPlay.getParameter(name, out paramInstance);
        float val;
        paramInstance.getValue(out val);
        return val;
    }
	}




