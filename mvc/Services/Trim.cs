using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mvc.Services
{
    public static class Trim
    {
        private static readonly string NamePattern = "^[А-Яа-я][а-я]*\\s[а-я]*\\s[а-я]*"; // ^[А-Яа-я][а-я]*\s[а-я]*\s[а-я]* - рабочий фильтр

        public static string NameFilter(string title)
        {
            string input = title;
            string target = "";
            Regex regex = new Regex(NamePattern);
            return regex.Replace(input, target);
        } 
    }
}
