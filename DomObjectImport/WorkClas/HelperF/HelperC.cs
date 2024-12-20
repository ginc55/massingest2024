using DomObjectImport.WorkClas.HelperObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    class HelperC
    {
        public static string TrimNumbers(string s)
        {
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            s = s.Trim();
            var result = s.TrimEnd(digits);
            result = result.Trim();

            var digits1 = new[] { '_', ' ' };

            result = result.TrimEnd(digits1);

            return result;
        }

        public static void XmlElementLinks(string ListItem, string elementName, List<string> lists, string kolona)
        {
            if (HelperC.TrimNumbers(ListItem.Trim()) == elementName)//link
            {
                if (!string.IsNullOrWhiteSpace(kolona))
                {
                    lists.Add(kolona);
                }

            }

        }
        public static void ColectAllLanguageFieldValues(string ListItem, string elementName, List<string> resultlists, string cellValue)
        {
            string temp = "_language";

            if (string.Equals(HelperC.TrimNumbers(ListItem), elementName + temp, StringComparison.OrdinalIgnoreCase))
            {
                resultlists.Add(cellValue.ToString());
            }

        }

        public static int GetIndexOfListItem(string ListItem, string elementName)
        {
            int index = 0;
            if (string.Equals(HelperC.TrimNumbers(ListItem), elementName, StringComparison.OrdinalIgnoreCase))
            {
                index++;
            }
            return index;
        }

        public static LanguageListO GettingCorectLanguageList(string elementName, List<string> sourceLanguageList, List<string> scientificCommentLanguageList, List<string> descriptionLanguageList,
            List<string> tableOfContentsLanguageList, List<string> abstractLanguageList, List<string> alternativeTitleLanguageList, List<string> formatExtentLanguageList, IndexObjectOfLanguageList tempIndexListO)
        {
            LanguageListO tempO = new LanguageListO();
            tempO.tempLanguageList = new List<string>();
            tempO.indexInList = 0;

            List<string> resultTempList = new List<string>();

            if (string.Equals(HelperC.TrimNumbers(elementName), "source", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = sourceLanguageList;
                tempO.indexInList = tempIndexListO.sourceInt;

            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "scientificComment", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = scientificCommentLanguageList;
                tempO.indexInList = tempIndexListO.scientificCommentInt;
            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "description", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = descriptionLanguageList;
                tempO.indexInList = tempIndexListO.descriptionInt;
            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "tableOfContents", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = tableOfContentsLanguageList;
                tempO.indexInList = tempIndexListO.tableOfContentsInt;
            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "abstract", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = abstractLanguageList;
                tempO.indexInList = tempIndexListO.abstractInt;
            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "alternativeTitle", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = alternativeTitleLanguageList;
                tempO.indexInList = tempIndexListO.alternativeTitleInt;
            }
            else if (string.Equals(HelperC.TrimNumbers(elementName), "formatExtent", StringComparison.OrdinalIgnoreCase))
            {
                tempO.tempLanguageList = formatExtentLanguageList;
                tempO.indexInList = tempIndexListO.formatExtentInt;
            }

            return tempO;
        }

    }
}
