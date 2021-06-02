using HSEApiTraining.Models.Customer;
using System;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface IBanService
    {
        string Ban(AddBanRequest request);
        string DeleteBanned(int id);
        (IEnumerable<BannedPhone> BannedPhones, string Error) GetAll();
        string DeleteAll();
    }

    public class BanService : IBanService
    {
        private IBanRepository _banRepository;

        public BanService(IBanRepository banRepository)
            => _banRepository = banRepository;

        public string Ban(AddBanRequest request)
            => _banRepository.Ban(request);

        public (IEnumerable<BannedPhone> BannedPhones, string Error) GetAll()
            => _banRepository.GetAll();

        public string DeleteBanned(int id)
            => _banRepository.DeleteBanned(id);

        public string DeleteAll()
            => _banRepository.DeleteAll();
    }
}
