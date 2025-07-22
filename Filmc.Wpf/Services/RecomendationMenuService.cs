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
            ItemSimilarity<Film>[] rec = service.CreateRecomendations();

            ItemSimilarity<FilmViewModel>[] viewModels = new ItemSimilarity<FilmViewModel>[rec.Length];

            for (int i = 0; i < rec.Length; i++)
            {
                var recomendation = rec[i];
                var viewModel = tablesViewModel.FilmVMs
                    .First(x => x.Model == recomendation.Item);

                viewModels[i] = new ItemSimilarity<FilmViewModel>(viewModel, recomendation.Similarity);
            }

            _menuViewModel.OpenMenu(viewModels);
        }

        public void CloseRecomendations()
        {
            _menuViewModel.CloseMenu();
        }
    }
}
