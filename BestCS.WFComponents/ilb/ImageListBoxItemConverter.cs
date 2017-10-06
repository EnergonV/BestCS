using System;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;

namespace Controls.Development
{
	/// <summary>
	/// Converts the object to ImageListBoxItem
	/// </summary>
	public class ImageListBoxItemConverter : ExpandableObjectConverter
	{
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            // Always call the base to see if it can perform the conversion.
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(InstanceDescriptor)) 
            {
                ConstructorInfo ci = typeof(ImageListBoxItem).GetConstructor(new Type[]{
                                                                                         typeof(string),
                                                                                         typeof(int)});
                ImageListBoxItem item = (ImageListBoxItem)value;
                return new InstanceDescriptor(ci, new object[]{item.Text, item.ImageIndex}, true);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
	}
}
