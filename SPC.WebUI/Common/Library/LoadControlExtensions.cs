using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace SPC.WebUI.Common.Library
{
    /// <summary>
    /// Class for the LoadControl extension method(s)
    /// </summary>
    public static class LoadControlExtensions
    {
        /// <summary>
        /// Loads a user control with a constructor with a signature matching the supplied params
        /// Control must implement a blank default constructor as well as the custom one or we will error
        /// </summary>
        /// <param name="templateControl">Template control base object</param>
        /// <param name="controlPath">Path to the user control</param>
        /// <param name="constructorParams">Parameters for the constructor</param>
        /// <returns></returns>
        public static UserControl LoadControl(this TemplateControl templateControl, string controlPath, params object[] constructorParams)
        {
            // Load the control
            var control = templateControl.LoadControl(controlPath) as UserControl;

            // Get the types for the passed parameters
            Type[] paramTypes = new Type[constructorParams.Length];
            for (int paramLoop = 0; paramLoop < constructorParams.Length; paramLoop++)
                paramTypes[paramLoop] = constructorParams[paramLoop].GetType();

            // Get the constructor that matches our signature
            var constructor = control.GetType().BaseType.GetConstructor(paramTypes);

            // Call the constructor if we found it, otherwise throw
            if (constructor == null)
            {
                throw new ArgumentException("Required constructor signature not found.");
            }
            else
            {
                constructor.Invoke(control, constructorParams);
            }

            return control;
        }
    }
}
