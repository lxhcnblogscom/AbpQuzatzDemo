﻿/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：继承了AsyncCrudAppService<...>实现了接口定义的CRUD方法                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/2/26 14:33:32                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.AbpDemo.JobSchedule                                   
*│　类    名： JobInfoAppService                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Czar.AbpDemo.JobSchedule
{
    
    public class JobInfoAppService :
        AsyncCrudAppService<//实现了CRUD方法
            JobInfo,//JobInfo实体
            JobInfoDto,//用来展示书籍
            int,//JobInfo实体的主键
            PagedAndSortedResultRequestDto,//获取书籍的时候用于分页和排序
            CreateUpdateJobInfoDto,//用于创建书籍
            CreateUpdateJobInfoDto>,//用户更新书籍
        IJobInfoAppService
    {
        private readonly IJobInfoRepository _repository;
        //注入了IRepository<JobInfo, ins32>是默认为JobInfo创建的仓储.ABP会自动为每一个聚合根(或实体)创建仓储
        public JobInfoAppService(IJobInfoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [RemoteService(IsEnabled = false)]
        public async Task<List<JobInfoDto>> GetListByJobStatuAsync(JobStatu jobStatu)
        {
            var result = await _repository.GetListByJobStatuAsync(jobStatu);
            return ObjectMapper.Map<List<JobInfo>,List<JobInfoDto>>(result);
        }

        [RemoteService(IsEnabled = false)]
        public async Task<bool> ResumeSystemStoppedAsync()
        {
            return await _repository.ResumeSystemStoppedAsync();

        }

        [RemoteService(IsEnabled = false)]
        public async Task<bool> SystemStoppedAsync()
        {
            return await _repository.SystemStoppedAsync();
        }
    }
}
