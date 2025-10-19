using Newtonsoft.Json;
using System;
using System.Globalization;

namespace AbpNet8.Web.Startup
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private static string[] DateFormats { get; } =
         {
            "dd/MM/yyyy HH:mm:ss",
            "dd/MM/yyyy",
            "dd/MM/yyyy HH:mm",
            @"ddd, dd MMM yyyy HH:mm:ss 'GMT'",
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss"
        };

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var cul = CultureInfo.GetCultureInfo("vi-VN");
            return DateTime.ParseExact(reader.Value.ToString()
                .Replace("Janu", "Jan")
                .Replace("Febr", "Feb")
                .Replace("Marc", "Mar")
                .Replace("Apri", "Apr")
                .Replace("June", "Jun")
                .Replace("July", "Jul")
                .Replace("Augu", "Aug")
                .Replace("Sept", "Sep")
                .Replace("Octo", "Oct")
                .Replace("Nove", "Nov")
                .Replace("Dece", "Dec"), DateFormats, cul);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        private static string[] DateFormats { get; } =
         {
            "dd/MM/yyyy HH:mm:ss",
            "dd/MM/yyyy",
            "dd/MM/yyyy HH:mm",
            @"ddd, dd MMM yyyy HH:mm:ss 'GMT'",
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss"
        };

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var cul = CultureInfo.GetCultureInfo("vi-VN");
            return !string.IsNullOrWhiteSpace(reader.Value?.ToString()) ? DateTime.ParseExact(reader.Value.ToString()
                .Replace("Janu", "Jan")
                .Replace("Febr", "Feb")
                .Replace("Marc", "Mar")
                .Replace("Apri", "Apr")
                .Replace("June", "Jun")
                .Replace("July", "Jul")
                .Replace("Augu", "Aug")
                .Replace("Sept", "Sep")
                .Replace("Octo", "Oct")
                .Replace("Nove", "Nov")
                .Replace("Dece", "Dec"), DateFormats, cul) : null;
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
