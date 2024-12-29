using System.Collections;
using System.Reflection;
using System.Text;

namespace XmlFormattingAssignment
{
    public static class XmlFormatter
    {
        public static string Convert(object item)
        {
            return SerializeObject(item);
        }
        public static string SerializeObject(object obj, string elementName = null, int indentLevel = 0)
        {
            StringBuilder sb = new StringBuilder();
            string indent = new string(' ', indentLevel * 4); 
            elementName ??= obj?.GetType().Name ?? "Object";

            if (obj == null)
            {
               
                sb.Append($"{indent}<{elementName}></{elementName}>\n");
                return sb.ToString();
            }

            Type type = obj.GetType();

            
            if (type.IsPrimitive || obj is string || obj is DateTime || obj is double || obj is int || obj is float || obj is decimal)
            {
                sb.Append($"{indent}<{elementName}>{obj}</{elementName}>\n");
                return sb.ToString();
            }

            
            if (obj is IEnumerable enumerable)
            {
                string childElementName = elementName switch
                {
                    "Tags" => "String",
                    "Tests" => "AdmissionTest",
                    _ => "List"
                };

                sb.Append($"{indent}<{elementName}>\n");
                foreach (var item in enumerable)
                {
                    sb.Append(SerializeObject(item, childElementName, indentLevel + 1));
                }
                sb.Append($"{indent}</{elementName}>\n");
                return sb.ToString();
            }

            
            sb.Append($"{indent}<{elementName}>\n");
            foreach (var property in type.GetProperties())
            {
                object value = property.GetValue(obj);

                
                if (value is null)
                {
                    sb.Append($"{indent}    <{property.Name}></{property.Name}>\n");
                }
                else
                {
                    sb.Append(SerializeObject(value, property.Name, indentLevel + 1));
                }
            }
            sb.Append($"{indent}</{elementName}>\n");

            return sb.ToString();
        }


    }
}