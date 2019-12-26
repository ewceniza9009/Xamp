using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamp.UIEffects
{
    public class EffectsEvent: EventArgs
    {
        public Object Obj;
        public EffectsEvent(Object o) {
            this.Obj = o;
        }
    }

    public class EffectsEventListener {
        public void Click(EffectsEvent effectsEvent) {

        }
    }
}
