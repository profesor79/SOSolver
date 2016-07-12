// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Inquisitive_one2.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;

    using MongoDB.Driver;

    using SoQuestionsSolver;

    /// <summary>TODO The program.</summary>
    internal class Inquisitive_one2
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
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
                var _collection = ConnectionHelper.Connection<CompareOnSameDocumentFieldsSteps.Datapull>("datapull");

                var data = _collection.Aggregate();
                var a1 =
                    data.Project(
                        x =>
                        new
                            {
                                x.FSTicker, 
                                x.Sedol, 
                                x.Company, 
                                x.Exchange, 
                                x.LocalTicker, 
                                IsTrue = x.Sedol == x.FSTicker
                            });
                var a2 = a1.Match(x => x.IsTrue);

                var result = a2.ToList();

                foreach (var r in result)
                {
                    Console.WriteLine($"Company: {r.Company}\t FSTicker:{r.FSTicker}\t Sedol:{r.Sedol}");
                }

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
