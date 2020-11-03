﻿using System;
using System.ComponentModel.DataAnnotations;
using UserTasksService.Models;

namespace UserTasksService.IncomingModels
{
    public class CorrectTaskNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //string task = Convert.ToString(value);
            //try
            //{
            //    DateTime.ParseExact(
            //        task.Substring(0, 8),
            //        "yyyyMMdd",
            //        null,
            //        DateTimeStyles.None);

            //    Regex regex = new Regex(@"-\d{4}");
            //    return regex.IsMatch(task.Substring(8));
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return new TaskNumber(Convert.ToString(value)).IsValid;
        }
    }
}
