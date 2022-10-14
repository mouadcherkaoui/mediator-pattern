using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class HandleCommandOfTypeAttribute : Attribute
    {
        private readonly Type _commandType;
        public HandleCommandOfTypeAttribute(Type commandType)
        {
            this._commandType = commandType;
        }

        public Type CommandType
        {
            get { return _commandType; }
        }

        private Type _commandValidator;

        public Type Validator
        {
            get { return _commandValidator; }
            set { _commandValidator = value; }
        }

    }
}
