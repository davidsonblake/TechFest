using System.Reflection;
using FreshMvvm;
using PropertyChanged;

namespace TechFest.PageModels
{
    [ImplementPropertyChanged]
    public class BasePageModel : FreshBasePageModel
    {
        public IDataService DataService { get; private set; }

        public bool IsBusy { get; set; }

        public string Title { get; set; }

		public Assembly Assembly => typeof(App).GetTypeInfo().Assembly;

        public BasePageModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}