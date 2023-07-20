namespace SysCredit.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class DisplayOrientationChanged : ValueChangedMessage<DisplayOrientation>
{
    public DisplayOrientationChanged(DisplayOrientation Value) : base(Value)
    {
    }
}
