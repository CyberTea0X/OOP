using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class InterpreterPrototype
    {
        AbstractExpression expression;

        public InterpreterPrototype(AbstractExpression expression)
        {
            this.expression = expression;
        }

        public static int interpet(string rawtext)
        {
            return 0;
        }
    }
}
