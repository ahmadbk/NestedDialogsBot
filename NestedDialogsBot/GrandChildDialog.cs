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
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(GrandChildDialog)}-{nameof(BeginDialogAsync)}"));
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(
            DialogContext outerDc,
            CancellationToken cancellationToken = default)
        {
            var innerMostDc = GetInnerMostDialogContext(outerDc);
            await outerDc.Context.SendActivityAsync(MessageFactory.Text($"{nameof(GrandChildDialog)}-{nameof(ContinueDialogAsync)}"));
            
            //Ending the inner most dialog
            //Expected result: ChildDialog-ResumeDialog
            await innerMostDc.EndDialogAsync();
            return EndOfTurn;
        }
        
        /// <summary>
        /// Retrieve inner most DialogContext
        /// </summary>
        /// <param name="outerDc"></param>
        /// <returns></returns>
        private DialogContext GetInnerMostDialogContext(DialogContext outerDc)
        {
            if(outerDc.Child is null)
            {
                return outerDc;
            }

            if(outerDc.Child.ActiveDialog is null)
            {
                return outerDc;
            }
			
            return GetInnerMostDialogContext(outerDc.Child);
        }
    }
}
