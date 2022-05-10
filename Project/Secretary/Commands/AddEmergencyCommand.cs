using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class AddEmergencyCommand : CommandBase
    {

        public AddEmergencyCommand()
        {

        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
