using System;

using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;

using OSKEquipmentManager.Services;
using OSKEquipmentManager.Views;

namespace OSKEquipmentManager.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            Register<NavigationViewModel, NavigationViewPage>();
            Register<MainViewModel, MainPage>();
            Register<UpdateFormViewModel, UpdateFormPage>();
            Register<EquipmentListViewModel, EquipmentListPage>();
        }

        public EquipmentListViewModel EquipmentListViewModel => ServiceLocator.Current.GetInstance<EquipmentListViewModel>();

        public UpdateFormViewModel UpdateFormViewModel => ServiceLocator.Current.GetInstance<UpdateFormViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public NavigationViewModel NavigationViewModel => ServiceLocator.Current.GetInstance<NavigationViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
