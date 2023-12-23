using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
   public  List<A_Enemy> enemies = new List<A_Enemy>();
   public List<A_Enemy> awakeEnemies = new List<A_Enemy>();
   public GameObject spider;
   public float heatLevel = 2;
   public float heatLevelMax = 10;
   public float heatProgress = 0;
   public void Update()
   {
      if(awakeEnemies.Count < heatLevel)
      {
         if(heatLevel<heatLevelMax)
            heatLevel += heatProgress;
         awakeEnemies.Add(enemies[0]);
         enemies[0].idling = false;
         enemies.Remove(enemies[0]);
      }
   }
}