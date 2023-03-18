using UnityEngine;
    
public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                T[] allInstances = Resources.LoadAll<T>("");
                if (allInstances == null || allInstances.Length != 1) {
                    throw new System.Exception(typeof(T).Name + " has more than one instance");
                }
                _instance = allInstances[0];
            }
            return _instance;
        }
    }
}
