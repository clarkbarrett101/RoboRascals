using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
   public static List<A_Enemy> enemies = new List<A_Enemy>();
   int level = 1;
   public GameObject spider;
   public void Update()
   {
      if(enemies.Count == 0)
      {
         level++;
         for (int i = 0; i < level; i++)
         {
            Instantiate(spider, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
         }
      }
   }
}