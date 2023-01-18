using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class StateMethodAttribute : Attribute
{
   public int FromState { get; }

   public StateMethodAttribute(int fromState)
   {
      FromState = fromState;
   }
}