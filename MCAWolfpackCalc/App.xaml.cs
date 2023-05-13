using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace MCAWolfpackCalc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // set the UI culture based on operating system
        public static void SetCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
                return;

            // Copy all MergedDictionarys into a auxiliar list.
            List<ResourceDictionary> dictionaryList = Current.Resources.MergedDictionaries.ToList();

            // Search for the specified culture.
            string requestedCulture = string.Format("Resources\\Resources.{0}.xaml", culture);
            ResourceDictionary? resourceDictionary = dictionaryList.
                FirstOrDefault(d => d.Source.OriginalString == requestedCulture);

            if (resourceDictionary == null)
            {
                //If not found, select our default language.
                requestedCulture = "Resources\\Resources.xaml";
                resourceDictionary = dictionaryList.
                    FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
            }

            // If we have the requested resource, remove it from the list and place at the end.
            // Then this language will be our string table to use.
            if (resourceDictionary != null)
            {
                Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            // Inform the threads of the new culture.
            Thread.CurrentThread.CurrentCulture = new(culture);
            Thread.CurrentThread.CurrentUICulture = new(culture);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}