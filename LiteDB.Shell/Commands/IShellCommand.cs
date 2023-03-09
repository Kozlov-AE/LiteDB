using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteDBv4.Shell
{
    internal interface IShellCommand
    {
        bool IsCommand(StringScanner s);

        void Execute(StringScanner s, Env env);
    }
}