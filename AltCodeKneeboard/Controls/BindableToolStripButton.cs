using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AltCodeKneeboard.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    internal class BindableToolStripButton : ToolStripButton, IBindableComponent
    {
        private ControlBindingsCollection _DataBindings;
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (_DataBindings == null) _DataBindings = new ControlBindingsCollection(this);
                return _DataBindings;
            }
        }

        private BindingContext _BindingContext;
        public BindingContext BindingContext
        {
            get
            {
                if (_BindingContext == null) _BindingContext = new BindingContext();
                return _BindingContext;
            }
            set
            {
                _BindingContext = value;
            }
        }
    }
}
