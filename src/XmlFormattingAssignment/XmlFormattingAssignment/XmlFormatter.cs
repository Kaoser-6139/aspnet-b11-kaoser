using System;
using System.Collections;
using System.Reflection;
using System.Text;

public static class XmlFormatter
{
    public static string Convert(object item)
    {
        if (item == null) return string.Empty;

        var itemType = item.GetType();
        var xmlBuilder = new StringBuilder();
        var typeName = itemType.Name;

        // Start tag
        xmlBuilder.AppendLine($"<{typeName}>");

        foreach (var property in itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var propertyName = property.Name;
            var value = property.GetValue(item);

            if (value == null)
            {
                // Null values as empty tags
                xmlBuilder.AppendLine($"<{propertyName}></{propertyName}>");
            }
            else if (value is string || value.GetType().IsPrimitive || value is DateTime)
            {
                // Primitive and string values
                xmlBuilder.AppendLine($"<{propertyName}>{value}</{propertyName}>");
            }
            else if (value is IEnumerable enumerableValue)
            {
                // Handle collections (List, Array)
                xmlBuilder.AppendLine($"<{propertyName}>");
                foreach (var element in enumerableValue)
                {
                    xmlBuilder.Append(Convert(element));
                }
                xmlBuilder.AppendLine($"</{propertyName}>");
            }
            else
            {
                // Nested objects
                xmlBuilder.AppendLine($"<{propertyName}>");
                xmlBuilder.Append(Convert(value));
                xmlBuilder.AppendLine($"</{propertyName}>");
            }
        }

        // End tag
        xmlBuilder.AppendLine($"</{typeName}>");

        return xmlBuilder.ToString();
    }
}


//using System.Collections;
//using System.Reflection;
//using System.Text;

//namespace XmlFormattingAssignment
//{
//    public static class XmlFormatter
//    {
//        public static string Convert(object item)
//        {
//            if (item == null) return string.Empty;

//            StringBuilder sb = new StringBuilder();
//            ConvertToXml(item, sb);
//            return sb.ToString();
//        }

//        private static void ConvertToXml(object obj, StringBuilder sb, int paddingNum = 0)
//        {
//            if (obj == null) return; // Base condition

//            Type type = obj.GetType();

//            var objName = type.Name; // e.g., "Course"

//            PropertyInfo[] properties = type.GetProperties();

//            string padding = new string(' ', paddingNum);

//            sb.AppendLine($"{padding}<{objName}>");

//            foreach (var property in properties)
//            {
//                string propertyName = property.Name; // e.g., "Course_Id"

//                Type propertyType = property.PropertyType;

//                bool isObject = propertyType.IsClass && propertyType != typeof(string);
//                bool isArray = propertyType.IsArray;
//                bool isList = typeof(IEnumerable).IsAssignableFrom(propertyType) && propertyType != typeof(string);

//                var propertyValue = property.GetValue(obj);

//                if (propertyValue == null)
//                {
//                    sb.AppendLine($"{padding}  <{propertyName} />");
//                    continue;
//                }

//                if (isArray || isList)
//                {
//                    sb.AppendLine($"{padding}  <{propertyName}>");

//                    IEnumerable enumerable = (IEnumerable)propertyValue;
//                    foreach (var item in enumerable)
//                    {
//                        ConvertToXml(item, sb, paddingNum + 4);
//                    }

//                    sb.AppendLine($"{padding}  </{propertyName}>");
//                }
//                else if (isObject)
//                {
//                    sb.AppendLine($"{padding}  <{propertyName}>");
//                    ConvertToXml(propertyValue, sb, paddingNum + 4);
//                    sb.AppendLine($"{padding}  </{propertyName}>");
//                }
//                else
//                {
//                    sb.AppendLine($"{padding}  <{propertyName}>{propertyValue}</{propertyName}>");
//                }
//            }

//            sb.AppendLine($"{padding}</{objName}>");
//        }
//    }
//}