using Microsoft.Bot.Builder.Dialogs;

namespace NestedDialogsBot
{
    public static class BotFrameworkHelper
    {
        /// <summary>
        /// Retrieve inner most DialogContext
        /// </summary>
        /// <param name="outerDc"></param>
        /// <returns></returns>
        public static DialogContext GetInnerMostDialogContext(DialogContext outerDc)
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