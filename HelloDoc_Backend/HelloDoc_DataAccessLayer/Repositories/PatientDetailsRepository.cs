using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;

namespace HelloDoc_DataAccessLayer.Repositories
{
    public class PatientDetailsRepository : BaseRepository<PatientDetails>, IPatientDetailsRepository
    {
        public new readonly AppDbContext _dbContext;
        public PatientDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}