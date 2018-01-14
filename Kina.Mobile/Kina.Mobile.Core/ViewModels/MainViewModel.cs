﻿// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using System;

namespace Kina.Mobile.Core.ViewModels
{
	public class MainViewModel : MvxViewModel
	{
		private readonly IMvxNavigationService _navigationService;
		private readonly Services.IAppSettings _settings;

		public MainViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings)
		{
			_navigationService = navigationService;
			_settings = settings;

			ButtonText = Resources.AppResources.MainPageButton;
		}

		public IMvxCommand PressMeCommand =>
			new MvxCommand(() =>
			{
				ButtonText = Resources.AppResources.MainPageButtonPressed;
			});

		public IMvxAsyncCommand GoToSecondPageCommand =>
			new MvxAsyncCommand(async () =>
			{
				await _navigationService.Navigate<ShowsViewModel>();
			});

		public IMvxCommand OpenGithubUrlCommand =>
			new MvxCommand(() =>
			{
				Device.OpenUri(new Uri("https://github.com/JTOne123/XamFormsMvxTemplate"));
			});

		public string ButtonText { get; set; }

		public int SuperNumber
		{
			get { return _settings.SuperNumber; }
			set { _settings.SuperNumber = value; }
		}
	}
}