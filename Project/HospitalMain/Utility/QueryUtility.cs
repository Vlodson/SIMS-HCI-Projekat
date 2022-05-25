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
        public static String[] keywords = { "<to>", "<eq>", "<gt>", "<lt>" };
        public static String[] expression_seperator = { ":" };

        public static String GetQueryVariable(String query)
        {
            // since theres only one variable always, the left side of the leftover from the split is going to be it
            return query.Split(expression_seperator, StringSplitOptions.TrimEntries)[0];
        }

        public static List<String> GetQueryValues(String query)
        {
            // right side of : is the expression
            String expression = query.Split(expression_seperator, StringSplitOptions.TrimEntries)[1];

            // within this now split by keywords for a MAXIMUM of 2 values and MINIMUM of 1
            return new List<String>(expression.Split(keywords, StringSplitOptions.TrimEntries));
        }

        public static List<String> GetQueryOperations(String query)
        {
            // returning list can be of length 0 or 1

            List<String> keywords = new List<String>();
            Regex re = new Regex(@"<.*>");
            MatchCollection regex_mathes = re.Matches(query);

            foreach (Match match in regex_mathes)
                keywords.Add(match.Value);

            return keywords;
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
            String queryValue = GetQueryValues(query)[0];

            // get the type of query property
            Type propertyType = GetInstancePropertyType<T>(queryProperty);

            // do ==
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
    }
}
