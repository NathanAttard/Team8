using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConvMenu", menuName = "Buy Ammo Conversation/Create Menu", order = 1)]
public class ConversationScriptableObject : ScriptableObject
{
    public List<Conversation> MenuConversation;

    [System.Serializable]
    public class Conversation
    {
        public string Title;
    }
    


}
