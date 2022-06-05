using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Utility
{
    public class QueryUtility
    {
        public static String[] keywords = { "<to>", "<eq>", "<gt>", "<lt>", "<ge>", "<le>" };
        public static String[] expression_seperator = { ":" };

        public static String GetQueryVariable(String query)
        {
            // since theres only one variable always, the left side of the leftover from the split is going to be it
            return query.Split(expression_seperator, StringSplitOptions.TrimEntries)[0];
        }

        public static List<String> GetQueryValues(String query)
        {
            // right side of : is the expression
            String expression = query.Split(new String[] { ">" }, StringSplitOptions.TrimEntries)[1];
            String[] values = expression.Split(new String[] { " " }, StringSplitOptions.TrimEntries);

            // within this now split by keywords for a MAXIMUM of 2 values and MINIMUM of 1
            return new List<String>(values);
        }

        public static String GetQueryOperations(String query)
        {
            // returns the operation being done
            String keyword = "";
            Regex re = new Regex(@"<.*>");
            MatchCollection regex_mathes = re.Matches(query);

            foreach (Match match in regex_mathes)
                keyword = match.Value;

            return keyword;
        }

        public static object GetInstancePropertyValue<T>(T instance, String propertyName)
        {
            // takes string name of property
            // gets its value
            // returns it cast to correct type, since get property returns object
            return typeof(T).GetProperty(propertyName).GetValue(instance);
        }

        public static Type GetInstancePropertyType<T>(String propertyName)
        {
            return typeof(T).GetProperty(propertyName).PropertyType;
        }

        public static List<T> DoQuery<T>(List<T> list, String query)
        {
            // unpack query
            String queryProperty = GetQueryVariable(query);
            List<String> queryValue = GetQueryValues(query);
            String queryOperation = GetQueryOperations(query);

            // get the type of query property
            Type propertyType = GetInstancePropertyType<T>(queryProperty);

            List<T> retList;
            switch (queryOperation)
            {
                case "<eq>":
                    retList = DoEquals<T>(list, propertyType, queryValue[0], queryProperty);
                    break;
                case "<gt>":
                    retList = DoGreater<T>(list, propertyType, queryValue[0], queryProperty);
                    break;
                case "<lt>":
                    retList = DoLess<T>(list, propertyType, queryValue[0], queryProperty);
                    break;
                case "<ge>":
                    retList = DoGreaterEqual<T>(list, propertyType, queryValue[0], queryProperty);
                    break;
                case "<le>":
                    retList = DoLessEqual<T>(list, propertyType, queryValue[0], queryProperty);
                    break;
                case "<to>":
                    retList = DoTO<T>(list, propertyType, queryValue, queryProperty);
                    break;
                default:
                    retList = list;
                    break;
            }
            

            return retList;
        }

        private static List<T> DoEquals<T>(List<T> list, Type propertyType, String queryValue, String queryProperty)
        {
            List<T> retList = new List<T>();
            var value = Convert.ChangeType(queryValue, propertyType);
            foreach (T item in list)
            {
                var propertyValue = Convert.ChangeType(GetInstancePropertyValue<T>(item, queryProperty), propertyType);
                int comparison = Comparer.DefaultInvariant.Compare(propertyValue, value);
                if (comparison == 0)
                    retList.Add(item);
            }

            return retList;
        }

        private static List<T> DoGreater<T>(List<T> list, Type propertyType, String queryValue, String queryProperty)
        {
            List<T> retList = new List<T>();
            var value = Convert.ChangeType(queryValue, propertyType);
            foreach (T item in list)
            {
                var propertyValue = Convert.ChangeType(GetInstancePropertyValue<T>(item, queryProperty), propertyType);
                int comparison = Comparer.DefaultInvariant.Compare(propertyValue, value);
                if (comparison > 0)
                    retList.Add(item);
            }

            return retList;
        }

        private static List<T> DoLess<T>(List<T> list, Type propertyType, String queryValue, String queryProperty)
        {
            List<T> retList = new List<T>();
            var value = Convert.ChangeType(queryValue, propertyType);
            foreach (T item in list)
            {
                var propertyValue = Convert.ChangeType(GetInstancePropertyValue<T>(item, queryProperty), propertyType);
                int comparison = Comparer.DefaultInvariant.Compare(propertyValue, value);
                if (comparison < 0)
                    retList.Add(item);
            }

            return retList;
        }

        private static List<T> DoGreaterEqual<T>(List<T> list, Type propertyType, String queryValue, String queryProperty)
        {
            List<T> retList = new List<T>();
            var value = Convert.ChangeType(queryValue, propertyType);
            foreach (T item in list)
            {
                var propertyValue = Convert.ChangeType(GetInstancePropertyValue<T>(item, queryProperty), propertyType);
                int comparison = Comparer.DefaultInvariant.Compare(propertyValue, value);
                if (comparison >= 0)
                    retList.Add(item);
            }

            return retList;
        }

        private static List<T> DoLessEqual<T>(List<T> list, Type propertyType, String queryValue, String queryProperty)
        {
            List<T> retList = new List<T>();
            var value = Convert.ChangeType(queryValue, propertyType);
            foreach (T item in list)
            {
                var propertyValue = Convert.ChangeType(GetInstancePropertyValue<T>(item, queryProperty), propertyType);
                int comparison = Comparer.DefaultInvariant.Compare(propertyValue, value);
                if (comparison <= 0)
                    retList.Add(item);
            }

            return retList;
        }

        private static List<T> DoTO<T>(List<T> list, Type propertyType, List<String> queryValues, String queryProperty)
        {
            List<T> retList = new List<T>(list);
            retList = DoGreaterEqual<T>(retList, propertyType, queryValues[0], queryProperty);
            retList = DoLessEqual<T>(retList, propertyType, queryValues[1], queryProperty);
            return retList;
        }
    }
}
