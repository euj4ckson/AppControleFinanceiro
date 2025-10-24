using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Libraries.Utils.FixBugs
{
    public class KeyboardBugs
    {
        public static void HideKeyboardOnAndroid()
        {
#if ANDROID
            if (Platform.CurrentActivity is not null)
            {
                Platform.CurrentActivity.CurrentFocus.ClearFocus();
            }
#endif
        }
    }
}
