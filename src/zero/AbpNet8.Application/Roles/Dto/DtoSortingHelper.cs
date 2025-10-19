using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Roles.Dto
{
    public class Sort
    {
        public string selector { get; set; }
        public bool desc { get; set; }
    }

    public static class DtoSortingHelper
    {
        public static string ReplaceSorting(string sorting, Func<string, string> replaceFunc)
        {
            var sortFields = sorting.Split(',');
            for (var i = 0; i < sortFields.Length; i++)
            {
                sortFields[i] = replaceFunc(sortFields[i].Trim());
            }

            return string.Join(",", sortFields);
        }

        public static string ParseSort(string sorting)
        {
            if (string.IsNullOrEmpty(sorting))
            {
                return null;
            }

            var sort = JsonConvert.DeserializeObject<Sort[]>(sorting);

            return sort[0].selector + " " + (sort[0].desc ? "desc" : "asc");
        }

    }
}