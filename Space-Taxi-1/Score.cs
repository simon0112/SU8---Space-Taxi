using DIKUArcade;
using DIKUArcade.Timers;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using System.IO;
using System;
using System.Collections.Generic;
using DIKUArcade.Physics;

public class Score
{
   private int score;
   private Text display;
   public Score(Vec2F position, Vec2F extent) 
   {
      score = 0;
      display = new Text(score.ToString(), position, extent);
   }

   ///<summary> Adds points to the score-board</summary>
   ///<var name="PointWorth"> The amount of points that are to be added to the score</var>
   ///<returns> void </returns>
   public void AddPoint(int PointWorth)
   {
      score += PointWorth;
   }
   public void RenderScore() 
   {
      display.SetText(string.Format("Score: {0}", score.ToString()));
      display.SetColor(new Vec3I(255, 0, 0));
      display.RenderText();
   }
}