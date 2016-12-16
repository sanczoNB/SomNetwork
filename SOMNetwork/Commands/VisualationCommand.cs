using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using SOMNetwork.I_O;
using SOMNetwork.Logic;
using SOMNetwork.Model;

namespace SOMNetwork.Commands
{
    public class VisualationCommand : ConsoleCommand
    {
        private readonly DataLoader _dataLoader = new DataLoader();

        private readonly Visualizer _visualizer = new Visualizer();

        public VisualationCommand()
        {
            IsCommand("visual", "Visual cities");
        }

        public override int Run(string[] remainingArguments)
        {
            var pathToCoordinates = ConfigurationManager.AppSettings["FileWithInputsCoordinates"];
            var pathOrder = ConfigurationManager.AppSettings["Order"];

            var cordinates = _dataLoader.LoadCoordinates(pathToCoordinates);

            var order = _dataLoader.LoadOrder(pathOrder);

            var orderlinesses = cordinates.Select((x, i) => new Orderliness(order[i], x)).ToList();

            var bm = _visualizer.Visualize(orderlinesses);

            bm.Save(ConfigurationManager.AppSettings["Visualization"]);

            return 0;
        }
    }
}
