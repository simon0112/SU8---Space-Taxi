using System;
using System.IO;
using System.Reflection;
using DIKUArcade.Entities;
using SpaceTaxi_1.Enums;
using DIKUArcade.Math;

namespace SpaceTaxi_1.Utilities {
    public class Physics {
        private Vec2F Gravity = new Vec2F(0f, -0.00001f);

        ///<summary> Affects an entity with different physics depending on which way the entity is set to move </summary>
        ///<var name="ent"> The entity that is to be affected by the physics</var>
        ///<var name="moveDir"> The direction the entity should move</var>
        ///<returns> void </returns>
        public void PhysicsEffect(Entity ent, MoveDir moveDir) {
            switch (moveDir) {
                case MoveDir.None:
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (Gravity)*1);
                    }
                    break;
                case MoveDir.Left:
                    if (ent.Shape.AsDynamicShape().Direction.X >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction 
                        + (new Vec2F(-0.00005f, 0.0000f))*1);
                    }
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction 
                        + (Gravity)*1);
                    }
                    break;
                case MoveDir.Right:
                    if (ent.Shape.AsDynamicShape().Direction.X <= 0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction 
                        + (new Vec2F(0.00005f, 0.0000f))*1);
                    }
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (Gravity)*1);
                    }
                    break;
                case MoveDir.Up:
                    if (ent.Shape.AsDynamicShape().Direction.Y <= 0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (new Vec2F(0.0000f, 0.00005f))*1);
                    }
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (Gravity)*1);
                    }
                    break;
                case MoveDir.LeftUp:
                    if (ent.Shape.AsDynamicShape().Direction.X >= -0.005f || ent.Shape.AsDynamicShape().Direction.Y <= 0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (new Vec2F(-0.000070711f, 0.000070711f))*1);
                    }
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (Gravity)*1);
                    }
                    break;
                case MoveDir.RightUp:
                    if (ent.Shape.AsDynamicShape().Direction.X <= 0.005f || ent.Shape.AsDynamicShape().Direction.Y <= 0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction
                        + (new Vec2F(0.000070711f, 0.000070711f))*1);
                    }
                    if (ent.Shape.AsDynamicShape().Direction.Y >= -0.005f) {
                        ent.Shape.AsDynamicShape().ChangeDirection(ent.Shape.AsDynamicShape().Direction + (Gravity)*1);
                    }
                    break;
                case MoveDir.Crashed:
                    break;
            }
            ent.Shape.Move();
        }
    }
}
