using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace NestedDialogsBot
{
    public class GrandChildDialog : ComponentDialog
    {
        public GrandChildDialog() : base(nameof(GrandChildDialog))
        {
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var innerDc = CreateChildContext(outerDc);
            await innerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(GrandChildDialog)}-{nameof(BeginDialogAsync)}"), cancellationToken);
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(
            DialogContext outerDc,
            CancellationToken cancellationToken = default)
        {
            var innerDc = CreateChildContext(outerDc);
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(GrandChildDialog)}-{nameof(ContinueDialogAsync)}"), cancellationToken);
            
            //Ending the inner most dialog
            //Expected result: ChildDialog-ResumeDialog
            await innerDc.EndDialogAsync(cancellationToken: cancellationToken);
            return EndOfTurn;
        }
    }
}
