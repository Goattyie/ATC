using System;

namespace ATC.Wpf.Queries.Interfaces
{
    internal class DoubleDateInput : BaseInput
    {
        public double DoubleValue { get; set; }
        public DateTime DateValue { get; set; } = DateTime.Now;
    }

    internal interface IAbonentsByCallsCostDateSumQuery : IQuery<DoubleDateInput, AbonentsByCallsCostSumResult>
    {
    }
}
