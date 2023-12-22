using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System;
using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MString
    {
        /// <summary>
        /// Return a list of result strings that taken from input string between the <b><i>separator</i></b> characters.
        /// </summary>
        public static List<string> RemoveChar(this string inputString, char separator)
        {
            List<string> result = inputString.Split(separator).ToList();
            result.RemoveAll(i => i == "");

            return result;
        }

        /// <summary>
        /// Return a list of all character after <b><i>checker</i></b> until the next <b><i>checker</i></b> or end of string.
        /// </summary>
        public static List<string> GetAfter(this string input, char checker)
        {
            List<string> list = new();
            string getter = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 1 && input[i] == checker && input[i + 1] != checker)
                {
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        if (input[j] != checker) getter += input[j];
                        else
                        {
                            list.Add(getter);
                            getter = "";
                        }
                    }
                    list.Add(getter);
                    break;
                }
            }

            List<string> nullList = list.Where(i => i == "").ToList();
            foreach (var item in nullList) list.Remove(item);
            return list;
        }

        /// <summary>
        /// Return a list removed all items that contain shorter string.
        /// </summary>
        public static List<string> JustChild(this List<string> list)
        {
            List<string> newList = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                foreach (var item in list)
                {
                    if (list[i].Contains(item) && list[i] != item) newList.Add(list[i]);
                }
            }

            foreach (var item in newList) list.Remove(item);

            return list.Distinct().ToList();
        }

        /// <summary>
        /// Return a list of all numeric string get from original list.
        /// </summary>
        public static List<int> GetNumbers(this List<string> list)
        {
            List<int> listOfNumbers = new();
            foreach (var item in list) if (item.All(Char.IsDigit)) listOfNumbers.Add(int.Parse(item));
            return listOfNumbers;
        }

        /// <summary>
        /// Return a new distinct string from input string.
        /// <br>For example: <i>Hello World => Helo Wrd</i></br>
        /// </summary>
        public static string DistinctString(this string input)
            => new string(input.Distinct().ToArray());

        /// <summary>
        /// Return index of line that contains the <b><i>target</i></b> string.
        /// </summary>
        public static int GetLineIndex(this List<string> arrayLine, string target)
        {
            foreach (string line in arrayLine)
            {
                if (line.Contains(target)) return arrayLine.IndexOf(line);
            }
            return 0;
        }

        /// <summary>
        /// Convert object to string with given format.
        /// <br> Eg: string blaStr = aPerson.ToString("My name is {FirstName} {LastName}.") </br>
        /// <br> Cre: <i> https://gist.github.com/omgwtfgames/f917ca28581761b8100f </i> </br>
        /// </summary>
        public static string ToString(this object thisObject, string format)
        {
            return ToString(thisObject, format, null);
        }
        public static string ToString(this object anObject, string aFormat, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            Type type = anObject.GetType();
            Regex reg = new Regex(@"({)([^}]+)(})", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(aFormat);
            int startIndex = 0;
            foreach (Match m in mc)
            {
                Group g = m.Groups[2]; // It's second in the match between { and }
                int length = g.Index - startIndex - 1;
                sb.Append(aFormat.Substring(startIndex, length));

                string toGet = string.Empty;
                string toFormat = string.Empty;
                int formatIndex = g.Value.IndexOf(":"); // Formatting would be to the right of a :
                if (formatIndex == -1) // No formatting, no worries
                {
                    toGet = g.Value;
                }
                else // Pickup the formatting
                {
                    toGet = g.Value.Substring(0, formatIndex);
                    toFormat = g.Value.Substring(formatIndex + 1);
                }

                // First try properties
                PropertyInfo retrievedProperty = type.GetProperty(toGet);
                Type retrievedType = null;
                object retrievedObject = null;
                if (retrievedProperty != null)
                {
                    retrievedType = retrievedProperty.PropertyType;
                    retrievedObject = retrievedProperty.GetValue(anObject, null);
                }
                else // Try fields
                {
                    FieldInfo retrievedField = type.GetField(toGet);
                    if (retrievedField != null)
                    {
                        retrievedType = retrievedField.FieldType;
                        retrievedObject = retrievedField.GetValue(anObject);
                    }
                }

                if (retrievedType != null) // Found something
                {
                    string result = string.Empty;
                    if (toFormat == string.Empty) // No format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, null) as string;
                    }
                    else // Format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, new object[] { toFormat, formatProvider }) as string;
                    }
                    sb.Append(result);
                }
                else // Didn't find a property with that name, so be gracious and put it back
                {
                    sb.Append("{");
                    sb.Append(g.Value);
                    sb.Append("}");
                }
                startIndex = g.Index + g.Length + 1;
            }
            if (startIndex < aFormat.Length) // Include the rest (end) of the string
            {
                sb.Append(aFormat.Substring(startIndex));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check if a string is null or equals to "".
        /// </summary>
        public static bool IsNull(this string input)
            => input == null || input == "" ? true : false;

        /// <summary>
        /// Insert a substring to original string by index.
        /// </summary>
        public static string Insert(this string input, int index, string insert)
            => input.Substring(0, index) + insert + input.Substring(index);

        /// <summary>
        /// Truncates a string to a specified length and appends an ellipsis "..." to the end of the string.
        /// </summary>
        public static string Truncate(this string input, int maxLength)
            => string.IsNullOrEmpty(input) ? input : input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...";

        /// <summary>
        /// Uppercase first character of all the words in a string.
        /// </summary>
        public static string ToTitleCase(this string value)
            => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()).Replace("'", "'");

        /// <summary>
        /// Reverse a string.
        /// </summary>
        public static string Reverse(this string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Compare a string with another string, can ignore letter case.
        /// </summary>
        public static bool CompareTo(this string string1, string string2, bool ignoreCase = true)
        {
            bool compare = false;
            if (ignoreCase) compare = string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase) == 0;
            else compare = string1 == string2;

            return compare;
        }

        /// <summary>
        /// Return true if input string contains one out of the strings in list.
        /// </summary>
        public static bool ContainsOneOf(this string input, List<string> contained)
        {
            foreach (var item in contained) if (input.Contains(item)) return true;
            return false;
        }

        /// <summary>
        /// Return true if the input string contains all elements in list.
        /// </summary>
        public static bool ContainsAll(this string input, List<string> contained)
        {
            foreach (var item in contained) if (!input.Contains(item)) return false;
            return true;
        }
    }

    public static class YMFormat
    {
        #region 💱 Unit Format
        public static string DecimalFormat(this int number, int digit) => number.ToString($"D{digit}");
        public static string FloatFormat(this float number, int digit) => number.ToString($"F{digit}");
        public static string HexadecimalFormat(this float number, int digit) => number.ToString($"X{digit}");
        public static string CommaSeparated(this int number) => number.ToString("#,#", CultureInfo.CurrentCulture);
        #endregion
        #region 🕓 Time Format
        /// <summary>
        /// Format the interger time into time format. <br></br><br></br>
        /// 
        /// <i>"00:00" | "colon"</i>: Time'll be like 59:01, 01:20:59, ect <br></br>
        /// <i>"hhmmss" | "letter"</i>: Time'll be like 59m01s, 01h20m59s, ect <br></br>
        /// </summary>
        public static string TimeFormat(this int time, string type)
        {
            switch (type)
            {
                case "00:00":
                case "00:00:00":
                case "00:00:00:00":
                case "Colon":
                case "colon":
                    return TimeFormatColon(time);
                case "hhmmss":
                case "ddhhmmss":
                case "Letter":
                case "letter":
                    return TimeFormatLetter(time);
            }
            return "";
        }
        public static string TimeFormatColon(int time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            if (time < 3600) return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            else if (time < 86400) return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            else return $"{timeSpan.Days:D2}:{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        public static string TimeFormatLetter(this float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            if (time < 60) return $"{timeSpan.Seconds:D2}s";
            else if (time < 3600) return $"{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
            else if (time < 86400) return $"{timeSpan.Hours:D2}h{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
            else return $"{timeSpan.Days:D2}d{timeSpan.Hours:D2}h{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
        }

        #endregion
    }

    public static class YMSpecificString
    {
        /// <summary>
        /// Convert all words in a text that match the input word into color format, ignore letter case.
        /// </summary>
        public static string ConvertToColorFormat2(this string text, string word, string hexColor)
        {
            string pattern = $@"\b{word}\b";
            return Regex.Replace(text, pattern, match =>
            {
                string matchedText = match.Value;
                string formattedText = $"<color={hexColor}><b>{matchedText}</b></color>";
                if (matchedText == word)
                {
                    return formattedText;
                }
                else if (matchedText.ToUpper() == word.ToUpper())
                {
                    return formattedText.ToUpper();
                }
                else if (matchedText.ToLower() == word.ToLower())
                {
                    return formattedText.ToLower();
                }
                else
                {
                    return match.Value;
                }
            }, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Convert all words in a text that match the input word into color format, ignore letter case. <br></br><br></br>
        /// Example: "This is an example".ConvertToColorFormat("example", "#FFFFFF") <br></br>
        /// </summary>
        public static string ConvertToColorFormat(this string text, string word, string hexColor)
        {
            string pattern = $@"\b{word}\b";
            return Regex.Replace(text, pattern, $"<color={hexColor}><b>$&</b></color>", RegexOptions.IgnoreCase);
        }

    }
}