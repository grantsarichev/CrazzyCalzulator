using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;

public class CalculationModule
{
   private readonly string _invalidPattern = @"[a-zA-Zа-яА-Я-)(*&^%$#!@]";
   private readonly string _validPattern = @"\d+\+\d+=";
   public UniTask<int> Calculate(string expression)
   {
      AnalysisExpression(expression);
      return ResultCalculation(expression);
   }

   private UniTask<int> ResultCalculation(string expression)
   {
      var operandsPattern = @"\d+";
      var matches = Regex.Matches(expression, operandsPattern);
      var operand1 = int.Parse(matches[0].Value);
      var operand2 = int.Parse(matches[1].Value);
      
      return UniTask.FromResult(operand1+operand2);
   }
   private void AnalysisExpression(string expression)
   {
      LinguisticAnalysis(expression);
   }

   private void LinguisticAnalysis(string expression)
   {
      if (Regex.IsMatch(expression, _invalidPattern) || !Regex.IsMatch(expression, _validPattern))
      { 
         throw new InvalidExpressionException();
      }
   }
}
