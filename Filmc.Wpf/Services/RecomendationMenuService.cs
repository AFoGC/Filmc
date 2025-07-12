using Filmc.Entities.Entities;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class RecomendationMenuService
    {
        private RecomendationMenuViewModel _menuViewModel;
        private readonly ProfilesService _profilesService;

        public RecomendationMenuService(RecomendationMenuViewModel menuViewModel, ProfilesService profilesService)
        {
            _menuViewModel = menuViewModel;
            _profilesService = profilesService;
        }

        public void OpenRecomendations(FilmTablesViewModel tablesViewModel)
        {
            RepositoriesFacade repositories = _profilesService.SelectedProfile.TablesContext;
            FilmsRecomendationService service = new FilmsRecomendationService(repositories);
            Tuple<Film, Similarity>[] rec = service.CreateRecomendations();

            FilmViewModel[] viewModels = new FilmViewModel[rec.Length];

            for (int i = 0; i < rec.Length; i++)
            {
                Film film = rec[i].Item1;
                viewModels[i] = tablesViewModel.FilmVMs
                    .First(x => x.Model == film);
            }

            _menuViewModel.OpenMenu(viewModels);
        }

        public void CloseRecomendations()
        {
            _menuViewModel.CloseMenu();
        }
    }
}
