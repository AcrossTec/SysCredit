namespace SysCredit.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppThemeChanged : ValueChangedMessage<AppTheme>
{
    public AppThemeChanged(AppTheme Value) : base(Value)
    {
    }
}
