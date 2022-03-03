using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace NestedDialogsBot
{
    public class ChildDialog : ComponentDialog
    {
        public ChildDialog() : base(nameof(ChildDialog))
        {
            AddDialog(new GrandChildDialog());
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null,
            CancellationToken cancellationToken = default)
        {
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ChildDialog)}-{nameof(BeginDialogAsync)}"));
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(
            DialogContext outerDc,
            CancellationToken cancellationToken = default)
        {
            var innerDc = CreateChildContext(outerDc);
            await innerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ChildDialog)}-{nameof(ContinueDialogAsync)}"));
            await innerDc.BeginDialogAsync(nameof(GrandChildDialog));
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(
            DialogContext outerDc,
            DialogReason reason,
            object result = null,
            CancellationToken cancellationToken = default)
        {
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ChildDialog)}-{nameof(ResumeDialogAsync)}"));
            await outerDc.EndDialogAsync();
            return EndOfTurn;
        }
    }
}
