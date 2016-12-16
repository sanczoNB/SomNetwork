using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ManyConsole;
using NDesk.Options;
using SOMNetwork.Helpers;
using SOMNetwork.I_O;
using SOMNetwork.Logic;
using SOMNetwork.Logic.NeighborhoodFunction;
using SOMNetwork.Model;

namespace SOMNetwork.Commands
{
    class LearnCommand : ConsoleCommand
    {
        private string _fileName;

        private bool shouldVisual;

        private readonly DataLoader _dataLoader = new DataLoader();

        private readonly MatrixHelpers _matrixHelpers = new MatrixHelpers();

        private readonly Visualizer _visualizer = new Visualizer();

        public LearnCommand()
        {
            IsCommand("learn", "Learn network and save results");

            Options = new OptionSet
            {
                {"f|fileName=", "Specify the file name", s => _fileName = s },
                {"v|visual", "Schould be visual",v => shouldVisual = v != null }
            };


        }

        public override int Run(string[] remainingArguments)
        {
            InitializeArgumentsFromConfinIfItIsNecessary();

           var inputs =  _dataLoader.LoadCoordinates(_fileName);

            var normalizedInputs = inputs.Select(_matrixHelpers.Normalize).ToList();

            var maxInputOne = normalizedInputs.Select(x => x.At(0, 0)).Max();

            var maxInputTwo = normalizedInputs.Select(x => x.At(0, 1)).Max();

            var network = new SOMNet(maxInputOne, maxInputTwo, inputs.Count);

            var rmax = 4;

            //var neighborhoodFunction = new Guassian();
            var neighborhoodFunction = new Guassian();

            var teacher = new Teacher(network, neighborhoodFunction, rmax);

            teacher.Learn(normalizedInputs);

            var order = new List<int>();

            foreach (var input in inputs)
            {
                order.Add(network.GetIndexOfTheMostMatchesNeuron(input));
            }

            var saver = new DataSaver();
            
            saver.SaveWeights(network);
            saver.SaveNormalizedInputs(normalizedInputs);
            saver.SaveOrder(order);

            if (shouldVisual)
            {
                var orderlinesses = inputs.Select((x, i) => new Orderliness(order[i], x)).ToList();

                var bm = _visualizer.Visualize(orderlinesses);

                bm.Save(ConfigurationManager.AppSettings["Visualization"]);
            }

            return 0;
        }

        private void InitializeArgumentsFromConfinIfItIsNecessary()
        {
            _fileName = _fileName ?? ConfigurationManager.AppSettings["FileWithInputsCoordinates"];
        }
    }
}
