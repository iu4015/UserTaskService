using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UserTasksService.Models
{
    public class TaskNumber
    {
        private const string dateFormat = "yyyyMMdd";
        private const int dateFormatStartIndex = 0;
        private const int dateFormatLength = 8;
        private const string delimitter = "-";
        private const int numberStartIndex = 9;
        private const string numberPattern = @"\d{4}";

        public int Number { get; private set; }
        public DateTime Date { get; private set; }

        public bool IsValid { get; private set; }


        public TaskNumber(string taskNumber)
        {
            try
            {
                Date = DateTime.ParseExact(
                    taskNumber.Substring(dateFormatStartIndex, dateFormatLength),
                    dateFormat,
                    null,
                    DateTimeStyles.None);

                Regex regex = new Regex(delimitter + numberPattern);
                if (regex.IsMatch(taskNumber.Substring(numberStartIndex - delimitter.Length)))
                {
                    IsValid = true;
                    Number = Convert.ToInt32(taskNumber.Substring(numberStartIndex));
                }
                else
                    IsValid = false;
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        public TaskNumber(int number, DateTime date)
        {
            Number = number;
            Date = date;
            IsValid = true;
        }

        public override string ToString()
        {
            return Date.ToString(dateFormat) + delimitter + string.Format("{0:0000}", Number);
        }
    }
}
