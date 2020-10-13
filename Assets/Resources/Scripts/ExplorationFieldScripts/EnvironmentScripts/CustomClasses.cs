//this scrript holds custom game classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CustomClasses
{
		[System.Serializable]
		public class SpellRef {
			public string name { get; set; }
			public bool equipped { get; set; }

		}
		[System.Serializable]
		public class ItemRef {
			public string coords { get; set; }
			public bool used { get; set; }

		}
			[System.Serializable]
			public class enemy {
			public string name { get; set; }
			public int spawnRate { get; set; }

		}

        [System.Serializable]
			public class Quest {
            public  string QuestProctor { get; set; }
			public  string QuestName { get; set; }
            public  string QuestReward { get; set; }
            public  string QuestDesc { get; set; }
            public  string QuestProgress { get; set; }
            public string QuestText { get; set; }
            public string QuestTextRepeat { get; set; }
            public string QuestCompleteText { get; set; }
            
             public List<string> QuestTalkToText{ get; set; }
			public  int QuestPayment { get; set; }
            
          
            public List<string> QuestTalkTo{ get; set; }
            public List<bool> QuestTalkToProgress{ get; set; }
          
            public List<string> QuestDefeat{ get; set; }
            public List<int> QuestDefeatAmount{ get; set; }
            public List<int> QuestDefeatProgress{ get; set; }

            public bool CompletedTasks{ get; set; }
            public bool Completed{ get; set; }
            

		}

		[System.Serializable]
		public struct SerializableVector3
 {
     /// <summary>
     /// x component
     /// </summary>
     public float x;
     
     /// <summary>
     /// y component
     /// </summary>
     public float y;
     
     /// <summary>
     /// z component
     /// </summary>
     public float z;
     
     /// <summary>
     /// Constructor
     /// </summary>
     /// <param name="rX"></param>
     /// <param name="rY"></param>
     /// <param name="rZ"></param>
     public SerializableVector3(float rX, float rY, float rZ)
     {
         x = rX;
         y = rY;
         z = rZ;
     }
     
     /// <summary>
     /// Returns a string representation of the object
     /// </summary>
     /// <returns></returns>
     public override string ToString()
     {
         return string.Format("[{0}, {1}, {2}]", x, y, z);
     }
     
     /// <summary>
     /// Automatic conversion from SerializableVector3 to Vector3
     /// </summary>
     /// <param name="rValue"></param>
     /// <returns></returns>
     public static implicit operator Vector3(SerializableVector3 rValue)
     {
         return new Vector3(rValue.x, rValue.y, rValue.z);
     }
     
     /// <summary>
     /// Automatic conversion from Vector3 to SerializableVector3
     /// </summary>
     /// <param name="rValue"></param>
     /// <returns></returns>
     public static implicit operator SerializableVector3(Vector3 rValue)
     {
         return new SerializableVector3(rValue.x, rValue.y, rValue.z);
     }
 }
}

