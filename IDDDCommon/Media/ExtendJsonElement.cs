using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace IDDDCommon.Media
{
    public static class ExtendJsonElement
    {
        public static bool IsJsonPrimitive(this JsonElement @this)
        {
            return @this.ValueKind == JsonValueKind.String ||
                   @this.ValueKind == JsonValueKind.False ||
                   @this.ValueKind == JsonValueKind.True ||
                   @this.ValueKind == JsonValueKind.Number;
        }
        
        public static string StringValue(this JsonElement element, params string[] keys)
        {
            return element.ToNavigateElement(keys).ToString();
        }
        
        public static long? LongValue(this JsonElement element, params string[] keys)
        {
            return element.ToNavigateElement(keys).TryGetInt64(out var res) ? res : (long?) null;
        }
        
        public static float? FloatValue(this JsonElement element, params string[] keys)
        {
            return element.ToNavigateElement(keys).TryGetInt64(out var res) ? res : (float?) null;
        }
        
        public static DateTime? DateTimeValue(this JsonElement element, params string[] keys)
        {
            return element.ToNavigateElement(keys).TryGetDateTime(out var res) ? res : (DateTime?) null;
        }
        
        public static JsonElement ToNavigateElement(this JsonElement stringObject, string[] keys)
        {
            if (keys == null) throw new ArgumentNullException();
            if (keys.Length == 0) throw new ArgumentOutOfRangeException(nameof(keys), "Must specify one or more keys");

            if (stringObject.TryGetProperty(keys.First(), out var element) && element.IsJsonPrimitive())
                return element;
            
            if (element.ValueKind == JsonValueKind.Undefined)
                throw new JsonException($"{string.Join('.', keys)} is not find in Json Key.");

            foreach (var key in keys.Skip(1))
            {
                if (element.IsJsonPrimitive() || element.ValueKind == JsonValueKind.Null) 
                    break;
                
                if (element.ValueKind == JsonValueKind.Undefined)
                    throw new KeyNotFoundException();
                
                element.TryGetProperty(key, out element);
            }

            return element;
        }
    }
}