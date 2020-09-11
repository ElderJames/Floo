using System;

namespace Floo.Core.Shared.HttpProxy
{
    internal class ApiParameterDescriptor : ICloneable
    {
        public string Name { get; set; }

        public int Index { get; set; }

        public Type ParameterType { get; set; }

        public object Value { get; set; }

        public bool IsUriParameterType { get; set; }

        public override string ToString()
        {
            return $"[{ParameterType.FullName}]:{this.Name} = {this.Value}";
        }

        public object Clone()
        {
            return new ApiParameterDescriptor
            {
                Index = this.Index,
                IsUriParameterType = this.IsUriParameterType,
                Name = this.Name,
                ParameterType = this.ParameterType,
                Value = this.Value
            };
        }
    }
}