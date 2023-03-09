﻿using System;

namespace LiteDBv4.Shell.Commands
{
    [Help(
        Category = "Shell",
        Name = "pretty",
        Syntax = "pretty [on|off]",
        Description = "Print all json results with identation/break lines",
        Examples = new string[] {
            "pretty"
        }
    )]
    internal class Pretty : IShellCommand
    {
        public bool IsCommand(StringScanner s)
        {
            return s.Scan(@"pretty\s*").Length > 0;
        }

        public void Execute(StringScanner s, Env env)
        {
            env.Display.Pretty = !(s.Scan(@"off\s*").Length > 0);
        }
    }
}