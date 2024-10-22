﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Exeptions
{
    public class IncorrectFormatException : Exception
    {
        public IncorrectFormatException(string wrongRow) : base($"Wrong format in row: {wrongRow}") { }

        public static implicit operator string(IncorrectFormatException v)
        {
            throw new NotImplementedException();
        }
    }
}
