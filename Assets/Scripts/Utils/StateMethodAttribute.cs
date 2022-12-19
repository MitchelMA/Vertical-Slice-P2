using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class StateMethodAttribute : Attribute
{
   public int State { get; }

   public StateMethodAttribute(int state)
   {
      State = state;
   }
}