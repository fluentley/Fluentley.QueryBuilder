using System;
using System.Linq;

namespace Fluentley.QueryBuilder.Processors
{
    internal class OptionProcessor
    {
        public TOption Process<TInterfaceOption, TOption, TData>(Action<TInterfaceOption> optionAction,
            IQueryable<TData> data)
            where TOption : TInterfaceOption
        {
            var option = (TOption) Activator.CreateInstance(typeof(TOption), data);
            optionAction?.Invoke(option);
            return option;
        }
    }
}