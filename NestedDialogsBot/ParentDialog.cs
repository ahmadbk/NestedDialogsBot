using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace NestedDialogsBot
{
    public class ParentDialog : ComponentDialog
    {
        public ParentDialog() : base(nameof(ParentDialog))
        {
            AddDialog(new ChildDialog());
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null,
            CancellationToken cancellationToken = default)
        {
            var innerDc = CreateChildContext(outerDc);
            await innerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ParentDialog)}-{nameof(BeginDialogAsync)}"), cancellationToken);
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(
            DialogContext outerDc,
            CancellationToken cancellationToken = default)
        {
            var innerDc = CreateChildContext(outerDc);
            await innerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ParentDialog)}-{nameof(ContinueDialogAsync)}"), cancellationToken);
            await innerDc.BeginDialogAsync(nameof(ChildDialog), cancellationToken: cancellationToken);
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(
            DialogContext outerDc,
            DialogReason reason,
            object result = null,
            CancellationToken cancellationToken = default)
        {
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(ParentDialog)}-{nameof(ResumeDialogAsync)}"), cancellationToken);
            return EndOfTurn;
        }
    }
}
