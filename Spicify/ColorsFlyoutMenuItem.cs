using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicify
{
    public class ColorsFlyoutMenuItem
    {
        public ColorsFlyoutMenuItem()
        {
            TargetType = typeof(ColorsFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}