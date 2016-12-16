using System;
using System.Collections.Generic;
using ManyConsole;
using SOMNetwork.Logic.NeighborhoodFunction;

namespace SOMNetwork
{
    class Program
    {
        static int Main(string[] args)
        {
            var nf = new Guassian();

            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine("Wartość guassaina przy i = {0} to {1}",i,nf.GiveValue(i,1));
            }


            // locate any commands in the assembly (or use an IoC container, or whatever source)
            var commands = GetCommands();



            // then run them.
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);


        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
