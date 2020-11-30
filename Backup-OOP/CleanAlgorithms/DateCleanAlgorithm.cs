using System;
using System.Collections.Generic;
using System.Linq;
using Backup_OOP.Interfaces;

namespace Backup_OOP.CleanAlgorithms
{
    public class DateCleanAlgorithm : ICleanAlgorithm
    {
        private readonly DateTime _date;

        public DateCleanAlgorithm(DateTime date)
        {
            _date = date;
        }
        public int GetForRemove(List<RestorePoint> restorePoints)
        {
            int countForRemove = restorePoints
                .Count(x => x.RestoreTime < _date);
            return countForRemove;
        }

    }
}
