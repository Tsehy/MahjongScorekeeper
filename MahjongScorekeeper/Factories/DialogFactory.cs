using Avalonia.Controls;
using MahjongScorekeeper.Data;
using System;

namespace MahjongScorekeeper.Factories;

public class DialogFactory(Func<DialogViewType, Window> factory)
{
    public Window GetDialogView(DialogViewType dialogViewType) => factory.Invoke(dialogViewType);
}
