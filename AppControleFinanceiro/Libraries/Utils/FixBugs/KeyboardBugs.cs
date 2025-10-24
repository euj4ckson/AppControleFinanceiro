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
            var activity = Platform.CurrentActivity;
            var focus = activity?.CurrentFocus;

            if (focus != null)
                focus.ClearFocus();
        #endif
        }

    }
}
