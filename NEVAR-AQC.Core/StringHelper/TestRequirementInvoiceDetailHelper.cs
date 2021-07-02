using NEVAR_AQC.Core.Entities;
using System;
using System.Collections.Generic;

namespace NEVAR_AQC.Core.StringHelper
{
    /// <summary>
    /// Handle for test requirement invoice detail
    /// </summary>
    public class TestRequirementInvoiceDetailHelper
    {
        /// <summary>
        /// Convert Char To List
        /// </summary>
        /// <param name="input">format: Name~Amount~TestTarget~TestMethod|</param>
        /// <returns>List<Invoice_TestRequirementDetailModel></returns>
        public static ICollection<IDTestRequirementEntity> ConvertCharToList(string input)
        {
            return null;
            //if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            //{
            //    return null;
            //}

            //var inputArray = input.Split('|');
            //var resultArray = new List<IDTestRequirementEntity>();
            //foreach (var itemArray in inputArray)
            //{
            //    if (string.IsNullOrEmpty(itemArray) || string.IsNullOrWhiteSpace(itemArray))
            //    {
            //        return resultArray;
            //    }
            //    var resultItem = new IDTestRequirementEntity();
            //    var itemDetail = itemArray.Split('~');
            //    resultItem.SpecimenNameOrSymbol = itemDetail[0].ToString();
            //    resultItem.Amount = Convert.ToInt32(itemDetail[1]);
            //    resultArray.Add(resultItem);
            //}
            //return resultArray;
        }
    }

    public class CalibrationRequirementInvoiceDetailHelper
    {
        /// <summary>
        /// Convert Char To List
        /// </summary>
        /// <param name="input">format: Name~Amount~TestTarget~TestMethod|</param>
        /// <returns>List<Invoice_TestRequirementDetailModel></returns>
        public static ICollection<IDCalibrationRequirementEntity> ConvertCharToList(string input)
        {
            return null;
            //if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            //{
            //    return null;
            //}

            //var inputArray = input.Split('|');
            //var resultArray = new List<Invoice_CalibrationRequirementDetailEntity>();
            //foreach (var itemArray in inputArray)
            //{
            //    if (string.IsNullOrEmpty(itemArray) || string.IsNullOrWhiteSpace(itemArray))
            //    {
            //        return resultArray;
            //    }
            //    var resultItem = new Invoice_CalibrationRequirementDetailEntity();
            //    var itemDetail = itemArray.Split('~');
            //    resultItem.SymbolSpecimenName = itemDetail[0].ToString();
            //    resultItem.Amount = Convert.ToInt32(itemDetail[1]);
            //    resultItem.TestTarget = itemDetail[2].ToString();
            //    resultItem.TestMethod = itemDetail[3].ToString();
            //    resultArray.Add(resultItem);
            //}
            //return resultArray;
        }
    }
}