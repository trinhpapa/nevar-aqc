namespace NEVAR_AQC.Core.StringHelper
{
   public static class NumberToStringHelper
   {
      public static string ToNumberString(this int value)
      {
         var result = value < 10 ? "0" + value.ToString() : value.ToString();
         return result;
      }
   }
}