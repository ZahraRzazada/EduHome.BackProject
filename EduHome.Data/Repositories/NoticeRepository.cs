using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class NoticeRepository : Repository<Notice>, INoticeRepository
    {
        public NoticeRepository(EduDbContext context) : base(context)
        {

        }
    }
}

