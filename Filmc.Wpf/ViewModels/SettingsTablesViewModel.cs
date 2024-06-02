﻿using Filmc.Entities.Entities;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Wpf.ViewCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class SettingsTablesViewModel : BaseViewModel
    {
        private readonly ProfilesService _profilesService;

        private FilmGenreRepository? _filmGenres;
        private BookGenreRepository? _bookGenres;
        private FilmTagRepository? _filmTags;
        private BookTagRepository? _bookTags;

        private readonly EntityObserver<FilmGenre, FilmGenreViewModel> _filmGenreEntityObserver;
        private readonly EntityObserver<BookGenre, BookGenreViewModel> _bookGenreEntityObserver;
        private readonly EntityObserver<FilmTag, FilmTagViewModel> _filmTagEntityObserver;
        private readonly EntityObserver<BookTag, BookTagViewModel> _bookTagEntityObserver;

        public SettingsTablesViewModel(GlobalSettingsService settingsService)
        {
            _profilesService = settingsService.ProfilesService;

            ProfileVMs = new ObservableCollection<ProfileViewModel>();
            FilmGenresVMs = new ObservableCollection<FilmGenreViewModel>();
            BookGenresVMs = new ObservableCollection<BookGenreViewModel>();
            FilmTagsVMs = new ObservableCollection<FilmTagViewModel>();
            BookTagsVMs = new ObservableCollection<BookTagViewModel>();

            _filmGenreEntityObserver = new EntityObserver<FilmGenre, FilmGenreViewModel>(FilmGenresVMs, CreateFilmGenre);
            _bookGenreEntityObserver = new EntityObserver<BookGenre, BookGenreViewModel>(BookGenresVMs, CreateBookGenre);
            _filmTagEntityObserver = new EntityObserver<FilmTag, FilmTagViewModel>(FilmTagsVMs, CreateFilmTag);
            _bookTagEntityObserver = new EntityObserver<BookTag, BookTagViewModel>(BookTagsVMs, CreateBookTag);

            ProfilesInit();
            OnProfileChanged(_profilesService.SelectedProfile);
            _profilesService.SelectedProfileChanged += OnProfileChanged;
            _profilesService.ProfileAdded += OnProfileAdded;
            _profilesService.ProfileRemoved += ProfileRemoved;
        }

        public ObservableCollection<ProfileViewModel> ProfileVMs { get; }
        public ObservableCollection<FilmGenreViewModel> FilmGenresVMs { get; }
        public ObservableCollection<BookGenreViewModel> BookGenresVMs { get; }
        public ObservableCollection<FilmTagViewModel> FilmTagsVMs { get; }
        public ObservableCollection<BookTagViewModel> BookTagsVMs { get; }

        public FilmGenreRepository? FilmGenres
        {
            get => _filmGenres;
            set { _filmGenres = value; OnPropertyChanged(); }
        }
        public BookGenreRepository? BookGenres
        {
            get => _bookGenres;
            set { _bookGenres = value; OnPropertyChanged(); }
        }
        public FilmTagRepository? FilmTags
        {
            get => _filmTags;
            set { _filmTags = value; OnPropertyChanged(); }
        }
        public BookTagRepository? BookTags
        {
            get => _bookTags;
            set { _bookTags = value; OnPropertyChanged(); }
        }

        private void ProfilesInit()
        {
            foreach (var profile in _profilesService.Profiles)
                ProfileVMs.Add(new ProfileViewModel(profile));
        }

        private void OnProfileAdded(Profile newProfile)
        {
            ProfileVMs.Add(new ProfileViewModel(newProfile));
        }

        private void ProfileRemoved(Profile oldProfile)
        {
            ProfileViewModel? profileVM = ProfileVMs.FirstOrDefault(x => x.Profile == oldProfile);

            if (profileVM != null)
                ProfileVMs.Remove(profileVM);
        }

        private void OnProfileChanged(Profile newProfile)
        {
            foreach (var profileVm in ProfileVMs)
            {
                profileVm.IsSelected = false;

                if (profileVm.Profile == newProfile)
                    profileVm.IsSelected = true;
            }

            FilmGenres = newProfile.TablesContext.FilmGenres;
            BookGenres = newProfile.TablesContext.BookGenres;
            FilmTags = newProfile.TablesContext.FilmTags;
            BookTags = newProfile.TablesContext.BookTags;

            _filmGenreEntityObserver.SetSource(FilmGenres);
            _bookGenreEntityObserver.SetSource(BookGenres);
            _filmTagEntityObserver.SetSource(FilmTags);
            _bookTagEntityObserver.SetSource(BookTags);
        }

        private FilmGenreViewModel CreateFilmGenre(FilmGenre entity)
        {
            return new FilmGenreViewModel(entity);
        }

        private BookGenreViewModel CreateBookGenre(BookGenre entity)
        {
            return new BookGenreViewModel(entity);
        }

        private FilmTagViewModel CreateFilmTag(FilmTag entity)
        {
            return new FilmTagViewModel(entity);
        }

        private BookTagViewModel CreateBookTag(BookTag entity)
        {
            return new BookTagViewModel(entity);
        }
    }
}
