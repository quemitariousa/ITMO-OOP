using System;
using System.Collections.Generic;
using System.Text;
using Backup_OOP.Interfaces;

namespace Backup_OOP.Tests.Mocks
{
    public class MockDateTimeProvider: IDateTimeProvider
    {
        private DateTime _dateTime;

        public MockDateTimeProvider(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public DateTime GetCurrentTime()
        {
            return _dateTime;
        }

        public void SetCurrentTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}
