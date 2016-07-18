// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="_Master.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;

    using SoQuestionsSolver;

    /// <summary>TODO The program.</summary>
    internal class Master
    {
        /// <summary>TODO The main.</summary>
        public static void Main2()
        {
            var worker = new Processor();
            worker.ProcessData();
            Console.ReadLine();
        }

        /// <summary>TODO The processor.</summary>
        public class Processor
        {
            /// <summary>TODO The process data.</summary>
            public async void ProcessData()
            {
                var start = DateTime.Now;
                var _collection = ConnectionHelper.Connection<CompareOnSameDocumentFieldsSteps.Datapull>(string.Empty);

                var stop = start - DateTime.Now;
                Console.WriteLine(stop.ToString());
            }
        }
    }

    /*
 * 
 
     * 
     *  */
}
