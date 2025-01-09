using AutoMapper;
using lab.azure_active_directory_auth.Core;
using lab.azure_active_directory_auth.Helpers;
using lab.azure_active_directory_auth.Models;
using lab.azure_active_directory_auth.Repositories;
using lab.azure_active_directory_auth.ViewModels;

namespace lab.azure_active_directory_auth.Managers
{
    public class MemberManager : IMemberManager
    {
        private readonly IMemberRepository _iMemberRepository;
        private readonly IMapper _iMapper;

        public MemberManager(IMemberRepository iMemberRepository
            , IMapper iMapper)
        {
            _iMemberRepository = iMemberRepository;
            _iMapper = iMapper;
        }

        public async Task<MemberViewModel> GetMemberAsync()
        {
            try
            {
                await Task.Yield();
                var dataIEnumerable = _iMemberRepository.GetMembersAsync();
                var data = dataIEnumerable.FirstOrDefault();
                return _iMapper.Map<Member, MemberViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MemberViewModel> GetMemberAsync(int id)
        {
            try
            {
                await Task.Yield();
                var data = _iMemberRepository.GetMemberAsync(id);
                return _iMapper.Map<Member, MemberViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
            try
            {
                await Task.Yield();
                var data = _iMemberRepository.GetMembersAsync();
                return _iMapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> InsertMemberAsync(MemberViewModel model)
        {
            try
            {
                await Task.Yield();
                var data = _iMapper.Map<MemberViewModel, Member>(model);

                var saveChange = _iMemberRepository.InsertMemberAsync(data);

                if (saveChange == true)
                {
                    return Result.Ok(MessageHelper.Save);
                }
                else
                {
                    return Result.Fail(MessageHelper.SaveFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> InsertMemberAsync(List<MemberViewModel> modelList)
        {
            try
            {
                await Task.Yield();
                var dataList = _iMapper.Map<List<MemberViewModel>, List<Member>>(modelList);

                var saveChange = _iMemberRepository.InsertMemberAsync(dataList);

                if (saveChange == true)
                {
                    return Result.Ok(MessageHelper.Save);
                }
                else
                {
                    return Result.Fail(MessageHelper.SaveFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> UpdateMemberAsync(MemberViewModel model)
        {
            try
            {
                await Task.Yield();
                var data = _iMapper.Map<MemberViewModel, Member>(model);

                var saveChange = _iMemberRepository.UpdateMemberAsync(data);

                if (saveChange == true)
                {
                    return Result.Ok(MessageHelper.Update);
                }
                else
                {
                    return Result.Fail(MessageHelper.UpdateFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> DeleteMemberAsync(int id)
        {
            try
            {
                await Task.Yield();
                var model = await GetMemberAsync(id);
                if (model != null)
                {
                    var data = _iMapper.Map<MemberViewModel, Member>(model);

                    var saveChange = _iMemberRepository.DeleteMemberAsync(data);

                    if (saveChange == true)
                    {
                        return Result.Ok(MessageHelper.Delete);
                    }
                    else
                    {
                        return Result.Fail(MessageHelper.DeleteFail);
                    }
                }
                else
                {
                    return Result.Fail(MessageHelper.DeleteFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public interface IMemberManager
    {
        Task<MemberViewModel> GetMemberAsync();
        Task<MemberViewModel> GetMemberAsync(int id);
        Task<IEnumerable<MemberViewModel>> GetMembersAsync();
        Task<Result> InsertMemberAsync(List<MemberViewModel> modelList);
        Task<Result> InsertMemberAsync(MemberViewModel model);
        Task<Result> UpdateMemberAsync(MemberViewModel model);
        Task<Result> DeleteMemberAsync(int id);
    }
}
