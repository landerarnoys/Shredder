using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Howest.NMCT.Shredder.Lib.Service;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            else
            {
                SimpleIoc.Default.Register<IShredderService, ShredderService>();
            }
            
            SimpleIoc.Default.Register<PlaceViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<TopTenPicturesViewModel>();
            SimpleIoc.Default.Register<TopTenVideosViewModel>();
            SimpleIoc.Default.Register<TopTenSpotsViewModel>();
            SimpleIoc.Default.Register<TopTenSpotsViewModel>();
            SimpleIoc.Default.Register<DetailMapPageViewModel>();
        }

        public PlaceViewModel PlaceViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PlaceViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }



        public MainPageViewModel MainPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MainPageViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public TopTenPicturesViewModel TopTenPicturesViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TopTenPicturesViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public TopTenVideosViewModel TopTenVideosViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TopTenVideosViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public TopTenSpotsViewModel TopTenSpotsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TopTenSpotsViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }



        public DetailMapPageViewModel DetailMapPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<DetailMapPageViewModel>();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
